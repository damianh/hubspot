namespace DamianH.HubSpot.MockServer.Repositories.Domain;

internal class DomainRepository
{
    private readonly Dictionary<string, Domain> _domains = new();
    private int _nextId = 1;

    public Domain Create(Domain domain)
    {
        domain.Id = _nextId++.ToString();
        domain.Created = DateTime.UtcNow;
        domain.Updated = DateTime.UtcNow;
        _domains[domain.Id] = domain;
        return domain;
    }

    public Domain? GetById(string id) => _domains.GetValueOrDefault(id);

    public List<Domain> GetAll(int offset = 0, int limit = 100) => _domains.Values
            .OrderBy(d => d.Domain1)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public Domain? Update(string id, Domain updatedDomain)
    {
        if (!_domains.ContainsKey(id))
        {
            return null;
        }

        updatedDomain.Id = id;
        updatedDomain.Updated = DateTime.UtcNow;
        _domains[id] = updatedDomain;
        return updatedDomain;
    }

    public bool Delete(string id) => _domains.Remove(id);

    public int Count() => _domains.Count;

    public void Clear()
    {
        _domains.Clear();
        _nextId = 1;
    }
}
