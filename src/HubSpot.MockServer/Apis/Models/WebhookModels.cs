namespace DamianH.HubSpot.MockServer.Apis.Models;

public class WebhookSubscriptionCreateRequest
{
    public string? EventType { get; set; }
    public string? ObjectTypeId { get; set; }
    public string? PropertyName { get; set; }
    public bool? Active { get; set; }
}

public class WebhookSubscriptionPatchRequest
{
    public bool? Active { get; set; }
}

public class WebhookSubscriptionBatchUpdateRequest
{
    public int? Id { get; set; }
    public bool? Active { get; set; }
}

public class WebhookBatchInputRequest
{
    public List<WebhookSubscriptionBatchUpdateRequest>? Inputs { get; set; }
}

public class WebhookSettingsChangeRequest
{
    public string? TargetUrl { get; set; }
    public WebhookThrottling? Throttling { get; set; }
}

public class WebhookThrottling
{
    public int? MaxConcurrentRequests { get; set; }
}

/// <summary>
/// Matches HubSpot's outbound webhook event payload shape.
/// HubSpot POSTs a JSON array of these to the configured TargetUrl.
/// See: https://developers.hubspot.com/docs/api/webhooks
/// </summary>
internal sealed record WebhookEventPayload
{
    public long EventId { get; init; }
    public string? SubscriptionId { get; init; }
    public int PortalId { get; init; }
    public int AppId { get; init; }
    public long OccurredAt { get; init; }
    public string? SubscriptionType { get; init; }
    public int AttemptNumber { get; init; }
    public string? ObjectId { get; init; }
    public string? PropertyName { get; init; }
    public string? PropertyValue { get; init; }
    public string ChangeSource { get; init; } = "API";
}

