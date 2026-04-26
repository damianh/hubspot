namespace DamianH.HubSpot.MockServer.Repositories.UrlMapping;

internal sealed class UrlMappingRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<long, UrlMapping> _mappings = new();
    private long _nextId = 1;

    public UrlMappingRepository(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public UrlMapping Create(UrlMapping mapping)
    {
        mapping.Id = _nextId++;
        mapping.Created = _timeProvider.GetUtcNow().ToUnixTimeMilliseconds();
        mapping.Updated = mapping.Created;
        _mappings[mapping.Id] = mapping;
        return mapping;
    }

    public UrlMapping? GetById(long id) => _mappings.GetValueOrDefault(id);

    public List<UrlMapping> GetAll() => _mappings.Values.OrderBy(m => m.RoutePrefix).ToList();

    public bool Delete(long id) => _mappings.Remove(id);
}
