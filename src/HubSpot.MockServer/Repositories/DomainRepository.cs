namespace DamianH.HubSpot.MockServer.Repositories;

public class DomainRepository
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

    public Domain? GetById(string id)
    {
        return _domains.GetValueOrDefault(id);
    }

    public List<Domain> GetAll(int offset = 0, int limit = 100)
    {
        return _domains.Values
            .OrderBy(d => d.Domain1)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

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

    public bool Delete(string id)
    {
        return _domains.Remove(id);
    }

    public int Count()
    {
        return _domains.Count;
    }

    public void Clear()
    {
        _domains.Clear();
        _nextId = 1;
    }
}

public class Domain
{
    public string? Id { get; set; }
    public string? Domain1 { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsResolving { get; set; }
    public bool IsLegacyDomain { get; set; }
    public bool IsUsedForBlogPost { get; set; }
    public bool IsUsedForSitePage { get; set; }
    public bool IsUsedForLandingPage { get; set; }
    public bool IsUsedForEmail { get; set; }
    public bool IsSetupComplete { get; set; }
    public string? CorrectCname { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
