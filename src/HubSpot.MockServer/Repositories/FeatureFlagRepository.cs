using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

public class FeatureFlagRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _featureFlags = new();

    public Task<List<JsonElement>> GetAllAsync()
    {
        var flags = _featureFlags.Values.ToList();
        return Task.FromResult(flags);
    }

    public Task<JsonElement?> GetAsync(string featureKey)
    {
        return Task.FromResult(
            _featureFlags.TryGetValue(featureKey, out var flag) 
                ? (JsonElement?)flag 
                : null
        );
    }

    public Task<JsonElement> EnableAsync(string featureKey, JsonElement? configuration = null)
    {
        var flag = JsonSerializer.SerializeToElement(new
        {
            key = featureKey,
            enabled = true,
            configuration = configuration ?? new JsonElement(),
            enabledAt = DateTimeOffset.UtcNow.ToString("o"),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _featureFlags[featureKey] = flag;
        return Task.FromResult(flag);
    }

    public Task<bool> DisableAsync(string featureKey)
    {
        if (!_featureFlags.TryGetValue(featureKey, out var existing))
        {
            return Task.FromResult(false);
        }

        var flag = JsonSerializer.SerializeToElement(new
        {
            key = featureKey,
            enabled = false,
            configuration = existing.TryGetProperty("configuration", out var config) ? config : new JsonElement(),
            enabledAt = existing.GetProperty("enabledAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _featureFlags[featureKey] = flag;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string featureKey)
    {
        return Task.FromResult(_featureFlags.TryRemove(featureKey, out _));
    }
}
