namespace DamianH.HubSpot.MockServer.Repositories.Subscription;

internal class Subscription
{
    public string? Id { get; set; }
    public string? EmailAddress { get; set; }
    public string? SubscriptionId { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
