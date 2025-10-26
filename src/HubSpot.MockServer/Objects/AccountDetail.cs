namespace DamianH.HubSpot.MockServer.Objects;

public class AccountDetail
{
    public int PortalId { get; set; }
    public string? AccountType { get; set; }
    public string? TimeZone { get; set; }
    public string? CompanyCurrency { get; set; }
    public List<string>? AdditionalCurrencies { get; set; }
    public string? DataHostingLocation { get; set; }
    public string? UiDomain { get; set; }
    public string? UtcOffset { get; set; }
    public long? UtcOffsetMilliseconds { get; set; }
}
