namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class CollectionResponseSimplePublicObject
{
    public List<SimplePublicObject> Results { get; set; } = [];
    public Paging? Paging { get; set; }
}

internal class Paging
{
    public NextPage? Next { get; set; }
}

internal class NextPage
{
    public string? After { get; set; }
    public string? Link { get; set; }
}
