using System.CommandLine;
using DamianH.HubSpot.Cli.Utilities;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class CrmObjectCommand
{
    public static Command Create(string objectType)
    {
        var displayName = objectType.Replace('_', ' ');
        var command = new Command(objectType, $"Manage {displayName}");

        command.Add(CreateListCommand(objectType));
        command.Add(CreateGetCommand(objectType));
        command.Add(CreateCreateCommand(objectType));
        command.Add(CreateUpdateCommand(objectType));
        command.Add(CreateDeleteCommand(objectType));
        command.Add(CreateSearchCommand(objectType));

        return command;
    }

    private static Command CreateListCommand(string objectType)
    {
        var command = new Command("list", $"List {objectType}");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results per page", DefaultValueFactory = _ => 10 };
        var propertiesOption = new Option<string[]>("--properties") { Description = "Properties to return", AllowMultipleArgumentsPerToken = true };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var archivedOption = new Option<bool>("--archived") { Description = "Return archived objects only", DefaultValueFactory = _ => false };

        command.Add(limitOption);
        command.Add(propertiesOption);
        command.Add(afterOption);
        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var properties = parseResult.GetValue(propertiesOption);
            var after = parseResult.GetValue(afterOption);
            var archived = parseResult.GetValue(archivedOption);

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                var result = await client.Crm.V3.Objects[objectType].GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (properties is { Length: > 0 })
                    {
                        q.QueryParameters.Properties = properties;
                    }

                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }
                }, cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No results", cli.Out);
                    return;
                }

                var items = ResponseMapper.MapCollection(result);
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

    private static Command CreateGetCommand(string objectType)
    {
        var command = new Command("get", $"Get a {objectType} by ID");

        var idArgument = new Argument<string>("id") { Description = "Object ID" };
        var propertiesOption = new Option<string[]>("--properties") { Description = "Properties to return", AllowMultipleArgumentsPerToken = true };
        var associationsOption = new Option<string[]>("--associations") { Description = "Associated object types to include", AllowMultipleArgumentsPerToken = true };

        command.Add(idArgument);
        command.Add(propertiesOption);
        command.Add(associationsOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var id = parseResult.GetValue(idArgument);
            var properties = parseResult.GetValue(propertiesOption);
            var associations = parseResult.GetValue(associationsOption);

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                var result = await client.Crm.V3.Objects[objectType][id!].GetAsync(q =>
                {
                    if (properties is { Length: > 0 })
                    {
                        q.QueryParameters.Properties = properties;
                    }

                    if (associations is { Length: > 0 })
                    {
                        q.QueryParameters.Associations = associations;
                    }
                }, cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Not found", cli.Error);
                    return;
                }

                var mapped = ResponseMapper.MapObject(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateCreateCommand(string objectType)
    {
        var command = new Command("create", $"Create a new {objectType}");

        var propertiesOption = new Option<string[]>("--properties", "-p")
        {
            Description = "Properties as key=value pairs",
            Required = true,
            AllowMultipleArgumentsPerToken = true,
        };

        command.Add(propertiesOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var properties = parseResult.GetValue(propertiesOption)!;

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var parsed = PropertyParser.Parse(properties);
                var input = new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = parsed,
                    },
                };

                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                var result = await client.Crm.V3.Objects[objectType].PostAsync(input, cancellationToken: cancellationToken);

                if (result is not null)
                {
                    var mapped = ResponseMapper.MapObject(result);
                    cli.Formatter.WriteObject(mapped, cli.Out);
                    cli.Formatter.WriteMessage($"Created (ID: {result.Id})", cli.Out);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateUpdateCommand(string objectType)
    {
        var command = new Command("update", $"Update a {objectType} by ID");

        var idArgument = new Argument<string>("id") { Description = "Object ID" };
        var propertiesOption = new Option<string[]>("--properties", "-p")
        {
            Description = "Properties as key=value pairs",
            Required = true,
            AllowMultipleArgumentsPerToken = true,
        };

        command.Add(idArgument);
        command.Add(propertiesOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var id = parseResult.GetValue(idArgument);
            var properties = parseResult.GetValue(propertiesOption)!;

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var parsed = PropertyParser.Parse(properties);
                var input = new SimplePublicObjectInput
                {
                    Properties = new SimplePublicObjectInput_properties
                    {
                        AdditionalData = parsed,
                    },
                };

                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                var result = await client.Crm.V3.Objects[objectType][id!].PatchAsync(input, cancellationToken: cancellationToken);

                if (result is not null)
                {
                    var mapped = ResponseMapper.MapObject(result);
                    cli.Formatter.WriteObject(mapped, cli.Out);
                }
                else
                {
                    cli.Formatter.WriteMessage("Updated", cli.Out);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateDeleteCommand(string objectType)
    {
        var command = new Command("delete", $"Delete a {objectType} by ID");

        var idArgument = new Argument<string>("id") { Description = "Object ID" };

        command.Add(idArgument);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var id = parseResult.GetValue(idArgument);

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                await client.Crm.V3.Objects[objectType][id!].DeleteAsync(cancellationToken: cancellationToken);

                var result = new Dictionary<string, object?> { ["deleted"] = true, ["id"] = id };
                cli.Formatter.WriteObject(result, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateSearchCommand(string objectType)
    {
        var command = new Command("search", $"Search {objectType}");

        var filterOption = new Option<string[]>("--filter") { Description = "Filter: 'propertyName OPERATOR value'", AllowMultipleArgumentsPerToken = true };
        var queryOption = new Option<string?>("--query") { Description = "Free-text search query" };
        var propertiesOption = new Option<string[]>("--properties") { Description = "Properties to return", AllowMultipleArgumentsPerToken = true };
        var limitOption = new Option<int>("--limit") { Description = "Maximum results", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var sortOption = new Option<string[]>("--sort") { Description = "Sort properties", AllowMultipleArgumentsPerToken = true };

        command.Add(filterOption);
        command.Add(queryOption);
        command.Add(propertiesOption);
        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(sortOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var filters = parseResult.GetValue(filterOption);
            var query = parseResult.GetValue(queryOption);
            var properties = parseResult.GetValue(propertiesOption);
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var sorts = parseResult.GetValue(sortOption);

            var cli = CreateCliContext(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var searchRequest = new PublicObjectSearchRequest
                {
                    Limit = limit,
                };

                if (query is not null)
                {
                    searchRequest.Query = query;
                }

                if (after is not null)
                {
                    searchRequest.After = after;
                }

                if (properties is { Length: > 0 })
                {
                    searchRequest.Properties = [.. properties];
                }

                if (sorts is { Length: > 0 })
                {
                    searchRequest.Sorts = [.. sorts];
                }

                if (filters is { Length: > 0 })
                {
                    var parsedFilters = new List<Filter>();
                    foreach (var filterExpr in filters)
                    {
                        var parts = filterExpr.Split(' ', 3);
                        if (parts.Length < 3)
                        {
                            throw new ArgumentException(
                                $"Invalid filter format: '{filterExpr}'. Expected: 'propertyName OPERATOR value'");
                        }

                        var filterOp = ParseOperator(parts[1]);
                        parsedFilters.Add(new Filter
                        {
                            PropertyName = parts[0],
                            Operator = filterOp,
                            Value = parts[2],
                        });
                    }

                    searchRequest.FilterGroups =
                    [
                        new FilterGroup { Filters = parsedFilters },
                    ];
                }

                var client = new HubSpotCRMObjectsV3Client(cli.Adapter);
                var result = await client.Crm.V3.Objects[objectType].Search.PostAsync(searchRequest, cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No results", cli.Out);
                    return;
                }

                var items = ResponseMapper.MapCollection(result);
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet && result.Total is { } total)
                {
                    cli.Error.WriteLine($"Total: {total}");
                }

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

    private static CliContext? CreateCliContext(ParseResult parseResult) => CliContext.FromParseResult(parseResult);

    private static Filter_operator ParseOperator(string op) => op.ToUpperInvariant() switch
    {
        "EQ" => Filter_operator.EQ,
        "NEQ" => Filter_operator.NEQ,
        "LT" => Filter_operator.LT,
        "LTE" => Filter_operator.LTE,
        "GT" => Filter_operator.GT,
        "GTE" => Filter_operator.GTE,
        "BETWEEN" => Filter_operator.BETWEEN,
        "IN" => Filter_operator.IN,
        "HAS" => Filter_operator.HAS_PROPERTY,
        "NOT_HAS" => Filter_operator.NOT_HAS_PROPERTY,
        "CONTAINS" => Filter_operator.CONTAINS_TOKEN,
        _ => throw new ArgumentException($"Unknown filter operator: '{op}'. Valid: EQ, NEQ, LT, LTE, GT, GTE, BETWEEN, IN, HAS, NOT_HAS, CONTAINS"),
    };
}
