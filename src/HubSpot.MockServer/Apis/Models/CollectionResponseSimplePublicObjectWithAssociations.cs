namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class CollectionResponseSimplePublicObjectWithAssociations
{
    public required List<SimplePublicObjectWithAssociations> Results { get; set; }
    public Paging? Paging { get; set; }
}
