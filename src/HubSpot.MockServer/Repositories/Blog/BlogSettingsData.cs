namespace DamianH.HubSpot.MockServer.Repositories.Blog;

internal class BlogSettingsData
{
    public required string BlogId { get; set; }
    public required string Name { get; set; }
    public string Language { get; set; } = "en";
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public List<string> PublicAccessRules { get; set; } = [];
    public string? HtmlTitle { get; set; }
    public string? Domain { get; set; }
}
