using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

/// <summary>
/// Repository for managing HubSpot owners (users and teams)
/// </summary>
public class OwnerRepository
{
    private readonly ConcurrentDictionary<string, Owner> _owners = new();
    private int _nextId = 1;

    public record Owner(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string Type, // USER or TEAM
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default);

    public OwnerRepository() => SeedDefaultOwners();

    private void SeedDefaultOwners()
    {
        var now = DateTime.UtcNow;

        // Seed some default users
        AddOwner(new Owner("1", "admin@example.com", "Admin", "User", "USER", now, now));
        AddOwner(new Owner("2", "sales@example.com", "Sales", "Rep", "USER", now, now));
        AddOwner(new Owner("3", "support@example.com", "Support", "Agent", "USER", now, now));
        AddOwner(new Owner("100", "sales-team@example.com", "Sales", "Team", "TEAM", now, now));
    }

    public Owner? GetOwner(string ownerId) => _owners.GetValueOrDefault(ownerId);

    public IReadOnlyList<Owner> GetAllOwners(string? email = null)
    {
        var query = _owners.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(o => o.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        return query
            .OrderBy(o => o.Email)
            .ToList();
    }

    public Owner CreateOwner(
        string email,
        string firstName,
        string lastName,
        string type = "USER")
    {
        var now = DateTime.UtcNow;
        var id = Interlocked.Increment(ref _nextId).ToString();
        var owner = new Owner(id, email, firstName, lastName, type, now, now);

        AddOwner(owner);
        return owner;
    }

    public void Clear()
    {
        _owners.Clear();
        _nextId = 1;
        SeedDefaultOwners();
    }

    private void AddOwner(Owner owner) => _owners[owner.Id] = owner;
}
