namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class BatchResponseSimplePublicObject
{
    public required string Status { get; set; }
    public required List<CreatedResponseSimplePublicObject> Results { get; set; }
    public required DateTime StartedAt { get; set; }
    public required DateTime CompletedAt { get; set; }
}
