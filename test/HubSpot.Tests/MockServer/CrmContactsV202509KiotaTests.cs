using DamianH.HubSpot.KiotaClient.CRM.Objects.V202509;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V202509.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmContactsV202509KiotaTests : IAsyncLifetime
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
    public async Task Can_create_contact()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "firstname", "Test" },
                        { "lastname", "Contact" },
                        { "email", "test@example.com" }
                    }
                }
            });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_get_contact_by_id()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "email", "get@example.com" } }
                }
            });
        var id = created!.Id!;

        var retrieved = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"][id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_contact()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "firstname", "Original" } }
                }
            });
        var id = created!.Id!;

        var updated = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"][id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "firstname", "Updated" } }
                }
            });

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("firstname", "Updated");
    }

    [Fact]
    public async Task Can_list_contacts()
    {
        for (int i = 0; i < 3; i++)
        {
            await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
                new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object> { { "email", $"list{i}@example.com" } }
                    }
                });
        }

        var response = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].GetAsync(
            c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_contacts()
    {
        var response = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].Batch.Create.PostAsync(
            new BatchInputSimplePublicObjectBatchInputForCreate
            {
                Inputs =
                [
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "email", "batch1@example.com" } }
                        }
                    },
                    new SimplePublicObjectBatchInputForCreate
                    {
                        Properties = new SimplePublicObjectBatchInputForCreate_properties
                        {
                            AdditionalData = new Dictionary<string, object> { { "email", "batch2@example.com" } }
                        }
                    }
                ]
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Can_search_contacts()
    {
        await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "email", "search@example.com" } }
                }
            });

        var response = await _client.Crm.Objects.TwoZeroTwoFiveZeroNine["contacts"].Search.PostAsync(
            new PublicObjectSearchRequest
            {
                FilterGroups = [],
                Sorts = [],
                Query = "",
                Properties = [],
                Limit = 10,
                After = "0"
            });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
    }
}
