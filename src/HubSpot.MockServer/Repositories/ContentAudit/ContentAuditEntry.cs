namespace DamianH.HubSpot.MockServer.Repositories.ContentAudit;

internal class ContentAuditEntry
{
    public string? Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string? EventType { get; set; } // "CREATED", "UPDATED", "DELETED", "PUBLISHED"
    public string? ObjectType { get; set; } // "BLOG_POST", "PAGE", "DOMAIN", etc.
    public string? ObjectId { get; set; }
    public string? UserId { get; set; }
    public string? UserEmail { get; set; }
    public Dictionary<string, object>? Changes { get; set; }
}
