namespace DamianH.HubSpot.MockServer.Repositories.ContentAudit;

internal class ContentAuditRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly List<ContentAuditEntry> _entries = [];
    private int _nextId = 1;

    public ContentAuditRepository(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public ContentAuditEntry AddEntry(ContentAuditEntry entry)
    {
        entry.Id = _nextId++.ToString();
        entry.Timestamp = _timeProvider.GetUtcNow().UtcDateTime;
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
