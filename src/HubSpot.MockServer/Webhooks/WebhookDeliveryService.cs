using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Repositories.Webhook;
using Microsoft.Extensions.Hosting;

namespace DamianH.HubSpot.MockServer.Webhooks;

internal sealed class WebhookDeliveryService(
    WebhookEventChannel channel,
    WebhookRepository webhookRepository,
    IHttpClientFactory httpClientFactory) : BackgroundService
{
    // Maps route segment → HubSpot object type prefix used in event type strings
    private static readonly Dictionary<string, string> ObjectTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["contacts"] = "contact",
        ["companies"] = "company",
        ["0-3"] = "deal",
        ["deals"] = "deal",
        ["tickets"] = "ticket",
        ["products"] = "product",
        ["line_items"] = "line_item",
        ["line-items"] = "line_item",
        ["quotes"] = "quote",
        ["calls"] = "call",
        ["emails"] = "email",
        ["meetings"] = "meeting",
        ["notes"] = "note",
        ["tasks"] = "task",
        ["communications"] = "communication",
        ["postal_mail"] = "postal_mail",
        ["feedback_submissions"] = "feedback_submission",
    };

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private long _eventIdCounter;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var webhookEvent in channel.Reader.ReadAllAsync(stoppingToken))
        {
            await DeliverAsync(webhookEvent, stoppingToken);
        }
    }

    private async Task DeliverAsync(WebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        var hubSpotObjectType = MapObjectType(webhookEvent.ObjectType);
        var occurredAtMs = webhookEvent.OccurredAt.ToUnixTimeMilliseconds();

        foreach (var appId in webhookRepository.GetAppIds())
        {
            var settings = webhookRepository.GetSettings(appId);
            if (settings?.TargetUrl is not { Length: > 0 } targetUrl)
            {
                continue;
            }

            var subscriptions = webhookRepository.ListSubscriptions(appId).Results ?? [];
            var payloads = new List<WebhookEventPayload>();

            foreach (var subscription in subscriptions)
            {
                if (subscription.Active != true) continue;

                var eventTypeName = subscription.EventType?.ToString();
                if (eventTypeName is null) continue;

                var expectedEventTypeName = webhookEvent switch
                {
                    ObjectCreatedEvent => $"{hubSpotObjectType}Creation",
                    ObjectPropertyChangedEvent => $"{hubSpotObjectType}PropertyChange",
                    ObjectDeletedEvent => $"{hubSpotObjectType}Deletion",
                    _ => null
                };

                if (expectedEventTypeName is null) continue;

                // Case-insensitive match against subscription event type enum name
                if (!string.Equals(eventTypeName, expectedEventTypeName, StringComparison.OrdinalIgnoreCase))
                {
                    // Also handle generic "object.*" subscriptions
                    var genericExpected = webhookEvent switch
                    {
                        ObjectCreatedEvent => "ObjectCreation",
                        ObjectPropertyChangedEvent => "ObjectPropertyChange",
                        ObjectDeletedEvent => "ObjectDeletion",
                        _ => null
                    };
                    if (genericExpected is null ||
                        !string.Equals(eventTypeName, genericExpected, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                if (webhookEvent is ObjectPropertyChangedEvent propEvent)
                {
                    // Filter by propertyName if subscription specifies one
                    if (!string.IsNullOrEmpty(subscription.PropertyName) &&
                        !string.Equals(subscription.PropertyName, propEvent.PropertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    payloads.Add(new WebhookEventPayload
                    {
                        EventId = Interlocked.Increment(ref _eventIdCounter),
                        SubscriptionId = subscription.Id,
                        AppId = int.TryParse(appId, out var aid) ? aid : 0,
                        OccurredAt = occurredAtMs,
                        SubscriptionType = $"{hubSpotObjectType}.propertyChange",
                        AttemptNumber = 0,
                        ObjectId = webhookEvent.ObjectId,
                        PropertyName = propEvent.PropertyName,
                        PropertyValue = propEvent.NewValue,
                        ChangeSource = "API"
                    });
                }
                else
                {
                    var subscriptionTypeSuffix = webhookEvent switch
                    {
                        ObjectCreatedEvent => "creation",
                        ObjectDeletedEvent => "deletion",
                        _ => "unknown"
                    };

                    payloads.Add(new WebhookEventPayload
                    {
                        EventId = Interlocked.Increment(ref _eventIdCounter),
                        SubscriptionId = subscription.Id,
                        AppId = int.TryParse(appId, out var aid) ? aid : 0,
                        OccurredAt = occurredAtMs,
                        SubscriptionType = $"{hubSpotObjectType}.{subscriptionTypeSuffix}",
                        AttemptNumber = 0,
                        ObjectId = webhookEvent.ObjectId,
                        ChangeSource = "API"
                    });
                }
            }

            if (payloads.Count == 0) continue;

            try
            {
                var client = httpClientFactory.CreateClient(nameof(WebhookDeliveryService));
                await client.PostAsJsonAsync(targetUrl, payloads, SerializerOptions, cancellationToken);
            }
            catch (HttpRequestException)
            {
                // Best-effort delivery — don't crash the service on transient failures
            }
            catch (TaskCanceledException)
            {
                // Delivery timed out — ignore
            }
        }
    }

    private static string MapObjectType(string routeObjectType) =>
        ObjectTypeMap.TryGetValue(routeObjectType, out var mapped) ? mapped : routeObjectType;
}
