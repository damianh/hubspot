namespace DamianH.HubSpot.MockServer.Repositories;

public class MediaBridgeRepository
{
    private readonly Dictionary<string, MediaAsset> _assets = new();
    private int _nextId = 1;

    public MediaAsset Create(MediaAsset asset)
    {
        asset.Id = _nextId++.ToString();
        asset.CreatedAt = DateTime.UtcNow;
        asset.UpdatedAt = DateTime.UtcNow;
        _assets[asset.Id] = asset;
        return asset;
    }

    public MediaAsset? GetById(string id)
    {
        return _assets.GetValueOrDefault(id);
    }

    public List<MediaAsset> GetAll(int offset = 0, int limit = 100)
    {
        return _assets.Values
            .OrderByDescending(a => a.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public MediaAsset? Update(string id, MediaAsset updatedAsset)
    {
        if (!_assets.ContainsKey(id))
        {
            return null;
        }

        updatedAsset.Id = id;
        updatedAsset.UpdatedAt = DateTime.UtcNow;
        _assets[id] = updatedAsset;
        return updatedAsset;
    }

    public bool Delete(string id)
    {
        return _assets.Remove(id);
    }

    public int Count()
    {
        return _assets.Count;
    }

    public void Clear()
    {
        _assets.Clear();
        _nextId = 1;
    }
}

public class MediaAsset
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Type { get; set; } // "IMAGE", "VIDEO", "DOCUMENT"
    public long? Size { get; set; }
    public string? MimeType { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? ExternalId { get; set; }
    public string? ProviderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
