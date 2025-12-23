namespace DamianH.HubSpot.MockServer.Repositories.Export;

internal class ExportJob
{
    public required string Id { get; init; }
    public ExportStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? StartedAt { get; init; }
    public DateTimeOffset? CompletedAt { get; set; }
    public required string ExportName { get; init; }
    public required string ExportType { get; init; }
    public required string Format { get; init; }
    public required string ObjectType { get; init; }
    public required List<string> Properties { get; init; }
}
