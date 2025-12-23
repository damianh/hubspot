namespace DamianH.HubSpot.MockServer.Repositories.Sequence;

internal record Sequence
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public List<SequenceStep> Steps { get; init; } = [];
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
