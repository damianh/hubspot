using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterExportsApi(WebApplication app, ExportRepository exportRepo)
    {
        var v3 = app.MapGroup("/crm/v3/exports");

        // POST /crm/v3/exports/export/async
        v3.MapPost("export/async", ([FromBody] ExportCreateRequest request) =>
        {
            var job = exportRepo.CreateExport(
                request.ExportName ?? $"export-{DateTime.UtcNow:yyyyMMdd-HHmmss}",
                request.ExportType ?? "VIEW",
                request.ObjectType,
                request.Properties,
                request.Format,
                request.StartDate,
                request.EndDate
            );

            return Results.Accepted($"/crm/v3/exports/{job.Id}", new
            {
                id = job.Id,
                status = job.Status.ToString(),
                createdAt = job.CreatedAt,
                startedAt = job.StartedAt,
                exportName = job.ExportName,
                exportType = job.ExportType,
                format = job.Format,
                objectType = job.ObjectType
            });
        });

        // GET /crm/v3/exports/export/async/tasks/{exportId}/status
        v3.MapGet("export/async/tasks/{exportId}/status", (string exportId, ExportRepository repo) =>
        {
            var job = repo.GetExport(exportId);
            if (job == null)
            {
                return Results.NotFound(new { message = $"Export not found: {exportId}" });
            }

            return Results.Ok(new
            {
                id = job.Id,
                status = job.Status.ToString(),
                createdAt = job.CreatedAt,
                startedAt = job.StartedAt,
                completedAt = job.CompletedAt,
                exportName = job.ExportName,
                exportType = job.ExportType,
                format = job.Format,
                objectType = job.ObjectType
            });
        });

        // GET /crm/v3/exports/export/async/tasks/{exportId}
        v3.MapGet("export/async/tasks/{exportId}", (string exportId, ExportRepository repo) =>
        {
            var job = repo.GetExport(exportId);
            if (job == null)
            {
                return Results.NotFound(new { message = $"Export not found: {exportId}" });
            }

            var file = repo.GetExportFile(exportId);

            return Results.Ok(new
            {
                id = job.Id,
                status = job.Status.ToString(),
                createdAt = job.CreatedAt,
                startedAt = job.StartedAt,
                completedAt = job.CompletedAt,
                exportName = job.ExportName,
                exportType = job.ExportType,
                format = job.Format,
                objectType = job.ObjectType,
                fileUrl = file != null ? $"/crm/v3/exports/{job.Id}/download" : null
            });
        });

        // GET /crm/v3/exports/{exportId}/download
        v3.MapGet("{exportId}/download", (string exportId, ExportRepository repo) =>
        {
            var job = repo.GetExport(exportId);
            if (job == null)
            {
                return Results.NotFound(new { message = $"Export not found: {exportId}" });
            }

            var file = repo.GetExportFile(exportId);
            if (file == null)
            {
                return Results.NotFound(new { message = $"Export file not ready: {exportId}" });
            }

            var fileName = $"{job.ExportName}.{job.Format.ToLower()}";
            return Results.File(file, "text/csv", fileName);
        });

        // GET /crm/v3/exports
        v3.MapGet("", (string? after, int? limit, ExportRepository repo) =>
        {
            var result = repo.ListExports(after, limit ?? 10);
            
            return Results.Ok(new
            {
                results = result.Results.Select(job => new
                {
                    id = job.Id,
                    status = job.Status.ToString(),
                    createdAt = job.CreatedAt,
                    exportName = job.ExportName,
                    exportType = job.ExportType,
                    objectType = job.ObjectType
                }).ToList(),
                paging = result.Paging
            });
        });

        // POST /crm/v3/exports/{exportId}/cancel
        v3.MapPost("{exportId}/cancel", (string exportId, ExportRepository repo) =>
        {
            var job = repo.CancelExport(exportId);
            if (job == null)
            {
                return Results.NotFound(new { message = $"Export not found: {exportId}" });
            }

            return Results.Ok(new
            {
                id = job.Id,
                status = job.Status.ToString(),
                createdAt = job.CreatedAt,
                completedAt = job.CompletedAt,
                exportName = job.ExportName
            });
        });
    }
}

public record ExportCreateRequest(
    string ObjectType,
    string? ExportName = null,
    string? ExportType = null,
    string? Format = null,
    List<string>? Properties = null,
    DateTimeOffset? StartDate = null,
    DateTimeOffset? EndDate = null
);
