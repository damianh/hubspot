using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class TimelineRepository
{
    private readonly ConcurrentDictionary<string, TimelineEventTemplate> _templates = new();
    private readonly ConcurrentDictionary<string, TimelineEvent> _events = new();
    private readonly ConcurrentDictionary<string, List<string>> _objectEvents = new();

    public TimelineEventTemplate CreateEventTemplate(
        string name,
        string objectType,
        List<EventToken> tokens,
        string headerTemplate,
        string? detailTemplate = null)
    {
        var templateId = GenerateTemplateId();
        var template = new TimelineEventTemplate
        {
            Id = templateId,
            Name = name,
            ObjectType = objectType,
            Tokens = tokens,
            HeaderTemplate = headerTemplate,
            DetailTemplate = detailTemplate ?? "<div>{{tokens}}</div>",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _templates[templateId] = template;

        return template;
    }

    public TimelineEventTemplate? GetEventTemplate(string templateId) => _templates.GetValueOrDefault(templateId);

    public List<TimelineEventTemplate> ListEventTemplates() => _templates.Values.OrderBy(t => t.Name).ToList();

    public TimelineEventTemplate? UpdateEventTemplate(string templateId,
        string? name = null,
        List<EventToken>? tokens = null,
        string? headerTemplate = null,
        string? detailTemplate = null)
    {
        if (!_templates.TryGetValue(templateId, out var template))
        {
            return null;
        }

        if (name != null)
        {
            template.Name = name;
        }

        if (tokens != null)
        {
            template.Tokens = tokens;
        }

        if (headerTemplate != null)
        {
            template.HeaderTemplate = headerTemplate;
        }

        if (detailTemplate != null)
        {
            template.DetailTemplate = detailTemplate;
        }

        template.UpdatedAt = DateTimeOffset.UtcNow;

        return template;
    }

    public bool DeleteEventTemplate(string templateId) => _templates.TryRemove(templateId, out _);

    public TimelineEvent CreateEvent(
        string eventTemplateId,
        string objectType,
        string objectId,
        Dictionary<string, string> tokens,
        DateTimeOffset? timestamp = null)
    {
        var eventId = GenerateEventId();
        var timelineEvent = new TimelineEvent
        {
            Id = eventId,
            EventTemplateId = eventTemplateId,
            ObjectType = objectType,
            ObjectId = objectId,
            Tokens = tokens,
            Timestamp = timestamp ?? DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _events[eventId] = timelineEvent;

        // Track event by object
        var key = $"{objectType}:{objectId}";
        if (!_objectEvents.ContainsKey(key))
        {
            _objectEvents[key] = [];
        }

        _objectEvents[key].Add(eventId);

        return timelineEvent;
    }

    public TimelineEvent? GetEvent(string eventId) => _events.GetValueOrDefault(eventId);

    public List<TimelineEvent> ListEvents(string objectType, string objectId)
    {
        var key = $"{objectType}:{objectId}";
        if (!_objectEvents.TryGetValue(key, out var eventIds))
        {
            return [];
        }

        return eventIds
            .Select(id => _events.GetValueOrDefault(id))
            .Where(e => e != null)
            .Cast<TimelineEvent>()
            .OrderByDescending(e => e.Timestamp)
            .ToList();
    }

    public bool DeleteEvent(string eventId)
    {
        if (!_events.TryRemove(eventId, out var evt))
        {
            return false;
        }

        // Remove from object events
        var key = $"{evt.ObjectType}:{evt.ObjectId}";
        if (_objectEvents.TryGetValue(key, out var eventIds))
        {
            eventIds.Remove(eventId);
        }

        return true;
    }

    private static int _templateIdCounter;
    private static string GenerateTemplateId() => $"template-{Interlocked.Increment(ref _templateIdCounter)}";

    private static int _eventIdCounter;
    private static string GenerateEventId() => $"event-{Interlocked.Increment(ref _eventIdCounter)}";
}

public class TimelineEventTemplate
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required string ObjectType { get; init; }
    public required List<EventToken> Tokens { get; set; }
    public required string HeaderTemplate { get; set; }
    public required string DetailTemplate { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class EventToken
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public required string Type { get; init; } // string, number, date, enumeration
    public List<string>? Options { get; init; } // For enumeration type
}

public class TimelineEvent
{
    public required string Id { get; init; }
    public required string EventTemplateId { get; init; }
    public required string ObjectType { get; init; }
    public required string ObjectId { get; init; }
    public required Dictionary<string, string> Tokens { get; init; }
    public DateTimeOffset Timestamp { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
