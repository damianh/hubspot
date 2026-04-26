using DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V202603;
using DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V202603.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Shouldly;

namespace DamianH.HubSpot.MockServer;

public sealed class MarketingSingleSendV202603KiotaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotMarketingSinglesendV202603Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotMarketingSinglesendV202603Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_send_single_email_v202603()
    {
        var request = new PublicSingleSendRequestEgg
        {
            EmailId = 12345L,
            Message = new PublicSingleSendEmail
            {
                To = "test@example.com"
            }
        };

        var result = await _client.Marketing.EmailCampaigns.TwoZeroTwoSixZeroThree.SingleSend.PostAsync(request);

        result.ShouldNotBeNull();
        result.StatusId.ShouldNotBeNullOrEmpty();
        result.Status.ShouldBe(EmailSendStatusView_status.PENDING);
    }
}
