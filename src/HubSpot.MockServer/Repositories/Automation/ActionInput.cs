namespace DamianH.HubSpot.MockServer.Repositories.Automation;

public record ActionInput
{
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = ""; // TEXT, NUMBER, BOOLEAN, etc.
    public string Label { get; init; } = string.Empty;
    public bool Required { get; init; }
}
