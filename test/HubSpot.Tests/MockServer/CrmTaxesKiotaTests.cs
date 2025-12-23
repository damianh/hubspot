using DamianH.HubSpot.KiotaClient.CRM.Taxes.V3;
using DamianH.HubSpot.KiotaClient.CRM.Taxes.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmTaxesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMTaxesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMTaxesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_tax()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_tax_name", "Sales Tax" },
                    { "hs_tax_rate", "8.5" },
                    { "hs_tax_amount", "42.50" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Taxes.PostAsync(input);
        var tax = created!.Entity!;

        tax.ShouldNotBeNull();
        tax.Id.ShouldNotBeNullOrEmpty();
        tax.Properties.ShouldNotBeNull();
        tax.Properties.AdditionalData.ShouldContainKeyAndValue("hs_tax_name", "Sales Tax");
        tax.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_tax_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_tax_name", "VAT" },
                    { "hs_tax_rate", "20" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Taxes.PostAsync(input);
        var taxId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Taxes[taxId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(taxId);
    }

    [Fact]
    public async Task Can_update_tax()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_tax_name", "State Tax" },
                    { "hs_tax_rate", "6.5" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Taxes.PostAsync(input);
        var taxId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_tax_rate", "7.0" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Taxes[taxId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_tax_rate", "7.0");
    }

    [Fact]
    public async Task Can_list_taxes()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_tax_name", $"Tax {i}" },
                        { "hs_tax_rate", $"{i * 2}.5" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Taxes.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Taxes.GetAsync(config =>
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
