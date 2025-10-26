namespace DamianH.HubSpot.MockServer.Repositories.UrlRedirect;

internal class UrlRedirectRepository
{
    private readonly Dictionary<string, UrlRedirect> _redirects = new();
    private int _nextId = 1;

    public UrlRedirect Create(UrlRedirect redirect)
    {
        redirect.Id = _nextId++.ToString();
        redirect.Created = DateTime.UtcNow;
        redirect.Updated = DateTime.UtcNow;
        _redirects[redirect.Id] = redirect;
        return redirect;
    }

    public UrlRedirect? GetById(string id) => _redirects.GetValueOrDefault(id);

    public List<UrlRedirect> GetAll(int offset = 0, int limit = 100) => _redirects.Values
            .OrderBy(r => r.RoutePrefix)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public UrlRedirect? Update(string id, UrlRedirect updatedRedirect)
    {
        if (!_redirects.ContainsKey(id))
        {
            return null;
        }

        updatedRedirect.Id = id;
        updatedRedirect.Updated = DateTime.UtcNow;
        _redirects[id] = updatedRedirect;
        return updatedRedirect;
    }

    public bool Delete(string id) => _redirects.Remove(id);

    public int Count() => _redirects.Count;

    public void Clear()
    {
        _redirects.Clear();
        _nextId = 1;
    }
}

public class UrlRedirect
{
    public string? Id { get; set; }
    public string? RoutePrefix { get; set; }
    public string? Destination { get; set; }
    public int RedirectStyle { get; set; } // 301 or 302
    public bool IsOnlyAfterNotFound { get; set; }
    public bool IsMatchFullUrl { get; set; }
    public bool IsMatchQueryString { get; set; }
    public bool IsPattern { get; set; }
    public int Precedence { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
