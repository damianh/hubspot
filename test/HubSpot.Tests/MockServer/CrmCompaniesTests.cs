using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;
using DamianH.HubSpot.KiotaClient.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Xunit.Abstractions;

namespace DamianH.HubSpot.MockServer;
public class CrmCompaniesTests(ITestOutputHelper outputHelper) : IAsyncLifetime
{
    private HubSpotMockServer           _server = null!;
    private HubSpotCRMCompaniesV3Client _client = null!;

    public async Task InitializeAsync()
    {
        var services = new ServiceCollection()
            .AddLogging(logging => logging.AddXUnit(outputHelper))
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCompaniesV3Client(requestAdapter);
        var hubSpotCrmContactsClient = new HubSpotCRMCompaniesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_company()
    {
        var request = CreateCompanyRequest();

        var simplePublicObject = await _client.Crm.V3.Objects.Companies.PostAsync(request);

        simplePublicObject.ShouldNotBeNull();
        simplePublicObject.Id.ShouldNotBeNullOrWhiteSpace();
        simplePublicObject.Properties.ShouldNotBeNull();
        simplePublicObject.Properties.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Foo");
        simplePublicObject.Properties.AdditionalData
            .ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain, "example.com");
    }
    
    [Fact]
    public async Task Can_get_company_with_no_properties()
    {
        var request        = CreateCompanyRequest();
        var createdCompany = (await _client.Crm.V3.Objects.Companies.PostAsync(request))!;

        var retrievedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync();

        retrievedCompany!.Id.ShouldNotBeNullOrWhiteSpace();
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData.Count.ShouldBe(0);
    }
    
    [Fact]
    public async Task Can_get_company_with_specified_properties()
    {
        var request        = CreateCompanyRequest();
        var createdCompany = (await _client.Crm.V3.Objects.Companies.PostAsync(request))!;

        var retrievedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync(
            requestConfiguration =>
            {
                requestConfiguration.QueryParameters.Properties =
                [
                    PropertyNames.CrmCompany.Domain,
                ];
            });

        retrievedCompany!.Id.ShouldNotBeNullOrWhiteSpace();
        retrievedCompany.Properties.ShouldNotBeNull();
        retrievedCompany.Properties.AdditionalData.Count.ShouldBe(1);
        retrievedCompany.Properties.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain, "example.com");
    }
    
    [Fact]
    public async Task Can_update_company_with_changed_property()
    {
        var request        = CreateCompanyRequest();
        var createdCompany = (await _client.Crm.V3.Objects.Companies.PostAsync(request))!;

        var updatedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].PatchAsync(
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
        
        var retrievedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync(
            requestConfiguration =>
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
    public async Task Can_update_company_with_new_property()
    {
        var request         = CreateCompanyRequest();
        var createdCompany  = (await _client.Crm.V3.Objects.Companies.PostAsync(request))!;
        var newPropertyName = "NewProperty";
        var updatedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].PatchAsync(
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
        
        var retrievedCompany = await _client.Crm.V3.Objects.Companies[createdCompany.Id].GetAsync(
            requestConfiguration =>
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
    public async Task Can_list_companies()
    {
        for (var i = 0; i < 100; i++)
        {
            var name           = $"Company-{i}";
            var request        = CreateCompanyRequest(name);
            await _client.Crm.V3.Objects.Companies.PostAsync(request);
        }

        var page = await _client.Crm.V3.Objects.Companies.GetAsync(parameters =>
        {
            parameters.QueryParameters.Limit = 10;
            parameters.QueryParameters.Properties =
            [
                PropertyNames.CrmCompany.Name
            ];
        });

        page.ShouldNotBeNull();
    }
    
    public Task DisposeAsync() => _server.DisposeAsync().AsTask();
    
    private static SimplePublicObjectInputForCreate CreateCompanyRequest(string name = "Foo") =>
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
            Associations = new List<PublicAssociationsForObject>
            {
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
                            AssociationTypeId   = 456,
                            AssociationCategory = AssociationSpec_associationCategory.HUBSPOT_DEFINED
                        }
                    ]
                }
            }
        };
}
