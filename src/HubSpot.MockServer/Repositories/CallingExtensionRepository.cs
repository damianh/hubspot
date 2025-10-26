using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class CallingExtensionRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _settings = new();
    private readonly ConcurrentDictionary<string, List<CallRecording>> _recordings = new();

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
            ? MergeSettings(existing, body, appId)
            : CreateSettingsObject(appId, body);

        _settings[appId] = settings;
        return Task.FromResult(settings);
    }

    public Task MarkAsReadyAsync(string appId)
    {
        if (_settings.TryGetValue(appId, out var existing))
        {
            var updated = SetPropertyValue(existing, "isReady", true);
            _settings[appId] = updated;
        }
        return Task.CompletedTask;
    }

    public Task DeleteSettingsAsync(string appId)
    {
        _settings.TryRemove(appId, out _);
        _recordings.TryRemove(appId, out _);
        return Task.CompletedTask;
    }

    public Task<CallRecording> AddRecordingAsync(string engagementId, string recordingId, JsonElement body)
    {
        var recording = new CallRecording
        {
            Id = recordingId,
            EngagementId = engagementId,
            Url = body.TryGetProperty("url", out var url) ? url.GetString() : null,
            Duration = body.TryGetProperty("duration", out var duration) ? duration.GetInt32() : 0,
            Transcript = body.TryGetProperty("transcript", out var transcript) ? transcript.GetString() : null,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var recordings = _recordings.GetOrAdd(engagementId, _ => []);
        recordings.Add(recording);

        return Task.FromResult(recording);
    }

    public Task<List<CallRecording>> GetRecordingsAsync(string engagementId)
    {
        var recordings = _recordings.GetValueOrDefault(engagementId) ?? [];
        return Task.FromResult(recordings);
    }

    private static JsonElement CreateSettingsObject(string appId, JsonElement body)
    {
        var settings = new Dictionary<string, object?>
        {
            ["id"] = appId,
            ["name"] = body.TryGetProperty("name", out var name) ? name.GetString() : "Calling App",
            ["url"] = body.TryGetProperty("url", out var url) ? url.GetString() : "",
            ["height"] = body.TryGetProperty("height", out var height) ? height.GetInt32() : 600,
            ["width"] = body.TryGetProperty("width", out var width) ? width.GetInt32() : 400,
            ["isReady"] = body.TryGetProperty("isReady", out var isReady) && isReady.GetBoolean(),
            ["supportsCustomObjects"] = body.TryGetProperty("supportsCustomObjects", out var supports) && supports.GetBoolean(),
            ["createdAt"] = DateTimeOffset.UtcNow,
            ["updatedAt"] = DateTimeOffset.UtcNow
        };

        return JsonSerializer.SerializeToElement(settings);
    }

    private static JsonElement MergeSettings(JsonElement existing, JsonElement updates, string appId)
    {
        var settings = JsonSerializer.Deserialize<Dictionary<string, object?>>(existing.GetRawText())
            ?? new Dictionary<string, object?>();

        if (updates.TryGetProperty("name", out var name))
        {
            settings["name"] = name.GetString();
        }

        if (updates.TryGetProperty("url", out var url))
        {
            settings["url"] = url.GetString();
        }

        if (updates.TryGetProperty("height", out var height))
        {
            settings["height"] = height.GetInt32();
        }

        if (updates.TryGetProperty("width", out var width))
        {
            settings["width"] = width.GetInt32();
        }

        if (updates.TryGetProperty("isReady", out var isReady))
        {
            settings["isReady"] = isReady.GetBoolean();
        }

        if (updates.TryGetProperty("supportsCustomObjects", out var supports))
        {
            settings["supportsCustomObjects"] = supports.GetBoolean();
        }

        settings["updatedAt"] = DateTimeOffset.UtcNow;

        return JsonSerializer.SerializeToElement(settings);
    }

    private static JsonElement SetPropertyValue(JsonElement element, string propertyName, object value)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(element.GetRawText())
            ?? new Dictionary<string, object?>();
        dict[propertyName] = value;
        dict["updatedAt"] = DateTimeOffset.UtcNow;
        return JsonSerializer.SerializeToElement(dict);
    }
}

public class CallRecording
{
    public string Id { get; set; } = string.Empty;
    public string EngagementId { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int Duration { get; set; }
    public string? Transcript { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
