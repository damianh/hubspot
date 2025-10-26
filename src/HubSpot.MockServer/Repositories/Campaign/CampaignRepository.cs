namespace DamianH.HubSpot.MockServer.Repositories.Campaign;

internal class CampaignRepository
{
    private readonly Dictionary<string, Objects.Campaign> _campaigns = new();
    private int _nextId = 1;

    public Objects.Campaign Create(Objects.Campaign campaign)
    {
        campaign.Id = _nextId++.ToString();
        campaign.CreatedAt = DateTime.UtcNow;
        campaign.UpdatedAt = DateTime.UtcNow;
        _campaigns[campaign.Id] = campaign;
        return campaign;
    }

    public Objects.Campaign? GetById(string id)
    {
        _campaigns.TryGetValue(id, out var campaign);
        return campaign;
    }

    public Objects.Campaign Update(string id, Objects.Campaign campaign)
    {
        if (!_campaigns.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Campaign {id} not found");
        }

        campaign.Id = id;
        campaign.UpdatedAt = DateTime.UtcNow;
        _campaigns[id] = campaign;
        return campaign;
    }

    public void Delete(string id) => _campaigns.Remove(id);

    public IEnumerable<Objects.Campaign> GetAll() => _campaigns.Values;

    public void Clear()
    {
        _campaigns.Clear();
        _nextId = 1;
    }
}
