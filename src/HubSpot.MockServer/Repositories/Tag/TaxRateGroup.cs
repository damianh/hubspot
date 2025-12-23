using DamianH.HubSpot.MockServer.Repositories.Tag;

namespace DamianH.HubSpot.MockServer.Objects;

internal class TaxRateGroup
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public List<TaxRate> TaxRates { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
