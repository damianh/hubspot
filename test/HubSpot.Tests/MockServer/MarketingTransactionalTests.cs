using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3;
using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class MarketingTransactionalTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotMarketingTransactionalSingleSendV3Client _client = null!;

    public ValueTask InitializeAsync() => new(InitializeAsyncCore());

    private async Task InitializeAsyncCore()
    {
        _server = await HubSpotMockServer.StartAsync();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotMarketingTransactionalSingleSendV3Client(adapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task CanSendTransactionalEmail()
    {
        // Arrange
        var request = new PublicSingleSendRequestEgg
        {
            EmailId = 123456,
            Message = new PublicSingleSendEmail
            {
                To = "test@example.com"
            }
        };

        // Act
        var response = await _client.Marketing.V3.Transactional.SingleEmail.Send.PostAsync(request);

        // Assert
        response.ShouldNotBeNull();
        response.StatusId.ShouldNotBeNull();
        response.Status.ShouldBe(EmailSendStatusView_status.COMPLETE);
        response.SendResult.ShouldBe(EmailSendStatusView_sendResult.SENT);
        response.RequestedAt.ShouldNotBeNull();
        response.StartedAt.ShouldNotBeNull();
        response.CompletedAt.ShouldNotBeNull();
        response.EventId.ShouldNotBeNull();
    }

    [Fact]
    public async Task CanCreateSmtpToken()
    {
        // Arrange
        var request = new SmtpApiTokenRequestEgg
        {
            CampaignName = "Test Campaign",
            CreateContact = true
        };

        // Act
        var response = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(request);

        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldNotBeNull();
        response.CampaignName.ShouldBe("Test Campaign");
        response.CreateContact.ShouldBe(true);
        response.EmailCampaignId.ShouldNotBeNull();
        response.Password.ShouldNotBeNull();
        response.CreatedAt.ShouldNotBeNull();
        response.CreatedBy.ShouldNotBeNull();
    }

    [Fact]
    public async Task CanListSmtpTokens()
    {
        // Arrange
        var token1 = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Campaign 1"
        });
        var token2 = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Campaign 2"
        });

        // Act
        var response = await _client.Marketing.V3.Transactional.SmtpTokens.GetAsync();

        // Assert
        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(2);
        response.Results.ShouldContain(t => t.Id == token1!.Id);
        response.Results.ShouldContain(t => t.Id == token2!.Id);
        
        // Password should not be included in list results
        response.Results.ShouldAllBe(t => t.Password == null);
    }

    [Fact]
    public async Task CanListSmtpTokensWithFilter()
    {
        // Arrange
        var token1 = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Filtered Campaign"
        });
        await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Other Campaign"
        });

        // Act
        var response = await _client.Marketing.V3.Transactional.SmtpTokens.GetAsync(config =>
        {
            config.QueryParameters.CampaignName = "Filtered Campaign";
        });

        // Assert
        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.ShouldAllBe(t => t.CampaignName == "Filtered Campaign");
    }

    [Fact]
    public async Task CanGetSmtpTokenById()
    {
        // Arrange
        var created = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Get Test"
        });

        // Act
        var response = await _client.Marketing.V3.Transactional.SmtpTokens[created!.Id!].GetAsync();

        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe(created.Id);
        response.CampaignName.ShouldBe("Get Test");
        
        // Password should not be included when getting by ID
        response.Password.ShouldBeNull();
    }

    [Fact]
    public async Task CanDeleteSmtpToken()
    {
        // Arrange
        var created = await _client.Marketing.V3.Transactional.SmtpTokens.PostAsync(new SmtpApiTokenRequestEgg
        {
            CampaignName = "Delete Test"
        });

        // Act
        await _client.Marketing.V3.Transactional.SmtpTokens[created!.Id!].DeleteAsync();

        // Assert - trying to get the deleted token should throw 404
        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(
            async () => await _client.Marketing.V3.Transactional.SmtpTokens[created.Id!].GetAsync());
    }
}

