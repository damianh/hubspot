using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

public class MarketingEmailRepository
{
    private readonly Dictionary<string, MarketingEmail> _emails = new();
    private int _nextId = 1;

    public MarketingEmail Create(MarketingEmail email)
    {
        email.Id = _nextId++.ToString();
        email.CreatedAt = DateTime.UtcNow;
        email.UpdatedAt = DateTime.UtcNow;
        _emails[email.Id] = email;
        return email;
    }

    public MarketingEmail? GetById(string id)
    {
        _emails.TryGetValue(id, out var email);
        return email;
    }

    public MarketingEmail Update(string id, MarketingEmail email)
    {
        if (!_emails.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Marketing email {id} not found");
        }

        email.Id = id;
        email.UpdatedAt = DateTime.UtcNow;
        _emails[id] = email;
        return email;
    }

    public void Delete(string id)
    {
        _emails.Remove(id);
    }

    public IEnumerable<MarketingEmail> GetAll()
    {
        return _emails.Values;
    }

    public void Clear()
    {
        _emails.Clear();
        _nextId = 1;
    }
}
