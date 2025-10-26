namespace DamianH.HubSpot.MockServer.Repositories.HubDb;

internal class HubDbColumn
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public string? Type { get; set; } // TEXT, NUMBER, URL, DATE, etc.
    public Dictionary<string, object>? Options { get; set; }
}
