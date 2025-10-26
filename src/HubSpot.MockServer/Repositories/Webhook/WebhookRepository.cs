using System.Collections.Concurrent;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models;
using DamianH.HubSpot.MockServer.Apis.Models;

namespace DamianH.HubSpot.MockServer.Repositories.Webhook;

internal class WebhookRepository
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, SubscriptionResponse>> _subscriptions = new();
    private readonly ConcurrentDictionary<string, SettingsResponse> _settings = new();
    private int _subscriptionCounter;

    public SubscriptionResponse CreateSubscription(string appId, WebhookSubscriptionCreateRequest request)
    {
        var subscriptionId = Interlocked.Increment(ref _subscriptionCounter).ToString();
        var now = DateTimeOffset.UtcNow;

        SubscriptionResponse_eventType? eventType = null;
        if (!string.IsNullOrEmpty(request.EventType))
        {
            eventType = MapEventType(request.EventType);
        }

        var subscription = new SubscriptionResponse
        {
            Id = subscriptionId,
            EventType = eventType,
            ObjectTypeId = request.ObjectTypeId,
            PropertyName = request.PropertyName,
            Active = request.Active ?? true,
            CreatedAt = now,
            UpdatedAt = now
        };

        var appSubscriptions = _subscriptions.GetOrAdd(appId, _ => new ConcurrentDictionary<string, SubscriptionResponse>());
        appSubscriptions[subscriptionId] = subscription;

        return subscription;
    }

    private static SubscriptionResponse_eventType MapEventType(string eventTypeString) => eventTypeString switch
    {
        "contact.propertyChange" => SubscriptionResponse_eventType.ContactPropertyChange,
        "company.propertyChange" => SubscriptionResponse_eventType.CompanyPropertyChange,
        "deal.propertyChange" => SubscriptionResponse_eventType.DealPropertyChange,
        "ticket.propertyChange" => SubscriptionResponse_eventType.TicketPropertyChange,
        "product.propertyChange" => SubscriptionResponse_eventType.ProductPropertyChange,
        "line_item.propertyChange" => SubscriptionResponse_eventType.Line_itemPropertyChange,
        "contact.creation" => SubscriptionResponse_eventType.ContactCreation,
        "contact.deletion" => SubscriptionResponse_eventType.ContactDeletion,
        "contact.privacyDeletion" => SubscriptionResponse_eventType.ContactPrivacyDeletion,
        "company.creation" => SubscriptionResponse_eventType.CompanyCreation,
        "company.deletion" => SubscriptionResponse_eventType.CompanyDeletion,
        "deal.creation" => SubscriptionResponse_eventType.DealCreation,
        "deal.deletion" => SubscriptionResponse_eventType.DealDeletion,
        "ticket.creation" => SubscriptionResponse_eventType.TicketCreation,
        "ticket.deletion" => SubscriptionResponse_eventType.TicketDeletion,
        "product.creation" => SubscriptionResponse_eventType.ProductCreation,
        "product.deletion" => SubscriptionResponse_eventType.ProductDeletion,
        "line_item.creation" => SubscriptionResponse_eventType.Line_itemCreation,
        "line_item.deletion" => SubscriptionResponse_eventType.Line_itemDeletion,
        "conversation.creation" => SubscriptionResponse_eventType.ConversationCreation,
        "conversation.deletion" => SubscriptionResponse_eventType.ConversationDeletion,
        "conversation.newMessage" => SubscriptionResponse_eventType.ConversationNewMessage,
        "conversation.privacyDeletion" => SubscriptionResponse_eventType.ConversationPrivacyDeletion,
        "conversation.propertyChange" => SubscriptionResponse_eventType.ConversationPropertyChange,
        "contact.merge" => SubscriptionResponse_eventType.ContactMerge,
        "company.merge" => SubscriptionResponse_eventType.CompanyMerge,
        "deal.merge" => SubscriptionResponse_eventType.DealMerge,
        "ticket.merge" => SubscriptionResponse_eventType.TicketMerge,
        "product.merge" => SubscriptionResponse_eventType.ProductMerge,
        "line_item.merge" => SubscriptionResponse_eventType.Line_itemMerge,
        "contact.restore" => SubscriptionResponse_eventType.ContactRestore,
        "company.restore" => SubscriptionResponse_eventType.CompanyRestore,
        "deal.restore" => SubscriptionResponse_eventType.DealRestore,
        "ticket.restore" => SubscriptionResponse_eventType.TicketRestore,
        "product.restore" => SubscriptionResponse_eventType.ProductRestore,
        "line_item.restore" => SubscriptionResponse_eventType.Line_itemRestore,
        "contact.associationChange" => SubscriptionResponse_eventType.ContactAssociationChange,
        "company.associationChange" => SubscriptionResponse_eventType.CompanyAssociationChange,
        "deal.associationChange" => SubscriptionResponse_eventType.DealAssociationChange,
        "ticket.associationChange" => SubscriptionResponse_eventType.TicketAssociationChange,
        "line_item.associationChange" => SubscriptionResponse_eventType.Line_itemAssociationChange,
        "object.propertyChange" => SubscriptionResponse_eventType.ObjectPropertyChange,
        "object.creation" => SubscriptionResponse_eventType.ObjectCreation,
        "object.deletion" => SubscriptionResponse_eventType.ObjectDeletion,
        "object.merge" => SubscriptionResponse_eventType.ObjectMerge,
        "object.restore" => SubscriptionResponse_eventType.ObjectRestore,
        "object.associationChange" => SubscriptionResponse_eventType.ObjectAssociationChange,
        _ => throw new ArgumentException($"Unknown event type: {eventTypeString}")
    };

    public SubscriptionResponse? GetSubscription(string appId, string subscriptionId)
    {
        if (_subscriptions.TryGetValue(appId, out var appSubscriptions))
        {
            appSubscriptions.TryGetValue(subscriptionId, out var subscription);
            return subscription;
        }
        return null;
    }

    public SubscriptionListResponse ListSubscriptions(string appId)
    {
        var appSubscriptions = _subscriptions.GetOrAdd(appId, _ => new ConcurrentDictionary<string, SubscriptionResponse>());

        return new SubscriptionListResponse
        {
            Results = appSubscriptions.Values.ToList()
        };
    }

    public SubscriptionResponse? UpdateSubscription(string appId, string subscriptionId, WebhookSubscriptionPatchRequest request)
    {
        if (_subscriptions.TryGetValue(appId, out var appSubscriptions) &&
            appSubscriptions.TryGetValue(subscriptionId, out var subscription))
        {
            if (request.Active.HasValue)
            {
                subscription.Active = request.Active.Value;
            }
            subscription.UpdatedAt = DateTimeOffset.UtcNow;

            return subscription;
        }
        return null;
    }

    public bool DeleteSubscription(string appId, string subscriptionId) =>
        _subscriptions.TryGetValue(appId, out var appSubscriptions) && appSubscriptions.TryRemove(subscriptionId, out _);

    public BatchResponseSubscriptionResponse BatchUpdateSubscriptions(string appId, WebhookBatchInputRequest request)
    {
        var results = new List<SubscriptionResponse>();

        foreach (var updateRequest in request.Inputs ?? [])
        {
            if (updateRequest.Id == null)
            {
                continue;
            }

            var updated = UpdateSubscription(appId, updateRequest.Id.Value.ToString(), new WebhookSubscriptionPatchRequest
            {
                Active = updateRequest.Active
            });

            if (updated != null)
            {
                results.Add(updated);
            }
        }

        return new BatchResponseSubscriptionResponse
        {
            Status = BatchResponseSubscriptionResponse_status.COMPLETE,
            Results = results,
            StartedAt = DateTimeOffset.UtcNow,
            CompletedAt = DateTimeOffset.UtcNow
        };
    }

    public SettingsResponse? GetSettings(string appId)
    {
        _settings.TryGetValue(appId, out var settings);
        return settings;
    }

    public SettingsResponse UpdateSettings(string appId, WebhookSettingsChangeRequest request)
    {
        var now = DateTimeOffset.UtcNow;

        var throttling = request.Throttling != null
            ? new ThrottlingSettings
            {
                MaxConcurrentRequests = request.Throttling.MaxConcurrentRequests ?? 100
            }
            : null;

        var settings = new SettingsResponse
        {
            TargetUrl = request.TargetUrl,
            Throttling = throttling,
            CreatedAt = _settings.TryGetValue(appId, out var setting) ? setting.CreatedAt : now,
            UpdatedAt = now
        };

        _settings[appId] = settings;
        return settings;
    }

    public bool ClearSettings(string appId) => _settings.TryRemove(appId, out _);
}
