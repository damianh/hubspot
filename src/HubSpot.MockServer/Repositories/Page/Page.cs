namespace DamianH.HubSpot.MockServer.Repositories.Page;

internal class Page
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? State { get; set; } // "DRAFT", "PUBLISHED", "SCHEDULED"
    public string? HtmlTitle { get; set; }
    public Dictionary<string, object>? PageBody { get; set; }
    public string? MetaDescription { get; set; }
    public bool? UseFeaturedImage { get; set; }
    public string? FeaturedImage { get; set; }
    public string? FeaturedImageAltText { get; set; }
    public DateTime? PublishDate { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string? Language { get; set; }
    public string? TranslatedFromId { get; set; }
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}
