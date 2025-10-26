using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories.Currency;

internal class CurrencyRepository
{
    private readonly TimeProvider _timeProvider;
    private CompanyCurrency _companyCurrency;
    private readonly Dictionary<string, Currency> _currencies = new();
    private readonly Dictionary<string, ExchangeRate> _exchangeRates = new();
    private int _exchangeRateIdCounter = 1;

    public CurrencyRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;

        _companyCurrency = new CompanyCurrency
        {
            CurrencyCode = "USD",
            UpdatedAt = _timeProvider.GetUtcNow()
        };

        InitializeDefaultCurrencies();
        InitializeDefaultExchangeRates();
    }

    private void InitializeDefaultCurrencies()
    {
        var currencies = new[]
        {
            new Currency { CurrencyCode = "USD", DisplayName = "US Dollar", Symbol = "$", IsVisible = true, CreatedAt = _timeProvider.GetUtcNow(), UpdatedAt = _timeProvider.GetUtcNow() },
            new Currency { CurrencyCode = "EUR", DisplayName = "Euro", Symbol = "€", IsVisible = true, CreatedAt = _timeProvider.GetUtcNow(), UpdatedAt = _timeProvider.GetUtcNow() },
            new Currency { CurrencyCode = "GBP", DisplayName = "British Pound", Symbol = "£", IsVisible = true, CreatedAt = _timeProvider.GetUtcNow(), UpdatedAt = _timeProvider.GetUtcNow() },
            new Currency { CurrencyCode = "JPY", DisplayName = "Japanese Yen", Symbol = "¥", IsVisible = false, CreatedAt = _timeProvider.GetUtcNow(), UpdatedAt = _timeProvider.GetUtcNow() },
            new Currency { CurrencyCode = "CAD", DisplayName = "Canadian Dollar", Symbol = "C$", IsVisible = true, CreatedAt = _timeProvider.GetUtcNow(), UpdatedAt = _timeProvider.GetUtcNow() },
        };

        foreach (var currency in currencies)
        {
            _currencies[currency.CurrencyCode] = currency;
        }
    }

    private void InitializeDefaultExchangeRates()
    {
        AddExchangeRate("USD", "EUR", 0.85m);
        AddExchangeRate("USD", "GBP", 0.73m);
        AddExchangeRate("USD", "JPY", 110.0m);
        AddExchangeRate("USD", "CAD", 1.25m);
    }

    public CompanyCurrency GetCompanyCurrency() => _companyCurrency;

    public void UpdateCompanyCurrency(string currencyCode) => _companyCurrency = new CompanyCurrency
    {
        CurrencyCode = currencyCode,
        UpdatedAt = _timeProvider.GetUtcNow()
    };

    public List<Currency> GetSupportedCurrencyCodes() => _currencies.Values.ToList();

    public void AddCurrency(Currency currency)
    {
        currency.CreatedAt = _timeProvider.GetUtcNow();
        currency.UpdatedAt = _timeProvider.GetUtcNow();
        _currencies[currency.CurrencyCode] = currency;
    }

    public void UpdateCurrencyVisibility(string currencyCode, bool isVisible)
    {
        if (_currencies.TryGetValue(currencyCode, out var currency))
        {
            currency.IsVisible = isVisible;
            currency.UpdatedAt = _timeProvider.GetUtcNow();
        }
    }

    public List<ExchangeRate> GetExchangeRates(int? limit = null, string? after = null)
    {
        var query = _exchangeRates.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(after))
        {
            query = query.Where(er => string.Compare(er.Id, after) > 0);
        }

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        return query.OrderBy(er => er.Id).ToList();
    }

    public ExchangeRate? GetExchangeRate(string id) => _exchangeRates.GetValueOrDefault(id);

    public List<ExchangeRate> GetExchangeRatesByIds(List<string> ids) => ids
            .Select(id => _exchangeRates.GetValueOrDefault(id))
            .Where(er => er != null)
            .Cast<ExchangeRate>()
            .ToList();

    public ExchangeRate CreateExchangeRate(string fromCurrencyCode, string toCurrencyCode, decimal rate) => AddExchangeRate(fromCurrencyCode, toCurrencyCode, rate);

    private ExchangeRate AddExchangeRate(string fromCurrencyCode, string toCurrencyCode, decimal rate)
    {
        var exchangeRate = new ExchangeRate
        {
            Id = _exchangeRateIdCounter++.ToString(),
            FromCurrencyCode = fromCurrencyCode,
            ToCurrencyCode = toCurrencyCode,
            ExchangeRateValue = rate,
            EffectiveDate = _timeProvider.GetUtcNow(),
            CreatedAt = _timeProvider.GetUtcNow(),
            UpdatedAt = _timeProvider.GetUtcNow()
        };

        _exchangeRates[exchangeRate.Id] = exchangeRate;
        return exchangeRate;
    }

    public void UpdateExchangeRate(string id, decimal newRate)
    {
        if (_exchangeRates.TryGetValue(id, out var exchangeRate))
        {
            exchangeRate.ExchangeRateValue = newRate;
            exchangeRate.UpdatedAt = _timeProvider.GetUtcNow();
        }
    }

    public void DeleteExchangeRate(string id) => _exchangeRates.Remove(id);

    public List<ExchangeRate> GetCurrentExchangeRates() => _exchangeRates.Values
            .OrderByDescending(er => er.EffectiveDate)
            .ToList();
}
