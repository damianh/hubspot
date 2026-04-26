using DamianH.HubSpot.KiotaClient.CRM.Carts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Carts.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCartsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCartsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCartsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_cart()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_cart_status", "OPEN" },
                    { "hs_cart_total", "1499.99" },
                    { "hs_currency_code", "USD" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Carts.PostAsync(input);
        var cart = created!;

        cart.ShouldNotBeNull();
        cart.Id.ShouldNotBeNullOrEmpty();
        cart.Properties.ShouldNotBeNull();
        cart.Properties.AdditionalData.ShouldContainKeyAndValue("hs_cart_status", "OPEN");
        cart.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_cart_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_cart_status", "OPEN" },
                    { "hs_cart_total", "599.99" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Carts.PostAsync(input);
        var cartId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Carts[cartId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(cartId);
    }

    [Fact]
    public async Task Can_update_cart()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_cart_status", "OPEN" },
                    { "hs_cart_total", "100.00" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Carts.PostAsync(input);
        var cartId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_cart_status", "ABANDONED" },
                    { "hs_cart_total", "150.00" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Carts[cartId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_cart_status", "ABANDONED");
    }

    [Fact]
    public async Task Can_list_carts()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_cart_status", "OPEN" },
                        { "hs_cart_total", $"{i * 100}.00" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Carts.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Carts.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_carts()
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
                            { "hs_cart_status", "OPEN" },
                            { "hs_cart_total", "250.00" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_cart_status", "OPEN" },
                            { "hs_cart_total", "350.00" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Carts.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
