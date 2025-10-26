using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.Event;

internal class EventRepository
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
