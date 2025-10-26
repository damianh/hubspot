using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories.PropertyValidation;

internal class PropertyValidationRepository
{
    private readonly ConcurrentDictionary<string, List<JsonElement>> _validations = new();

    private static string GetKey(string objectType, string propertyName) => $"{objectType}:{propertyName}";

    public Task<List<JsonElement>> GetValidationsAsync(string objectType, string propertyName)
    {
        var key = GetKey(objectType, propertyName);
        return Task.FromResult(
            _validations.TryGetValue(key, out var validations)
                ? validations.ToList()
                : []
        );
    }

    public Task<JsonElement> CreateValidationAsync(string objectType, string propertyName, JsonElement validation)
    {
        var key = GetKey(objectType, propertyName);
        var validationId = Guid.NewGuid().ToString();

        var validationObject = JsonSerializer.SerializeToElement(new
        {
            id = validationId,
            objectType,
            propertyName,
            type = validation.TryGetProperty("type", out var type) ? type.GetString() : "REGEX",
            pattern = validation.TryGetProperty("pattern", out var pattern) ? pattern.GetString() : "",
            message = validation.TryGetProperty("message", out var message) ? message.GetString() : "Validation failed",
            enabled = validation.TryGetProperty("enabled", out var enabled) ? enabled.GetBoolean() : true,
            createdAt = DateTimeOffset.UtcNow.ToString("o"),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        var validations = _validations.GetOrAdd(key, _ => []);
        validations.Add(validationObject);

        return Task.FromResult(validationObject);
    }

    public Task<JsonElement?> UpdateValidationAsync(string objectType, string propertyName, string validationId, JsonElement updates)
    {
        var key = GetKey(objectType, propertyName);
        if (!_validations.TryGetValue(key, out var validations))
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var index = validations.FindIndex(v =>
            v.TryGetProperty("id", out var id) && id.GetString() == validationId);

        if (index < 0)
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var existing = validations[index];
        var updated = JsonSerializer.SerializeToElement(new
        {
            id = validationId,
            objectType,
            propertyName,
            type = updates.TryGetProperty("type", out var type) ? type.GetString() : existing.GetProperty("type").GetString(),
            pattern = updates.TryGetProperty("pattern", out var pattern) ? pattern.GetString() : existing.GetProperty("pattern").GetString(),
            message = updates.TryGetProperty("message", out var message) ? message.GetString() : existing.GetProperty("message").GetString(),
            enabled = updates.TryGetProperty("enabled", out var enabled) ? enabled.GetBoolean() : existing.GetProperty("enabled").GetBoolean(),
            createdAt = existing.GetProperty("createdAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        validations[index] = updated;
        return Task.FromResult<JsonElement?>(updated);
    }

    public Task<bool> DeleteValidationAsync(string objectType, string propertyName, string validationId)
    {
        var key = GetKey(objectType, propertyName);
        if (!_validations.TryGetValue(key, out var validations))
        {
            return Task.FromResult(false);
        }

        var index = validations.FindIndex(v =>
            v.TryGetProperty("id", out var id) && id.GetString() == validationId);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        validations.RemoveAt(index);
        return Task.FromResult(true);
    }
}
