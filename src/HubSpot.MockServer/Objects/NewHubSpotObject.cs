namespace DamianH.HubSpot.MockServer.Objects;

internal class NewHubSpotObject(
    IReadOnlyDictionary<string, string>     properties,
    IReadOnlyCollection<HubSpotAssociation> associations)
{
    public IReadOnlyDictionary<string, string>     InitialProperties   => properties;
    public IReadOnlyCollection<HubSpotAssociation> InitialAssociations => associations;
}
