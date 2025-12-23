namespace DamianH.HubSpot.MockServer.Repositories.Blog;

internal class BlogSettingsRevision
{
    public required string Id { get; set; }
    public required string BlogId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Name { get; set; }
    public string? Language { get; set; }
}
