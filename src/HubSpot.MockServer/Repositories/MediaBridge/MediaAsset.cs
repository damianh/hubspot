namespace DamianH.HubSpot.MockServer.Repositories.MediaBridge;

internal class MediaAsset
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Type { get; set; } // "IMAGE", "VIDEO", "DOCUMENT"
    public long? Size { get; set; }
    public string? MimeType { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? ExternalId { get; set; }
    public string? ProviderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
