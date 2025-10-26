namespace DamianH.HubSpot.MockServer.Objects;

public class ExchangeRate
{
    public required string Id { get; set; }
    public required string FromCurrencyCode { get; set; }
    public required string ToCurrencyCode { get; set; }
    public decimal ExchangeRateValue { get; set; }
    public DateTimeOffset EffectiveDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
