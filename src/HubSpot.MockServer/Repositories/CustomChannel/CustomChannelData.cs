namespace DamianH.HubSpot.MockServer.Repositories.CustomChannel;

internal class CustomChannelData
{
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public bool Active { get; set; }
}
