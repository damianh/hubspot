using DamianH.HubSpot.KiotaClient;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V202509;
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

    #region V202509 Client Tests

    [Fact]
    public async Task V202509_Can_create_company()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "V202509 Company" },
                    { PropertyNames.CrmCompany.Domain, "v202509.com" }
                }
            }
        };

        var created = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        var company = created!.Entity!;

        company.ShouldNotBeNull();
        company.Id.ShouldNotBeNullOrEmpty();
        company.Properties.ShouldNotBeNull();
        company.Properties.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "V202509 Company");
        company.Properties.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain, "v202509.com");
    }

    [Fact]
    public async Task V202509_Can_get_company_with_no_properties()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "Test Company" },
                    { PropertyNames.CrmCompany.Domain, "test.com" }
                }
            }
        };

        var created = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        var companyId = created!.Entity!.Id!;

        var retrieved = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(companyId);
        retrieved.Properties.ShouldNotBeNull();
        retrieved.Properties.AdditionalData.Count.ShouldBe(0);
    }

    [Fact]
    public async Task V202509_Can_get_company_with_specified_properties()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "Property Test" },
                    { PropertyNames.CrmCompany.Domain, "proptest.com" }
                }
            }
        };

        var created = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        var companyId = created!.Entity!.Id!;

        var retrieved = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId]
            .GetAsync(requestConfiguration =>
            {
                requestConfiguration.QueryParameters.Properties = [PropertyNames.CrmCompany.Domain];
            });

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(companyId);
        retrieved.Properties.ShouldNotBeNull();
        retrieved.Properties.AdditionalData.Count.ShouldBe(1);
        retrieved.Properties.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Domain, "proptest.com");
    }

    [Fact]
    public async Task V202509_Can_update_company()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "Original Name" }
                }
            }
        };

        var created = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        var companyId = created!.Entity!.Id!;

        var updateInput = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInput
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "Updated Name" }
                }
            }
        };

        var updated = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Updated Name");
    }

    [Fact]
    public async Task V202509_Can_delete_company()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
        {
            Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { PropertyNames.CrmCompany.Name, "To Delete" }
                }
            }
        };

        var created = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        var companyId = created!.Entity!.Id!;

        await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId].DeleteAsync();

        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId].GetAsync();
        });
    }

    [Fact]
    public async Task V202509_Can_list_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        for (var i = 0; i < 15; i++)
        {
            var input = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, $"List Company {i}" }
                    }
                }
            };
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(input);
        }

        var page = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
            config.QueryParameters.Properties = [PropertyNames.CrmCompany.Name];
        });

        page.ShouldNotBeNull();
        page.Results.ShouldNotBeNull();
        page.Results.Count.ShouldBe(10);
        page.Paging.ShouldNotBeNull();
        page.Paging.Next.ShouldNotBeNull();
    }

    [Fact]
    public async Task V202509_Can_batch_create_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var batchInput = new KiotaClient.CRM.Companies.V202509.Models.BatchInputSimplePublicObjectBatchInputForCreate
        {
            Inputs =
            [
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputForCreate
                {
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "Batch 1" }
                        }
                    }
                },
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputForCreate
                {
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "Batch 2" }
                        }
                    }
                }
            ]
        };

        var response = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch 2");
    }

    [Fact]
    public async Task V202509_Can_batch_read_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var company1 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Batch Read 1" }
                    }
                }
            });

        var company2 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Batch Read 2" }
                    }
                }
            });

        var batchReadInput = new KiotaClient.CRM.Companies.V202509.Models.BatchReadInputSimplePublicObjectId
        {
            Inputs =
            [
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectId { Id = company1!.Entity!.Id! },
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectId { Id = company2!.Entity!.Id! }
            ],
            Properties = [PropertyNames.CrmCompany.Name]
        };

        var response = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Batch.Read.PostAsync(batchReadInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch Read 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch Read 2");
    }

    [Fact]
    public async Task V202509_Can_batch_update_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var company1 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Original 1" }
                    }
                }
            });

        var company2 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Original 2" }
                    }
                }
            });

        var batchUpdateInput = new KiotaClient.CRM.Companies.V202509.Models.BatchInputSimplePublicObjectBatchInput
        {
            Inputs =
            [
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInput
                {
                    Id = company1!.Entity!.Id!,
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "Batch Updated 1" }
                        }
                    }
                },
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInput
                {
                    Id = company2!.Entity!.Id!,
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "Batch Updated 2" }
                        }
                    }
                }
            ]
        };

        var response = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Batch.Update.PostAsync(batchUpdateInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch Updated 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Batch Updated 2");
    }

    [Fact]
    public async Task V202509_Can_batch_upsert_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var existing = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Existing for Upsert" }
                    }
                }
            });

        var batchUpsertInput = new KiotaClient.CRM.Companies.V202509.Models.BatchInputSimplePublicObjectBatchInputUpsert
        {
            Inputs =
            [
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputUpsert
                {
                    Id = existing!.Entity!.Id!,
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "Upserted Existing" }
                        }
                    }
                },
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputUpsert
                {
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, "New via Upsert" }
                        }
                    }
                }
            ]
        };

        var response = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Batch.Upsert.PostAsync(batchUpsertInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task V202509_Can_batch_archive_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var company1 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "To Archive 1" }
                    }
                }
            });

        var company2 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "To Archive 2" }
                    }
                }
            });

        var batchArchiveInput = new KiotaClient.CRM.Companies.V202509.Models.BatchInputSimplePublicObjectId
        {
            Inputs =
            [
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectId { Id = company1!.Entity!.Id! },
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectId { Id = company2!.Entity!.Id! }
            ]
        };

        await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Batch.Archive.PostAsync(batchArchiveInput);

        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][company1.Entity.Id].GetAsync();
        });

        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][company2!.Entity!.Id!].GetAsync();
        });
    }

    [Fact]
    public async Task V202509_Can_paginate_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        for (var i = 0; i < 25; i++)
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
                new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
                {
                    Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { PropertyNames.CrmCompany.Name, $"Pagination Company {i}" }
                        }
                    }
                });
        }

        var firstPage = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
            config.QueryParameters.Properties = [PropertyNames.CrmCompany.Name];
        });

        firstPage.ShouldNotBeNull();
        firstPage.Results.ShouldNotBeNull();
        firstPage.Results.Count.ShouldBe(10);
        firstPage.Paging.ShouldNotBeNull();
        firstPage.Paging.Next.ShouldNotBeNull();

        var after = firstPage.Paging.Next.After;
        after.ShouldNotBeNullOrWhiteSpace();

        var secondPage = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
            config.QueryParameters.After = after;
            config.QueryParameters.Properties = [PropertyNames.CrmCompany.Name];
        });

        secondPage.ShouldNotBeNull();
        secondPage.Results.ShouldNotBeNull();
        secondPage.Results.Count.ShouldBe(10);
    }

    [Fact]
    public async Task V202509_Can_search_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Search Target Company" },
                        { PropertyNames.CrmCompany.Domain, "searchtarget.com" }
                    }
                }
            });

        await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Other Company" },
                        { PropertyNames.CrmCompany.Domain, "other.com" }
                    }
                }
            });

        var searchRequest = new KiotaClient.CRM.Companies.V202509.Models.PublicObjectSearchRequest
        {
            FilterGroups =
            [
                new KiotaClient.CRM.Companies.V202509.Models.FilterGroup
                {
                    Filters =
                    [
                        new KiotaClient.CRM.Companies.V202509.Models.Filter
                        {
                            PropertyName = PropertyNames.CrmCompany.Name,
                            Operator = KiotaClient.CRM.Companies.V202509.Models.Filter_operator.EQ,
                            Value = "Search Target Company"
                        }
                    ]
                }
            ],
            Properties = [PropertyNames.CrmCompany.Name, PropertyNames.CrmCompany.Domain]
        };

        var searchResults = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Search.PostAsync(searchRequest);

        searchResults.ShouldNotBeNull();
        searchResults.Results.ShouldNotBeNull();
        searchResults.Results.Count.ShouldBe(1);
        searchResults.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue(PropertyNames.CrmCompany.Name, "Search Target Company");
    }

    [Fact]
    public async Task V202509_Can_merge_companies()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var company1 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Company To Merge From" },
                        { PropertyNames.CrmCompany.Domain, "mergefrom.com" }
                    }
                }
            });

        var company2 = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "Company To Merge Into" }
                    }
                }
            });

        var mergeRequest = new KiotaClient.CRM.Companies.V202509.Models.PublicMergeInput
        {
            ObjectIdToMerge = company1!.Entity!.Id!,
            PrimaryObjectId = company2!.Entity!.Id!
        };

        var mergedCompany = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].Merge.PostAsync(mergeRequest);

        mergedCompany.ShouldNotBeNull();
        mergedCompany.Id.ShouldBe(company2.Entity.Id);

        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][company1.Entity.Id].GetAsync();
        });
    }

    [Fact]
    public async Task V202509_Can_gdpr_delete_company()
    {
        var v202509Client = new HubSpotCRMCompaniesV202509Client(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        });

        var company = await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].PostAsync(
            new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate
            {
                Properties = new KiotaClient.CRM.Companies.V202509.Models.SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { PropertyNames.CrmCompany.Name, "GDPR Delete Target" }
                    }
                }
            });

        var companyId = company!.Entity!.Id!;

        await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"].GdprDelete.PostAsync(new KiotaClient.CRM.Companies.V202509.Models.PublicGdprDeleteInput
        {
            ObjectId = companyId
        });

        await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(async () =>
        {
            await v202509Client.Crm.Objects.TwoZeroTwoFiveZeroNine["companies"][companyId].GetAsync();
        });
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
