using DamianH.HubSpot.KiotaClient.CRM.Products.V3;
using DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmProductsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMProductsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMProductsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_product()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Enterprise Software License" },
                    { "price", "999.00" },
                    { "description", "Annual software license for enterprise edition" },
                    { "hs_sku", "ENT-LIC-001" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Products.PostAsync(input);
        var product = created!;

        product.ShouldNotBeNull();
        product.Id.ShouldNotBeNullOrEmpty();
        product.Properties.ShouldNotBeNull();
        product.Properties.AdditionalData.ShouldContainKeyAndValue("name", "Enterprise Software License");
        product.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_product_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Professional Services Package" },
                    { "price", "5000.00" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Products.PostAsync(input);
        var productId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Products[productId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(productId);
    }

    [Fact]
    public async Task Can_update_product()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Basic License" },
                    { "price", "99.00" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Products.PostAsync(input);
        var productId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "price", "149.00" },
                    { "description", "Updated pricing tier" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Products[productId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("price", "149.00");
    }

    [Fact]
    public async Task Can_list_products()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "name", $"Product {i}" },
                        { "price", $"{i * 100}.00" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Products.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Products.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_products()
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
                            { "name", "Batch Product 1" },
                            { "price", "299.00" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Batch Product 2" },
                            { "price", "399.00" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Products.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
