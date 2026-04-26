using DamianH.HubSpot.KiotaClient.CRM.Invoices.V3;
using DamianH.HubSpot.KiotaClient.CRM.Invoices.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmInvoicesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMInvoicesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMInvoicesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_invoice()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_invoice_number", "INV-001" },
                    { "hs_invoice_status", "DRAFT" },
                    { "hs_total_amount", "1000.00" },
                    { "hs_due_date", DateTimeOffset.UtcNow.AddDays(30).ToUnixTimeMilliseconds().ToString() }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Invoices.PostAsync(input);
        var invoice = created!;

        invoice.ShouldNotBeNull();
        invoice.Id.ShouldNotBeNullOrEmpty();
        invoice.Properties.ShouldNotBeNull();
        invoice.Properties.AdditionalData.ShouldContainKeyAndValue("hs_invoice_number", "INV-001");
        invoice.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_invoice_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_invoice_number", "INV-002" },
                    { "hs_invoice_status", "SENT" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Invoices.PostAsync(input);
        var invoiceId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Invoices[invoiceId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(invoiceId);
    }

    [Fact]
    public async Task Can_update_invoice()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_invoice_number", "INV-003" },
                    { "hs_invoice_status", "DRAFT" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Invoices.PostAsync(input);
        var invoiceId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_invoice_status", "SENT" },
                    { "hs_total_amount", "1500.00" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Invoices[invoiceId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_invoice_status", "SENT");
    }

    [Fact]
    public async Task Can_list_invoices()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_invoice_number", $"INV-{100 + i}" },
                        { "hs_invoice_status", "DRAFT" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Invoices.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Invoices.GetAsync(config =>
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
