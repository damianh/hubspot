namespace DamianH.HubSpot.MockServer.Repositories.Blog;

internal class BlogPostRevision
{
    public string? Id { get; set; }
    public string? PostId { get; set; }
    public DateTime CreatedAt { get; set; }
    public BlogPost? Content { get; set; }
}
