using System.Net.Http.Json;
using System.Text.Json.Serialization;
using static SimpleExec.Command;

var client = new HttpClient();
var catalog = await client.GetFromJsonAsync<HubSpotApiCatalog>("https://api.hubspot.com/public/api/spec/v1/specs");
const string ProjectPath = "HubSpot.KiotaClient";
var generatedPath = Path.Join(ProjectPath, "Generated");
var reportFilePath = Path.Join(generatedPath, "generated-report.txt");
if (Directory.Exists(generatedPath))
{
    Directory.Delete(generatedPath, true);
}

var succeeded = new List<string>();
var failed = new List<string>();

void RunKiota(string group, string name, int version, string openApiUrl)
{
    name = name.Replace(" ", "");
    name = name.Replace("-", "");
    group = group.Replace(" ", "");
    var namespaceName = $"DamianH.HubSpot.KiotaClient.{group}.{name}.V{version}";
    var output = Path.Join(generatedPath, group, name, $"V{version}");
    var className = $"HubSpot{group}{name}V{version}Client";

    var args = $"kiota generate --openapi {openApiUrl} --language csharp --output {output} --class-name {className} --namespace-name {namespaceName}";

    try
    {
        Run("dotnet", args);
        succeeded.Add(args);
    }
    catch (SimpleExec.ExitCodeException)
    {
        failed.Add(args);
    }
}

foreach (var result in catalog!.Results)
{
    foreach (var resultVersion in result.Versions)
    {
        RunKiota(result.Group, result.Name, resultVersion.Version, resultVersion.OpenApi);
    }
}

if (File.Exists("reportFilePath"))
{
    File.Delete(reportFilePath);
}

void WriteLine(string value)
{
    Console.WriteLine(value);
    File.AppendAllText(reportFilePath, value + Environment.NewLine);
}

WriteLine("Succeeded:");
foreach (var item in succeeded)
{
    WriteLine($"  {item}");
}

WriteLine("Failed:");
foreach (var item in failed)
{
    WriteLine($"  {item}");
}

internal class HubSpotApiCatalog
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; } = [];
}

internal class Result
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("group")]
    public required string Group { get; set; }

    [JsonPropertyName("versions")]
    public List<ApiVersion> Versions { get; set; } = [];
}

internal class ApiVersion
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("stage")]
    public required string Stage { get; set; }

    [JsonPropertyName("requirements")]
    public required ApiRequirements Requirements { get; set; }

    [JsonPropertyName("introduction")]
    public required string Introduction { get; set; }

    [JsonPropertyName("useCase")]
    public required string UseCase { get; set; }

    [JsonPropertyName("openApi")]
    public required string OpenApi { get; set; }

    [JsonPropertyName("relatedDocumentation")]
    public List<RelatedDocumentation> RelatedDocumentation { get; set; } = [];

    [JsonPropertyName("documentationBanner")]
    public required string DocumentationBanner { get; set; }
}

internal class ApiRequirements
{
    [JsonPropertyName("marketing")]
    public required string Marketing { get; set; }

    [JsonPropertyName("sales")]
    public required string Sales { get; set; }

    [JsonPropertyName("service")]
    public required string Service { get; set; }

    [JsonPropertyName("cms")]
    public required string Cms { get; set; }
}

internal class RelatedDocumentation
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("url")]
    public required string Url { get; set; }
}
