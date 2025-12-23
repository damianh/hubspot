using DamianH.HubSpot.KiotaClient.CRM.Contracts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Contracts.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmContractsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMContractsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMContractsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_contract()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_contract_name", "Annual Service Agreement" },
                    { "hs_contract_value", "50000" },
                    { "hs_contract_status", "ACTIVE" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contracts.PostAsync(input);
        var contract = created!.Entity!;

        contract.ShouldNotBeNull();
        contract.Id.ShouldNotBeNullOrEmpty();
        contract.Properties.ShouldNotBeNull();
        contract.Properties.AdditionalData.ShouldContainKeyAndValue("hs_contract_name", "Annual Service Agreement");
        contract.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_contract_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_contract_name", "Software License Agreement" },
                    { "hs_contract_value", "25000" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contracts.PostAsync(input);
        var contractId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Contracts[contractId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contractId);
    }

    [Fact]
    public async Task Can_update_contract()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_contract_name", "Consulting Contract" },
                    { "hs_contract_status", "DRAFT" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contracts.PostAsync(input);
        var contractId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_contract_status", "SIGNED" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Contracts[contractId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_contract_status", "SIGNED");
    }

    [Fact]
    public async Task Can_list_contracts()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_contract_name", $"Contract {i}" },
                        { "hs_contract_value", (i * 10000).ToString() }
                    }
                }
            };
            await _client.Crm.V3.Objects.Contracts.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Contracts.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
