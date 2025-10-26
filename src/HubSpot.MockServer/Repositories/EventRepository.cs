using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class EventRepository
{
    private readonly ConcurrentDictionary<string, CustomEvent> _events = new();
    private readonly ConcurrentDictionary<string, EventDefinition> _definitions = new();
    private long _nextEventId = 1;
    private long _nextDefinitionId = 1;

    public void SendEvent(CustomEvent customEvent)
    {
        customEvent.Id = _nextEventId++.ToString();
        customEvent.OccurredAt = customEvent.OccurredAt == default
            ? DateTime.UtcNow
            : customEvent.OccurredAt;
        _events[customEvent.Id] = customEvent;
    }

    public IEnumerable<CustomEvent> GetEvents(string? eventName = null, DateTime? after = null)
    {
        var events = _events.Values.AsEnumerable();

        if (eventName != null)
        {
            events = events.Where(e => e.EventName == eventName);
        }

        if (after.HasValue)
        {
            events = events.Where(e => e.OccurredAt >= after.Value);
        }

        return events.OrderByDescending(e => e.OccurredAt).ToList();
    }

    public List<CustomEvent> GetAllEvents() => _events.Values.OrderByDescending(e => e.OccurredAt).ToList();

    public EventDefinition CreateDefinition(EventDefinition definition)
    {
        definition.Id = _nextDefinitionId++.ToString();
        definition.CreatedAt = DateTime.UtcNow;
        _definitions[definition.Id] = definition;
        return definition;
    }

    public EventDefinition? GetDefinition(string definitionId) => _definitions.GetValueOrDefault(definitionId);

    public IEnumerable<EventDefinition> GetAllDefinitions() => _definitions.Values.ToList();

    public bool DeleteDefinition(string definitionId) => _definitions.TryRemove(definitionId, out _);
}

public class CustomEvent
{
    public string? Id { get; set; }
    public string EventName { get; set; } = null!;
    public string? Email { get; set; }
    public string? ObjectId { get; set; }
    public string? ObjectType { get; set; }
    public DateTime OccurredAt { get; set; }
    public Dictionary<string, object>? Properties { get; set; }
}

public class EventDefinition
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<EventPropertyDefinition>? PropertyDefinitions { get; set; }
}

public class EventPropertyDefinition
{
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public string Type { get; set; } = "string";
}
