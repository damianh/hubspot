namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class PublicObjectSearchRequest
{
    public List<FilterGroup>? FilterGroups { get; set; }
    public List<Sort>? Sorts { get; set; }
    public int? Limit { get; set; }
    public string? After { get; set; }
}

internal class FilterGroup
{
    public List<Filter>? Filters { get; set; }
}

internal class Filter
{
    public required string PropertyName { get; set; }
    public string? Operator { get; set; }
    public string? Value { get; set; }
}

internal class Sort
{
    public required string PropertyName { get; set; }
    public string? Order { get; set; }
}
