namespace DamianH.HubSpot.MockServer.Objects;

public class UserAccount
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<string> RoleIds { get; set; } = new();
    public List<string> PrimaryTeamId { get; set; } = new();
    public List<string> SecondaryTeamIds { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class RolePermissionSet
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public List<string> Permissions { get; set; } = new();
}

public class Team
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}
