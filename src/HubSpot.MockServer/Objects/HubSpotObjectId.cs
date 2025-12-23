using ValueOf;

namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotObjectId : ValueOf<int, HubSpotObjectId>
{
    public static HubSpotObjectId From(string item)
    {
        var int64 = Convert.ToInt32(item);
        return From(int64);
    }
}
