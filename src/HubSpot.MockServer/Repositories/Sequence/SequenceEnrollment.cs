namespace DamianH.HubSpot.MockServer.Repositories.Sequence;

internal record SequenceEnrollment
{
    public string EnrollmentId { get; init; } = string.Empty;
    public string ContactId { get; init; } = string.Empty;
    public string SequenceId { get; init; } = string.Empty;
    public int UserId { get; init; }
    public string State { get; init; } = "ENROLLED"; // ENROLLED, PAUSED, COMPLETED, UNENROLLED, etc.
    public DateTimeOffset EnrolledAt { get; init; }
}
