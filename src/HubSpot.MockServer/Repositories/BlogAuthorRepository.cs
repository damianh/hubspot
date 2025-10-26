namespace DamianH.HubSpot.MockServer.Repositories;

internal class BlogAuthorRepository
{
    private readonly Dictionary<string, BlogAuthor> _authors = new();
    private readonly Dictionary<string, List<string>> _languageGroups = new();
    private int _nextId = 1;

    public BlogAuthor Create(BlogAuthor author)
    {
        author.Id = _nextId++.ToString();
        author.Created = DateTime.UtcNow;
        author.Updated = DateTime.UtcNow;
        _authors[author.Id] = author;
        return author;
    }

    public BlogAuthor? GetById(string id) => _authors.GetValueOrDefault(id);

    public List<BlogAuthor> GetAll(int offset = 0, int limit = 100) =>
        _authors.Values
            .OrderBy(a => a.FullName)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public BlogAuthor? Update(string id, BlogAuthor updatedAuthor)
    {
        if (!_authors.ContainsKey(id))
        {
            return null;
        }

        updatedAuthor.Id = id;
        updatedAuthor.Updated = DateTime.UtcNow;
        _authors[id] = updatedAuthor;
        return updatedAuthor;
    }

    public bool Delete(string id) =>
        _authors.Remove(id);

    public List<BlogAuthor> BatchCreate(List<BlogAuthor> authors) =>
        authors.Select(Create).ToList();

    public List<BlogAuthor> BatchRead(List<string> ids) =>
        ids.Select(id => _authors.GetValueOrDefault(id))
            .Where(a => a != null)
            .Cast<BlogAuthor>()
            .ToList();

    public List<BlogAuthor> BatchUpdate(List<BlogAuthor> authors)
    {
        var results = new List<BlogAuthor>();
        foreach (var author in authors)
        {
            if (author.Id != null)
            {
                var updated = Update(author.Id, author);
                if (updated != null)
                {
                    results.Add(updated);
                }
            }
        }
        return results;
    }

    public int BatchDelete(List<string> ids) => ids.Count(Delete);

    public void AttachToLanguageGroup(string authorId, string languageGroupId)
    {
        if (!_languageGroups.ContainsKey(languageGroupId))
        {
            _languageGroups[languageGroupId] = [];
        }

        if (!_languageGroups[languageGroupId].Contains(authorId))
        {
            _languageGroups[languageGroupId].Add(authorId);
        }
    }

    public void DetachFromLanguageGroup(string authorId)
    {
        foreach (var group in _languageGroups.Values)
        {
            group.Remove(authorId);
        }
    }

    public int Count() => _authors.Count;

    public void Clear()
    {
        _authors.Clear();
        _languageGroups.Clear();
        _nextId = 1;
    }
}

public class BlogAuthor
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Slug { get; set; }
    public string? Bio { get; set; }
    public string? Website { get; set; }
    public string? Twitter { get; set; }
    public string? Facebook { get; set; }
    public string? Linkedin { get; set; }
    public string? Avatar { get; set; }
    public string? Language { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
