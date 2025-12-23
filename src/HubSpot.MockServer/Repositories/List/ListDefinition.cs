namespace DamianH.HubSpot.MockServer.Repositories.List;

internal class ListDefinition
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ObjectTypeId { get; set; }
    public string? ProcessingType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public object? FilterBranch { get; set; }
}
