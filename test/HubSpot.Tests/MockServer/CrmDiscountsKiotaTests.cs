using DamianH.HubSpot.KiotaClient.CRM.Discounts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Discounts.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmDiscountsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMDiscountsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMDiscountsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_discount()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_discount_name", "Black Friday 2024" },
                    { "hs_discount_percentage", "25" },
                    { "hs_discount_code", "BF2024" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Discounts.PostAsync(input);
        var discount = created!;

        discount.ShouldNotBeNull();
        discount.Id.ShouldNotBeNullOrEmpty();
        discount.Properties.ShouldNotBeNull();
        discount.Properties.AdditionalData.ShouldContainKeyAndValue("hs_discount_name", "Black Friday 2024");
        discount.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_discount_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_discount_name", "Holiday Sale" },
                    { "hs_discount_percentage", "15" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Discounts.PostAsync(input);
        var discountId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Discounts[discountId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(discountId);
    }

    [Fact]
    public async Task Can_update_discount()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_discount_name", "Summer Sale" },
                    { "hs_discount_percentage", "10" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Discounts.PostAsync(input);
        var discountId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_discount_percentage", "20" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Discounts[discountId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_discount_percentage", "20");
    }

    [Fact]
    public async Task Can_list_discounts()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_discount_name", $"Discount {i}" },
                        { "hs_discount_percentage", (i * 5).ToString() }
                    }
                }
            };
            await _client.Crm.V3.Objects.Discounts.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Discounts.GetAsync(config =>
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
