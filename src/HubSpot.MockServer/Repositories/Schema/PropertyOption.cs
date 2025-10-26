namespace DamianH.HubSpot.MockServer.Repositories.Schema;

internal class PropertyOption
{
    public required string Label { get; init; }
    public required string Value { get; init; }
    public int? DisplayOrder { get; init; }
    public bool Hidden { get; init; }
}
