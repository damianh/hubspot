using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories.UserProvisioning;

internal class UserProvisioningRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<string, UserAccount> _users = new();
    private readonly Dictionary<string, RolePermissionSet> _roles = new();
    private readonly Dictionary<string, Team> _teams = new();
    private int _userIdCounter = 1;

    public UserProvisioningRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        InitializeDefaultData();
    }

    private void InitializeDefaultData()
    {
        _roles["admin"] = new RolePermissionSet
        {
            Id = "admin",
            Name = "Super Admin",
            Permissions = ["ADMIN", "VIEW", "EDIT", "DELETE"]
        };
        _roles["sales"] = new RolePermissionSet
        {
            Id = "sales",
            Name = "Sales",
            Permissions = ["VIEW", "EDIT"]
        };
        _roles["marketing"] = new RolePermissionSet
        {
            Id = "marketing",
            Name = "Marketing",
            Permissions = ["VIEW", "EDIT"]
        };

        _teams["team1"] = new Team { Id = "team1", Name = "Sales Team" };
        _teams["team2"] = new Team { Id = "team2", Name = "Marketing Team" };
        _teams["team3"] = new Team { Id = "team3", Name = "Support Team" };

        CreateUser(new UserAccount
        {
            Id = _userIdCounter++.ToString(),
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            RoleIds = ["admin"],
            PrimaryTeamId = ["team1"],
            CreatedAt = _timeProvider.GetUtcNow(),
            UpdatedAt = _timeProvider.GetUtcNow()
        });
    }

    public List<UserAccount> GetUsers(int? limit = null, string? after = null, string? email = null)
    {
        var query = _users.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(u => u.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(after))
        {
            query = query.Where(u => string.Compare(u.Id, after) > 0);
        }

        query = query.OrderBy(u => u.Id);

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        return query.ToList();
    }

    public UserAccount? GetUser(string identifier, string idProperty = "id")
    {
        if (idProperty == "email")
        {
            return _users.Values.FirstOrDefault(u => u.Email.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        }
        return _users.GetValueOrDefault(identifier);
    }

    public UserAccount CreateUser(UserAccount user)
    {
        if (string.IsNullOrEmpty(user.Id))
        {
            user.Id = _userIdCounter++.ToString();
        }
        user.CreatedAt = _timeProvider.GetUtcNow();
        user.UpdatedAt = _timeProvider.GetUtcNow();
        _users[user.Id] = user;
        return user;
    }

    public UserAccount? UpdateUser(string identifier, UserAccount updates, string idProperty = "id")
    {
        var user = GetUser(identifier, idProperty);
        if (user == null)
        {
            return null;
        }

        user.FirstName = updates.FirstName;
        user.LastName = updates.LastName;
        user.RoleIds = updates.RoleIds;
        user.PrimaryTeamId = updates.PrimaryTeamId;
        user.SecondaryTeamIds = updates.SecondaryTeamIds;
        user.UpdatedAt = _timeProvider.GetUtcNow();

        return user;
    }

    public bool DeleteUser(string identifier, string idProperty = "id")
    {
        var user = GetUser(identifier, idProperty);
        if (user == null)
        {
            return false;
        }

        return _users.Remove(user.Id);
    }

    public List<RolePermissionSet> GetRoles() => _roles.Values.ToList();

    public List<Team> GetTeams() => _teams.Values.ToList();
}
