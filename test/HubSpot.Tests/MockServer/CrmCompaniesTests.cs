using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;
using DamianH.HubSpot.KiotaClient.Extensions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCompaniesTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCompaniesV3Client _v3Client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _v3Client = new HubSpotCRMCompaniesV3Client(requestAdapter);
    }

    #region V3 Client Tests

    [Fact]
    public async Task V3_Can_create_company()
    {
        var request = CreateV3CompanyRequest();

        var response = await _v3Client.Crm.V3.Objects.Companies.PostAsync(request);
        var simplePublicObject = response!.Entity!;

        simplePublicObject.ShouldNotBeNull();
        simplePublicObject.Id.ShouldNotBeNullOrWhiteSpace();
        simplePublicObject.Properties.ShouldNotBeNull();
        simplePublicObject.Properties.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Foo");
        simplePublicObject.Properties.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain, "example.com");
    }

    [Fact]
    public async Task V3_Can_get_company_with_no_properties()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;

        var retrievedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync();

        retrievedCompany!.Id.ShouldNotBeNullOrWhiteSpace();
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData.Count.ShouldBe(0);
    }

    [Fact]
    public async Task V3_Can_get_company_with_specified_properties()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;

        var retrievedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id]
            .GetAsync(requestConfiguration =>
            {
                requestConfiguration.QueryParameters.Properties =
                [
                    PropertyNames.CrmCompany.Domain,
                ];
            });

        retrievedCompany!.Id.ShouldNotBeNullOrWhiteSpace();
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData.Count.ShouldBe(1);
        retrievedCompany.Properties.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain,
            "example.com");
    }

    [Fact]
    public async Task V3_Can_update_company_with_changed_property()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;

        var updatedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Bar" }
                    }
                }
            });
        updatedCompany!.Properties!.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Bar");

        var retrievedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id]
            .GetAsync(requestConfiguration =>
            {
                requestConfiguration.QueryParameters.Properties =
                [
                    PropertyNames.CrmCompany.Name
                ];
            });
        retrievedCompany!.Id.ShouldBe(createdCompany.Id);
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Bar");
    }

    [Fact]
    public async Task V3_Can_update_company_with_new_property()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;
        var newPropertyName = "NewProperty";
        var updatedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].PatchAsync(
            new SimplePublicObjectInput
            {
                Properties = new SimplePublicObjectInput_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { newPropertyName, "Bar" }
                    }
                }
            });
        updatedCompany!.Properties!.AdditionalData
            .ShouldContainKeyAndValue(newPropertyName, "Bar");

        var retrievedCompany = await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id]
            .GetAsync(requestConfiguration =>
            {
                requestConfiguration.QueryParameters.Properties =
                [
                    newPropertyName
                ];
            });
        retrievedCompany!.Id.ShouldBe(createdCompany.Id);
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData
            .ShouldContainKeyAndValue(newPropertyName, "Bar");
    }

    [Fact]
    public async Task V3_Can_list_companies()
    {
        for (var i = 0; i < 25; i++)
        {
            var name = $"Company-{i}";
            var request = CreateV3CompanyRequest(name);
            await _v3Client.Crm.V3.Objects.Companies.PostAsync(request);
        }

        var page = await _v3Client.Crm.V3.Objects.Companies.GetAsync(parameters =>
        {
            parameters.QueryParameters.Limit = 10;
            parameters.QueryParameters.Properties =
            [
                PropertyNames.CrmCompany.Name
            ];
        });

        page.ShouldNotBeNull();
        page.Results.ShouldNotBeNull();
        page.Results.Count.ShouldBe(10);
        page.Paging.ShouldNotBeNull();
        page.Paging.Next.ShouldNotBeNull();
        page.Paging.Next.After.ShouldNotBeNull();
    }

    [Fact]
    public async Task V3_Can_list_companies_with_paging()
    {
        for (var i = 0; i < 25; i++)
        {
            var name = $"Company-{i}";
            var request = CreateV3CompanyRequest(name);
            await _v3Client.Crm.V3.Objects.Companies.PostAsync(request);
        }

        var page1 = await _v3Client.Crm.V3.Objects.Companies.GetAsync(parameters =>
        {
            parameters.QueryParameters.Limit = 10;
        });

        page1.ShouldNotBeNull();
        page1.Results!.Count.ShouldBe(10);

        var page2 = await _v3Client.Crm.V3.Objects.Companies.GetAsync(parameters =>
        {
            parameters.QueryParameters.Limit = 10;
            parameters.QueryParameters.After = page1.Paging!.Next!.After;
        });

        page2.ShouldNotBeNull();
        page2.Results!.Count.ShouldBe(10);

        // IDs should be different
        page1.Results.First().Id.ShouldNotBe(page2.Results.First().Id);
    }

    [Fact]
    public async Task V3_Can_archive_company()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;

        await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].DeleteAsync();

        // Attempting to get the archived company should return 404
        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync();
        });
    }

    [Fact]
    public async Task V3_Can_list_archived_companies()
    {
        var request = CreateV3CompanyRequest();
        var createdCompany = (await _v3Client.Crm.V3.Objects.Companies.PostAsync(request))!.Entity!;

        await _v3Client.Crm.V3.Objects.Companies[createdCompany.Id].DeleteAsync();

        var archivedList = await _v3Client.Crm.V3.Objects.Companies.GetAsync(parameters =>
        {
            parameters.QueryParameters.Archived = true;
        });

        archivedList.ShouldNotBeNull();
        archivedList.Results!.Count.ShouldBeGreaterThan(0);
        archivedList.Results.Any(c => c.Id == createdCompany.Id).ShouldBeTrue();
    }

    #endregion

    public ValueTask DisposeAsync() => _server.DisposeAsync();

    private static SimplePublicObjectInputForCreate CreateV3CompanyRequest(string name = "Foo") =>
        new()
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, name },
                    { PropertyNames.CrmCompany.Domain, "example.com" }
                }
            },
            Associations =
            [
                new()
                {
                    To = new PublicObjectId
                    {
                        Id = "123",
                    },
                    Types =
                    [
                        new AssociationSpec
                        {
                            AssociationTypeId = 456,
                            AssociationCategory = AssociationSpec_associationCategory.HUBSPOT_DEFINED
                        }
                    ]
                }
            ]
        };
}
