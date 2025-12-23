using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmCompaniesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCompaniesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCompaniesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_company()
    {
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

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(input);
        var company = created!.Entity!;

        company.ShouldNotBeNull();
        company.Id.ShouldNotBeNullOrEmpty();
        company.Properties.ShouldNotBeNull();
        company.Properties.AdditionalData.ShouldContainKeyAndValue("name", "Acme Corporation");
        company.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_company_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Tech Innovations Ltd" },
                    { "domain", "techinnovations.com" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(input);
        var companyId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Companies[companyId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(companyId);
    }

    [Fact]
    public async Task Can_update_company()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "StartupCo" },
                    { "city", "Austin" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(input);
        var companyId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "city", "New York" },
                    { "state", "NY" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Companies[companyId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("city", "New York");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("state", "NY");
    }

    [Fact]
    public async Task Can_delete_company()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Company to Delete" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Companies.PostAsync(input);
        var companyId = created!.Entity!.Id!;

        await _client.Crm.V3.Objects.Companies[companyId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Companies[companyId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_companies()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "name", $"Company {i}" },
                        { "domain", $"company{i}.com" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Companies.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Companies.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_companies()
    {
        var batchInput = new BatchInputSimplePublicObjectBatchInputForCreate
        {
            Inputs =
            [
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Batch Company 1" },
                            { "domain", "batch1.com" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Batch Company 2" },
                            { "domain", "batch2.com" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Companies.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Batch Company 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Batch Company 2");
    }

    [Fact]
    public async Task Can_batch_read_companies()
    {
        var company1 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Read Company 1" },
                    { "domain", "read1.com" }
                }
            }
        });

        var company2 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Read Company 2" },
                    { "domain", "read2.com" }
                }
            }
        });

        var batchReadInput = new BatchReadInputSimplePublicObjectId
        {
            Inputs =
            [
                new SimplePublicObjectId { Id = company1!.Entity!.Id! },
                new SimplePublicObjectId { Id = company2!.Entity!.Id! }
            ],
            Properties = ["name", "domain"]
        };

        var response = await _client.Crm.V3.Objects.Companies.Batch.Read.PostAsync(batchReadInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Read Company 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Read Company 2");
    }

    [Fact]
    public async Task Can_batch_update_companies()
    {
        var company1 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Update Company 1" }
                }
            }
        });

        var company2 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Update Company 2" }
                }
            }
        });

        var batchUpdateInput = new BatchInputSimplePublicObjectBatchInput
        {
            Inputs =
            [
                new SimplePublicObjectBatchInput
                {
                    Id = company1!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Updated Company 1" },
                            { "city", "Boston" }
                        }
                    }
                },
                new SimplePublicObjectBatchInput
                {
                    Id = company2!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Updated Company 2" },
                            { "city", "Seattle" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Companies.Batch.Update.PostAsync(batchUpdateInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Updated Company 1");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("name", "Updated Company 2");
    }

    [Fact]
    public async Task Can_batch_upsert_companies()
    {
        var existingCompany = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Existing Company" }
                }
            }
        });

        var batchUpsertInput = new BatchInputSimplePublicObjectBatchInputUpsert
        {
            Inputs =
            [
                new SimplePublicObjectBatchInputUpsert
                {
                    Id = existingCompany!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "Upserted Existing Company" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputUpsert
                {
                    Properties = new SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "name", "New Upserted Company" },
                            { "domain", "newupsert.com" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Companies.Batch.Upsert.PostAsync(batchUpsertInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Can_batch_archive_companies()
    {
        var company1 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Archive Company 1" }
                }
            }
        });

        var company2 = await _client.Crm.V3.Objects.Companies.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Archive Company 2" }
                }
            }
        });

        var batchArchiveInput = new BatchInputSimplePublicObjectId
        {
            Inputs =
            [
                new SimplePublicObjectId { Id = company1!.Entity!.Id! },
                new SimplePublicObjectId { Id = company2!.Entity!.Id! }
            ]
        };

        await _client.Crm.V3.Objects.Companies.Batch.Archive.PostAsync(batchArchiveInput);

        await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Companies[company1.Entity.Id].GetAsync();
        });

        await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Companies[company2!.Entity!.Id!].GetAsync();
        });
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
