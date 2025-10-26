using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterFiles(WebApplication app)
    {
        var files = app.MapGroup("/files/v3");

        files.MapPost("/files", async (
            HttpRequest request,
            FileRepository repo) =>
        {
            if (!request.HasFormContentType)
            {
                return Results.BadRequest("Must be multipart/form-data");
            }

            var form = await request.ReadFormAsync();
            var file = form.Files.FirstOrDefault();
            if (file == null)
            {
                return Results.BadRequest("No file uploaded");
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var content = ms.ToArray();

            var metadata = repo.UploadFile(
                file.FileName,
                file.ContentType ?? "application/octet-stream",
                content);

            return Results.Ok(new
            {
                id = metadata.Id,
                name = metadata.Name,
                type = metadata.Type,
                extension = metadata.Extension,
                size = metadata.Size,
                url = metadata.Url,
                createdAt = metadata.CreatedAt,
                updatedAt = metadata.UpdatedAt
            });
        });

        files.MapGet("/files", (
            FileRepository repo) =>
        {
            var allFiles = repo.GetAllFiles();
            return Results.Ok(new
            {
                results = allFiles.Select(f => new
                {
                    id = f.Id,
                    name = f.Name,
                    type = f.Type,
                    extension = f.Extension,
                    size = f.Size,
                    url = f.Url,
                    createdAt = f.CreatedAt,
                    updatedAt = f.UpdatedAt
                })
            });
        });

        files.MapGet("/files/{fileId}", (
            string fileId,
            FileRepository repo) =>
        {
            var file = repo.GetFile(fileId);
            if (file == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new
            {
                id = file.Id,
                name = file.Name,
                type = file.Type,
                extension = file.Extension,
                size = file.Size,
                url = file.Url,
                createdAt = file.CreatedAt,
                updatedAt = file.UpdatedAt
            });
        });

        files.MapPatch("/files/{fileId}", (
            string fileId,
            [FromBody] FileUpdateRequest request,
            FileRepository repo) =>
        {
            var updated = repo.UpdateFile(fileId, request.Name);
            if (updated == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new
            {
                id = updated.Id,
                name = updated.Name,
                type = updated.Type,
                extension = updated.Extension,
                size = updated.Size,
                url = updated.Url,
                createdAt = updated.CreatedAt,
                updatedAt = updated.UpdatedAt
            });
        });

        files.MapDelete("/files/{fileId}", (
            string fileId,
            FileRepository repo) =>
        {
            var deleted = repo.DeleteFile(fileId);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        files.MapGet("/files/{fileId}/signed-url", (
            string fileId,
            FileRepository repo) =>
        {
            var file = repo.GetFile(fileId);
            if (file == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new
            {
                url = $"https://mock.hubspot.com/files/signed/{fileId}?expires=9999999999",
                expiresAt = DateTime.UtcNow.AddHours(1)
            });
        });

        files.MapGet("/files/{fileId}/download", (
            string fileId,
            FileRepository repo) =>
        {
            var file = repo.GetFile(fileId);
            if (file == null)
            {
                return Results.NotFound();
            }

            var content = repo.GetFileContent(fileId);
            if (content == null)
            {
                return Results.NotFound();
            }

            return Results.File(content, file.Type, file.Name);
        });
    }

    private record FileUpdateRequest
    {
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string? Name { get; init; }
    }
}
