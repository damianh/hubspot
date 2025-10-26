using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class BlogSettingsRepository
{
    private readonly ConcurrentDictionary<string, BlogSettingsData> _settings = new();
    private readonly ConcurrentDictionary<string, List<BlogSettingsRevision>> _revisions = new();
    private readonly ConcurrentDictionary<string, List<string>> _languageGroups = new();

    public BlogSettingsData Create(string blogId, string name, string? language = null)
    {
        var settings = new BlogSettingsData
        {
            BlogId = blogId,
            Name = name,
            Language = language ?? "en",
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            PublicAccessRules = new List<string>(),
            HtmlTitle = name,
            Domain = "example.com"
        };
        _settings[blogId] = settings;
        AddRevision(blogId, settings);
        return settings;
    }

    public BlogSettingsData? Get(string blogId) => _settings.TryGetValue(blogId, out var settings) ? settings : null;

    public IEnumerable<BlogSettingsData> List()
    {
        return _settings.Values.OrderBy(s => s.BlogId).ToList();
    }

    public BlogSettingsData? Update(string blogId, Action<BlogSettingsData> updateAction)
    {
        if (!_settings.TryGetValue(blogId, out var settings)) return null;
        
        updateAction(settings);
        settings.Updated = DateTimeOffset.UtcNow;
        AddRevision(blogId, settings);
        return settings;
    }

    public IEnumerable<BlogSettingsRevision> GetRevisions(string blogId)
    {
        return _revisions.TryGetValue(blogId, out var revs) ? revs : Enumerable.Empty<BlogSettingsRevision>();
    }

    public BlogSettingsRevision? GetRevision(string blogId, string revisionId)
    {
        if (!_revisions.TryGetValue(blogId, out var revs)) return null;
        return revs.FirstOrDefault(r => r.Id == revisionId);
    }

    private void AddRevision(string blogId, BlogSettingsData settings)
    {
        if (!_revisions.ContainsKey(blogId))
        {
            _revisions[blogId] = new List<BlogSettingsRevision>();
        }
        
        var revision = new BlogSettingsRevision
        {
            Id = Guid.NewGuid().ToString(),
            BlogId = blogId,
            Timestamp = DateTimeOffset.UtcNow,
            Name = settings.Name,
            Language = settings.Language
        };
        _revisions[blogId].Add(revision);
    }

    public void AttachToLanguageGroup(string primaryId, string variantId)
    {
        if (!_languageGroups.ContainsKey(primaryId))
        {
            _languageGroups[primaryId] = new List<string>();
        }
        if (!_languageGroups[primaryId].Contains(variantId))
        {
            _languageGroups[primaryId].Add(variantId);
        }
    }

    public void DetachFromLanguageGroup(string id)
    {
        foreach (var group in _languageGroups.Values)
        {
            group.Remove(id);
        }
    }
}

public class BlogSettingsData
{
    public required string BlogId { get; set; }
    public required string Name { get; set; }
    public string Language { get; set; } = "en";
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public List<string> PublicAccessRules { get; set; } = new();
    public string? HtmlTitle { get; set; }
    public string? Domain { get; set; }
}

public class BlogSettingsRevision
{
    public required string Id { get; set; }
    public required string BlogId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Name { get; set; }
    public string? Language { get; set; }
}
