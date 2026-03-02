using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CRM.Properties.V3;
using PropertyModels = DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class PropertiesCommand
{
    public static Command Create()
    {
        var command = new Command("properties", "Manage CRM property definitions");

        command.Add(CreateListCommand());
        command.Add(CreateGetCommand());
        command.Add(CreateGroupsListCommand());

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List property definitions for an object type");

        var objectTypeArg = new Argument<string>("objectType") { Description = "CRM object type (e.g., contacts, companies)" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived properties", DefaultValueFactory = _ => false };

        command.Add(objectTypeArg);
        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var objectType = parseResult.GetValue(objectTypeArg)!;
            var archived = parseResult.GetValue(archivedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMPropertiesV3Client(cli.Adapter);
                var result = await client.Crm.V3.Properties[objectType].GetAsync(q =>
                {
                    q.QueryParameters.Archived = archived;
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No properties found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapProperty).ToList();
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
        var command = new Command("get", "Get a property definition");

        var objectTypeArg = new Argument<string>("objectType") { Description = "CRM object type" };
        var propertyNameArg = new Argument<string>("propertyName") { Description = "Property name" };

        command.Add(objectTypeArg);
        command.Add(propertyNameArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var objectType = parseResult.GetValue(objectTypeArg)!;
            var propertyName = parseResult.GetValue(propertyNameArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMPropertiesV3Client(cli.Adapter);
                var result = await client.Crm.V3.Properties[objectType][propertyName].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Property not found", cli.Error);
                    return;
                }

                var mapped = MapProperty(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateGroupsListCommand()
    {
        var command = new Command("groups", "List property groups for an object type");

        var objectTypeArg = new Argument<string>("objectType") { Description = "CRM object type" };

        command.Add(objectTypeArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var objectType = parseResult.GetValue(objectTypeArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMPropertiesV3Client(cli.Adapter);
                var result = await client.Crm.V3.Properties[objectType].Groups.GetAsync(cancellationToken: cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No property groups found", cli.Out);
                    return;
                }

                var items = result.Results.Select(g => (IDictionary<string, object?>)new Dictionary<string, object?>(StringComparer.Ordinal)
                {
                    ["name"] = g.Name,
                    ["label"] = g.Label,
                    ["displayOrder"] = g.DisplayOrder,
                    ["archived"] = g.Archived,
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

    private static IDictionary<string, object?> MapProperty(PropertyModels.Property prop)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["name"] = prop.Name,
            ["label"] = prop.Label,
            ["type"] = prop.Type,
            ["fieldType"] = prop.FieldType,
            ["groupName"] = prop.GroupName,
            ["description"] = prop.Description,
            ["displayOrder"] = prop.DisplayOrder,
            ["hidden"] = prop.Hidden,
            ["hasUniqueValue"] = prop.HasUniqueValue,
            ["calculated"] = prop.Calculated,
            ["formField"] = prop.FormField,
            ["hubspotDefined"] = prop.HubspotDefined,
            ["archived"] = prop.Archived,
        };

        if (prop.Options is { Count: > 0 })
        {
            result["options"] = prop.Options.Select(o => new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                ["value"] = o.Value,
                ["label"] = o.Label,
                ["displayOrder"] = o.DisplayOrder,
                ["hidden"] = o.Hidden,
            }).ToList();
        }

        return result;
    }
}
