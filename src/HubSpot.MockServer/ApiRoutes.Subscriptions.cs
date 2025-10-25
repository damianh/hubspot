using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer;

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
    }
}
