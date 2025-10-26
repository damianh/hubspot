namespace DamianH.HubSpot.MockServer.Repositories.MediaBridge;

internal class MediaBridgeRepository
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

    public MediaAsset? GetById(string id) => _assets.GetValueOrDefault(id);

    public List<MediaAsset> GetAll(int offset = 0, int limit = 100) => _assets.Values
            .OrderByDescending(a => a.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToList();

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

    public bool Delete(string id) => _assets.Remove(id);

    public int Count() => _assets.Count;

    public void Clear()
    {
        _assets.Clear();
        _nextId = 1;
    }
}
