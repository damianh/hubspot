namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class BatchResponseSimplePublicUpsertObject
{
    public required string Status { get; set; }
    public required List<SimplePublicObject> Results { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? RequestedAt { get; set; }
}
