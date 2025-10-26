using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.SourceCode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsSourceCodeApi(WebApplication app)
    {
        var group = app.MapGroup("/cms/v3/source-code/{environment}");

        group.MapGet("/content/{**path}", (SourceCodeRepository repository, string environment, string path) =>
        {
            var file = repository.GetByPath(path);
            return file == null
                ? Results.NotFound(new { message = $"File {path} not found" })
                : Results.Ok(MapFileToResponse(file));
        });

        group.MapGet("/content", (SourceCodeRepository repository, string environment, HttpContext context) =>
        {
            var files = repository.GetAll();

            return Results.Ok(new
            {
                total = files.Count,
                results = files.Select(MapFileToResponse)
            });
        });

        group.MapPost("/content/{**path}", async (SourceCodeRepository repository, string environment, string path, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var file = MapFileFromRequest(request);
            file.Path = path;
            var created = repository.Create(file);

            return Results.Ok(MapFileToResponse(created));
        });

        group.MapPut("/content/{**path}", async (SourceCodeRepository repository, string environment, string path, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var file = MapFileFromRequest(request);
            var updated = repository.Update(path, file);

            if (updated == null)
            {
                return Results.NotFound(new { message = $"File {path} not found" });
            }

            return Results.Ok(MapFileToResponse(updated));
        });

        group.MapDelete("/content/{**path}", (SourceCodeRepository repository, string environment, string path) =>
        {
            var success = repository.Delete(path);
            if (!success)
            {
                return Results.NotFound(new { message = $"File {path} not found" });
            }

            return Results.NoContent();
        });

        // Metadata endpoints
        group.MapGet("/metadata/{**path}", (SourceCodeRepository repository, string environment, string path) =>
        {
            var file = repository.GetByPath(path);
            if (file == null)
            {
                return Results.NotFound(new { message = $"File {path} not found" });
            }

            return Results.Ok(new
            {
                id = file.Id,
                path = file.Path,
                type = file.Type,
                createdAt = file.CreatedAt,
                updatedAt = file.UpdatedAt
            });
        });
    }

    private static object MapFileToResponse(SourceCodeFile file) => new
    {
        id = file.Id,
        path = file.Path,
        content = file.Content,
        type = file.Type,
        createdAt = file.CreatedAt,
        updatedAt = file.UpdatedAt
    };

    private static SourceCodeFile MapFileFromRequest(Dictionary<string, object> request) => new SourceCodeFile
    {
        Path = request.GetValueOrDefault("path")?.ToString(),
        Content = request.GetValueOrDefault("content")?.ToString(),
        Type = request.GetValueOrDefault("type")?.ToString()
    };
}
