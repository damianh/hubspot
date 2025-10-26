using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

public class TaxRateRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<string, TaxRateGroup> _taxRateGroups = new();
    private int _taxRateGroupIdCounter = 1;

    public TaxRateRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        InitializeDefaultData();
    }

    private void InitializeDefaultData()
    {
        CreateTaxRateGroup(new TaxRateGroup
        {
            Id = _taxRateGroupIdCounter++.ToString(),
            Name = "US Sales Tax",
            Country = "US",
            TaxRates = new List<TaxRate>
            {
                new TaxRate { Id = "1", Name = "Standard Rate", Rate = 0.07m, IsDefault = true },
                new TaxRate { Id = "2", Name = "Reduced Rate", Rate = 0.05m, IsDefault = false }
            },
            CreatedAt = _timeProvider.GetUtcNow(),
            UpdatedAt = _timeProvider.GetUtcNow()
        });

        CreateTaxRateGroup(new TaxRateGroup
        {
            Id = _taxRateGroupIdCounter++.ToString(),
            Name = "EU VAT",
            Country = "EU",
            TaxRates = new List<TaxRate>
            {
                new TaxRate { Id = "3", Name = "Standard VAT", Rate = 0.20m, IsDefault = true },
                new TaxRate { Id = "4", Name = "Reduced VAT", Rate = 0.10m, IsDefault = false }
            },
            CreatedAt = _timeProvider.GetUtcNow(),
            UpdatedAt = _timeProvider.GetUtcNow()
        });
    }

    public List<TaxRateGroup> GetTaxRateGroups(int? limit = null, string? after = null)
    {
        var query = _taxRateGroups.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(after))
        {
            query = query.Where(g => string.Compare(g.Id, after) > 0);
        }

        query = query.OrderBy(g => g.Id);

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        return query.ToList();
    }

    public TaxRateGroup? GetTaxRateGroup(string id)
    {
        return _taxRateGroups.GetValueOrDefault(id);
    }

    public TaxRateGroup CreateTaxRateGroup(TaxRateGroup group)
    {
        if (string.IsNullOrEmpty(group.Id))
        {
            group.Id = _taxRateGroupIdCounter++.ToString();
        }
        group.CreatedAt = _timeProvider.GetUtcNow();
        group.UpdatedAt = _timeProvider.GetUtcNow();
        _taxRateGroups[group.Id] = group;
        return group;
    }
}
