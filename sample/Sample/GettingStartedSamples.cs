using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;
using DamianH.HubSpot.MockServer;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.Sample;

/// <summary>
/// Demonstrates basic mock server setup and CRM company operations.
/// Each test is self-contained and shows a distinct capability.
/// </summary>
public class GettingStartedSamples : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCompaniesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        // Start an in-memory HubSpot mock server — no external dependencies required
        _server = await HubSpotMockServer.StartNew();

        // Create a Kiota HTTP adapter pointed at the mock server
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };

        _client = new HubSpotCRMCompaniesV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task CreateCompany_ReturnsCreatedCompanyWithId()
    {
        var ct = TestContext.Current.CancellationToken;

        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Acme Corporation" },
                    { "domain", "acme.com" },
                    { "city", "San Francisco" },
                    { "industry", "Technology" }
                }
            }
        };

        var result = await _client.Crm.V3.Objects.Companies.PostAsync(input, cancellationToken: ct);
        var company = result!.Entity!;

        company.ShouldNotBeNull();
        company.Id.ShouldNotBeNullOrEmpty();
        company.Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Acme Corporation");
        company.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task ReadCompany_ById_ReturnsCompany()
    {
        var ct = TestContext.Current.CancellationToken;

        // Create first, then read back by ID
        var created = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "name", "Read Me Inc" } }
            }
        }, cancellationToken: ct);
        var companyId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Companies[companyId].GetAsync(cancellationToken: ct);

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(companyId);
    }

    [Fact]
    public async Task UpdateCompany_PatchesProperties()
    {
        var ct = TestContext.Current.CancellationToken;

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "name", "Old Name" }, { "city", "Seattle" } }
            }
        }, cancellationToken: ct);
        var companyId = created!.Entity!.Id!;

        var updated = await _client.Crm.V3.Objects.Companies[companyId].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "city", "Austin" }, { "state", "TX" } }
            }
        }, cancellationToken: ct);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("city", "Austin");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("state", "TX");
    }

    [Fact]
    public async Task DeleteCompany_RemovesFromList()
    {
        var ct = TestContext.Current.CancellationToken;

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "name", "To Be Deleted" } }
            }
        }, cancellationToken: ct);
        var companyId = created!.Entity!.Id!;

        // Delete — returns 204 No Content on success
        await _client.Crm.V3.Objects.Companies[companyId].DeleteAsync(cancellationToken: ct);

        // Subsequent read should throw (404)
        await Should.ThrowAsync<Exception>(async () =>
            await _client.Crm.V3.Objects.Companies[companyId].GetAsync(cancellationToken: ct));
    }

    [Fact]
    public async Task ListCompanies_ReturnsPaginatedResults()
    {
        var ct = TestContext.Current.CancellationToken;

        // Create several companies
        for (var i = 1; i <= 3; i++)
        {
            await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", $"Pagination Co {i}" } }
                }
            }, cancellationToken: ct);
        }

        var results = await _client.Crm.V3.Objects.Companies.GetAsync(q =>
        {
            q.QueryParameters.Limit = 2;
        }, cancellationToken: ct);

        results.ShouldNotBeNull();
        results.Results.ShouldNotBeNull();
        results.Results.Count.ShouldBeGreaterThanOrEqualTo(2);
    }
}
