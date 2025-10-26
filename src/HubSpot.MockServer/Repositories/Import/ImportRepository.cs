using System.Collections.Concurrent;
using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class ImportRepository(HubSpotObjectRepository? objectRepository = null)
{
    private readonly ConcurrentDictionary<string, ImportJob> _imports = new();
    private readonly ConcurrentDictionary<string, List<ImportRow>> _importData = new();
    private readonly ConcurrentDictionary<string, List<ImportError>> _importErrors = new();

    public ImportJob CreateImport(string importName, string objectType,
        List<Dictionary<string, string>> rows, Dictionary<string, string>? config = null)
    {
        var importId = GenerateImportId();
        var job = new ImportJob
        {
            Id = importId,
            State = ImportState.STARTED,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            ImportName = importName,
            ImportSource = "API",
            Metadata = new ImportMetadata
            {
                ObjectType = objectType,
                NumRowsProcessed = 0,
                NumRowsSucceeded = 0,
                NumRowsFailed = 0
            }
        };

        _imports[importId] = job;
        _importData[importId] = rows.Select((r, idx) => new ImportRow
        {
            RowIndex = idx,
            Data = r,
            ObjectId = null
        }).ToList();
        _importErrors[importId] = [];

        // Start async processing simulation
        Task.Run(() => ProcessImport(importId));

        return job;
    }

    public ImportJob? GetImport(string importId) => _imports.GetValueOrDefault(importId);

    public PagedResult<ImportJob> ListImports(string? after = null, int limit = 10)
    {
        var imports = _imports.Values.OrderByDescending(i => i.CreatedAt).ToList();

        var startIndex = 0;
        if (!string.IsNullOrEmpty(after))
        {
            var afterIndex = imports.FindIndex(i => i.Id == after);
            if (afterIndex >= 0)
            {
                startIndex = afterIndex + 1;
            }
        }

        var results = imports.Skip(startIndex).Take(limit).ToList();
        var hasMore = startIndex + limit < imports.Count;
        var nextAfter = hasMore && results.Count > 0 ? results[^1].Id : null;

        return new PagedResult<ImportJob>
        {
            Results = results,
            Paging = new PagingInfo
            {
                Next = nextAfter != null ? new NextPageInfo { After = nextAfter } : null
            }
        };
    }

    public ImportJob? CancelImport(string importId)
    {
        if (!_imports.TryGetValue(importId, out var job))
        {
            return null;
        }

        if (job.State == ImportState.STARTED || job.State == ImportState.PROCESSING)
        {
            job.State = ImportState.CANCELED;
            job.UpdatedAt = DateTimeOffset.UtcNow;
        }

        return job;
    }

    public PagedResult<ImportError> GetImportErrors(string importId, string? after = null, int limit = 50)
    {
        if (!_importErrors.TryGetValue(importId, out var errors))
        {
            return new PagedResult<ImportError>
            {
                Results = [],
                Paging = new PagingInfo { Next = null }
            };
        }

        var startIndex = 0;
        if (!string.IsNullOrEmpty(after))
        {
            if (int.TryParse(after, out var afterIdx))
            {
                startIndex = afterIdx + 1;
            }
        }

        var results = errors.Skip(startIndex).Take(limit).ToList();
        var hasMore = startIndex + limit < errors.Count;
        var nextAfter = hasMore ? (startIndex + limit).ToString() : null;

        return new PagedResult<ImportError>
        {
            Results = results,
            Paging = new PagingInfo
            {
                Next = nextAfter != null ? new NextPageInfo { After = nextAfter } : null
            }
        };
    }

    private async Task ProcessImport(string importId)
    {
        if (!_imports.TryGetValue(importId, out var job))
        {
            return;
        }

        job.State = ImportState.PROCESSING;
        job.UpdatedAt = DateTimeOffset.UtcNow;

        if (!_importData.TryGetValue(importId, out var rows))
        {
            job.State = ImportState.FAILED;
            job.UpdatedAt = DateTimeOffset.UtcNow;
            return;
        }

        var errors = _importErrors[importId];
        var objectType = job.Metadata.ObjectType;

        foreach (var row in rows)
        {
            if (job.State == ImportState.CANCELED)
            {
                break;
            }

            // Basic validation
            var validationError = ValidateRow(row.Data, objectType);
            if (validationError != null)
            {
                errors.Add(new ImportError
                {
                    Id = $"error-{row.RowIndex}",
                    CreatedAt = row.RowIndex,
                    ObjectType = objectType,
                    SourceData = row.Data,
                    ErrorType = validationError
                });
                job.Metadata.NumRowsFailed++;
            }
            else
            {
                // Simulate object creation - just generate an ID for now
                row.ObjectId = $"imported-{objectType}-{row.RowIndex}";
                job.Metadata.NumRowsSucceeded++;
            }

            job.Metadata.NumRowsProcessed++;
        }

        job.State = job.State == ImportState.CANCELED ? ImportState.CANCELED : ImportState.DONE;
        job.UpdatedAt = DateTimeOffset.UtcNow;
    }

    private string? ValidateRow(Dictionary<string, string> data, string objectType)
    {
        // Basic validation - check for email if objectType is contact
        if (objectType.Equals("CONTACT", StringComparison.OrdinalIgnoreCase) ||
            objectType.Equals("contacts", StringComparison.OrdinalIgnoreCase))
        {
            if (data.TryGetValue("email", out var email))
            {
                if (!email.Contains('@'))
                {
                    return "INVALID_EMAIL";
                }
            }
        }

        return null; // No errors
    }

    private static int _importIdCounter;
    private static string GenerateImportId() => Interlocked.Increment(ref _importIdCounter).ToString();
}
