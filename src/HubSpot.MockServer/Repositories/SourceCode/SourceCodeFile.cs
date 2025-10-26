namespace DamianH.HubSpot.MockServer.Repositories.SourceCode;

internal class SourceCodeFile
{
    public string? Id { get; set; }
    public string? Path { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; } // "HTML", "CSS", "JS", "MODULE", etc.
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
