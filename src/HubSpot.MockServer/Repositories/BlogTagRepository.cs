namespace DamianH.HubSpot.MockServer.Repositories;

public class BlogTagRepository
{
    private readonly Dictionary<string, BlogTag> _tags = new();
    private readonly Dictionary<string, List<string>> _languageGroups = new();
    private int _nextId = 1;

    public BlogTag Create(BlogTag tag)
    {
        tag.Id = _nextId++.ToString();
        tag.Created = DateTime.UtcNow;
        tag.Updated = DateTime.UtcNow;
        _tags[tag.Id] = tag;
        return tag;
    }

    public BlogTag? GetById(string id) => _tags.GetValueOrDefault(id);

    public List<BlogTag> GetAll(int offset = 0, int limit = 100) => _tags.Values
            .OrderBy(t => t.Name)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public BlogTag? Update(string id, BlogTag updatedTag)
    {
        if (!_tags.ContainsKey(id))
        {
            return null;
        }

        updatedTag.Id = id;
        updatedTag.Updated = DateTime.UtcNow;
        _tags[id] = updatedTag;
        return updatedTag;
    }

    public bool Delete(string id) => _tags.Remove(id);

    public List<BlogTag> BatchCreate(List<BlogTag> tags) => tags.Select(Create).ToList();

    public List<BlogTag> BatchRead(List<string> ids) => ids.Select(id => _tags.GetValueOrDefault(id))
            .Where(t => t != null)
            .Cast<BlogTag>()
            .ToList();

    public List<BlogTag> BatchUpdate(List<BlogTag> tags)
    {
        var results = new List<BlogTag>();
        foreach (var tag in tags)
        {
            if (tag.Id != null)
            {
                var updated = Update(tag.Id, tag);
                if (updated != null)
                {
                    results.Add(updated);
                }
            }
        }
        return results;
    }

    public int BatchDelete(List<string> ids) => ids.Count(Delete);

    public void AttachToLanguageGroup(string tagId, string languageGroupId)
    {
        if (!_languageGroups.ContainsKey(languageGroupId))
        {
            _languageGroups[languageGroupId] = [];
        }

        if (!_languageGroups[languageGroupId].Contains(tagId))
        {
            _languageGroups[languageGroupId].Add(tagId);
        }
    }

    public void DetachFromLanguageGroup(string tagId)
    {
        foreach (var group in _languageGroups.Values)
        {
            group.Remove(tagId);
        }
    }

    public int Count() => _tags.Count;

    public void Clear()
    {
        _tags.Clear();
        _languageGroups.Clear();
        _nextId = 1;
    }
}

public class BlogTag
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Language { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
