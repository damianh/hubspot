namespace DamianH.HubSpot.MockServer.Repositories.Timeline;

internal class EventToken
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public required string Type { get; init; } // string, number, date, enumeration
    public List<string>? Options { get; init; } // For enumeration type
}
