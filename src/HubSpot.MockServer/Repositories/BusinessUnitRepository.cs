using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class BusinessUnitRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _businessUnits = new();
    private int _nextId = 1;

    public Task<List<JsonElement>> GetAllAsync()
    {
        var units = _businessUnits.Values.ToList();
        return Task.FromResult(units);
    }

    public Task<JsonElement?> GetByIdAsync(string businessUnitId) => Task.FromResult(
            _businessUnits.TryGetValue(businessUnitId, out var unit)
                ? (JsonElement?)unit
                : null
        );

    public Task<JsonElement> CreateAsync(JsonElement businessUnit)
    {
        var id = Interlocked.Increment(ref _nextId).ToString();

        var unit = JsonSerializer.SerializeToElement(new
        {
            id,
            name = businessUnit.TryGetProperty("name", out var name) ? name.GetString() : $"Business Unit {id}",
            logoMetadataId = businessUnit.TryGetProperty("logoMetadataId", out var logo) ? logo.GetString() : null,
            publicLabel = businessUnit.TryGetProperty("publicLabel", out var label) ? label.GetString() : null,
            domain = businessUnit.TryGetProperty("domain", out var domain) ? domain.GetString() : null,
            createdAt = DateTimeOffset.UtcNow.ToString("o"),
            updatedAt = DateTimeOffset.UtcNow.ToString("o"),
            archived = false
        });

        _businessUnits[id] = unit;
        return Task.FromResult(unit);
    }

    public Task<JsonElement?> UpdateAsync(string businessUnitId, JsonElement updates)
    {
        if (!_businessUnits.TryGetValue(businessUnitId, out var existing))
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var updated = JsonSerializer.SerializeToElement(new
        {
            id = businessUnitId,
            name = updates.TryGetProperty("name", out var name) ? name.GetString() : existing.GetProperty("name").GetString(),
            logoMetadataId = updates.TryGetProperty("logoMetadataId", out var logo) ? logo.GetString() : existing.TryGetProperty("logoMetadataId", out var existingLogo) ? existingLogo.GetString() : null,
            publicLabel = updates.TryGetProperty("publicLabel", out var label) ? label.GetString() : existing.TryGetProperty("publicLabel", out var existingLabel) ? existingLabel.GetString() : null,
            domain = updates.TryGetProperty("domain", out var domain) ? domain.GetString() : existing.TryGetProperty("domain", out var existingDomain) ? existingDomain.GetString() : null,
            createdAt = existing.GetProperty("createdAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o"),
            archived = existing.GetProperty("archived").GetBoolean()
        });

        _businessUnits[businessUnitId] = updated;
        return Task.FromResult<JsonElement?>(updated);
    }

    public Task<bool> ArchiveAsync(string businessUnitId)
    {
        if (!_businessUnits.TryGetValue(businessUnitId, out var existing))
        {
            return Task.FromResult(false);
        }

        var archived = JsonSerializer.SerializeToElement(new
        {
            id = businessUnitId,
            name = existing.GetProperty("name").GetString(),
            logoMetadataId = existing.TryGetProperty("logoMetadataId", out var logo) ? logo.GetString() : null,
            publicLabel = existing.TryGetProperty("publicLabel", out var label) ? label.GetString() : null,
            domain = existing.TryGetProperty("domain", out var domain) ? domain.GetString() : null,
            createdAt = existing.GetProperty("createdAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o"),
            archived = true
        });

        _businessUnits[businessUnitId] = archived;
        return Task.FromResult(true);
    }
}
