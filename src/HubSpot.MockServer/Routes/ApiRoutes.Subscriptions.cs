using DamianH.HubSpot.MockServer.Repositories.Subscription;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class Subscriptions
    {
        public static void RegisterSubscriptionsV3Api(WebApplication app)
        {
            var group = app.MapGroup("/communication-preferences/v3/subscriptions");

            group.MapPost("/{emailAddress}", CreateOrUpdateSubscription);
            group.MapGet("/{emailAddress}", GetSubscriptionStatus);

            var defsGroup = app.MapGroup("/communication-preferences/v3/definitions");
            defsGroup.MapGet("", ListDefinitions);
            defsGroup.MapPost("", CreateDefinition);
            defsGroup.MapGet("/{definitionId}", GetDefinition);
        }

        public static void RegisterSubscriptionsV4Api(WebApplication app)
        {
            var group = app.MapGroup("/communication-preferences/v4/subscriptions");

            group.MapPost("/status/email/{emailAddress}", CreateOrUpdateSubscriptionV4);
            group.MapGet("/status/email/{emailAddress}", GetSubscriptionStatusV4);
        }

        public static void RegisterSubscriptionsV3KiotaApi(WebApplication app)
        {
            // Kiota V3 client uses /communication-preferences/v3/status/email/{emailAddress} for GET
            app.MapGet("/communication-preferences/v3/status/email/{emailAddress}", (
                SubscriptionRepository repository,
                string emailAddress) =>
            {
                var subscriptions = repository.GetAllSubscriptions()
                    .Where(s => s.EmailAddress == emailAddress).ToList();
                return Results.Ok(new { subscriptionStatuses = subscriptions, recipient = emailAddress });
            });

            // Kiota V3 client uses /communication-preferences/v3/subscribe POST
            app.MapPost("/communication-preferences/v3/subscribe", (
                SubscriptionRepository repository,
                SubscribeRequest request) =>
            {
                var sub = new Subscription
                {
                    EmailAddress = request.EmailAddress ?? string.Empty,
                    SubscriptionId = request.SubscriptionId ?? string.Empty,
                    Status = "SUBSCRIBED"
                };
                var created = repository.CreateSubscription(sub);
                return Results.Ok(created);
            });

            // Kiota V3 client uses /communication-preferences/v3/unsubscribe POST
            app.MapPost("/communication-preferences/v3/unsubscribe", (
                SubscriptionRepository repository,
                SubscribeRequest request) =>
            {
                var sub = new Subscription
                {
                    EmailAddress = request.EmailAddress ?? string.Empty,
                    SubscriptionId = request.SubscriptionId ?? string.Empty,
                    Status = "NOT_SUBSCRIBED"
                };
                var created = repository.CreateSubscription(sub);
                return Results.Ok(created);
            });
        }

        private static IResult CreateOrUpdateSubscription(
            SubscriptionRepository repository,
            string emailAddress,
            Subscription subscription)
        {
            subscription.EmailAddress = emailAddress;
            var created = repository.CreateSubscription(subscription);
            return Results.Ok(created);
        }

        private static IResult GetSubscriptionStatus(
            SubscriptionRepository repository,
            string emailAddress)
        {
            var subscriptions = repository.GetAllSubscriptions()
                .Where(s => s.EmailAddress == emailAddress);
            return Results.Ok(new { results = subscriptions });
        }

        private static IResult CreateOrUpdateSubscriptionV4(
            SubscriptionRepository repository,
            string emailAddress,
            Subscription subscription)
        {
            subscription.EmailAddress = emailAddress;
            var created = repository.CreateSubscription(subscription);
            return Results.Ok(created);
        }

        private static IResult GetSubscriptionStatusV4(
            SubscriptionRepository repository,
            string emailAddress)
        {
            var subscriptions = repository.GetAllSubscriptions()
                .Where(s => s.EmailAddress == emailAddress);
            return Results.Ok(new { results = subscriptions });
        }

        private static IResult ListDefinitions(SubscriptionRepository repository)
        {
            var definitions = repository.GetAllDefinitions();
            return Results.Ok(new { results = definitions });
        }

        private static IResult CreateDefinition(
            SubscriptionRepository repository,
            SubscriptionDefinition definition)
        {
            var created = repository.CreateDefinition(definition);
            return Results.Ok(created);
        }

        private static IResult GetDefinition(
            SubscriptionRepository repository,
            string definitionId)
        {
            var definition = repository.GetDefinitionById(definitionId);
            return definition == null ? Results.NotFound() : Results.Ok(definition);
        }

        private record SubscribeRequest(string? EmailAddress, string? SubscriptionId, string? LegalBasisExplanation = null);

        public static void RegisterSubscriptionsV4KiotaApi(WebApplication app)
        {
            // V4 definitions endpoint
            app.MapGet("/communication-preferences/v4/definitions", (SubscriptionRepository repository) =>
            {
                var definitions = repository.GetAllDefinitions();
                return Results.Ok(new
                {
                    status = "COMPLETE",
                    numErrors = 0,
                    results = definitions.Select(d => new { id = d.Id, name = d.Name }),
                    requestedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });

            // V4 get/update status for a subscriber
            app.MapGet("/communication-preferences/v4/statuses/{subscriberIdString}", (
                SubscriptionRepository repository,
                string subscriberIdString) =>
            {
                var subscriptions = repository.GetAllSubscriptions()
                    .Where(s => s.EmailAddress == subscriberIdString).ToList();
                return Results.Ok(new
                {
                    status = "COMPLETE",
                    numErrors = 0,
                    results = subscriptions.Select(s => new { subscriptionId = s.SubscriptionId, status = s.Status, channel = "EMAIL" }),
                    requestedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });

            app.MapPost("/communication-preferences/v4/statuses/{subscriberIdString}", async (
                SubscriptionRepository repository,
                string subscriberIdString,
                HttpContext context) =>
            {
                var body = await System.Text.Json.JsonSerializer.DeserializeAsync<System.Text.Json.JsonElement>(context.Request.Body);
                var sub = new Subscription
                {
                    EmailAddress = subscriberIdString,
                    SubscriptionId = body.TryGetProperty("subscriptionId", out var sid) ? sid.GetString() ?? string.Empty : string.Empty,
                    Status = body.TryGetProperty("statusState", out var ss) ? ss.GetString() ?? "SUBSCRIBED" : "SUBSCRIBED"
                };
                repository.CreateSubscription(sub);
                return Results.Ok(new
                {
                    status = "COMPLETE",
                    numErrors = 0,
                    results = new[] { new { subscriptionId = sub.SubscriptionId, status = sub.Status, channel = "EMAIL" } },
                    requestedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });
        }
    }
}
