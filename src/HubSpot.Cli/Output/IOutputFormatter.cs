namespace DamianH.HubSpot.Cli.Output;

internal interface IOutputFormatter
{
    void WriteObject(IDictionary<string, object?> obj, TextWriter writer);

    void WriteCollection(IReadOnlyList<IDictionary<string, object?>> items, TextWriter writer);

    void WriteMessage(string message, TextWriter writer);
}
