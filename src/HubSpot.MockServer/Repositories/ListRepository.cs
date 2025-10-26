using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class ListRepository
{
    private readonly ConcurrentDictionary<string, ListDefinition> _lists = new();
    private readonly ConcurrentDictionary<string, HashSet<string>> _memberships = new();
    private long _nextListId = 1;

    public ListDefinition CreateList(ListDefinition list)
    {
        list.Id = _nextListId++.ToString();
        list.CreatedAt = DateTime.UtcNow;
        list.UpdatedAt = DateTime.UtcNow;
        _lists[list.Id] = list;
        _memberships[list.Id] = [];
        return list;
    }

    public ListDefinition? GetList(string listId)
    {
        return _lists.TryGetValue(listId, out var list) ? list : null;
    }

    public IEnumerable<ListDefinition> GetAllLists()
    {
        return _lists.Values.ToList();
    }

    public ListDefinition? UpdateList(string listId, ListDefinition updates)
    {
        if (!_lists.TryGetValue(listId, out var existing))
        {
            return null;
        }

        existing.Name = updates.Name ?? existing.Name;
        existing.FilterBranch = updates.FilterBranch ?? existing.FilterBranch;
        existing.UpdatedAt = DateTime.UtcNow;
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
            members.Add(id);

        if (_lists.TryGetValue(listId, out var list))
        {
            list.UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveMemberships(string listId, IEnumerable<string> recordIds)
    {
        if (!_memberships.TryGetValue(listId, out var members))
        {
            return;
        }

        foreach (var id in recordIds)
            members.Remove(id);

        if (_lists.TryGetValue(listId, out var list))
        {
            list.UpdatedAt = DateTime.UtcNow;
        }
    }

    public IEnumerable<string> GetMemberships(string listId)
    {
        return _memberships.TryGetValue(listId, out var members) 
            ? members.ToList() 
            : Enumerable.Empty<string>();
    }
}

public class ListDefinition
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ObjectTypeId { get; set; }
    public string? ProcessingType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public object? FilterBranch { get; set; }
}
