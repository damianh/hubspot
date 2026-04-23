using DamianH.HubSpot.KiotaClient.CRM.Objects.V3;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmPostalMailKiotaTests : IAsyncLifetime
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
    public async Task Can_create_postal_mail()
    {
        var result = await _client.Crm.V3.Objects["postal_mail"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Test Mail" } }
            }
        });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_postal_mail_by_id()
    {
        var created = await _client.Crm.V3.Objects["postal_mail"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Get Test" } }
            }
        });
        var id = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects["postal_mail"][id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_postal_mail()
    {
        var created = await _client.Crm.V3.Objects["postal_mail"].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Original" } }
            }
        });
        var id = created!.Id!;

        var updated = await _client.Crm.V3.Objects["postal_mail"][id].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Updated" } }
            }
        });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_mail_body", "Updated");
    }

    [Fact]
    public async Task Can_list_postal_mail()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.V3.Objects["postal_mail"].PostAsync(new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "hs_mail_body", $"Mail {i}" } }
                }
            });
        }

        var response = await _client.Crm.V3.Objects["postal_mail"].GetAsync(c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_postal_mail()
    {
        var response = await _client.Crm.V3.Objects["postal_mail"].Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Batch 1" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "hs_mail_body", "Batch 2" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }
}
