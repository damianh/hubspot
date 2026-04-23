using DamianH.HubSpot.KiotaClient.CRM.Products.V202603;
using DamianH.HubSpot.KiotaClient.CRM.Products.V202603.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmProductsV202603KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMProductsV202603Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMProductsV202603Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_product()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "name", "Test Product" },
                        { "price", "99.99" }
                    }
                }
            });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_product_by_id()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", "Get Test" } }
                }
            });
        var id = created!.Id!;

        var retrieved = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products[id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_product()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", "Original" } }
                }
            });
        var id = created!.Id!;

        var updated = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products[id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", "Updated" } }
                }
            });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Updated");
    }

    [Fact]
    public async Task Can_list_products()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.PostAsync(
                new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object> { { "name", $"Product {i}" } }
                    }
                });
        }

        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.GetAsync(
            c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_products()
    {
        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Products.Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "name", "Batch Product 1" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "name", "Batch Product 2" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
