namespace DamianH.HubSpot.MockServer.Objects;

public class ApiUsageData
{
    public required string Date { get; set; }
    public int TotalUsage { get; set; }
    public int ApiCallUsage { get; set; }
    public Dictionary<string, int> UsageBySource { get; set; } = new();
}
