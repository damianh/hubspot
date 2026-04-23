using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4;
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Item;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Shouldly;

namespace DamianH.HubSpot.MockServer;

public sealed class CommunicationPreferencesV4KiotaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCommunicationPreferencesSubscriptionsV4Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCommunicationPreferencesSubscriptionsV4Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_list_v4_definitions()
    {
        var result = await _client.CommunicationPreferences.V4.Definitions.GetAsync();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_v4_status_for_subscriber()
    {
        var result = await _client.CommunicationPreferences.V4.Statuses["test@example.com"].GetAsync(rc =>
        {
            rc.QueryParameters.ChannelAsGetChannelQueryParameterType = GetChannelQueryParameterType.EMAIL;
        });
        result.ShouldNotBeNull();
    }
}
