using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsUrlRedirectsApi(WebApplication app)
    {
        var group = app.MapGroup("/cms/v3/url-redirects");

        group.MapGet("/", (UrlRedirectRepository repository, HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;

            var redirects = repository.GetAll(offset, limit);
            var total = repository.Count();

            return Results.Ok(new
            {
                total,
                results = redirects.Select(MapRedirectToResponse)
            });
        });

        group.MapGet("/{urlRedirectId}", (UrlRedirectRepository repository, string urlRedirectId) =>
        {
            var redirect = repository.GetById(urlRedirectId);
            if (redirect == null)
            {
                return Results.NotFound(new { message = $"URL Redirect {urlRedirectId} not found" });
            }

            return Results.Ok(MapRedirectToResponse(redirect));
        });

        group.MapPost("/", async (UrlRedirectRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var redirect = MapRedirectFromRequest(request);
            var created = repository.Create(redirect);

            return Results.Ok(MapRedirectToResponse(created));
        });

        group.MapPatch("/{urlRedirectId}", async (UrlRedirectRepository repository, string urlRedirectId, HttpContext context) =>
        {
            var redirect = repository.GetById(urlRedirectId);
            if (redirect == null)
            {
                return Results.NotFound(new { message = $"URL Redirect {urlRedirectId} not found" });
            }

            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var updated = MapRedirectFromRequest(request, redirect);
            updated = repository.Update(urlRedirectId, updated);

            return Results.Ok(MapRedirectToResponse(updated!));
        });

        group.MapDelete("/{urlRedirectId}", (UrlRedirectRepository repository, string urlRedirectId) =>
        {
            var deleted = repository.Delete(urlRedirectId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"URL Redirect {urlRedirectId} not found" });
            }

            return Results.NoContent();
        });
    }

    private static object MapRedirectToResponse(UrlRedirect redirect) => new
    {
        id = redirect.Id,
        routePrefix = redirect.RoutePrefix,
        destination = redirect.Destination,
        redirectStyle = redirect.RedirectStyle,
        isOnlyAfterNotFound = redirect.IsOnlyAfterNotFound,
        isMatchFullUrl = redirect.IsMatchFullUrl,
        isMatchQueryString = redirect.IsMatchQueryString,
        isPattern = redirect.IsPattern,
        precedence = redirect.Precedence,
        created = redirect.Created,
        updated = redirect.Updated
    };

    private static UrlRedirect MapRedirectFromRequest(Dictionary<string, object> request, UrlRedirect? existing = null)
    {
        var redirect = existing ?? new UrlRedirect();

        if (request.TryGetValue("id", out var id))
        {
            redirect.Id = id.ToString();
        }

        if (request.TryGetValue("routePrefix", out var route))
        {
            redirect.RoutePrefix = route.ToString();
        }

        if (request.TryGetValue("destination", out var dest))
        {
            redirect.Destination = dest.ToString();
        }

        if (request.TryGetValue("redirectStyle", out var style))
        {
            redirect.RedirectStyle = int.Parse(style.ToString()!);
        }

        if (request.TryGetValue("isOnlyAfterNotFound", out var onlyAfter))
        {
            redirect.IsOnlyAfterNotFound = bool.Parse(onlyAfter.ToString()!);
        }

        if (request.TryGetValue("isMatchFullUrl", out var matchFull))
        {
            redirect.IsMatchFullUrl = bool.Parse(matchFull.ToString()!);
        }

        if (request.TryGetValue("isMatchQueryString", out var matchQuery))
        {
            redirect.IsMatchQueryString = bool.Parse(matchQuery.ToString()!);
        }

        if (request.TryGetValue("isPattern", out var isPattern))
        {
            redirect.IsPattern = bool.Parse(isPattern.ToString()!);
        }

        if (request.TryGetValue("precedence", out var prec))
        {
            redirect.Precedence = int.Parse(prec.ToString()!);
        }

        return redirect;
    }
}
