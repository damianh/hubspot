using DamianH.HubSpot.KiotaClient.CRM.Contacts.V202603;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V202603.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmContactsV202603KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMContactsV202603Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMContactsV202603Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_contact()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "firstname", "Test" },
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
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "email", "get@example.com" } }
                }
            });
        var id = created!.Id!;

        var retrieved = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts[id].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
    }

    [Fact]
    public async Task Can_update_contact()
    {
        var created = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "firstname", "Original" } }
                }
            });
        var id = created!.Id!;

        var updated = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts[id].PatchAsync(
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
            await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.PostAsync(
                new SimplePublicObjectInputForCreate
                {
                    Properties = new SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object> { { "email", $"list{i}@example.com" } }
                    }
                });
        }

        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.GetAsync(
            c => c.QueryParameters.Limit = 10);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_contacts()
    {
        var response = await _client.Crm.Objects.TwoZeroTwoSixZeroThree.Contacts.Batch.Create.PostAsync(
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
}
