using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class Webhooks
    {
        public static void RegisterWebhooksApi(WebApplication app)
        {
            var webhooksGroup = app.MapGroup("/webhooks/v3/{appId}");

            // Subscriptions
            var subscriptionsGroup = webhooksGroup.MapGroup("/subscriptions");
            subscriptionsGroup.MapGet("", ListSubscriptions);
            subscriptionsGroup.MapPost("", CreateSubscription);
            subscriptionsGroup.MapGet("/{subscriptionId}", GetSubscription);
            subscriptionsGroup.MapPatch("/{subscriptionId}", UpdateSubscription);
            subscriptionsGroup.MapDelete("/{subscriptionId}", DeleteSubscription);

            // Batch operations
            subscriptionsGroup.MapPost("/batch/update", BatchUpdateSubscriptions);

            // Settings
            var settingsGroup = webhooksGroup.MapGroup("/settings");
            settingsGroup.MapGet("", GetSettings);
            settingsGroup.MapPut("", UpdateSettings);
            settingsGroup.MapDelete("", ClearSettings);
        }

        private static IResult ListSubscriptions(
            WebhookRepository repository,
            string appId)
        {
            var result = repository.ListSubscriptions(appId);
            return Results.Ok(result);
        }

        private static async Task<IResult> CreateSubscription(
            WebhookRepository repository,
            string appId,
            HttpContext context)
        {
            var request = await context.Request.ReadFromJsonAsync<WebhookSubscriptionCreateRequest>();
            if (request == null)
            {
                return Results.BadRequest();
            }
            var subscription = repository.CreateSubscription(appId, request);
            return Results.Ok(subscription);
        }

        private static IResult GetSubscription(
            WebhookRepository repository,
            string appId,
            string subscriptionId)
        {
            var subscription = repository.GetSubscription(appId, subscriptionId);
            if (subscription == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(subscription);
        }

        private static async Task<IResult> UpdateSubscription(
            WebhookRepository repository,
            string appId,
            string subscriptionId,
            HttpContext context)
        {
            var request = await context.Request.ReadFromJsonAsync<WebhookSubscriptionPatchRequest>();
            if (request == null)
            {
                return Results.BadRequest();
            }
            var subscription = repository.UpdateSubscription(appId, subscriptionId, request);
            if (subscription == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(subscription);
        }

        private static IResult DeleteSubscription(
            WebhookRepository repository,
            string appId,
            string subscriptionId)
        {
            var success = repository.DeleteSubscription(appId, subscriptionId);
            if (!success)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        }

        private static async Task<IResult> BatchUpdateSubscriptions(
            WebhookRepository repository,
            string appId,
            HttpContext context)
        {
            var request = await context.Request.ReadFromJsonAsync<WebhookBatchInputRequest>();
            if (request == null)
            {
                return Results.BadRequest();
            }
            var result = repository.BatchUpdateSubscriptions(appId, request);
            return Results.Ok(result);
        }

        private static IResult GetSettings(
            WebhookRepository repository,
            string appId)
        {
            var settings = repository.GetSettings(appId);
            if (settings == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(settings);
        }

        private static async Task<IResult> UpdateSettings(
            WebhookRepository repository,
            string appId,
            HttpContext context)
        {
            var request = await context.Request.ReadFromJsonAsync<WebhookSettingsChangeRequest>();
            if (request == null)
            {
                return Results.BadRequest();
            }
            var settings = repository.UpdateSettings(appId, request);
            return Results.Ok(settings);
        }

        private static IResult ClearSettings(
            WebhookRepository repository,
            string appId)
        {
            var success = repository.ClearSettings(appId);
            if (!success)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        }
    }
}
