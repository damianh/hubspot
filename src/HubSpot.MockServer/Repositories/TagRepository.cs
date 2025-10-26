using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class TagRepository
{
    private readonly ConcurrentDictionary<string, TagData> _tags = new();
    private readonly ConcurrentDictionary<string, List<string>> _languageGroups = new();
    private int _nextId = 1;

    public TagData Create(string name, string? language = null)
    {
        var id = Interlocked.Increment(ref _nextId).ToString();
        var tag = new TagData
        {
            Id = id,
            Name = name,
            Language = language ?? "en",
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow
        };
        _tags[id] = tag;
        return tag;
    }

    public TagData? Get(string id) => _tags.GetValueOrDefault(id);

    public IEnumerable<TagData> List(int? limit = null, string? after = null)
    {
        var query = _tags.Values.OrderBy(t => t.Id).AsEnumerable();

        if (after != null)
        {
            query = query.SkipWhile(t => t.Id != after).Skip(1);
        }

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        return query.ToList();
    }

    public TagData? Update(string id, string name)
    {
        if (!_tags.TryGetValue(id, out var tag))
        {
            return null;
        }

        tag.Name = name;
        tag.Updated = DateTimeOffset.UtcNow;
        return tag;
    }

    public bool Delete(string id) => _tags.TryRemove(id, out _);

    public void AttachToLanguageGroup(string primaryId, string variantId)
    {
        if (!_languageGroups.ContainsKey(primaryId))
        {
            _languageGroups[primaryId] = [];
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

    public IEnumerable<string> GetLanguageGroup(string id) => _languageGroups.TryGetValue(id, out var group) ? group : Enumerable.Empty<string>();
}

public class TagData
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string Language { get; set; } = "en";
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}
