namespace DamianH.HubSpot.MockServer.Repositories.Tag;

internal class TaxRate
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public decimal Rate { get; set; }
    public bool IsDefault { get; set; }
}
