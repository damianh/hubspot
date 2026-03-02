using System.Text.Json;

namespace DamianH.HubSpot.Cli.Output;

internal sealed class JsonOutputFormatter : IOutputFormatter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public void WriteObject(IDictionary<string, object?> obj, TextWriter writer) => writer.WriteLine(JsonSerializer.Serialize(obj, SerializerOptions));

    public void WriteCollection(IReadOnlyList<IDictionary<string, object?>> items, TextWriter writer) => writer.WriteLine(JsonSerializer.Serialize(items, SerializerOptions));

    public void WriteMessage(string message, TextWriter writer)
    {
        var obj = new Dictionary<string, object?> { ["message"] = message };
        writer.WriteLine(JsonSerializer.Serialize(obj, SerializerOptions));
    }
}
