namespace DamianH.HubSpot.MockServer.Objects;

internal class MarketingEvent
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
