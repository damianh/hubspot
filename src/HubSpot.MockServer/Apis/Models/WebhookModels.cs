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
