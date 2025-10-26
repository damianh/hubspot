namespace DamianH.HubSpot.MockServer.Repositories;

internal class ContentAuditRepository
{
    private readonly List<ContentAuditEntry> _entries = [];
    private int _nextId = 1;

    public ContentAuditEntry AddEntry(ContentAuditEntry entry)
    {
        entry.Id = _nextId++.ToString();
        entry.Timestamp = DateTime.UtcNow;
        _entries.Add(entry);
        return entry;
    }

    public List<ContentAuditEntry> GetAll(int offset = 0, int limit = 100) => _entries
            .OrderByDescending(e => e.Timestamp)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public List<ContentAuditEntry> GetByObjectId(string objectId) => _entries
            .Where(e => e.ObjectId == objectId)
            .OrderByDescending(e => e.Timestamp)
            .ToList();

    public List<ContentAuditEntry> GetByUserId(string userId) => _entries
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.Timestamp)
            .ToList();

    public int Count() => _entries.Count;

    public void Clear()
    {
        _entries.Clear();
        _nextId = 1;
    }
}

public class ContentAuditEntry
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
