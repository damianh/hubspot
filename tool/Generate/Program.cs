using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

return await Generator.RunAsync();

internal static partial class Generator
{
    internal const string CatalogUrl = "https://api.hubspot.com/public/api/spec/v1/specs";

    internal static async Task<int> RunAsync()
    {
        var root = FindRepositoryRoot(AppContext.BaseDirectory);
        using var client = new HttpClient();
        var catalog = await client.GetFromJsonAsync<HubSpotApiCatalog>(CatalogUrl)
            ?? throw new InvalidDataException("HubSpot returned a null API catalog.");
        var entries = GetEntries(catalog);
        await RunAsyncCommand("dotnet", ["tool", "restore"], root);
        return await GenerateAsync(root, entries, async (entry, output) =>
        {
            await RunAsyncCommand("dotnet",
                ["tool", "run", "kiota", "generate", "--openapi", entry.OpenApi,
                 "--clear-cache", "--language", "csharp", "--output", output, "--class-name", entry.ClassName,
                 "--namespace-name", entry.NamespaceName], root);
        });
    }

    private static Task RunAsyncCommand(string command, string[] args, string root) =>
        SimpleExec.Command.RunAsync(command, args, workingDirectory: root);

    internal static string FindRepositoryRoot(string start)
    {
        for (var directory = new DirectoryInfo(start); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Join(directory.FullName, "tool", "Generate", "Generate.csproj")) &&
                File.Exists(Path.Join(directory.FullName, "src", "HubSpot.KiotaClient", "HubSpot.KiotaClient.csproj")))
            {
                return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException("Cannot locate the HubSpot repository from the generator executable.");
    }

    internal static List<GenerationEntry> GetEntries(HubSpotApiCatalog catalog)
    {
        if (catalog.Results is not { Count: > 0 })
        {
            throw new InvalidDataException("HubSpot returned an empty API catalog.");
        }

        var entries = new List<GenerationEntry>();
        var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var api in catalog.Results)
        {
            if (api is null)
            {
                throw new InvalidDataException("HubSpot returned a null API entry.");
            }
            if (api.Versions is not { Count: > 0 })
            {
                throw new InvalidDataException($"API '{api.Name}' has no versions.");
            }

            var group = Sanitize(api.Group, false);
            var name = Sanitize(api.Name, true);
            foreach (var version in api.Versions)
            {
                if (version is null)
                {
                    throw new InvalidDataException($"API '{api.Name}' has a null version.");
                }
                var label = "V" + Sanitize(version.Version, true, true);
                if (!Uri.TryCreate(version.OpenApi, UriKind.Absolute, out var uri) ||
                    uri.Scheme != Uri.UriSchemeHttps || uri.Host != "api.hubspot.com" ||
                    !uri.AbsolutePath.StartsWith("/public/api/spec/", StringComparison.Ordinal) ||
                    uri.UserInfo.Length != 0 || uri.Fragment.Length != 0)
                {
                    throw new InvalidDataException($"Invalid OpenAPI URL for {group}/{name}/{label}.");
                }

                var path = Path.Join(group, name, label);
                if (!paths.Add(path))
                {
                    throw new InvalidDataException($"Duplicate generated client identity: {path}.");
                }

                entries.Add(new(path, version.OpenApi,
                    $"HubSpot{group}{name}{label}Client",
                    $"DamianH.HubSpot.KiotaClient.{group}.{name}.{label}"));
            }
        }

        return entries.OrderBy(entry => entry.RelativePath, StringComparer.Ordinal).ToList();
    }

    private static string Sanitize(string? value, bool removeHyphens, bool version = false)
    {
        var sanitized = value?.Replace(" ", "");
        if (removeHyphens)
        {
            sanitized = sanitized?.Replace("-", "");
        }
        if (version)
        {
            sanitized = sanitized?.Replace(".", "");
        }

        if (string.IsNullOrEmpty(sanitized) ||
            !(version ? VersionPattern() : IdentifierPattern()).IsMatch(sanitized))
        {
            throw new InvalidDataException($"Invalid catalog identifier: '{value}'.");
        }

        return sanitized;
    }

    [GeneratedRegex(@"\A[A-Za-z_][A-Za-z0-9_]*\z")]
    private static partial Regex IdentifierPattern();

    [GeneratedRegex(@"\A[0-9][A-Za-z0-9_]*\z")]
    private static partial Regex VersionPattern();

    internal static async Task<int> GenerateAsync(
        string root, IReadOnlyList<GenerationEntry> entries, Func<GenerationEntry, string, Task> generate)
    {
        if (entries.Count == 0)
        {
            throw new InvalidDataException("Cannot generate an empty catalog.");
        }

        using var manifest = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Join(root, ".config", "dotnet-tools.json")));
        var kiotaVersion = manifest.RootElement.GetProperty("tools").GetProperty("microsoft.openapi.kiota")
            .GetProperty("version").GetString()
            ?? throw new InvalidDataException("The Kiota tool manifest must specify a version.");
        var project = Path.Join(root, "src", "HubSpot.KiotaClient");
        var output = Path.Join(project, "Generated");
        var staging = Path.Join(project, $"Generated.staging-{Guid.NewGuid():N}");
        var backup = Path.Join(project, $"Generated.backup-{Guid.NewGuid():N}");
        var failureReport = Path.Join(project, "generated-report-failed.txt");
        var report = new List<string> { $"Catalog: {CatalogUrl}", $"Kiota: {kiotaVersion}", $"Expected clients: {entries.Count}", "Succeeded:" };
        var succeeded = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
        var failures = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
        Directory.CreateDirectory(staging);
        try
        {
            await Parallel.ForEachAsync(entries, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (entry, _) =>
            {
                var destination = Path.Join(staging, entry.RelativePath);
                Console.WriteLine($"Generating {entry.RelativePath}");
                try
                {
                    await generate(entry, destination);
                    var lockPath = Path.Join(destination, "kiota-lock.json");
                    using var document = JsonDocument.Parse(await File.ReadAllTextAsync(lockPath));
                    if (document.RootElement.GetProperty("descriptionLocation").GetString() != entry.OpenApi ||
                        document.RootElement.GetProperty("kiotaVersion").GetString() != kiotaVersion ||
                        document.RootElement.GetProperty("clientClassName").GetString() != entry.ClassName ||
                        document.RootElement.GetProperty("clientNamespaceName").GetString() != entry.NamespaceName)
                    {
                        throw new InvalidDataException("Generated lock does not match the requested source, identity, or pinned Kiota version.");
                    }

                    succeeded[entry.RelativePath] = $"  {entry.RelativePath}: {entry.OpenApi}";
                }
                catch (Exception ex) when (ex is SimpleExec.ExitCodeException or IOException or InvalidDataException or JsonException or KeyNotFoundException or InvalidOperationException)
                {
                    failures[entry.RelativePath] = $"  {entry.RelativePath}: {entry.OpenApi}{Environment.NewLine}    {ex.Message}";
                    Console.Error.WriteLine(failures[entry.RelativePath]);
                }
            });

            report.AddRange(succeeded.OrderBy(item => item.Key, StringComparer.Ordinal).Select(item => item.Value));
            report.Add("Failed:");
            report.AddRange(failures.OrderBy(item => item.Key, StringComparer.Ordinal).Select(item => item.Value));
            if (failures.Count != 0)
            {
                await File.WriteAllLinesAsync(failureReport, report);
                Console.Error.WriteLine($"Generation failed; existing clients were preserved. See {failureReport}");
                return 1;
            }

            var actual = Directory.GetFiles(staging, "kiota-lock.json", SearchOption.AllDirectories)
                .Select(path => Path.GetRelativePath(staging, Path.GetDirectoryName(path)!))
                .ToHashSet(StringComparer.Ordinal);
            if (!actual.SetEquals(entries.Select(entry => entry.RelativePath)))
            {
                throw new InvalidDataException("Generated client inventory does not exactly match the catalog.");
            }

            await File.WriteAllLinesAsync(Path.Join(staging, "generated-report.txt"), report);
            var hadOutput = Directory.Exists(output);
            if (hadOutput)
            {
                Directory.Move(output, backup);
            }

            try
            {
                Directory.Move(staging, output);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                if (hadOutput)
                {
                    Directory.Move(backup, output);
                }
                throw;
            }

            if (hadOutput)
            {
                Directory.Delete(backup, true);
            }
            if (File.Exists(failureReport))
            {
                File.Delete(failureReport);
            }
            Console.WriteLine($"Generated all {entries.Count} clients successfully.");
            return 0;
        }
        finally
        {
            if (Directory.Exists(staging))
            {
                Directory.Delete(staging, true);
            }
        }
    }
}

internal sealed record GenerationEntry(string RelativePath, string OpenApi, string ClassName, string NamespaceName);

internal sealed class HubSpotApiCatalog
{
    [JsonPropertyName("results")]
    public List<ApiResult>? Results { get; set; }
}

internal sealed class ApiResult
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("group")]
    public required string Group { get; set; }

    [JsonPropertyName("versions")]
    public List<ApiVersion>? Versions { get; set; }
}

internal sealed class ApiVersion
{
    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("openApi")]
    public required string OpenApi { get; set; }
}
