namespace DamianH.HubSpot.MockServer.Repositories;

public class SiteSearchRepository
{
    private readonly List<SearchableContent> _content = [];
    private int _nextId = 1;

    public SearchableContent AddContent(SearchableContent content)
    {
        content.Id = _nextId++.ToString();
        content.IndexedAt = DateTime.UtcNow;
        _content.Add(content);
        return content;
    }

    public List<SearchableContent> Search(string query, int offset = 0, int limit = 20)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return _content.Skip(offset).Take(limit).ToList();
        }

        var lowerQuery = query.ToLowerInvariant();
        return _content
            .Where(c => 
                (c.Title?.ToLowerInvariant().Contains(lowerQuery) ?? false) ||
                (c.Description?.ToLowerInvariant().Contains(lowerQuery) ?? false) ||
                (c.Content?.ToLowerInvariant().Contains(lowerQuery) ?? false))
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public SearchableContent? GetById(string id)
    {
        return _content.FirstOrDefault(c => c.Id == id);
    }

    public bool Delete(string id)
    {
        var content = _content.FirstOrDefault(c => c.Id == id);
        if (content == null)
        {
            return false;
        }

        _content.Remove(content);
        return true;
    }

    public int Count()
    {
        return _content.Count;
    }

    public void Clear()
    {
        _content.Clear();
        _nextId = 1;
    }
}

public class SearchableContent
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
