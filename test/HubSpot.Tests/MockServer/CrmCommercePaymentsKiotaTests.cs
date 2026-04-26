using DamianH.HubSpot.KiotaClient.CRM.Objects.V3;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCommercePaymentsKiotaTests : IAsyncLifetime
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
    public async Task Can_create_commerce_payment()
    {
        var result = await _client.Crm.V3.Objects["commerce_payments"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "99.99" } }
            }
        });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_commerce_payment_by_id()
    {
        var created = await _client.Crm.V3.Objects["commerce_payments"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "50.00" } }
            }
        });
        var id = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects["commerce_payments"][id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_commerce_payment()
    {
        var created = await _client.Crm.V3.Objects["commerce_payments"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "100.00" } }
            }
        });
        var id = created!.Id!;

        var updated = await _client.Crm.V3.Objects["commerce_payments"][id].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "200.00" } }
            }
        });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_payment_amount", "200.00");
    }

    [Fact]
    public async Task Can_list_commerce_payments()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.V3.Objects["commerce_payments"].PostAsync(new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", $"{(i + 1) * 10}.00" } }
                }
            });
        }

        var response = await _client.Crm.V3.Objects["commerce_payments"].GetAsync(c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_commerce_payments()
    {
        var response = await _client.Crm.V3.Objects["commerce_payments"].Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "25.00" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_payment_amount", "75.00" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
