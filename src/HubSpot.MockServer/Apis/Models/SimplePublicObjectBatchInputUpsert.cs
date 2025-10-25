namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class SimplePublicObjectBatchInputUpsert
{
    public string? Id { get; set; }
    public required Dictionary<string, string> Properties { get; set; }
}
