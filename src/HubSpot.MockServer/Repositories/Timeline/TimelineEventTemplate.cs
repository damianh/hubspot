namespace DamianH.HubSpot.MockServer.Repositories.Timeline;

internal class TimelineEventTemplate
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required string ObjectType { get; init; }
    public required List<EventToken> Tokens { get; set; }
    public required string HeaderTemplate { get; set; }
    public required string DetailTemplate { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
}
