namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class SimplePublicObjectInputForCreate
{
    public Dictionary<string, string> Properties   { get; set; } = new();
    public Association[]              Associations { get; set; } = [];
}
