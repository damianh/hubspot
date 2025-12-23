namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class ImportJob
{
    public required string Id { get; init; }
    public ImportState State { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
    public required string ImportName { get; init; }
    public string ImportSource { get; init; } = "API";
    public bool OptOutImport { get; init; } = false;
    public required ImportMetadata Metadata { get; init; }
}
