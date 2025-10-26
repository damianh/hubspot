using System.Collections.Concurrent;
using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class ExportRepository(HubSpotObjectRepository? objectRepository = null)
{
    private readonly ConcurrentDictionary<string, ExportJob> _exports = new();
    private readonly ConcurrentDictionary<string, byte[]> _exportFiles = new();
    private readonly HubSpotObjectRepository? _objectRepository = objectRepository;

    public ExportJob CreateExport(string exportName, string exportType, string objectType, 
        List<string>? properties = null, string? format = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        var exportId = GenerateExportId();
        var job = new ExportJob
        {
            Id = exportId,
            Status = ExportStatus.PROCESSING,
            CreatedAt = DateTimeOffset.UtcNow,
            StartedAt = DateTimeOffset.UtcNow,
            ExportName = exportName,
            ExportType = exportType,
            Format = format ?? "CSV",
            ObjectType = objectType,
            Properties = properties ?? []
        };

        _exports[exportId] = job;

        // Start async processing simulation
        Task.Run(() => ProcessExport(exportId));

        return job;
    }

    public ExportJob? GetExport(string exportId)
    {
        return _exports.TryGetValue(exportId, out var job) ? job : null;
    }

    public PagedResult<ExportJob> ListExports(string? after = null, int limit = 10)
    {
        var exports = _exports.Values.OrderByDescending(e => e.CreatedAt).ToList();
        
        var startIndex = 0;
        if (!string.IsNullOrEmpty(after))
        {
            var afterIndex = exports.FindIndex(e => e.Id == after);
            if (afterIndex >= 0)
            {
                startIndex = afterIndex + 1;
            }
        }

        var results = exports.Skip(startIndex).Take(limit).ToList();
        var hasMore = startIndex + limit < exports.Count;
        var nextAfter = hasMore && results.Count > 0 ? results[^1].Id : null;

        return new PagedResult<ExportJob>
        {
            Results = results,
            Paging = new PagingInfo
            {
                Next = nextAfter != null ? new NextPageInfo { After = nextAfter } : null
            }
        };
    }

    public ExportJob? CancelExport(string exportId)
    {
        if (!_exports.TryGetValue(exportId, out var job))
        {
            return null;
        }

        if (job.Status == ExportStatus.PROCESSING)
        {
            job.Status = ExportStatus.CANCELED;
            job.CompletedAt = DateTimeOffset.UtcNow;
        }

        return job;
    }

    public byte[]? GetExportFile(string exportId)
    {
        return _exportFiles.TryGetValue(exportId, out var file) ? file : null;
    }

    private async Task ProcessExport(string exportId)
    {
        if (!_exports.TryGetValue(exportId, out var job))
        {
            return;
        }

        await Task.Delay(500); // Simulate processing delay

        // Generate mock CSV content
        var csv = GenerateMockCsvData(job.ObjectType, job.Properties);
        _exportFiles[exportId] = System.Text.Encoding.UTF8.GetBytes(csv);

        job.Status = ExportStatus.COMPLETE;
        job.CompletedAt = DateTimeOffset.UtcNow;
    }

    private string GenerateMockCsvData(string objectType, List<string> properties)
    {
        // Generate header
        var columns = properties.Count > 0 
            ? properties 
            : ["id", "createdAt", "updatedAt", "name", "email"];

        var header = string.Join(",", columns);
        var rows = new List<string> { header };

        // Generate 10 mock rows
        for (var i = 1; i <= 10; i++)
        {
            var values = columns.Select(col => col.ToLower() switch
            {
                "id" => $"{objectType.ToLower()}-{i}",
                "createdat" => DateTimeOffset.UtcNow.AddDays(-i).ToString("o"),
                "updatedat" => DateTimeOffset.UtcNow.ToString("o"),
                "name" => $"Sample {objectType} {i}",
                "email" => $"sample{i}@example.com",
                _ => $"value-{i}"
            });
            rows.Add(string.Join(",", values));
        }

        return string.Join("\n", rows);
    }

    private static int _exportIdCounter;
    private static string GenerateExportId() => Interlocked.Increment(ref _exportIdCounter).ToString();
}

public class ExportJob
{
    public required string Id { get; init; }
    public ExportStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? StartedAt { get; init; }
    public DateTimeOffset? CompletedAt { get; set; }
    public required string ExportName { get; init; }
    public required string ExportType { get; init; }
    public required string Format { get; init; }
    public required string ObjectType { get; init; }
    public required List<string> Properties { get; init; }
}

public enum ExportStatus
{
    PROCESSING,
    COMPLETE,
    FAILED,
    CANCELED
}
