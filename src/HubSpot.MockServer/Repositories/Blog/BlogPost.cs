namespace DamianH.HubSpot.MockServer.Repositories.Blog;

internal class BlogPost
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? ContentGroupId { get; set; }
    public string? BlogAuthorId { get; set; }
    public string? Campaign { get; set; }
    public string? State { get; set; } // "DRAFT", "PUBLISHED", "SCHEDULED"
    public Dictionary<string, object>? PostBody { get; set; }
    public string? PostSummary { get; set; }
    public string? RssBody { get; set; }
    public string? RssSummary { get; set; }
    public string? MetaDescription { get; set; }
    public bool? UseFeaturedImage { get; set; }
    public string? FeaturedImage { get; set; }
    public string? FeaturedImageAltText { get; set; }
    public DateTime? PublishDate { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public List<string>? TagIds { get; set; }
    public string? Language { get; set; }
    public string? TranslatedFromId { get; set; }
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
