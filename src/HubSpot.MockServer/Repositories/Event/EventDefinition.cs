namespace DamianH.HubSpot.MockServer.Repositories.Event;

internal class EventDefinition
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<EventPropertyDefinition>? PropertyDefinitions { get; set; }
}
