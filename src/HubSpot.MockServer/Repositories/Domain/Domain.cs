namespace DamianH.HubSpot.MockServer.Repositories.Domain;

internal class Domain
{
    public string? Id { get; set; }
    public string? Domain1 { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsResolving { get; set; }
    public bool IsLegacyDomain { get; set; }
    public bool IsUsedForBlogPost { get; set; }
    public bool IsUsedForSitePage { get; set; }
    public bool IsUsedForLandingPage { get; set; }
    public bool IsUsedForEmail { get; set; }
    public bool IsSetupComplete { get; set; }
    public string? CorrectCname { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
