namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotAssociation(
    HubSpotAssociationTo                        to,
    IReadOnlyCollection<HubSpotAssociationType> types)
{
    public HubSpotAssociationTo                        To    => to;
    public IReadOnlyCollection<HubSpotAssociationType> Types => types;
}