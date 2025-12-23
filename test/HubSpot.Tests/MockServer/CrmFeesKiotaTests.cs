using DamianH.HubSpot.KiotaClient.CRM.Fees.V3;
using DamianH.HubSpot.KiotaClient.CRM.Fees.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmFeesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMFeesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMFeesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_fee()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_fee_name", "Shipping Fee" },
                    { "hs_fee_amount", "15.00" },
                    { "hs_fee_type", "SHIPPING" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Fees.PostAsync(input);
        var fee = created!.Entity!;

        fee.ShouldNotBeNull();
        fee.Id.ShouldNotBeNullOrEmpty();
        fee.Properties.ShouldNotBeNull();
        fee.Properties.AdditionalData.ShouldContainKeyAndValue("hs_fee_name", "Shipping Fee");
        fee.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_fee_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_fee_name", "Processing Fee" },
                    { "hs_fee_amount", "5.00" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Fees.PostAsync(input);
        var feeId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Fees[feeId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(feeId);
    }

    [Fact]
    public async Task Can_update_fee()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_fee_name", "Handling Fee" },
                    { "hs_fee_amount", "10.00" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Fees.PostAsync(input);
        var feeId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_fee_amount", "12.50" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Fees[feeId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_fee_amount", "12.50");
    }

    [Fact]
    public async Task Can_list_fees()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_fee_name", $"Fee {i}" },
                        { "hs_fee_amount", $"{i * 5}.00" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Fees.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Fees.GetAsync(config =>
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
