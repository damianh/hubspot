namespace DamianH.HubSpot.MockServer.Apis.Models;

internal class ValueWithTimestamp
{
    public required string         SourceId        { get; set; }
    public required string         SourceType      { get; set; }
    public required string         SourceLabel     { get; set; }
    public required string         Value           { get; set; }
    public          int            UpdatedByUserId { get; set; } = 0;
    public required DateTimeOffset Timestamp       { get; set; }
}