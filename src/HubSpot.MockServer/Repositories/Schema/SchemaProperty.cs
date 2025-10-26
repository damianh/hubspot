namespace DamianH.HubSpot.MockServer.Repositories.Schema;

internal class SchemaProperty
{
    public required string Name { get; init; }
    public required string Label { get; set; }
    public required string Type { get; init; }
    public required string FieldType { get; init; }
    public string GroupName { get; set; } = "default";
    public string? Description { get; set; }
    public List<PropertyOption> Options { get; set; } = [];
    public int DisplayOrder { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool Archived { get; set; }
}
