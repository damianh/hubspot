namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class SimplePublicObjectBatchInput
{
    public required string Id { get; set; }
    public required Dictionary<string, string> Properties { get; set; }
}
