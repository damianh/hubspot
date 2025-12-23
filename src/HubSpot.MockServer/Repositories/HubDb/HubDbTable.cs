namespace DamianH.HubSpot.MockServer.Repositories.HubDb;

internal class HubDbTable
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Label { get; set; }
    public bool Published { get; set; }
    public List<HubDbColumn>? Columns { get; set; }
    public bool AllowPublicApiAccess { get; set; }
    public bool AllowChildTables { get; set; }
    public bool EnableChildTablePages { get; set; }
    public bool UseForPages { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
