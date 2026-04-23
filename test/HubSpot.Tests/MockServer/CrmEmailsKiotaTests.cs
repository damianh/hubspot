using DamianH.HubSpot.KiotaClient.CRM.Emails.V3;
using DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmEmailsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMEmailsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMEmailsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_email()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_email_subject", "Test Email" },
                    { "hs_email_text", "Email body content" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Emails.PostAsync(input);
        var email = created!;

        email.ShouldNotBeNull();
        email.Id.ShouldNotBeNullOrEmpty();
        email.Properties.ShouldNotBeNull();
        email.Properties.AdditionalData.ShouldContainKeyAndValue("hs_email_subject", "Test Email");
        email.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_email_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_email_subject", "Get Test" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Emails.PostAsync(input);
        var emailId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Emails[emailId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(emailId);
    }

    [Fact]
    public async Task Can_update_email()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_email_subject", "Original Subject" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Emails.PostAsync(input);
        var emailId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_email_subject", "Updated Subject" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Emails[emailId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_email_subject", "Updated Subject");
    }

    [Fact]
    public async Task Can_list_emails()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                        { "hs_email_subject", $"Email {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Emails.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Emails.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_emails()
    {
        var batchInput = new BatchInputSimplePublicObjectBatchInputForCreate
        {
            Inputs =
            [
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                            { "hs_email_subject", "Batch Email 1" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                            { "hs_email_subject", "Batch Email 2" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Emails.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
