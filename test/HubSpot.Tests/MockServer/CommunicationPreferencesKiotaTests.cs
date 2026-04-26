using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3;
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CommunicationPreferencesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCommunicationPreferencesSubscriptionsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCommunicationPreferencesSubscriptionsV3Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_get_subscription_status_for_email()
    {
        var result = await _client.CommunicationPreferences.V3.Status.Email["test@example.com"].GetAsync();

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_subscribe_email()
    {
        var result = await _client.CommunicationPreferences.V3.Subscribe.PostAsync(
            new PublicUpdateSubscriptionStatusRequest
            {
                EmailAddress = "subscribe@example.com",
                SubscriptionId = "sub-1"
            });

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_unsubscribe_email()
    {
        var result = await _client.CommunicationPreferences.V3.Unsubscribe.PostAsync(
            new PublicUpdateSubscriptionStatusRequest
            {
                EmailAddress = "unsubscribe@example.com",
                SubscriptionId = "sub-1"
            });

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_list_definitions()
    {
        var result = await _client.CommunicationPreferences.V3.Definitions.GetAsync();

        result.ShouldNotBeNull();
    }
}
