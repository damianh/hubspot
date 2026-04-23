using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3;
using OwnerModels = DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class OwnersCommand
{
    public static Command Create()
    {
        var command = new Command("owners", "Manage CRM owners");

        command.Add(CreateListCommand());
        command.Add(CreateGetCommand());

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List owners");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results per page", DefaultValueFactory = _ => 100 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var emailOption = new Option<string?>("--email") { Description = "Filter by email address" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived owners", DefaultValueFactory = _ => false };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(emailOption);
        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var email = parseResult.GetValue(emailOption);
            var archived = parseResult.GetValue(archivedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMCrmOwnersV3Client(cli.Adapter);
                var result = await client.Crm.V3.Owners.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }

                    if (email is not null)
                    {
                        q.QueryParameters.Email = email;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No owners found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapOwner).ToList();
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

    private static Command CreateGetCommand()
    {
        var command = new Command("get", "Get an owner by ID");

        var ownerIdArg = new Argument<string>("ownerId") { Description = "Owner ID" };

        command.Add(ownerIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var ownerId = parseResult.GetValue(ownerIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMCrmOwnersV3Client(cli.Adapter);
                // The indexer accepts int, so parse the ID
                if (!int.TryParse(ownerId, out var ownerIdInt))
                {
                    cli.Error.WriteLine($"Invalid owner ID: '{ownerId}'. Must be a numeric ID.");
                    return;
                }

                var result = await client.Crm.V3.Owners[ownerIdInt].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Owner not found", cli.Error);
                    return;
                }

                var mapped = MapOwner(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapOwner(OwnerModels.PublicOwner owner)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = owner.Id,
            ["email"] = owner.Email,
            ["firstName"] = owner.FirstName,
            ["lastName"] = owner.LastName,
            ["type"] = owner.Type?.ToString(),
            ["userId"] = owner.UserId,
            ["archived"] = owner.Archived,
            ["createdAt"] = owner.CreatedAt?.ToString("O"),
            ["updatedAt"] = owner.UpdatedAt?.ToString("O"),
        };

        if (owner.Teams is { Count: > 0 })
        {
            result["teams"] = owner.Teams.Select(t => new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                ["id"] = t.Id,
                ["name"] = t.Name,
                ["primary"] = t.Primary,
            }).ToList();
        }

        return result;
    }
}
