using DamianH.HubSpot.KiotaClient.CRM.Calls.V3;
using DamianH.HubSpot.KiotaClient.CRM.Calls.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCallsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCallsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCallsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_call()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_call_title", "Sales Call" },
                    { "hs_call_body", "Discussed pricing options" },
                    { "hs_call_duration", "1800000" },
                    { "hs_call_status", "COMPLETED" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Calls.PostAsync(input);
        var call = created!.Entity!;

        call.ShouldNotBeNull();
        call.Id.ShouldNotBeNullOrEmpty();
        call.Properties.ShouldNotBeNull();
        call.Properties.AdditionalData.ShouldContainKeyAndValue("hs_call_title", "Sales Call");
        call.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_call_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_call_title", "Support Call" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Calls.PostAsync(input);
        var callId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Calls[callId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(callId);
    }

    [Fact]
    public async Task Can_update_call()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_call_title", "Initial Call" },
                    { "hs_call_status", "SCHEDULED" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Calls.PostAsync(input);
        var callId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_call_title", "Completed Call" },
                    { "hs_call_status", "COMPLETED" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Calls[callId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_call_title", "Completed Call");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("hs_call_status", "COMPLETED");
    }

    [Fact]
    public async Task Can_list_calls()
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
                        { "hs_call_title", $"Call {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Calls.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Calls.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_calls()
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
                            { "hs_call_title", "Batch Call 1" }
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
                            { "hs_call_title", "Batch Call 2" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Calls.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
