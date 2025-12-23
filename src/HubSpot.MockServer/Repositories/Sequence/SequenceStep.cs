namespace DamianH.HubSpot.MockServer.Repositories.Sequence;

internal record SequenceStep
{
    public int StepNumber { get; init; }
    public string Type { get; init; } = ""; // EMAIL, TASK, etc.
    public int DelayMinutes { get; init; }
}
