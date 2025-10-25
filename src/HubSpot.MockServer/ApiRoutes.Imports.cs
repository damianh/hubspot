using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static void RegisterImportsApi(WebApplication app, ImportRepository importRepo)
    {
        var v3 = app.MapGroup("/crm/v3/imports");

        // POST /crm/v3/imports
        v3.MapPost("", ([FromBody] ImportCreateRequest request) =>
        {
            var job = importRepo.CreateImport(
                request.ImportName,
                request.ObjectType,
                request.Rows,
                request.Config
            );

            return Results.Created($"/crm/v3/imports/{job.Id}", new
            {
                id = job.Id,
                state = job.State.ToString(),
                createdAt = job.CreatedAt,
                updatedAt = job.UpdatedAt,
                importName = job.ImportName,
                importSource = job.ImportSource,
                optOutImport = job.OptOutImport,
                metadata = job.Metadata
            });
        });

        // GET /crm/v3/imports/{importId}
        v3.MapGet("{importId}", (string importId) =>
        {
            var job = importRepo.GetImport(importId);
            if (job == null)
                return Results.NotFound(new { message = $"Import not found: {importId}" });

            return Results.Ok(new
            {
                id = job.Id,
                state = job.State.ToString(),
                createdAt = job.CreatedAt,
                updatedAt = job.UpdatedAt,
                importName = job.ImportName,
                importSource = job.ImportSource,
                optOutImport = job.OptOutImport,
                metadata = job.Metadata
            });
        });

        // GET /crm/v3/imports
        v3.MapGet("", (string? after, int? limit) =>
        {
            var result = importRepo.ListImports(after, limit ?? 10);
            
            return Results.Ok(new
            {
                results = result.Results.Select(job => new
                {
                    id = job.Id,
                    state = job.State.ToString(),
                    createdAt = job.CreatedAt,
                    updatedAt = job.UpdatedAt,
                    importName = job.ImportName,
                    importSource = job.ImportSource,
                    metadata = job.Metadata
                }).ToList(),
                paging = result.Paging
            });
        });

        // POST /crm/v3/imports/{importId}/cancel
        v3.MapPost("{importId}/cancel", (string importId) =>
        {
            var job = importRepo.CancelImport(importId);
            if (job == null)
                return Results.NotFound(new { message = $"Import not found: {importId}" });

            return Results.Ok(new
            {
                id = job.Id,
                state = job.State.ToString(),
                createdAt = job.CreatedAt,
                updatedAt = job.UpdatedAt,
                importName = job.ImportName,
                metadata = job.Metadata
            });
        });

        // GET /crm/v3/imports/{importId}/errors
        v3.MapGet("{importId}/errors", (string importId, string? after, int? limit) =>
        {
            var result = importRepo.GetImportErrors(importId, after, limit ?? 50);
            
            return Results.Ok(new
            {
                results = result.Results,
                paging = result.Paging
            });
        });
    }
}

public record ImportCreateRequest(
    string ImportName,
    string ObjectType,
    List<Dictionary<string, string>> Rows,
    Dictionary<string, string>? Config = null
);
