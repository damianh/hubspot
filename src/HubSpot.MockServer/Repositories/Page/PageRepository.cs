namespace DamianH.HubSpot.MockServer.Repositories.Page;

internal class PageRepository
{
    private readonly Dictionary<string, Page> _pages = new();
    private readonly Dictionary<string, List<PageRevision>> _revisions = new();
    private readonly Dictionary<string, List<string>> _languageGroups = new();
    private int _nextId = 1;

    public Page Create(Page page)
    {
        page.Id = _nextId++.ToString();
        page.Created = DateTime.UtcNow;
        page.Updated = DateTime.UtcNow;
        _pages[page.Id] = page;

        AddRevision(page.Id, page);

        return page;
    }

    public Page? GetById(string id) => _pages.GetValueOrDefault(id);

    public List<Page> GetAll(int offset = 0, int limit = 100) => _pages.Values
            .OrderByDescending(p => p.Created)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public Page? Update(string id, Page updatedPage)
    {
        if (!_pages.ContainsKey(id))
        {
            return null;
        }

        updatedPage.Id = id;
        updatedPage.Updated = DateTime.UtcNow;
        _pages[id] = updatedPage;

        AddRevision(id, updatedPage);

        return updatedPage;
    }

    public bool Delete(string id)
    {
        var removed = _pages.Remove(id);
        if (removed)
        {
            _revisions.Remove(id);
        }
        return removed;
    }

    public List<Page> BatchCreate(List<Page> pages) => pages.Select(Create).ToList();

    public List<Page> BatchRead(List<string> ids) => ids.Select(id => _pages.GetValueOrDefault(id))
            .Where(p => p != null)
            .Cast<Page>()
            .ToList();

    public List<Page> BatchUpdate(List<Page> pages)
    {
        var results = new List<Page>();
        foreach (var page in pages)
        {
            if (page.Id != null)
            {
                var updated = Update(page.Id, page);
                if (updated != null)
                {
                    results.Add(updated);
                }
            }
        }
        return results;
    }

    public int BatchDelete(List<string> ids) => ids.Count(Delete);

    private void AddRevision(string pageId, Page page)
    {
        if (!_revisions.ContainsKey(pageId))
        {
            _revisions[pageId] = [];
        }

        var revision = new PageRevision
        {
            Id = (_revisions[pageId].Count + 1).ToString(),
            PageId = pageId,
            CreatedAt = DateTime.UtcNow,
            Content = page
        };

        _revisions[pageId].Add(revision);
    }

    public List<PageRevision> GetRevisions(string pageId) => _revisions.GetValueOrDefault(pageId) ?? [];

    public PageRevision? GetRevisionById(string pageId, string revisionId) => _revisions.GetValueOrDefault(pageId)
            ?.FirstOrDefault(r => r.Id == revisionId);

    public Page? RestoreRevision(string pageId, string revisionId)
    {
        var revision = GetRevisionById(pageId, revisionId);
        if (revision?.Content == null)
        {
            return null;
        }

        return Update(pageId, revision.Content);
    }

    public void AttachToLanguageGroup(string pageId, string languageGroupId)
    {
        if (!_languageGroups.ContainsKey(languageGroupId))
        {
            _languageGroups[languageGroupId] = [];
        }

        if (!_languageGroups[languageGroupId].Contains(pageId))
        {
            _languageGroups[languageGroupId].Add(pageId);
        }
    }

    public void DetachFromLanguageGroup(string pageId)
    {
        foreach (var group in _languageGroups.Values)
        {
            group.Remove(pageId);
        }
    }

    public int Count() => _pages.Count;

    public void Clear()
    {
        _pages.Clear();
        _revisions.Clear();
        _languageGroups.Clear();
        _nextId = 1;
    }
}
