using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

public class ObjectLibraryRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _objectLibraryItems = new();

    public Task<List<JsonElement>> GetAllAsync()
    {
        var items = _objectLibraryItems.Values.ToList();
        return Task.FromResult(items);
    }

    public Task<JsonElement?> GetByObjectTypeAsync(string objectType) => Task.FromResult(
            _objectLibraryItems.TryGetValue(objectType, out var item)
                ? (JsonElement?)item
                : null
        );

    public Task<JsonElement> CreateAsync(string objectType, JsonElement definition)
    {
        var item = JsonSerializer.SerializeToElement(new
        {
            objectType,
            name = definition.TryGetProperty("name", out var name) ? name.GetString() : objectType,
            description = definition.TryGetProperty("description", out var desc) ? desc.GetString() : "",
            primaryDisplayProperty = definition.TryGetProperty("primaryDisplayProperty", out var pdp) ? pdp.GetString() : "name",
            properties = definition.TryGetProperty("properties", out var props) ? props : new JsonElement(),
            associations = definition.TryGetProperty("associations", out var assocs) ? assocs : new JsonElement(),
            createdAt = DateTimeOffset.UtcNow.ToString("o"),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _objectLibraryItems[objectType] = item;
        return Task.FromResult(item);
    }

    public Task<JsonElement?> UpdateAsync(string objectType, JsonElement updates)
    {
        if (!_objectLibraryItems.TryGetValue(objectType, out var existing))
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var updated = JsonSerializer.SerializeToElement(new
        {
            objectType,
            name = updates.TryGetProperty("name", out var name) ? name.GetString() : existing.GetProperty("name").GetString(),
            description = updates.TryGetProperty("description", out var desc) ? desc.GetString() : existing.GetProperty("description").GetString(),
            primaryDisplayProperty = updates.TryGetProperty("primaryDisplayProperty", out var pdp) ? pdp.GetString() : existing.GetProperty("primaryDisplayProperty").GetString(),
            properties = updates.TryGetProperty("properties", out var props) ? props : existing.GetProperty("properties"),
            associations = updates.TryGetProperty("associations", out var assocs) ? assocs : existing.GetProperty("associations"),
            createdAt = existing.GetProperty("createdAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _objectLibraryItems[objectType] = updated;
        return Task.FromResult<JsonElement?>(updated);
    }

    public Task DeleteAsync(string objectType)
    {
        _objectLibraryItems.TryRemove(objectType, out _);
        return Task.CompletedTask;
    }
}
