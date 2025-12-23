namespace DamianH.HubSpot.MockServer.Repositories.Event;

internal class EventPropertyDefinition
{
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public string Type { get; set; } = "string";
}
