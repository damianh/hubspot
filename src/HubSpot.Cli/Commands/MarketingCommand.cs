using System.CommandLine;
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3;
using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3;
using CampaignModels = DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models;
using EmailModels = DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models;
using EventModels = DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models;
using TransactionalModels = DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models;

namespace DamianH.HubSpot.Cli.Commands;

internal static class MarketingCommand
{
    public static Command Create()
    {
        var command = new Command("marketing", "Marketing operations");

        command.Add(CreateCampaignsListCommand());
        command.Add(CreateCampaignsGetCommand());
        command.Add(CreateCampaignsDeleteCommand());
        command.Add(CreateEmailsListCommand());
        command.Add(CreateEmailsGetCommand());
        command.Add(CreateTransactionalSendCommand());
        command.Add(CreateEventsSearchCommand());
        command.Add(CreateEventsGetCommand());

        return command;
    }

    // ── Campaigns ─────────────────────────────────────────────

    private static Command CreateCampaignsListCommand()
    {
        var command = new Command("campaigns-list", "List marketing campaigns");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results per page (1-100)", DefaultValueFactory = _ => 50 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var nameOption = new Option<string?>("--name") { Description = "Filter by campaign name (partial match)" };
        var propertiesOption = new Option<string[]>("--properties") { Description = "Properties to include", AllowMultipleArgumentsPerToken = true };
        var sortOption = new Option<string?>("--sort") { Description = "Sort field (hs_name, createdAt, updatedAt; prefix with - for descending)" };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(nameOption);
        command.Add(propertiesOption);
        command.Add(sortOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var name = parseResult.GetValue(nameOption);
            var properties = parseResult.GetValue(propertiesOption);
            var sort = parseResult.GetValue(sortOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingCampaignsPublicApiV3Client(cli.Adapter);
                var result = await client.Marketing.V3.Campaigns.EmptyPathSegment.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }

                    if (name is not null)
                    {
                        q.QueryParameters.Name = name;
                    }

                    if (properties is { Length: > 0 })
                    {
                        q.QueryParameters.Properties = properties;
                    }

                    if (sort is not null)
                    {
                        q.QueryParameters.Sort = sort;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No campaigns found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapCampaign).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet && result.Paging?.Next?.After is { } nextCursor)
                {
                    cli.Error.WriteLine($"Next page cursor: {nextCursor}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateCampaignsGetCommand()
    {
        var command = new Command("campaigns-get", "Get a marketing campaign by GUID");

        var campaignGuidArg = new Argument<string>("campaignGuid") { Description = "Campaign GUID" };
        var propertiesOption = new Option<string[]>("--properties") { Description = "Properties to include", AllowMultipleArgumentsPerToken = true };

        command.Add(campaignGuidArg);
        command.Add(propertiesOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var campaignGuid = parseResult.GetValue(campaignGuidArg)!;
            var properties = parseResult.GetValue(propertiesOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingCampaignsPublicApiV3Client(cli.Adapter);
                var result = await client.Marketing.V3.Campaigns[campaignGuid].GetAsync(q =>
                {
                    if (properties is { Length: > 0 })
                    {
                        q.QueryParameters.Properties = properties;
                    }
                }, cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Campaign not found", cli.Error);
                    return;
                }

                var mapped = MapCampaignWithAssets(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateCampaignsDeleteCommand()
    {
        var command = new Command("campaigns-delete", "Delete a marketing campaign");

        var campaignGuidArg = new Argument<string>("campaignGuid") { Description = "Campaign GUID to delete" };

        command.Add(campaignGuidArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var campaignGuid = parseResult.GetValue(campaignGuidArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingCampaignsPublicApiV3Client(cli.Adapter);
                await client.Marketing.V3.Campaigns[campaignGuid].DeleteAsync(cancellationToken: cancellationToken);

                var mapped = new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    ["deleted"] = true,
                    ["campaignGuid"] = campaignGuid,
                };

                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    // ── Emails ────────────────────────────────────────────────

    private static Command CreateEmailsListCommand()
    {
        var command = new Command("emails-list", "List marketing emails");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results per page", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived emails", DefaultValueFactory = _ => false };
        var campaignOption = new Option<string?>("--campaign") { Description = "Filter by campaign GUID" };
        var isPublishedOption = new Option<bool?>("--is-published") { Description = "Filter by published status" };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(archivedOption);
        command.Add(campaignOption);
        command.Add(isPublishedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var archived = parseResult.GetValue(archivedOption);
            var campaign = parseResult.GetValue(campaignOption);
            var isPublished = parseResult.GetValue(isPublishedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingMarketingEmailsV3Client(cli.Adapter);
                var result = await client.Marketing.V3.Emails.EmptyPathSegment.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }

                    if (campaign is not null)
                    {
                        q.QueryParameters.Campaign = campaign;
                    }

                    if (isPublished.HasValue)
                    {
                        q.QueryParameters.IsPublished = isPublished.Value;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No emails found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapEmail).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet && result.Paging?.Next?.After is { } nextCursor)
                {
                    cli.Error.WriteLine($"Next page cursor: {nextCursor}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateEmailsGetCommand()
    {
        var command = new Command("emails-get", "Get a marketing email by ID");

        var emailIdArg = new Argument<string>("emailId") { Description = "Email ID" };

        command.Add(emailIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var emailId = parseResult.GetValue(emailIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingMarketingEmailsV3Client(cli.Adapter);
                var result = await client.Marketing.V3.Emails[emailId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Email not found", cli.Error);
                    return;
                }

                var mapped = MapEmail(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    // ── Transactional ─────────────────────────────────────────

    private static Command CreateTransactionalSendCommand()
    {
        var command = new Command("send-transactional", "Send a transactional email");

        var emailIdArg = new Argument<int>("emailId") { Description = "Transactional email template ID" };
        var toOption = new Option<string>("--to") { Description = "Recipient email address", Required = true };
        var contactPropertiesOption = new Option<string[]>("--contact-property")
        {
            Description = "Contact properties as key=value pairs",
            AllowMultipleArgumentsPerToken = true,
        };
        var customPropertiesOption = new Option<string[]>("--custom-property")
        {
            Description = "Custom template properties as key=value pairs",
            AllowMultipleArgumentsPerToken = true,
        };

        command.Add(emailIdArg);
        command.Add(toOption);
        command.Add(contactPropertiesOption);
        command.Add(customPropertiesOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var emailId = parseResult.GetValue(emailIdArg);
            var to = parseResult.GetValue(toOption)!;
            var contactProps = parseResult.GetValue(contactPropertiesOption);
            var customProps = parseResult.GetValue(customPropertiesOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var request = new TransactionalModels.PublicSingleSendRequestEgg
                {
                    EmailId = emailId,
                    Message = new TransactionalModels.PublicSingleSendEmail { To = to },
                };

                if (contactProps is { Length: > 0 })
                {
                    var cp = new TransactionalModels.PublicSingleSendRequestEgg_contactProperties();
                    foreach (var prop in contactProps)
                    {
                        var parts = prop.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            cp.AdditionalData[parts[0]] = parts[1];
                        }
                    }

                    request.ContactProperties = cp;
                }

                if (customProps is { Length: > 0 })
                {
                    var cp = new TransactionalModels.PublicSingleSendRequestEgg_customProperties();
                    foreach (var prop in customProps)
                    {
                        var parts = prop.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            cp.AdditionalData[parts[0]] = parts[1];
                        }
                    }

                    request.CustomProperties = cp;
                }

                var client = new HubSpotMarketingTransactionalSingleSendV3Client(cli.Adapter);
                var result = await client.Marketing.V3.Transactional.SingleEmail.Send.PostAsync(request, cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No response received", cli.Error);
                    return;
                }

                var mapped = new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    ["statusId"] = result.StatusId,
                    ["status"] = result.Status?.ToString(),
                    ["sendResult"] = result.SendResult?.ToString(),
                    ["requestedAt"] = result.RequestedAt?.ToString("O"),
                    ["startedAt"] = result.StartedAt?.ToString("O"),
                    ["completedAt"] = result.CompletedAt?.ToString("O"),
                };

                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    // ── Events ────────────────────────────────────────────────

    private static Command CreateEventsSearchCommand()
    {
        var command = new Command("events-search", "Search marketing events");

        var queryOption = new Option<string>("--query") { Description = "External event ID to search for", Required = true };

        command.Add(queryOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var query = parseResult.GetValue(queryOption)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingMarketingEventsV3Client(cli.Adapter);
                var result = await client.Marketing.V3.MarketingEvents.Events.Search.GetAsync(q =>
                {
                    q.QueryParameters.Q = query;
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No marketing events found", cli.Out);
                    return;
                }

                var items = result.Results.Select(r => (IDictionary<string, object?>)new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    ["externalEventId"] = r.ExternalEventId,
                    ["externalAccountId"] = r.ExternalAccountId,
                    ["objectId"] = r.ObjectId,
                    ["appId"] = r.AppId,
                }).ToList();

                cli.Formatter.WriteCollection(items, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateEventsGetCommand()
    {
        var command = new Command("event", "Get a marketing event by external ID");

        var externalEventIdArg = new Argument<string>("externalEventId") { Description = "External event ID" };
        var externalAccountIdOption = new Option<string>("--account-id") { Description = "External account ID", Required = true };

        command.Add(externalEventIdArg);
        command.Add(externalAccountIdOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var externalEventId = parseResult.GetValue(externalEventIdArg)!;
            var externalAccountId = parseResult.GetValue(externalAccountIdOption)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotMarketingMarketingEventsV3Client(cli.Adapter);
                var result = await client.Marketing.V3.MarketingEvents.Events[externalEventId].GetAsync(q =>
                {
                    q.QueryParameters.ExternalAccountId = externalAccountId;
                }, cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Event not found", cli.Error);
                    return;
                }

                var mapped = MapEvent(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    // ── Mappers ───────────────────────────────────────────────

    private static IDictionary<string, object?> MapCampaign(CampaignModels.PublicCampaign campaign)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = campaign.Id,
            ["createdAt"] = campaign.CreatedAt?.ToString("O"),
            ["updatedAt"] = campaign.UpdatedAt?.ToString("O"),
        };

        if (campaign.Properties?.AdditionalData is { Count: > 0 } props)
        {
            foreach (var kvp in props)
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }

    private static IDictionary<string, object?> MapCampaignWithAssets(CampaignModels.PublicCampaignWithAssets campaign)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = campaign.Id,
            ["createdAt"] = campaign.CreatedAt?.ToString("O"),
            ["updatedAt"] = campaign.UpdatedAt?.ToString("O"),
        };

        if (campaign.Properties?.AdditionalData is { Count: > 0 } props)
        {
            foreach (var kvp in props)
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        if (campaign.Assets?.AdditionalData is { Count: > 0 } assets)
        {
            result["assets"] = assets;
        }

        return result;
    }

    private static IDictionary<string, object?> MapEmail(EmailModels.PublicEmail email) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = email.Id,
        ["name"] = email.Name,
        ["subject"] = email.Subject,
        ["campaign"] = email.Campaign,
        ["campaignName"] = email.CampaignName,
        ["archived"] = email.Archived,
        ["activeDomain"] = email.ActiveDomain,
        ["createdAt"] = email.CreatedAt?.ToString("O"),
        ["updatedAt"] = email.UpdatedAt?.ToString("O"),
    };

    private static IDictionary<string, object?> MapEvent(EventModels.MarketingEventPublicReadResponse evt) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["eventName"] = evt.EventName,
        ["eventType"] = evt.EventType,
        ["externalEventId"] = evt.ExternalEventId,
        ["eventOrganizer"] = evt.EventOrganizer,
        ["eventDescription"] = evt.EventDescription,
        ["eventUrl"] = evt.EventUrl,
        ["eventCompleted"] = evt.EventCompleted,
        ["eventCancelled"] = evt.EventCancelled,
        ["startDateTime"] = evt.StartDateTime?.ToString("O"),
        ["endDateTime"] = evt.EndDateTime?.ToString("O"),
        ["createdAt"] = evt.CreatedAt?.ToString("O"),
        ["updatedAt"] = evt.UpdatedAt?.ToString("O"),
    };
}
