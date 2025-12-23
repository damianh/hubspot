namespace DamianH.HubSpot.MockServer.Repositories.MarketingEmail;

internal class MarketingEmail
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? HtmlBody { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? ReplyTo { get; set; }
    public string? CampaignId { get; set; }
    public string? State { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
