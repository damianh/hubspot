namespace DamianH.HubSpot.MockServer.Repositories.UrlRedirect;

internal class UrlRedirect
{
    public string? Id { get; set; }
    public string? RoutePrefix { get; set; }
    public string? Destination { get; set; }
    public int RedirectStyle { get; set; } // 301 or 302
    public bool IsOnlyAfterNotFound { get; set; }
    public bool IsMatchFullUrl { get; set; }
    public bool IsMatchQueryString { get; set; }
    public bool IsPattern { get; set; }
    public int Precedence { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
