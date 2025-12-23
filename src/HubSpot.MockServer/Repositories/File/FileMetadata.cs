namespace DamianH.HubSpot.MockServer.Repositories.File;

internal class FileMetadata
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Url { get; set; } = null!;
    public byte[] Content { get; set; } = null!;
}
