using DamianH.HubSpot.KiotaClient.CRM.Deals.V202603;
using DamianH.HubSpot.KiotaClient.CRM.Deals.V202603.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmDealsV202603KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMDealsV202603Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMDealsV202603Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_deal()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "dealname", "Test Deal" },
                        { "amount", "5000" }
                    }
                }
            });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_deal_by_id()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "dealname", "Get Test" } }
                }
            });
        var id = created!.Id!;

        var retrieved = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree[id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_deal()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "dealname", "Original" } }
                }
            });
        var id = created!.Id!;

        var updated = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree[id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "dealname", "Updated" } }
                }
            });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("dealname", "Updated");
    }

    [Fact]
    public async Task Can_list_deals()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.PostAsync(
                new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object> { { "dealname", $"Deal {i}" } }
                    }
                });
        }

        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.GetAsync(
            c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_deals()
    {
        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.ZeroThree.Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "dealname", "Batch Deal 1" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "dealname", "Batch Deal 2" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
