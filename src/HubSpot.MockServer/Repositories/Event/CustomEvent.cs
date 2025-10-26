namespace DamianH.HubSpot.MockServer.Repositories.Event;

internal class CustomEvent
{
    public string? Id { get; set; }
    public string EventName { get; set; } = null!;
    public string? Email { get; set; }
    public string? ObjectId { get; set; }
    public string? ObjectType { get; set; }
    public DateTime OccurredAt { get; set; }
    public Dictionary<string, object>? Properties { get; set; }
}
