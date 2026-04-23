namespace DamianH.HubSpot.MockServer.Repositories.UrlMapping;

internal sealed class UrlMapping
{
    public long Id { get; set; }
    public string? RoutePrefix { get; set; }
    public string? Destination { get; set; }
    public int RedirectStyle { get; set; }
    public bool IsOnlyAfterNotFound { get; set; }
    public bool IsMatchFullUrl { get; set; }
    public bool IsMatchQueryString { get; set; }
    public bool IsPattern { get; set; }
    public int Precedence { get; set; }
    public long Created { get; set; }
    public long Updated { get; set; }
}
