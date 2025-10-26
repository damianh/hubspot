using DamianH.HubSpot.KiotaClient.Account.AccountInfo.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class AccountInfoTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotAccountAccountInfoV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartAsync();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.BaseUri.ToString()
        };
        _client = new HubSpotAccountAccountInfoV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null)
        {
            await _server.DisposeAsync();
        }
    }

    [Fact]
    public async Task GetAccountDetails_ReturnsAccountInformation()
    {
        var details = await _client.AccountInfo.V3.Details.GetAsync();

        details.ShouldNotBeNull();
        details.PortalId.ShouldNotBeNull();
        details.TimeZone.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetDailyApiUsage_ReturnsUsageData()
    {
        var usage = await _client.AccountInfo.V3.ApiUsage.Daily.PrivateApps.GetAsync();

        usage.ShouldNotBeNull();
        usage.Results.ShouldNotBeNull();
        usage.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetPrivateAppsDailyUsage_ReturnsPrivateAppsData()
    {
        var usage = await _client.AccountInfo.V3.ApiUsage.Daily.PrivateApps.GetAsync();

        usage.ShouldNotBeNull();
        usage.Results.ShouldNotBeNull();
        usage.Results.ShouldNotBeEmpty();
    }
}
