using DamianH.HubSpot.KiotaClient.CRM.Products.V3;
using DamianH.HubSpot.KiotaClient.CRM.Tickets.V3;
using DamianH.HubSpot.KiotaClient.CRM.Quotes.V3;
using DamianH.HubSpot.KiotaClient.CRM.Communications.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using ProductsModels = DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models;
using TicketsModels = DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models;
using QuotesModels = DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Models;
using CommunicationsModels = DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models;

namespace DamianH.HubSpot.MockServer;

public class AdditionalCrmObjectsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMProductsV3Client _productsClient = null!;
    private HubSpotCRMTicketsV3Client _ticketsClient = null!;
    private HubSpotCRMQuotesV3Client _quotesClient = null!;
    private HubSpotCRMCommunicationsV3Client _communicationsClient = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _productsClient = new HubSpotCRMProductsV3Client(requestAdapter);
        _ticketsClient = new HubSpotCRMTicketsV3Client(requestAdapter);
        _quotesClient = new HubSpotCRMQuotesV3Client(requestAdapter);
        _communicationsClient = new HubSpotCRMCommunicationsV3Client(requestAdapter);
    }

    // Products Tests
    [Fact]
    public async Task Can_create_and_get_product()
    {
        var createRequest = new ProductsModels.SimplePublicObjectInputForCreate
        {
            Properties = new ProductsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["name"] = "Premium Widget",
                    ["price"] = "99.99",
                    ["description"] = "High quality widget"
                }
            }
        };

        var created = await _productsClient.Crm.V3.Objects.Products.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var productId = created!.Entity!.Id;
        productId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _productsClient.Crm.V3.Objects.Products[productId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["name", "price"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(productId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Premium Widget");
    }

    [Fact]
    public async Task Can_update_product()
    {
        var createRequest = new ProductsModels.SimplePublicObjectInputForCreate
        {
            Properties = new ProductsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["name"] = "Basic Widget",
                    ["price"] = "49.99"
                }
            }
        };

        var created = await _productsClient.Crm.V3.Objects.Products.PostAsync(createRequest);
        var productId = created!.Entity!.Id;

        var updateRequest = new ProductsModels.SimplePublicObjectInput
        {
            Properties = new ProductsModels.SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["price"] = "59.99"
                }
            }
        };

        var updated = await _productsClient.Crm.V3.Objects.Products[productId].PatchAsync(updateRequest);
        updated.ShouldNotBeNull();
        updated!.Properties!.AdditionalData.ShouldContainKeyAndValue("price", "59.99");
    }

    [Fact]
    public async Task Can_list_products()
    {
        var createRequest1 = new ProductsModels.SimplePublicObjectInputForCreate
        {
            Properties = new ProductsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { ["name"] = "Product 1" }
            }
        };
        await _productsClient.Crm.V3.Objects.Products.PostAsync(createRequest1);

        var createRequest2 = new ProductsModels.SimplePublicObjectInputForCreate
        {
            Properties = new ProductsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { ["name"] = "Product 2" }
            }
        };
        await _productsClient.Crm.V3.Objects.Products.PostAsync(createRequest2);

        var list = await _productsClient.Crm.V3.Objects.Products.GetAsync();
        list.ShouldNotBeNull();
        list!.Results.ShouldNotBeNull();
        list.Results!.Count.ShouldBeGreaterThanOrEqualTo(2);
    }

    // Tickets Tests
    [Fact]
    public async Task Can_create_and_get_ticket()
    {
        var createRequest = new TicketsModels.SimplePublicObjectInputForCreate
        {
            Properties = new TicketsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["subject"] = "Support Ticket",
                    ["content"] = "Need help with product",
                    ["hs_pipeline_stage"] = "1"
                }
            }
        };

        var created = await _ticketsClient.Crm.V3.Objects.Tickets.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var ticketId = created!.Entity!.Id;
        ticketId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _ticketsClient.Crm.V3.Objects.Tickets[ticketId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["subject", "content"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(ticketId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("subject", "Support Ticket");
    }

    [Fact]
    public async Task Can_update_ticket()
    {
        var createRequest = new TicketsModels.SimplePublicObjectInputForCreate
        {
            Properties = new TicketsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["subject"] = "Original Ticket",
                    ["hs_pipeline_stage"] = "1"
                }
            }
        };

        var created = await _ticketsClient.Crm.V3.Objects.Tickets.PostAsync(createRequest);
        var ticketId = created!.Entity!.Id;

        var updateRequest = new TicketsModels.SimplePublicObjectInput
        {
            Properties = new TicketsModels.SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_pipeline_stage"] = "2"
                }
            }
        };

        var updated = await _ticketsClient.Crm.V3.Objects.Tickets[ticketId].PatchAsync(updateRequest);
        updated.ShouldNotBeNull();
        updated!.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_pipeline_stage", "2");
    }

    // Quotes Tests
    [Fact]
    public async Task Can_create_and_get_quote()
    {
        var createRequest = new QuotesModels.SimplePublicObjectInputForCreate
        {
            Properties = new QuotesModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_title"] = "Q1 2024 Quote",
                    ["hs_expiration_date"] = "2024-12-31"
                }
            }
        };

        var created = await _quotesClient.Crm.V3.Objects.Quotes.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var quoteId = created!.Entity!.Id;
        quoteId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _quotesClient.Crm.V3.Objects.Quotes[quoteId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_title"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(quoteId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_title", "Q1 2024 Quote");
    }

    // Communications Tests
    [Fact]
    public async Task Can_create_and_get_communication()
    {
        var createRequest = new CommunicationsModels.SimplePublicObjectInputForCreate
        {
            Properties = new CommunicationsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_communication_channel_type"] = "SMS",
                    ["hs_communication_body"] = "Hello, this is a test message"
                }
            }
        };

        var created = await _communicationsClient.Crm.V3.Objects.Communications.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var commId = created!.Entity!.Id;
        commId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _communicationsClient.Crm.V3.Objects.Communications[commId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_communication_channel_type"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(commId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_communication_channel_type", "SMS");
    }

    [Fact]
    public async Task Can_delete_product()
    {
        var createRequest = new ProductsModels.SimplePublicObjectInputForCreate
        {
            Properties = new ProductsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { ["name"] = "Delete Me" }
            }
        };

        var created = await _productsClient.Crm.V3.Objects.Products.PostAsync(createRequest);
        var productId = created!.Entity!.Id;

        await _productsClient.Crm.V3.Objects.Products[productId].DeleteAsync();

        // Verify deletion
        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(
            async () => await _productsClient.Crm.V3.Objects.Products[productId].GetAsync());
    }

    [Fact]
    public async Task Can_delete_ticket()
    {
        var createRequest = new TicketsModels.SimplePublicObjectInputForCreate
        {
            Properties = new TicketsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["subject"] = "Delete Me",
                    ["hs_pipeline_stage"] = "1"
                }
            }
        };

        var created = await _ticketsClient.Crm.V3.Objects.Tickets.PostAsync(createRequest);
        var ticketId = created!.Entity!.Id;

        await _ticketsClient.Crm.V3.Objects.Tickets[ticketId].DeleteAsync();

        // Verify deletion
        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(
            async () => await _ticketsClient.Crm.V3.Objects.Tickets[ticketId].GetAsync());
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();
}
