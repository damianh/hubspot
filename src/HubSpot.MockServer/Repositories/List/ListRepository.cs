using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.List;

internal class ListRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly ConcurrentDictionary<string, ListDefinition> _lists = new();
    private readonly ConcurrentDictionary<string, HashSet<string>> _memberships = new();
    private long _nextListId = 1;



    public ListRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public ListDefinition CreateList(ListDefinition list)
    {
        list.Id = _nextListId++.ToString();
        list.CreatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        list.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        _lists[list.Id] = list;
        _memberships[list.Id] = [];
        return list;
    }

    public ListDefinition? GetList(string listId) => _lists.GetValueOrDefault(listId);

    public IEnumerable<ListDefinition> GetAllLists() => _lists.Values.ToList();

    public ListDefinition? UpdateList(string listId, ListDefinition updates)
    {
        if (!_lists.TryGetValue(listId, out var existing))
        {
            return null;
        }

        existing.Name = updates.Name ?? existing.Name;
        existing.FilterBranch = updates.FilterBranch ?? existing.FilterBranch;
        existing.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        return existing;
    }

    public bool DeleteList(string listId)
    {
        _memberships.TryRemove(listId, out _);
        return _lists.TryRemove(listId, out _);
    }

    public void AddMemberships(string listId, IEnumerable<string> recordIds)
    {
        if (!_memberships.TryGetValue(listId, out var members))
        {
            return;
        }

        foreach (var id in recordIds)
        {
            members.Add(id);
        }

        if (_lists.TryGetValue(listId, out var list))
        {
            list.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        }
    }

    public void RemoveMemberships(string listId, IEnumerable<string> recordIds)
    {
        if (!_memberships.TryGetValue(listId, out var members))
        {
            return;
        }

        foreach (var id in recordIds)
        {
            members.Remove(id);
        }

        if (_lists.TryGetValue(listId, out var list))
        {
            list.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        }
    }

    public IEnumerable<string> GetMemberships(string listId) => _memberships.TryGetValue(listId, out var members)
            ? members.ToList()
            : Enumerable.Empty<string>();
}
