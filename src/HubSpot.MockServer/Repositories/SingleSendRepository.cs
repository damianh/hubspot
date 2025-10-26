using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

public class SingleSendRepository
{
    private readonly Dictionary<string, SingleSendEmail> _emails = new();
    private int _nextId = 1;

    public SingleSendEmail Create(SingleSendEmail email)
    {
        email.Id = _nextId++.ToString();
        email.CreatedAt = DateTime.UtcNow;
        email.UpdatedAt = DateTime.UtcNow;
        _emails[email.Id] = email;
        return email;
    }

    public SingleSendEmail? GetById(string id)
    {
        _emails.TryGetValue(id, out var email);
        return email;
    }

    public SingleSendEmail Update(string id, SingleSendEmail email)
    {
        if (!_emails.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Single send email {id} not found");
        }

        email.Id = id;
        email.UpdatedAt = DateTime.UtcNow;
        _emails[id] = email;
        return email;
    }

    public void Delete(string id) => _emails.Remove(id);

    public IEnumerable<SingleSendEmail> GetAll() => _emails.Values;

    public void Clear()
    {
        _emails.Clear();
        _nextId = 1;
    }
}
