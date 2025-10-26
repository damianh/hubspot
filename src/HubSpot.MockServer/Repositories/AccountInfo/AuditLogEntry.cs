namespace DamianH.HubSpot.MockServer.Repositories.AccountInfo;

internal class AuditLogEntry
{
    public required string Id { get; set; }
    public required string EventType { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
    public required string UserId { get; set; }
    public string? UserEmail { get; set; }
    public string? IpAddress { get; set; }
    public string? Action { get; set; }
    public Dictionary<string, object> Details { get; set; } = new();
}
