namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotObjectIdGenerator
{
    private int _idCounter;

    public HubSpotObjectId Generate()
    {
        var id = _idCounter++;
        var hubSpotObjectId = HubSpotObjectId.From(id);
        return hubSpotObjectId;
    }
}
