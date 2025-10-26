namespace DamianH.HubSpot.MockServer.Repositories.CallingExtension;

internal class CallRecording
{
    public string Id { get; set; } = string.Empty;
    public string EngagementId { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int Duration { get; set; }
    public string? Transcript { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
