namespace DamianH.HubSpot.MockServer.Repositories.MarketingEmail;

internal class MarketingEmailRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<string, MarketingEmail> _emails = new();
    private int _nextId = 1;



    public MarketingEmailRepository(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public MarketingEmail Create(MarketingEmail email)
    {
        email.Id = _nextId++.ToString();
        email.CreatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        email.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
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
        email.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        _emails[id] = email;
        return email;
    }

    public void Delete(string id) => _emails.Remove(id);

    public IEnumerable<MarketingEmail> GetAll() => _emails.Values;

    public void Clear()
    {
        _emails.Clear();
        _nextId = 1;
    }
}
