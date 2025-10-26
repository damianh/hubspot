namespace DamianH.HubSpot.MockServer.Repositories.MarketingEvent;

internal class MarketingEventRepository
{
    private readonly Dictionary<string, Objects.MarketingEvent> _events = new();
    private int _nextId = 1;

    public Objects.MarketingEvent Create(Objects.MarketingEvent marketingEvent)
    {
        marketingEvent.Id = _nextId++.ToString();
        marketingEvent.CreatedAt = DateTime.UtcNow;
        marketingEvent.UpdatedAt = DateTime.UtcNow;
        _events[marketingEvent.Id] = marketingEvent;
        return marketingEvent;
    }

    public Objects.MarketingEvent? GetById(string id)
    {
        _events.TryGetValue(id, out var marketingEvent);
        return marketingEvent;
    }

    public Objects.MarketingEvent Update(string id, Objects.MarketingEvent marketingEvent)
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

    public IEnumerable<Objects.MarketingEvent> GetAll() => _events.Values;

    public void Clear()
    {
        _events.Clear();
        _nextId = 1;
    }
}
