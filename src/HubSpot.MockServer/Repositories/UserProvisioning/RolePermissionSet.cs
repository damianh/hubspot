namespace DamianH.HubSpot.MockServer.Repositories.UserProvisioning;

internal class RolePermissionSet
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public List<string> Permissions { get; set; } = [];
}
