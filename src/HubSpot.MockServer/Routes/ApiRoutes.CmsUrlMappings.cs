using DamianH.HubSpot.MockServer.Repositories.UrlMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsUrlMappingsApi(WebApplication app)
    {
        var group = app.MapGroup("/url-mappings/v3/url-mappings");

        group.MapGet("/", (UrlMappingRepository repository) =>
        {
            var mappings = repository.GetAll();
            return Results.Ok(mappings.Select(MapUrlMappingToResponse));
        });

        group.MapPost("/", async (UrlMappingRepository repository, HttpContext context) =>
        {
            var body = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(context.Request.Body);
            if (body == null)
                return Results.BadRequest(new { message = "Invalid request body" });

            var mapping = MapUrlMappingFromBody(body);
            var created = repository.Create(mapping);
            return Results.Ok(MapUrlMappingToResponse(created));
        });

        group.MapGet("/{id:long}", (UrlMappingRepository repository, long id) =>
        {
            var mapping = repository.GetById(id);
            if (mapping == null)
                return Results.NotFound(new { message = $"URL Mapping {id} not found" });
            return Results.Ok(MapUrlMappingToResponse(mapping));
        });

        group.MapDelete("/{id:long}", (UrlMappingRepository repository, long id) =>
        {
            var deleted = repository.Delete(id);
            if (!deleted)
                return Results.NotFound(new { message = $"URL Mapping {id} not found" });
            return Results.NoContent();
        });
    }

    private static object MapUrlMappingToResponse(UrlMapping mapping) => new
    {
        id = mapping.Id,
        routePrefix = mapping.RoutePrefix,
        destination = mapping.Destination,
        redirectStyle = mapping.RedirectStyle,
        isOnlyAfterNotFound = mapping.IsOnlyAfterNotFound,
        isMatchFullUrl = mapping.IsMatchFullUrl,
        isMatchQueryString = mapping.IsMatchQueryString,
        isPattern = mapping.IsPattern,
        precedence = mapping.Precedence,
        created = mapping.Created,
        updated = mapping.Updated
    };

    private static UrlMapping MapUrlMappingFromBody(Dictionary<string, JsonElement> body)
    {
        var mapping = new UrlMapping();

        if (body.TryGetValue("routePrefix", out var rp)) mapping.RoutePrefix = rp.GetString();
        if (body.TryGetValue("destination", out var dest)) mapping.Destination = dest.GetString();
        if (body.TryGetValue("redirectStyle", out var rs)) mapping.RedirectStyle = rs.GetInt32();
        if (body.TryGetValue("isOnlyAfterNotFound", out var oanf)) mapping.IsOnlyAfterNotFound = oanf.GetBoolean();
        if (body.TryGetValue("isMatchFullUrl", out var mfu)) mapping.IsMatchFullUrl = mfu.GetBoolean();
        if (body.TryGetValue("isMatchQueryString", out var mqs)) mapping.IsMatchQueryString = mqs.GetBoolean();
        if (body.TryGetValue("isPattern", out var ip)) mapping.IsPattern = ip.GetBoolean();
        if (body.TryGetValue("precedence", out var prec)) mapping.Precedence = prec.GetInt32();

        return mapping;
    }
}
