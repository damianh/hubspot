using DamianH.HubSpot.KiotaClient.CRM.Orders.V3;
using DamianH.HubSpot.KiotaClient.CRM.Orders.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmOrdersKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMOrdersV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMOrdersV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_order()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_order_name", "Order #12345" },
                    { "hs_order_total", "2499.99" },
                    { "hs_order_status", "PENDING" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Orders.PostAsync(input);
        var order = created!;

        order.ShouldNotBeNull();
        order.Id.ShouldNotBeNullOrEmpty();
        order.Properties.ShouldNotBeNull();
        order.Properties.AdditionalData.ShouldContainKeyAndValue("hs_order_name", "Order #12345");
        order.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_order_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_order_name", "Order #67890" },
                    { "hs_order_total", "999.99" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Orders.PostAsync(input);
        var orderId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Orders[orderId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(orderId);
    }

    [Fact]
    public async Task Can_update_order()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_order_name", "Initial Order" },
                    { "hs_order_status", "PENDING" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Orders.PostAsync(input);
        var orderId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_order_status", "SHIPPED" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Orders[orderId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_order_status", "SHIPPED");
    }

    [Fact]
    public async Task Can_list_orders()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_order_name", $"Order #{i}" },
                        { "hs_order_total", $"{i * 500}.00" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Orders.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Orders.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_orders()
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
                            { "hs_order_name", "Batch Order 1" },
                            { "hs_order_total", "1200.00" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_order_name", "Batch Order 2" },
                            { "hs_order_total", "1500.00" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Orders.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
