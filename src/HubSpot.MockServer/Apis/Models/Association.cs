namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class Association
{
    public AssociationTo To { get; set; } = null!;
    public AssociationType[] Types { get; set; } = [];
}
