namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class ImportMetadata
{
    public required string ObjectType { get; init; }
    public int NumRowsProcessed { get; set; }
    public int NumRowsSucceeded { get; set; }
    public int NumRowsFailed { get; set; }
}
