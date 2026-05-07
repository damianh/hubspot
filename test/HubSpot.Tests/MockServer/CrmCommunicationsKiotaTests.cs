using DamianH.HubSpot.KiotaClient.CRM.Communications.V3;
using DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCommunicationsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCommunicationsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCommunicationsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_communication()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_communication_channel_type", "EMAIL" },
                    { "hs_communication_body", "Customer inquiry about product features" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Communications.PostAsync(input);
        var communication = created!;

        communication.ShouldNotBeNull();
        communication.Id.ShouldNotBeNullOrEmpty();
        communication.Properties.ShouldNotBeNull();
        communication.Properties.AdditionalData.ShouldContainKeyAndValue("hs_communication_channel_type", "EMAIL");
        communication.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_communication_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_communication_channel_type", "PHONE" },
                    { "hs_communication_body", "Follow-up call" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Communications.PostAsync(input);
        var communicationId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Communications[communicationId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(communicationId);
    }

    [Fact]
    public async Task Can_update_communication()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_communication_channel_type", "EMAIL" },
                    { "hs_communication_body", "Initial message" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Communications.PostAsync(input);
        var communicationId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_communication_body", "Updated message with additional details" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Communications[communicationId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_communication_body", "Updated message with additional details");
    }

    [Fact]
    public async Task Can_delete_communication()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_communication_channel_type", "SMS" },
                    { "hs_communication_body", "Communication to delete" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Communications.PostAsync(input);
        var communicationId = created!.Id!;

        await _client.Crm.V3.Objects.Communications[communicationId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Communications[communicationId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_communications()
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
                        { "hs_communication_channel_type", "EMAIL" },
                        { "hs_communication_body", $"Communication {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Communications.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Communications.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_communications()
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
                            { "hs_communication_channel_type", "EMAIL" },
                            { "hs_communication_body", "Batch Communication 1" }
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
                            { "hs_communication_channel_type", "PHONE" },
                            { "hs_communication_body", "Batch Communication 2" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Communications.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
