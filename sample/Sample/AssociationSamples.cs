using System.Net.Http.Json;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3;
using DamianH.HubSpot.MockServer;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using ContactModels = DamianH.HubSpot.KiotaClient.CRM.Contacts.V3.Models;
using CompanyModels = DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;

namespace DamianH.HubSpot.Sample;

/// <summary>
/// Demonstrates creating contacts and companies, then associating them using the V3 associations API.
/// </summary>
public class AssociationSamples : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMContactsV3Client _contactsClient = null!;
    private HubSpotCRMCompaniesV3Client _companiesClient = null!;
    private HttpClient _httpClient = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();

        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };

        _contactsClient = new HubSpotCRMContactsV3Client(adapter);
        _companiesClient = new HubSpotCRMCompaniesV3Client(adapter);
        _httpClient = new HttpClient { BaseAddress = _server.Uri };
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task AssociateContactWithCompany_ThenRetrieve()
    {
        var ct = TestContext.Current.CancellationToken;

        // Create a contact
        var contact = await _contactsClient.Crm.V3.Objects.Contacts.PostAsync(
            new ContactModels.SimplePublicObjectInputForCreate
            {
                Properties = new ContactModels.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "email", "jane.doe@example.com" },
                        { "firstname", "Jane" },
                        { "lastname", "Doe" }
                    }
                }
            }, cancellationToken: ct);
        var contactId = contact!.Id!;

        // Create a company
        var company = await _companiesClient.Crm.V3.Objects.Companies.PostAsync(
            new CompanyModels.SimplePublicObjectInputForCreate
            {
                Properties = new CompanyModels.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "name", "Example Corp" },
                        { "domain", "example.com" }
                    }
                }
            }, cancellationToken: ct);
        var companyId = company!.Id!;

        // Associate the contact with the company (V3 association)
        var associateResponse = await _httpClient.PutAsync(
            $"/crm/v3/objects/contacts/{contactId}/associations/companies/{companyId}/1",
            null, ct);
        associateResponse.EnsureSuccessStatusCode();

        // Retrieve associations for the contact
        var listResponse = await _httpClient.GetAsync(
            $"/crm/v3/objects/contacts/{contactId}/associations/companies", ct);
        listResponse.EnsureSuccessStatusCode();

        var associations = await listResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>(ct);
        associations.ShouldNotBeNull();
        associations.ShouldContainKey("results");
    }

    [Fact]
    public async Task BatchAssociateContacts_ThenRemove()
    {
        var ct = TestContext.Current.CancellationToken;

        // Create two contacts and one company
        var contact1 = await _contactsClient.Crm.V3.Objects.Contacts.PostAsync(
            new ContactModels.SimplePublicObjectInputForCreate
            {
                Properties = new ContactModels.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "email", "alice@example.com" } }
                }
            }, cancellationToken: ct);

        var contact2 = await _contactsClient.Crm.V3.Objects.Contacts.PostAsync(
            new ContactModels.SimplePublicObjectInputForCreate
            {
                Properties = new ContactModels.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "email", "bob@example.com" } }
                }
            }, cancellationToken: ct);

        var company = await _companiesClient.Crm.V3.Objects.Companies.PostAsync(
            new CompanyModels.SimplePublicObjectInputForCreate
            {
                Properties = new CompanyModels.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", "Batch Corp" } }
                }
            }, cancellationToken: ct);

        // Batch associate both contacts with the company
        var batchRequest = new
        {
            inputs = new[]
            {
                new
                {
                    from = new { id = contact1!.Id },
                    to = new { id = company!.Id },
                    types = new[] { new { associationCategory = "HUBSPOT_DEFINED", associationTypeId = 1 } }
                },
                new
                {
                    from = new { id = contact2!.Id },
                    to = new { id = company.Id },
                    types = new[] { new { associationCategory = "HUBSPOT_DEFINED", associationTypeId = 1 } }
                }
            }
        };

        var batchResponse = await _httpClient.PostAsJsonAsync(
            "/crm/v3/associations/contacts/companies/batch/create", batchRequest, ct);
        batchResponse.EnsureSuccessStatusCode();

        // Remove the first association
        var removeResponse = await _httpClient.DeleteAsync(
            $"/crm/v3/objects/contacts/{contact1.Id}/associations/companies/{company.Id}/1", ct);
        removeResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }
}
