namespace DamianH.HubSpot.MockServer.Repositories.Timeline;

internal class TimelineEvent
{
    public required string Id { get; init; }
    public required string EventTemplateId { get; init; }
    public required string ObjectType { get; init; }
    public required string ObjectId { get; init; }
    public required Dictionary<string, string> Tokens { get; init; }
    public DateTimeOffset Timestamp { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
