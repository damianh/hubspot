using DamianH.HubSpot.KiotaClient.Settings.TaxRates.V1;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class TaxRatesTests : IAsyncLifetime
{
    private HubSpotMockServer? _server;
    private HubSpotSettingsTaxRatesV1Client? _client;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartAsync();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.BaseUri.ToString()
        };
        _client = new HubSpotSettingsTaxRatesV1Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null)
        {
            await _server.DisposeAsync();
        }
    }

    [Fact]
    public async Task GetTaxRateGroups_ReturnsGroupsList()
    {
        var groups = await _client.TaxRates.V1.TaxRates.GetAsync();

        groups.ShouldNotBeNull();
        groups.Results.ShouldNotBeNull();
        groups.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetTaxRateGroups_WithPagination_ReturnsPagedResults()
    {
        var groups = await _client.TaxRates.V1.TaxRates.GetAsync();

        groups.ShouldNotBeNull();
        groups.Results.ShouldNotBeNull();
        groups.Results.Count.ShouldBeGreaterThan(0);
    }
}
