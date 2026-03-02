namespace DamianH.HubSpot.Cli.Output;

internal static class OutputFormatterFactory
{
    public static IOutputFormatter Create(OutputFormat format) => format switch
    {
        OutputFormat.Json => new JsonOutputFormatter(),
        OutputFormat.Table => new TableOutputFormatter(),
        OutputFormat.Csv => new CsvOutputFormatter(),
        _ => throw new ArgumentOutOfRangeException(nameof(format), format, "Unsupported output format"),
    };
}
