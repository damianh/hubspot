using DamianH.HubSpot.KiotaClient.Meta.Origins.V1;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Shouldly;

namespace DamianH.HubSpot.MockServer;

public sealed class MetaOriginsV1KiotaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotMetaOriginsV1Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotMetaOriginsV1Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_list_ip_ranges()
    {
        var result = await _client.Meta.NetworkOrigins.TwoZeroTwoFiveZeroNine.IpRanges.GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_list_ip_ranges_simple()
    {
        var result = await _client.Meta.NetworkOrigins.TwoZeroTwoFiveZeroNine.IpRanges.Simple.GetAsync();

        result.ShouldNotBeNull();
    }
}
