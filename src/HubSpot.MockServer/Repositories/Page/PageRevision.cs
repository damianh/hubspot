namespace DamianH.HubSpot.MockServer.Repositories.Page;

internal class PageRevision
{
    public string? Id { get; set; }
    public string? PageId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Page? Content { get; set; }
}
