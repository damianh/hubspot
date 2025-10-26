namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class ImportRow
{
    public required int RowIndex { get; init; }
    public required Dictionary<string, string> Data { get; init; }
    public string? ObjectId { get; set; }
}
