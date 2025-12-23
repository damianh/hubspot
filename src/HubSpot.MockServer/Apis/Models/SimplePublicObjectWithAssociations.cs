namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class SimplePublicObjectWithAssociations
{
    public required string Id { get; set; }
    public required Dictionary<string, string> Properties { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
    public required bool Archived { get; set; }
    public required Dictionary<string, object> Associations { get; set; }
}
