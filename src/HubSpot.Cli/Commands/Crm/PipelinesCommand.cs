using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3;
using PipelineModels = DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models;

namespace DamianH.HubSpot.Cli.Commands.Crm;

internal static class PipelinesCommand
{
    public static Command Create()
    {
        var command = new Command("pipelines", "Manage CRM pipelines");

        command.Add(CreateListCommand());
        command.Add(CreateGetCommand());

        return command;
    }

    private static Command CreateListCommand()
    {
        var command = new Command("list", "List pipelines for an object type");

        var objectTypeArg = new Argument<string>("objectType") { Description = "CRM object type (typically deals or tickets)" };

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
                var client = new HubSpotCRMPipelinesV3Client(cli.Adapter);
                var result = await client.Crm.V3.Pipelines[objectType].GetAsync(cancellationToken: cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No pipelines found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapPipeline).ToList();
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
        var command = new Command("get", "Get a pipeline with stages");

        var objectTypeArg = new Argument<string>("objectType") { Description = "CRM object type" };
        var pipelineIdArg = new Argument<string>("pipelineId") { Description = "Pipeline ID" };

        command.Add(objectTypeArg);
        command.Add(pipelineIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var objectType = parseResult.GetValue(objectTypeArg)!;
            var pipelineId = parseResult.GetValue(pipelineIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCRMPipelinesV3Client(cli.Adapter);
                var result = await client.Crm.V3.Pipelines[objectType][pipelineId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Pipeline not found", cli.Error);
                    return;
                }

                var mapped = MapPipeline(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapPipeline(PipelineModels.Pipeline pipeline)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = pipeline.Id,
            ["label"] = pipeline.Label,
            ["displayOrder"] = pipeline.DisplayOrder,
            ["archived"] = pipeline.Archived,
            ["createdAt"] = pipeline.CreatedAt?.ToString("O"),
            ["updatedAt"] = pipeline.UpdatedAt?.ToString("O"),
            ["stageCount"] = pipeline.Stages?.Count ?? 0,
        };

        if (pipeline.Stages is { Count: > 0 })
        {
            result["stages"] = pipeline.Stages.Select(s => new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                ["id"] = s.Id,
                ["label"] = s.Label,
                ["displayOrder"] = s.DisplayOrder,
                ["archived"] = s.Archived,
            }).ToList();
        }

        return result;
    }
}
