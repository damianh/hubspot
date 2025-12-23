namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class SimplePublicObject
{
    public bool Archived { get; set; } = false;
    public DateTimeOffset? ArchivedAt { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required string Id { get; set; }
    public Dictionary<string, string> Properties { get; set; } = new();
    public Dictionary<string, List<ValueWithTimestamp>>? PropertiesWithHistory { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
}
