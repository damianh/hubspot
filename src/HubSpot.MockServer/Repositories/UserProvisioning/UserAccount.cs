namespace DamianH.HubSpot.MockServer.Repositories.UserProvisioning;

internal class UserAccount
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<string> RoleIds { get; set; } = [];
    public List<string> PrimaryTeamId { get; set; } = [];
    public List<string> SecondaryTeamIds { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
