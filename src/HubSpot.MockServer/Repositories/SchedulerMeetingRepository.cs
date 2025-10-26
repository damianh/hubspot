using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

public class SchedulerMeetingRepository
{
    private readonly ConcurrentDictionary<string, JsonElement> _meetingLinks = new();
    private int _nextId = 1;

    public Task<List<JsonElement>> GetAllLinksAsync()
    {
        var links = _meetingLinks.Values.ToList();
        return Task.FromResult(links);
    }

    public Task<JsonElement?> GetLinkAsync(string linkId)
    {
        return Task.FromResult(
            _meetingLinks.TryGetValue(linkId, out var link) 
                ? (JsonElement?)link 
                : null
        );
    }

    public Task<JsonElement> CreateLinkAsync(JsonElement linkDefinition)
    {
        var id = Interlocked.Increment(ref _nextId).ToString();
        
        var link = JsonSerializer.SerializeToElement(new
        {
            id,
            name = linkDefinition.TryGetProperty("name", out var name) ? name.GetString() : $"Meeting Link {id}",
            slug = linkDefinition.TryGetProperty("slug", out var slug) ? slug.GetString() : $"meeting-{id}",
            durationMinutes = linkDefinition.TryGetProperty("durationMinutes", out var duration) ? duration.GetInt32() : 30,
            availabilityType = linkDefinition.TryGetProperty("availabilityType", out var type) ? type.GetString() : "OWNER",
            ownerId = linkDefinition.TryGetProperty("ownerId", out var owner) ? owner.GetString() : null,
            active = linkDefinition.TryGetProperty("active", out var active) ? active.GetBoolean() : true,
            description = linkDefinition.TryGetProperty("description", out var desc) ? desc.GetString() : "",
            url = $"https://meetings.hubspot.com/meeting-{id}",
            createdAt = DateTimeOffset.UtcNow.ToString("o"),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _meetingLinks[id] = link;
        return Task.FromResult(link);
    }

    public Task<JsonElement?> UpdateLinkAsync(string linkId, JsonElement updates)
    {
        if (!_meetingLinks.TryGetValue(linkId, out var existing))
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var updated = JsonSerializer.SerializeToElement(new
        {
            id = linkId,
            name = updates.TryGetProperty("name", out var name) ? name.GetString() : existing.GetProperty("name").GetString(),
            slug = updates.TryGetProperty("slug", out var slug) ? slug.GetString() : existing.GetProperty("slug").GetString(),
            durationMinutes = updates.TryGetProperty("durationMinutes", out var duration) ? duration.GetInt32() : existing.GetProperty("durationMinutes").GetInt32(),
            availabilityType = updates.TryGetProperty("availabilityType", out var type) ? type.GetString() : existing.GetProperty("availabilityType").GetString(),
            ownerId = updates.TryGetProperty("ownerId", out var owner) ? owner.GetString() : existing.TryGetProperty("ownerId", out var existingOwner) ? existingOwner.GetString() : null,
            active = updates.TryGetProperty("active", out var active) ? active.GetBoolean() : existing.GetProperty("active").GetBoolean(),
            description = updates.TryGetProperty("description", out var desc) ? desc.GetString() : existing.GetProperty("description").GetString(),
            url = existing.GetProperty("url").GetString(),
            createdAt = existing.GetProperty("createdAt").GetString(),
            updatedAt = DateTimeOffset.UtcNow.ToString("o")
        });

        _meetingLinks[linkId] = updated;
        return Task.FromResult<JsonElement?>(updated);
    }

    public Task<bool> DeleteLinkAsync(string linkId)
    {
        return Task.FromResult(_meetingLinks.TryRemove(linkId, out _));
    }

    public Task<JsonElement> GetAvailabilityAsync(string linkId, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        // Return mock availability data
        var availability = JsonSerializer.SerializeToElement(new
        {
            linkId,
            startDate = startDate.ToString("yyyy-MM-dd"),
            endDate = endDate.ToString("yyyy-MM-dd"),
            availableSlots = new[]
            {
                new { startTime = startDate.AddHours(9).ToString("o"), endTime = startDate.AddHours(9.5).ToString("o") },
                new { startTime = startDate.AddHours(10).ToString("o"), endTime = startDate.AddHours(10.5).ToString("o") },
                new { startTime = startDate.AddHours(14).ToString("o"), endTime = startDate.AddHours(14.5).ToString("o") }
            }
        });

        return Task.FromResult(availability);
    }
}
