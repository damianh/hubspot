namespace DamianH.HubSpot.MockServer.Repositories.HubDb;

internal class HubDbRow
{
    public string? Id { get; set; }
    public Dictionary<string, object>? Values { get; set; }
    public string? Path { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
