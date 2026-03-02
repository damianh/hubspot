using System.CommandLine;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3;
using WebhookModels = DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models;

namespace DamianH.HubSpot.Cli.Commands;

internal static class WebhooksCommand
{
    public static Command Create()
    {
        var command = new Command("webhooks", "Webhook subscription management");

        command.Add(CreateSettingsGetCommand());
        command.Add(CreateListCommand());
        command.Add(CreateGetCommand());
        command.Add(CreateCreateCommand());
        command.Add(CreateDeleteCommand());

        return command;
    }

    private static Command CreateSettingsGetCommand()
    {
        var command = new Command("settings", "Get webhook settings for an app");

        var appIdArg = new Argument<int>("appId") { Description = "HubSpot app ID" };

        command.Add(appIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var appId = parseResult.GetValue(appIdArg);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotWebhooksWebhooksV3Client(cli.Adapter);
                var result = await client.Webhooks.V3[appId].Settings.GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No webhook settings found", cli.Error);
                    return;
                }

                var mapped = new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    ["targetUrl"] = result.TargetUrl,
                    ["createdAt"] = result.CreatedAt?.ToString("O"),
                    ["updatedAt"] = result.UpdatedAt?.ToString("O"),
                };

                if (result.Throttling is { } throttling)
                {
                    mapped["throttling.maxConcurrentRequests"] = throttling.MaxConcurrentRequests;
                }

                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List webhook subscriptions for an app");

        var appIdArg = new Argument<int>("appId") { Description = "HubSpot app ID" };

        command.Add(appIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var appId = parseResult.GetValue(appIdArg);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotWebhooksWebhooksV3Client(cli.Adapter);
                var result = await client.Webhooks.V3[appId].Subscriptions.GetAsync(cancellationToken: cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No webhook subscriptions found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapSubscription).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateGetCommand()
    {
        var command = new Command("get", "Get a webhook subscription");

        var appIdArg = new Argument<int>("appId") { Description = "HubSpot app ID" };
        var subscriptionIdArg = new Argument<int>("subscriptionId") { Description = "Subscription ID" };

        command.Add(appIdArg);
        command.Add(subscriptionIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var appId = parseResult.GetValue(appIdArg);
            var subscriptionId = parseResult.GetValue(subscriptionIdArg);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotWebhooksWebhooksV3Client(cli.Adapter);
                var result = await client.Webhooks.V3[appId].Subscriptions[subscriptionId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Subscription not found", cli.Error);
                    return;
                }

                var mapped = MapSubscription(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateCreateCommand()
    {
        var command = new Command("create", "Create a webhook subscription");

        var appIdArg = new Argument<int>("appId") { Description = "HubSpot app ID" };
        var eventTypeOption = new Option<string>("--event-type") { Description = "Event type (e.g., contact.creation, deal.propertyChange)", Required = true };
        var activeOption = new Option<bool>("--active") { Description = "Activate the subscription", DefaultValueFactory = _ => true };
        var objectTypeIdOption = new Option<string?>("--object-type-id") { Description = "Object type ID for object-specific events" };
        var propertyNameOption = new Option<string?>("--property-name") { Description = "Property name for propertyChange events" };

        command.Add(appIdArg);
        command.Add(eventTypeOption);
        command.Add(activeOption);
        command.Add(objectTypeIdOption);
        command.Add(propertyNameOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var appId = parseResult.GetValue(appIdArg);
            var eventType = parseResult.GetValue(eventTypeOption)!;
            var active = parseResult.GetValue(activeOption);
            var objectTypeId = parseResult.GetValue(objectTypeIdOption);
            var propertyName = parseResult.GetValue(propertyNameOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var body = new WebhookModels.SubscriptionCreateRequest
                {
                    Active = active,
                    ObjectTypeId = objectTypeId,
                    PropertyName = propertyName,
                };

                // Parse event type string to enum (e.g., "contact.creation" -> "ContactCreation")
                if (Enum.TryParse<WebhookModels.SubscriptionCreateRequest_eventType>(eventType.Replace(".", ""), ignoreCase: true, out var parsed))
                {
                    body.EventType = parsed;
                }

                var client = new HubSpotWebhooksWebhooksV3Client(cli.Adapter);
                var result = await client.Webhooks.V3[appId].Subscriptions.PostAsync(body, cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No response received", cli.Error);
                    return;
                }

                var mapped = MapSubscription(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateDeleteCommand()
    {
        var command = new Command("delete", "Delete a webhook subscription");

        var appIdArg = new Argument<int>("appId") { Description = "HubSpot app ID" };
        var subscriptionIdArg = new Argument<int>("subscriptionId") { Description = "Subscription ID" };

        command.Add(appIdArg);
        command.Add(subscriptionIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var appId = parseResult.GetValue(appIdArg);
            var subscriptionId = parseResult.GetValue(subscriptionIdArg);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotWebhooksWebhooksV3Client(cli.Adapter);
                await client.Webhooks.V3[appId].Subscriptions[subscriptionId].DeleteAsync(cancellationToken: cancellationToken);

                cli.Formatter.WriteMessage($"Subscription {subscriptionId} deleted", cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapSubscription(WebhookModels.SubscriptionResponse sub) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = sub.Id,
        ["eventType"] = sub.EventType?.ToString(),
        ["active"] = sub.Active,
        ["objectTypeId"] = sub.ObjectTypeId,
        ["propertyName"] = sub.PropertyName,
        ["createdAt"] = sub.CreatedAt?.ToString("O"),
        ["updatedAt"] = sub.UpdatedAt?.ToString("O"),
    };
}
