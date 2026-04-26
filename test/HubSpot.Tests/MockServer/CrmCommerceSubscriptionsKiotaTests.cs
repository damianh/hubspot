using DamianH.HubSpot.KiotaClient.CRM.Objects.V3;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCommerceSubscriptionsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMObjectsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMObjectsV3Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_commerce_subscription()
    {
        var result = await _client.Crm.V3.Objects["commerce_subscriptions"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Test Subscription" } }
            }
        });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_commerce_subscription_by_id()
    {
        var created = await _client.Crm.V3.Objects["commerce_subscriptions"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Get Test" } }
            }
        });
        var id = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects["commerce_subscriptions"][id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_commerce_subscription()
    {
        var created = await _client.Crm.V3.Objects["commerce_subscriptions"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Original" } }
            }
        });
        var id = created!.Id!;

        var updated = await _client.Crm.V3.Objects["commerce_subscriptions"][id].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Updated" } }
            }
        });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_subscription_name", "Updated");
    }

    [Fact]
    public async Task Can_list_commerce_subscriptions()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.V3.Objects["commerce_subscriptions"].PostAsync(new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", $"Sub {i}" } }
                }
            });
        }

        var response = await _client.Crm.V3.Objects["commerce_subscriptions"].GetAsync(c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_commerce_subscriptions()
    {
        var response = await _client.Crm.V3.Objects["commerce_subscriptions"].Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Batch 1" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_subscription_name", "Batch 2" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
