using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class CampaignRepository
{
    private readonly Dictionary<string, Campaign> _campaigns = new();
    private int _nextId = 1;

    public Campaign Create(Campaign campaign)
    {
        campaign.Id = _nextId++.ToString();
        campaign.CreatedAt = DateTime.UtcNow;
        campaign.UpdatedAt = DateTime.UtcNow;
        _campaigns[campaign.Id] = campaign;
        return campaign;
    }

    public Campaign? GetById(string id)
    {
        _campaigns.TryGetValue(id, out var campaign);
        return campaign;
    }

    public Campaign Update(string id, Campaign campaign)
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

    public IEnumerable<Campaign> GetAll() => _campaigns.Values;

    public void Clear()
    {
        _campaigns.Clear();
        _nextId = 1;
    }
}
