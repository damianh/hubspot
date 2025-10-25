namespace DamianH.HubSpot.MockServer.Objects;

public class MarketingEvent
{
    public string? Id { get; set; }
    public string? EventName { get; set; }
    public string? EventType { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? EventOrganizer { get; set; }
    public string? EventDescription { get; set; }
    public string? EventUrl { get; set; }
    public bool EventCancelled { get; set; }
    public string? CustomProperties { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class MarketingEmail
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

public class Campaign
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? Type { get; set; }
    public string? AppId { get; set; }
    public string? AppName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SingleSendEmail
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? HtmlBody { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? ReplyTo { get; set; }
    public string? State { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class Subscription
{
    public string? Id { get; set; }
    public string? EmailAddress { get; set; }
    public string? SubscriptionId { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class SubscriptionDefinition
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public bool IsInternal { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
