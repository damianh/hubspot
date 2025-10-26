using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsDomainsApi(this IEndpointRouteBuilder app, DomainRepository repository)
    {
        var group = app.MapGroup("/cms/v3/domains");

        group.MapGet("/", (HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;

            var domains = repository.GetAll(offset, limit);
            var total = repository.Count();

            return Results.Ok(new
            {
                total,
                results = domains.Select(MapDomainToResponse)
            });
        });

        group.MapGet("/{domainId}", (string domainId) =>
        {
            var domain = repository.GetById(domainId);
            if (domain == null)
            {
                return Results.NotFound(new { message = $"Domain {domainId} not found" });
            }

            return Results.Ok(MapDomainToResponse(domain));
        });

        // Note: In real HubSpot API, domains are typically managed through UI
        // These endpoints are for testing purposes
        group.MapPost("/", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var domain = MapDomainFromRequest(request);
            var created = repository.Create(domain);

            return Results.Ok(MapDomainToResponse(created));
        });

        group.MapPatch("/{domainId}", async (string domainId, HttpContext context) =>
        {
            var domain = repository.GetById(domainId);
            if (domain == null)
            {
                return Results.NotFound(new { message = $"Domain {domainId} not found" });
            }

            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var updated = MapDomainFromRequest(request, domain);
            updated = repository.Update(domainId, updated);

            return Results.Ok(MapDomainToResponse(updated!));
        });

        group.MapDelete("/{domainId}", (string domainId) =>
        {
            var deleted = repository.Delete(domainId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Domain {domainId} not found" });
            }

            return Results.NoContent();
        });
    }

    private static object MapDomainToResponse(Domain domain) => new
    {
        id = domain.Id,
        domain = domain.Domain1,
        isPrimary = domain.IsPrimary,
        isResolving = domain.IsResolving,
        isLegacyDomain = domain.IsLegacyDomain,
        isUsedForBlogPost = domain.IsUsedForBlogPost,
        isUsedForSitePage = domain.IsUsedForSitePage,
        isUsedForLandingPage = domain.IsUsedForLandingPage,
        isUsedForEmail = domain.IsUsedForEmail,
        isSetupComplete = domain.IsSetupComplete,
        correctCname = domain.CorrectCname,
        created = domain.Created,
        updated = domain.Updated
    };

    private static Domain MapDomainFromRequest(Dictionary<string, object> request, Domain? existing = null)
    {
        var domain = existing ?? new Domain();

        if (request.TryGetValue("id", out var id))
        {
            domain.Id = id.ToString();
        }

        if (request.TryGetValue("domain", out var dom))
        {
            domain.Domain1 = dom.ToString();
        }

        if (request.TryGetValue("isPrimary", out var isPrim))
        {
            domain.IsPrimary = bool.Parse(isPrim.ToString()!);
        }

        if (request.TryGetValue("isResolving", out var isRes))
        {
            domain.IsResolving = bool.Parse(isRes.ToString()!);
        }

        if (request.TryGetValue("isLegacyDomain", out var isLeg))
        {
            domain.IsLegacyDomain = bool.Parse(isLeg.ToString()!);
        }

        if (request.TryGetValue("isUsedForBlogPost", out var isBlog))
        {
            domain.IsUsedForBlogPost = bool.Parse(isBlog.ToString()!);
        }

        if (request.TryGetValue("isUsedForSitePage", out var isSite))
        {
            domain.IsUsedForSitePage = bool.Parse(isSite.ToString()!);
        }

        if (request.TryGetValue("isUsedForLandingPage", out var isLanding))
        {
            domain.IsUsedForLandingPage = bool.Parse(isLanding.ToString()!);
        }

        if (request.TryGetValue("isUsedForEmail", out var isEmail))
        {
            domain.IsUsedForEmail = bool.Parse(isEmail.ToString()!);
        }

        if (request.TryGetValue("isSetupComplete", out var isSetup))
        {
            domain.IsSetupComplete = bool.Parse(isSetup.ToString()!);
        }

        if (request.TryGetValue("correctCname", out var cname))
        {
            domain.CorrectCname = cname.ToString();
        }

        return domain;
    }
}
