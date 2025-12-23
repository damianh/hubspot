namespace DamianH.HubSpot.MockServer.Repositories.Transcription;

internal class Transcription
{
    public string Id { get; set; } = string.Empty;
    public string EngagementId { get; set; } = string.Empty;
    public string? Text { get; set; }
    public double Confidence { get; set; }
    public string? Language { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
