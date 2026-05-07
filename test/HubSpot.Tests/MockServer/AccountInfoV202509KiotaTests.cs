using DamianH.HubSpot.KiotaClient.Account.AccountInfo.V202509;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class AccountInfoV202509KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotAccountAccountInfoV202509Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotAccountAccountInfoV202509Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_get_account_details()
    {
        var result = await _client.AccountInfo.TwoZeroTwoFiveZeroNine.Details.GetAsync();

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_private_apps_daily_api_usage()
    {
        var result = await _client.AccountInfo.TwoZeroTwoFiveZeroNine.ApiUsage.Daily.PrivateApps.GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }
}
