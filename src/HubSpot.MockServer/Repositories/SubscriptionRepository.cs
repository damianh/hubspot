using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

public class SubscriptionRepository
{
    private readonly Dictionary<string, Subscription> _subscriptions = new();
    private readonly Dictionary<string, SubscriptionDefinition> _definitions = new();
    private int _nextSubscriptionId = 1;
    private int _nextDefinitionId = 1;

    public Subscription CreateSubscription(Subscription subscription)
    {
        subscription.Id = _nextSubscriptionId++.ToString();
        subscription.CreatedAt = DateTime.UtcNow;
        subscription.UpdatedAt = DateTime.UtcNow;
        _subscriptions[subscription.Id] = subscription;
        return subscription;
    }

    public Subscription? GetSubscriptionById(string id)
    {
        _subscriptions.TryGetValue(id, out var subscription);
        return subscription;
    }

    public Subscription UpdateSubscription(string id, Subscription subscription)
    {
        if (!_subscriptions.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Subscription {id} not found");
        }

        subscription.Id = id;
        subscription.UpdatedAt = DateTime.UtcNow;
        _subscriptions[id] = subscription;
        return subscription;
    }

    public void DeleteSubscription(string id) => _subscriptions.Remove(id);

    public IEnumerable<Subscription> GetAllSubscriptions() => _subscriptions.Values;

    public SubscriptionDefinition CreateDefinition(SubscriptionDefinition definition)
    {
        definition.Id = _nextDefinitionId++.ToString();
        definition.CreatedAt = DateTime.UtcNow;
        definition.UpdatedAt = DateTime.UtcNow;
        _definitions[definition.Id] = definition;
        return definition;
    }

    public SubscriptionDefinition? GetDefinitionById(string id)
    {
        _definitions.TryGetValue(id, out var definition);
        return definition;
    }

    public IEnumerable<SubscriptionDefinition> GetAllDefinitions() => _definitions.Values;

    public void Clear()
    {
        _subscriptions.Clear();
        _definitions.Clear();
        _nextSubscriptionId = 1;
        _nextDefinitionId = 1;
    }
}
