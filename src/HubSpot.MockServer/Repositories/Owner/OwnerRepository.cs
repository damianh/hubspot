using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.Owner;

/// <summary>
/// Repository for managing HubSpot owners (users and teams)
/// </summary>
internal class OwnerRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly ConcurrentDictionary<string, Owner> _owners = new();
    private int _nextId = 1;

    public OwnerRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        SeedDefaultOwners();
    }

    private void SeedDefaultOwners()
    {
        var now = _timeProvider.GetUtcNow().UtcDateTime;

        // Seed some default users with teams
        var salesTeam = new Team("100", "Sales Team", true);
        var supportTeam = new Team("101", "Support Team", false);

        AddOwner(new Owner("1", "admin@example.com", "Admin", "User", "PERSON", now, now, 
            UserId: 1001, UserIdIncludingInactive: 1001));
        AddOwner(new Owner("2", "sales@example.com", "Sales", "Rep", "PERSON", now, now, 
            UserId: 1002, UserIdIncludingInactive: 1002, Teams: [salesTeam]));
        AddOwner(new Owner("3", "support@example.com", "Support", "Agent", "PERSON", now, now, 
            UserId: 1003, UserIdIncludingInactive: 1003, Teams: [supportTeam]));
        AddOwner(new Owner("100", "sales-team@example.com", "Sales", "Team", "QUEUE", now, now));
    }

    public Owner? GetOwner(string ownerId) => _owners.GetValueOrDefault(ownerId);

    public (IReadOnlyList<Owner> Results, string? NextAfter) GetAllOwners(
        string? email = null, 
        bool? archived = null,
        string? after = null, 
        int? limit = null)
    {
        var query = _owners.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(o => o.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        if (archived.HasValue)
        {
            query = query.Where(o => o.Archived == archived.Value);
        }

        // Order by ID for consistent pagination
        query = query.OrderBy(o => int.Parse(o.Id));

        // Apply pagination using "after" cursor
        if (!string.IsNullOrWhiteSpace(after))
        {
            query = query.SkipWhile(o => o.Id != after).Skip(1);
        }

        var pageSize = limit ?? 100;
        var results = query.Take(pageSize + 1).ToList();
        
        string? nextAfter = null;
        if (results.Count > pageSize)
        {
            nextAfter = results[pageSize - 1].Id;
            results = results.Take(pageSize).ToList();
        }

        return (results, nextAfter);
    }

    public Owner CreateOwner(
        string email,
        string firstName,
        string lastName,
        string type = "PERSON",
        List<Team>? teams = null)
    {
        var now = _timeProvider.GetUtcNow().UtcDateTime;
        var id = Interlocked.Increment(ref _nextId).ToString();
        var userId = Interlocked.Increment(ref _nextId) + 1000;
        var owner = new Owner(id, email, firstName, lastName, type, now, now, 
            UserId: userId, UserIdIncludingInactive: userId, Teams: teams);

        AddOwner(owner);
        return owner;
    }

    public Owner? ArchiveOwner(string ownerId)
    {
        if (!_owners.TryGetValue(ownerId, out var owner))
            return null;

        var archived = owner with { Archived = true, UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime };
        _owners[ownerId] = archived;
        return archived;
    }

    public void Clear()
    {
        _owners.Clear();
        _nextId = 1;
        SeedDefaultOwners();
    }

    private void AddOwner(Owner owner) => _owners[owner.Id] = owner;
}
