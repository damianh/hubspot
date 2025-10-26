namespace DamianH.HubSpot.MockServer.Objects;

internal class Currency
{
    public required string CurrencyCode { get; set; }
    public required string DisplayName { get; set; }
    public required string Symbol { get; set; }
    public bool IsVisible { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
