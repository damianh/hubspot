namespace DamianH.HubSpot.MockServer.Repositories.Automation;

internal record CustomAction
{
    public string Id { get; init; } = string.Empty;
    public int AppId { get; init; }
    public string Label { get; init; } = string.Empty;
    public string ActionUrl { get; init; } = string.Empty;
    public List<ActionInput> InputFields { get; init; } = [];
}
