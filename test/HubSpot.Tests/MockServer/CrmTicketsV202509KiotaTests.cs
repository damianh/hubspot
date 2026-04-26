using DamianH.HubSpot.KiotaClient.CRM.Objects.V202509;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V202509.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmTicketsV202509KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMObjectsV202509Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMObjectsV202509Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_ticket()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "subject", "Test Ticket" },
                        { "hs_pipeline_stage", "1" }
                    }
                }
            });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_ticket_by_id()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "subject", "Get Test" } }
                }
            });
        var id = created!.Id!;

        var retrieved = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"][id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_ticket()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "subject", "Original" } }
                }
            });
        var id = created!.Id!;

        var updated = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"][id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "subject", "Updated" } }
                }
            });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("subject", "Updated");
    }

    [Fact]
    public async Task Can_list_tickets()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].PostAsync(
                new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object> { { "subject", $"Ticket {i}" } }
                    }
                });
        }

        var response = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].GetAsync(
            c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_tickets()
    {
        var response = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["tickets"].Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "subject", "Batch Ticket 1" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "subject", "Batch Ticket 2" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
