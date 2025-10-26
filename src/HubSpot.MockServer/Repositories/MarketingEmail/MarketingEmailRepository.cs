namespace DamianH.HubSpot.MockServer.Repositories.MarketingEmail;

internal class MarketingEmailRepository
{
    private readonly Dictionary<string, Objects.MarketingEmail> _emails = new();
    private int _nextId = 1;

    public Objects.MarketingEmail Create(Objects.MarketingEmail email)
    {
        email.Id = _nextId++.ToString();
        email.CreatedAt = DateTime.UtcNow;
        email.UpdatedAt = DateTime.UtcNow;
        _emails[email.Id] = email;
        return email;
    }

    public Objects.MarketingEmail? GetById(string id)
    {
        _emails.TryGetValue(id, out var email);
        return email;
    }

    public Objects.MarketingEmail Update(string id, Objects.MarketingEmail email)
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

    public void Delete(string id) => _emails.Remove(id);

    public IEnumerable<Objects.MarketingEmail> GetAll() => _emails.Values;

    public void Clear()
    {
        _emails.Clear();
        _nextId = 1;
    }
}
