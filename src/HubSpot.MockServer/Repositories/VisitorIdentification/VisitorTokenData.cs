namespace DamianH.HubSpot.MockServer.Repositories.VisitorIdentification;

internal class VisitorTokenData
{
    public string Token { get; set; } = string.Empty;
    public string VisitorId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}
