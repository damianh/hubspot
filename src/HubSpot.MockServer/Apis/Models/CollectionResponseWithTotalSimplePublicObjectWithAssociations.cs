namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class CollectionResponseWithTotalSimplePublicObjectWithAssociations
{
    public required List<SimplePublicObjectWithAssociations> Results { get; set; }
    public required int Total { get; set; }
    public Paging? Paging { get; set; }
}
