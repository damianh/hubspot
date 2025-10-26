namespace DamianH.HubSpot.MockServer.Objects;

public class TaxRateGroup
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public List<TaxRate> TaxRates { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class TaxRate
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public decimal Rate { get; set; }
    public bool IsDefault { get; set; }
}
