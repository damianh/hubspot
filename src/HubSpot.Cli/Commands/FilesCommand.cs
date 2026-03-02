using System.CommandLine;
using DamianH.HubSpot.KiotaClient.Files.Files.V3;
using FileModels = DamianH.HubSpot.KiotaClient.Files.Files.V3.Models;

namespace DamianH.HubSpot.Cli.Commands;

internal static class FilesCommand
{
    public static Command Create()
    {
        var command = new Command("files", "File management operations");

        command.Add(CreateSearchCommand());
        command.Add(CreateGetCommand());
        command.Add(CreateDeleteCommand());

        return command;
    }

    private static Command CreateSearchCommand()
    {
        var command = new Command("search", "Search files");

        var nameOption = new Option<string?>("--name") { Description = "Filter by file name" };
        var typeOption = new Option<string?>("--type") { Description = "Filter by file type" };
        var extensionOption = new Option<string?>("--extension") { Description = "Filter by file extension" };
        var pathOption = new Option<string?>("--path") { Description = "Filter by file path" };
        var limitOption = new Option<int>("--limit") { Description = "Maximum results", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var sortOption = new Option<string[]?>("--sort") { Description = "Sort by field(s)", AllowMultipleArgumentsPerToken = true };

        command.Add(nameOption);
        command.Add(typeOption);
        command.Add(extensionOption);
        command.Add(pathOption);
        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(sortOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var name = parseResult.GetValue(nameOption);
            var type = parseResult.GetValue(typeOption);
            var extension = parseResult.GetValue(extensionOption);
            var path = parseResult.GetValue(pathOption);
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var sort = parseResult.GetValue(sortOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotFilesFilesV3Client(cli.Adapter);
                var result = await client.Files.V3.Files.Search.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    if (name is not null)
                    {
                        q.QueryParameters.Name = name;
                    }

                    if (type is not null)
                    {
                        q.QueryParameters.Type = type;
                    }

                    if (extension is not null)
                    {
                        q.QueryParameters.Extension = extension;
                    }

                    if (path is not null)
                    {
                        q.QueryParameters.Path = path;
                    }

                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }

                    if (sort is { Length: > 0 })
                    {
                        q.QueryParameters.Sort = sort;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No files found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapFile).ToList();
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
        var command = new Command("get", "Get a file by ID");

        var fileIdArg = new Argument<string>("fileId") { Description = "File ID" };

        command.Add(fileIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var fileId = parseResult.GetValue(fileIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotFilesFilesV3Client(cli.Adapter);
                var result = await client.Files.V3.Files[fileId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("File not found", cli.Error);
                    return;
                }

                var mapped = MapFile(result);
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
        var command = new Command("delete", "Delete a file");

        var fileIdArg = new Argument<string>("fileId") { Description = "File ID" };

        command.Add(fileIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var fileId = parseResult.GetValue(fileIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotFilesFilesV3Client(cli.Adapter);
                await client.Files.V3.Files[fileId].DeleteAsync(cancellationToken: cancellationToken);

                cli.Formatter.WriteMessage($"File {fileId} deleted", cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapFile(FileModels.FileObject file) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = file.Id,
        ["name"] = file.Name,
        ["extension"] = file.Extension,
        ["size"] = file.Size,
        ["encoding"] = file.Encoding,
        ["url"] = file.DefaultHostingUrl,
        ["access"] = file.Access?.ToString(),
        ["archived"] = file.Archived,
        ["width"] = file.Width,
        ["height"] = file.Height,
        ["createdAt"] = file.CreatedAt?.ToString("O"),
        ["updatedAt"] = file.UpdatedAt?.ToString("O"),
    };
}
