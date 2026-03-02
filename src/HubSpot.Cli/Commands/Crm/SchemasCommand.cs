using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CRM.Schemas.V3;
using SchemaModels = DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class SchemasCommand
{
    public static Command Create()
    {
        var command = new Command("schemas", "Manage custom object schemas");

        command.Add(CreateListCommand());
        command.Add(CreateGetCommand());

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List custom object schemas");

        var archivedOption = new Option<bool>("--archived") { Description = "Include archived schemas", DefaultValueFactory = _ => false };

        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var archived = parseResult.GetValue(archivedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMSchemasV3Client(cli.Adapter);
                var result = await client.CrmObjectSchemas.V3.Schemas.GetAsync(q =>
                {
                    q.QueryParameters.Archived = archived;
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No schemas found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapSchema).ToList();
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
        var command = new Command("get", "Get a custom object schema");

        var objectTypeArg = new Argument<string>("objectType") { Description = "Object type or schema ID" };

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
                var client = new HubSpotCRMSchemasV3Client(cli.Adapter);
                var result = await client.CrmObjectSchemas.V3.Schemas[objectType].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Schema not found", cli.Error);
                    return;
                }

                var mapped = MapSchema(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapSchema(SchemaModels.ObjectSchema schema)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = schema.Id,
            ["name"] = schema.Name,
            ["fullyQualifiedName"] = schema.FullyQualifiedName,
            ["objectTypeId"] = schema.ObjectTypeId,
            ["description"] = schema.Description,
            ["primaryDisplayProperty"] = schema.PrimaryDisplayProperty,
            ["archived"] = schema.Archived,
            ["createdAt"] = schema.CreatedAt?.ToString("O"),
            ["updatedAt"] = schema.UpdatedAt?.ToString("O"),
            ["propertyCount"] = schema.Properties?.Count ?? 0,
            ["associationCount"] = schema.Associations?.Count ?? 0,
        };

        if (schema.Labels is { } labels)
        {
            result["singularLabel"] = labels.Singular;
            result["pluralLabel"] = labels.Plural;
        }

        if (schema.RequiredProperties is { Count: > 0 })
        {
            result["requiredProperties"] = string.Join(", ", schema.RequiredProperties);
        }

        if (schema.SearchableProperties is { Count: > 0 })
        {
            result["searchableProperties"] = string.Join(", ", schema.SearchableProperties);
        }

        return result;
    }
}
