namespace DamianH.HubSpot.MockServer.Repositories.Schema;

internal class ObjectSchema
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Dictionary<string, string> Labels { get; set; }
    public required string PrimaryDisplayProperty { get; set; }
    public List<string> RequiredProperties { get; set; } = [];
    public List<string> SearchableProperties { get; set; } = [];
    public List<string> SecondaryDisplayProperties { get; set; } = [];
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool Archived { get; set; }
}
