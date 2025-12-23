namespace DamianH.HubSpot.MockServer.Repositories.Automation;

internal record CallbackCompletion
{
    public string CallbackId { get; init; } = string.Empty;
    public string Status { get; init; } = ""; // SUCCESS, FAILURE
    public Dictionary<string, object> OutputFields { get; init; } = new();
    public DateTimeOffset CompletedAt { get; init; }
}
