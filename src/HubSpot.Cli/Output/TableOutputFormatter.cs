using System.Text;

namespace DamianH.HubSpot.Cli.Output;

internal sealed class TableOutputFormatter : IOutputFormatter
{
    public void WriteObject(IDictionary<string, object?> obj, TextWriter writer) => WriteCollection([obj], writer);

    public void WriteCollection(IReadOnlyList<IDictionary<string, object?>> items, TextWriter writer)
    {
        if (items.Count == 0)
        {
            writer.WriteLine("(no results)");
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

        // Calculate column widths
        var widths = new int[columns.Count];
        for (var i = 0; i < columns.Count; i++)
        {
            widths[i] = columns[i].Length;
        }

        foreach (var item in items)
        {
            for (var i = 0; i < columns.Count; i++)
            {
                var value = item.TryGetValue(columns[i], out var v) ? FormatValue(v) : "";
                if (value.Length > widths[i])
                {
                    widths[i] = value.Length;
                }
            }
        }

        // Write header
        var sb = new StringBuilder();
        for (var i = 0; i < columns.Count; i++)
        {
            if (i > 0)
            {
                sb.Append("  ");
            }

            sb.Append(columns[i].PadRight(widths[i]));
        }

        writer.WriteLine(sb.ToString().TrimEnd());

        // Write separator
        sb.Clear();
        for (var i = 0; i < columns.Count; i++)
        {
            if (i > 0)
            {
                sb.Append("  ");
            }

            sb.Append(new string('-', widths[i]));
        }

        writer.WriteLine(sb.ToString().TrimEnd());

        // Write rows
        foreach (var item in items)
        {
            sb.Clear();
            for (var i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append("  ");
                }

                var value = item.TryGetValue(columns[i], out var v) ? FormatValue(v) : "";
                sb.Append(value.PadRight(widths[i]));
            }

            writer.WriteLine(sb.ToString().TrimEnd());
        }
    }

    public void WriteMessage(string message, TextWriter writer) => writer.WriteLine(message);

    private static string FormatValue(object? value) => value?.ToString() ?? "";
}
