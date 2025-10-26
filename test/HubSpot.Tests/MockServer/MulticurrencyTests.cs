using DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class MulticurrencyTests : IAsyncLifetime
{
    private HubSpotMockServer? _server;
    private HubSpotSettingsMulticurrencyV3Client? _client;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartAsync();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.BaseUri.ToString()
        };
        _client = new HubSpotSettingsMulticurrencyV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null)
        {
            await _server.DisposeAsync();
        }
    }

    [Fact]
    public async Task GetCompanyCurrency_ReturnsCurrentCurrency()
    {
        var currency = await _client.Settings.V3.Currencies.CompanyCurrency.GetAsync();

        currency.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetSupportedCurrencyCodes_ReturnsCurrencyList()
    {
        var codes = await _client.Settings.V3.Currencies.Codes.GetAsync();

        codes.ShouldNotBeNull();
        codes.Results.ShouldNotBeNull();
        codes.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetExchangeRates_ReturnsRatesList()
    {
        var rates = await _client.Settings.V3.Currencies.ExchangeRates.GetAsync();

        rates.ShouldNotBeNull();
        rates.Results.ShouldNotBeNull();
        rates.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetCurrentExchangeRates_ReturnsCurrentRates()
    {
        var rates = await _client.Settings.V3.Currencies.ExchangeRates.Current.GetAsync();

        rates.ShouldNotBeNull();
        rates.Results.ShouldNotBeNull();
        rates.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetCentralFxRatesInformation_ReturnsInformation()
    {
        var info = await _client.Settings.V3.Currencies.CentralFxRates.Information.GetAsync();

        info.ShouldNotBeNull();
    }
}
