using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories.Transcription;

internal class TranscriptionRepository
{
    private readonly ConcurrentDictionary<string, Transcription> _transcriptions = new();

    public Task<Transcription> CreateTranscriptionAsync(string engagementId, JsonElement body)
    {
        var transcription = new Transcription
        {
            Id = Guid.NewGuid().ToString(),
            EngagementId = engagementId,
            Text = body.TryGetProperty("text", out var text) ? text.GetString() : "",
            Confidence = body.TryGetProperty("confidence", out var conf) ? conf.GetDouble() : 1.0,
            Language = body.TryGetProperty("language", out var lang) ? lang.GetString() : "en",
            CreatedAt = DateTimeOffset.UtcNow
        };

        _transcriptions[engagementId] = transcription;
        return Task.FromResult(transcription);
    }

    public Task<Transcription?> GetTranscriptionAsync(string engagementId)
    {
        _transcriptions.TryGetValue(engagementId, out var transcription);
        return Task.FromResult(transcription);
    }

    public Task DeleteTranscriptionAsync(string engagementId)
    {
        _transcriptions.TryRemove(engagementId, out _);
        return Task.CompletedTask;
    }
}

public class Transcription
{
    public string Id { get; set; } = string.Empty;
    public string EngagementId { get; set; } = string.Empty;
    public string? Text { get; set; }
    public double Confidence { get; set; }
    public string? Language { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
