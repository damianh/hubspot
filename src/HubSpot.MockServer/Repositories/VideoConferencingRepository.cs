using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

public class VideoConferencingRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _settings = new();

    public Task<JsonElement?> GetSettingsAsync(string appId)
    {
        _settings.TryGetValue(appId, out var settings);
        return Task.FromResult<JsonElement?>(settings.ValueKind != JsonValueKind.Undefined ? settings : null);
    }

    public Task<JsonElement> CreateSettingsAsync(string appId, JsonElement body)
    {
        var settings = CreateSettingsObject(appId, body);
        _settings[appId] = settings;
        return Task.FromResult(settings);
    }

    public Task<JsonElement> UpdateSettingsAsync(string appId, JsonElement body)
    {
        var existing = _settings.GetValueOrDefault(appId);
        var settings = existing.ValueKind != JsonValueKind.Undefined 
            ? MergeSettings(existing, body)
            : CreateSettingsObject(appId, body);
        
        _settings[appId] = settings;
        return Task.FromResult(settings);
    }

    public Task DeleteSettingsAsync(string appId)
    {
        _settings.TryRemove(appId, out _);
        return Task.CompletedTask;
    }

    private static JsonElement CreateSettingsObject(string appId, JsonElement body)
    {
        var settings = new Dictionary<string, object?>
        {
            ["id"] = appId,
            ["name"] = body.TryGetProperty("name", out var name) ? name.GetString() : "Video Conferencing App",
            ["url"] = body.TryGetProperty("url", out var url) ? url.GetString() : "",
            ["isReady"] = body.TryGetProperty("isReady", out var isReady) && isReady.GetBoolean(),
            ["createdAt"] = DateTimeOffset.UtcNow,
            ["updatedAt"] = DateTimeOffset.UtcNow
        };

        return JsonSerializer.SerializeToElement(settings);
    }

    private static JsonElement MergeSettings(JsonElement existing, JsonElement updates)
    {
        var settings = JsonSerializer.Deserialize<Dictionary<string, object?>>(existing.GetRawText()) 
            ?? new Dictionary<string, object?>();

        if (updates.TryGetProperty("name", out var name))
            settings["name"] = name.GetString();
        if (updates.TryGetProperty("url", out var url))
            settings["url"] = url.GetString();
        if (updates.TryGetProperty("isReady", out var isReady))
            settings["isReady"] = isReady.GetBoolean();

        settings["updatedAt"] = DateTimeOffset.UtcNow;

        return JsonSerializer.SerializeToElement(settings);
    }
}
