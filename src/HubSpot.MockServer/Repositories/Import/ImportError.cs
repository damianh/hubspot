namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class ImportError
{
    public required string Id { get; init; }
    public required int CreatedAt { get; init; }
    public required string ObjectType { get; init; }
    public required Dictionary<string, string> SourceData { get; init; }
    public required string ErrorType { get; init; }
}
