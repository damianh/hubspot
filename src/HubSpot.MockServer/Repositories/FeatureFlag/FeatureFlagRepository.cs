using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories.FeatureFlag;

public class FeatureFlagRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly ConcurrentDictionary<string, JsonElement> _featureFlags = new();

    public FeatureFlagRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public Task<List<JsonElement>> GetAllAsync()
    {
        var flags = _featureFlags.Values.ToList();
        return Task.FromResult(flags);
    }

    public Task<JsonElement?> GetAsync(string featureKey) => Task.FromResult(
            _featureFlags.TryGetValue(featureKey, out var flag)
                ? (JsonElement?)flag
                : null
        );

    public Task<JsonElement> EnableAsync(string featureKey, JsonElement? configuration = null)
    {
        var flag = JsonSerializer.SerializeToElement(new
        {
            key = featureKey,
            enabled = true,
            configuration = configuration ?? new JsonElement(),
            enabledAt = _timeProvider.GetUtcNow().ToString("o"),
            updatedAt = _timeProvider.GetUtcNow().ToString("o")
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
            updatedAt = _timeProvider.GetUtcNow().ToString("o")
        });

        _featureFlags[featureKey] = flag;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string featureKey) => Task.FromResult(_featureFlags.TryRemove(featureKey, out _));
}
