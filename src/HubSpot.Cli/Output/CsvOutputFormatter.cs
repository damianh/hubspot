namespace DamianH.HubSpot.Cli.Output;

internal sealed class CsvOutputFormatter : IOutputFormatter
{
    public void WriteObject(IDictionary<string, object?> obj, TextWriter writer) => WriteCollection([obj], writer);

    public void WriteCollection(IReadOnlyList<IDictionary<string, object?>> items, TextWriter writer)
    {
        if (items.Count == 0)
        {
            return;
        }

        // Collect all unique keys preserving insertion order
        var columns = new List<string>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var item in items)
        {
            foreach (var key in item.Keys)
            {
                if (seen.Add(key))
                {
                    columns.Add(key);
                }
            }
        }

        // Write header
        writer.WriteLine(string.Join(",", columns.Select(EscapeCsvField)));

        // Write rows
        foreach (var item in items)
        {
            var values = columns.Select(col =>
            {
                var value = item.TryGetValue(col, out var v) ? v?.ToString() ?? "" : "";
                return EscapeCsvField(value);
            });
            writer.WriteLine(string.Join(",", values));
        }
    }

    public void WriteMessage(string message, TextWriter writer) => writer.WriteLine(message);

    private static string EscapeCsvField(string value)
    {
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }
}
