using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class MarketingEventRepository
{
    private readonly Dictionary<string, MarketingEvent> _events = new();
    private int _nextId = 1;

    public MarketingEvent Create(MarketingEvent marketingEvent)
    {
        marketingEvent.Id = _nextId++.ToString();
        marketingEvent.CreatedAt = DateTime.UtcNow;
        marketingEvent.UpdatedAt = DateTime.UtcNow;
        _events[marketingEvent.Id] = marketingEvent;
        return marketingEvent;
    }

    public MarketingEvent? GetById(string id)
    {
        _events.TryGetValue(id, out var marketingEvent);
        return marketingEvent;
    }

    public MarketingEvent Update(string id, MarketingEvent marketingEvent)
    {
        if (!_events.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Marketing event {id} not found");
        }

        marketingEvent.Id = id;
        marketingEvent.UpdatedAt = DateTime.UtcNow;
        _events[id] = marketingEvent;
        return marketingEvent;
    }

    public void Delete(string id) => _events.Remove(id);

    public IEnumerable<MarketingEvent> GetAll() => _events.Values;

    public void Clear()
    {
        _events.Clear();
        _nextId = 1;
    }
}
