using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsSiteSearchApi(this IEndpointRouteBuilder app, SiteSearchRepository repository)
    {
        var group = app.MapGroup("/cms/v3/site-search");

        group.MapGet("/search", (HttpContext context) =>
        {
            var query = context.Request.Query["q"].ToString();
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 20;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;

            var results = repository.Search(query, offset, limit);
            var total = repository.Count();

            return Results.Ok(new
            {
                total,
                results = results.Select(MapSearchContentToResponse)
            });
        });

        group.MapPost("/index", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var content = MapSearchContentFromRequest(request);
            var indexed = repository.AddContent(content);

            return Results.Ok(MapSearchContentToResponse(indexed));
        });

        group.MapDelete("/index/{contentId}", (string contentId) =>
        {
            var success = repository.Delete(contentId);
            if (!success)
            {
                return Results.NotFound(new { message = $"Content {contentId} not found" });
            }

            return Results.NoContent();
        });
    }

    private static object MapSearchContentToResponse(SearchableContent content) => new
    {
        id = content.Id,
        title = content.Title,
        description = content.Description,
        content = content.Content,
        url = content.Url,
        type = content.Type,
        language = content.Language,
        indexedAt = content.IndexedAt,
        metadata = content.Metadata
    };

    private static SearchableContent MapSearchContentFromRequest(Dictionary<string, object> request)
    {
        var content = new SearchableContent
        {
            Title = request.GetValueOrDefault("title")?.ToString(),
            Description = request.GetValueOrDefault("description")?.ToString(),
            Content = request.GetValueOrDefault("content")?.ToString(),
            Url = request.GetValueOrDefault("url")?.ToString(),
            Type = request.GetValueOrDefault("type")?.ToString(),
            Language = request.GetValueOrDefault("language")?.ToString()
        };

        if (request.TryGetValue("metadata", out var metadataObj))
        {
            var metadataJson = JsonSerializer.Serialize(metadataObj);
            content.Metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataJson);
        }

        return content;
    }
}
