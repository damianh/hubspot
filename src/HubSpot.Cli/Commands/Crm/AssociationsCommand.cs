using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4;
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class AssociationsCommand
{
    public static Command Create()
    {
        var command = new Command("associations", "Manage CRM associations (V4)");

        command.Add(CreateBatchReadCommand());
        command.Add(CreateBatchCreateCommand());

        return command;
    }

    private static Command CreateBatchReadCommand()
    {
        var command = new Command("batch-read", "Batch read associations between object types");

        var fromTypeArg = new Argument<string>("fromObjectType") { Description = "Source object type (e.g., contacts)" };
        var toTypeArg = new Argument<string>("toObjectType") { Description = "Target object type (e.g., companies)" };
        var idsOption = new Option<string[]>("--ids") { Description = "Object IDs to read associations for", Required = true, AllowMultipleArgumentsPerToken = true };

        command.Add(fromTypeArg);
        command.Add(toTypeArg);
        command.Add(idsOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var fromType = parseResult.GetValue(fromTypeArg)!;
            var toType = parseResult.GetValue(toTypeArg)!;
            var ids = parseResult.GetValue(idsOption)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var batchInput = new BatchInputPublicFetchAssociationsBatchRequest
                {
                    Inputs = ids.Select(id => new PublicFetchAssociationsBatchRequest { Id = id }).ToList(),
                };

                var client = new HubSpotCRMAssociationsV4Client(cli.Adapter);
                var result = await client.Crm.V4.Associations[fromType][toType].Batch.Read.PostAsync(
                    batchInput,
                    cancellationToken: cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No associations found", cli.Out);
                    return;
                }

                var items = result.Results.Select(r =>
                {
                    var mapped = new Dictionary<string, object?>(StringComparer.Ordinal)
                    {
                        ["fromObjectId"] = r.From?.Id,
                    };

                    if (r.To is { Count: > 0 })
                    {
                        mapped["associations"] = r.To.Select(a =>
                        {
                            var assoc = new Dictionary<string, object?>(StringComparer.Ordinal)
                            {
                                ["toObjectId"] = a.ToObjectId,
                            };

                            if (a.AssociationTypes is { Count: > 0 })
                            {
                                assoc["types"] = a.AssociationTypes.Select(t => new Dictionary<string, object?>(StringComparer.Ordinal)
                                {
                                    ["category"] = t.Category?.ToString(),
                                    ["typeId"] = t.TypeId,
                                    ["label"] = t.Label,
                                }).ToList();
                            }

                            return assoc;
                        }).ToList();
                    }

                    return (IDictionary<string, object?>)mapped;
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

    private static Command CreateBatchCreateCommand()
    {
        var command = new Command("batch-create", "Batch create associations between objects");

        var fromTypeArg = new Argument<string>("fromObjectType") { Description = "Source object type" };
        var toTypeArg = new Argument<string>("toObjectType") { Description = "Target object type" };
        var fromIdOption = new Option<string>("--from") { Description = "Source object ID", Required = true };
        var toIdOption = new Option<string>("--to") { Description = "Target object ID", Required = true };
        var typeIdOption = new Option<int>("--type-id") { Description = "Association type ID", Required = true };
        var categoryOption = new Option<string>("--category") { Description = "Association category (HUBSPOT_DEFINED, USER_DEFINED, INTEGRATOR_DEFINED)", DefaultValueFactory = _ => "HUBSPOT_DEFINED" };

        command.Add(fromTypeArg);
        command.Add(toTypeArg);
        command.Add(fromIdOption);
        command.Add(toIdOption);
        command.Add(typeIdOption);
        command.Add(categoryOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var fromType = parseResult.GetValue(fromTypeArg)!;
            var toType = parseResult.GetValue(toTypeArg)!;
            var fromId = parseResult.GetValue(fromIdOption)!;
            var toId = parseResult.GetValue(toIdOption)!;
            var typeId = parseResult.GetValue(typeIdOption);
            var category = parseResult.GetValue(categoryOption)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var parsedCategory = ParseCategory(category);
                var batchInput = new BatchInputPublicAssociationMultiPost
                {
                    Inputs =
                    [
                        new PublicAssociationMultiPost
                        {
                            From = new PublicObjectId { Id = fromId },
                            To = new PublicObjectId { Id = toId },
                            Types =
                            [
                                new AssociationSpec
                                {
                                    AssociationCategory = parsedCategory,
                                    AssociationTypeId = typeId,
                                },
                            ],
                        },
                    ],
                };

                var client = new HubSpotCRMAssociationsV4Client(cli.Adapter);
                var result = await client.Crm.V4.Associations[fromType][toType].Batch.Create.PostAsync(
                    batchInput,
                    cancellationToken: cancellationToken);

                if (result?.Results is { Count: > 0 })
                {
                    var items = result.Results.Select(r => (IDictionary<string, object?>)new Dictionary<string, object?>(StringComparer.Ordinal)
                    {
                        ["fromObjectId"] = r.FromObjectId,
                        ["fromObjectTypeId"] = r.FromObjectTypeId,
                        ["toObjectId"] = r.ToObjectId,
                        ["toObjectTypeId"] = r.ToObjectTypeId,
                    }).ToList();

                    cli.Formatter.WriteCollection(items, cli.Out);
                }
                else
                {
                    cli.Formatter.WriteMessage("Association created", cli.Out);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static AssociationSpec_associationCategory ParseCategory(string category) => category.ToUpperInvariant() switch
    {
        "HUBSPOT_DEFINED" => AssociationSpec_associationCategory.HUBSPOT_DEFINED,
        "USER_DEFINED" => AssociationSpec_associationCategory.USER_DEFINED,
        "INTEGRATOR_DEFINED" => AssociationSpec_associationCategory.INTEGRATOR_DEFINED,
        _ => throw new ArgumentException(
            $"Unknown association category: '{category}'. Valid: HUBSPOT_DEFINED, USER_DEFINED, INTEGRATOR_DEFINED"),
    };
}
