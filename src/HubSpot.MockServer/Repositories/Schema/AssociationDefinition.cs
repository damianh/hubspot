namespace DamianH.HubSpot.MockServer.Repositories.Schema;

internal class AssociationDefinition
{
    public required string Id { get; init; }
    public required string FromObjectType { get; init; }
    public required string ToObjectType { get; init; }
    public required string Name { get; init; }
    public required string Label { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}
