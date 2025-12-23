namespace DamianH.HubSpot.MockServer.Repositories.SiteSearch;

internal class SearchableContent
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public string? Url { get; set; }
    public string? Type { get; set; } // "PAGE", "BLOG_POST", "LANDING_PAGE"
    public string? Language { get; set; }
    public DateTime IndexedAt { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
