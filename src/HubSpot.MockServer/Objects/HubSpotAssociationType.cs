namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotAssociationType(int associationTypeId, string associationCategory)
{
    public int    AssociationTypeId   => associationTypeId;
    public string AssociationCategory => associationCategory;
}