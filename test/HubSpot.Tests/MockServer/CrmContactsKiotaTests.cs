using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmContactsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMContactsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMContactsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_contact()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "john.doe@example.com" },
                    { "firstname", "John" },
                    { "lastname", "Doe" },
                    { "phone", "555-1234" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contact = created!.Entity!;

        contact.ShouldNotBeNull();
        contact.Id.ShouldNotBeNullOrEmpty();
        contact.Properties.ShouldNotBeNull();
        contact.Properties.AdditionalData.ShouldContainKeyAndValue("email", "john.doe@example.com");
        contact.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_contact_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "jane.smith@example.com" },
                    { "firstname", "Jane" },
                    { "lastname", "Smith" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contactId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Contacts[contactId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contactId);
    }

    [Fact]
    public async Task Can_update_contact()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "bob.jones@example.com" },
                    { "firstname", "Bob" },
                    { "lastname", "Jones" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contactId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "phone", "555-9876" },
                    { "jobtitle", "Senior Developer" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Contacts[contactId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("phone", "555-9876");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("jobtitle", "Senior Developer");
    }

    [Fact]
    public async Task Can_delete_contact()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "delete.me@example.com" },
                    { "firstname", "Delete" },
                    { "lastname", "Me" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contactId = created!.Entity!.Id!;

        await _client.Crm.V3.Objects.Contacts[contactId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Contacts[contactId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_contacts()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "email", $"contact{i}@example.com" },
                        { "firstname", $"Contact{i}" },
                        { "lastname", "Test" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Contacts.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_contacts()
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
                            { "email", "batch1@example.com" },
                            { "firstname", "Batch" },
                            { "lastname", "One" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "email", "batch2@example.com" },
                            { "firstname", "Batch" },
                            { "lastname", "Two" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Contacts.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Can_batch_read_contacts()
    {
        // Create contacts first
        var contact1 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batchread1@example.com" },
                    { "firstname", "BatchRead" },
                    { "lastname", "One" }
                }
            }
        });

        var contact2 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batchread2@example.com" },
                    { "firstname", "BatchRead" },
                    { "lastname", "Two" }
                }
            }
        });

        // Batch read
        var batchReadInput = new BatchReadInputSimplePublicObjectId
        {
            Inputs =
            [
                new SimplePublicObjectId { Id = contact1!.Entity!.Id! },
                new SimplePublicObjectId { Id = contact2!.Entity!.Id! }
            ]
        };

        var response = await _client.Crm.V3.Objects.Contacts.Batch.Read.PostAsync(batchReadInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("email", "batchread1@example.com");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("email", "batchread2@example.com");
    }

    [Fact]
    public async Task Can_batch_update_contacts()
    {
        // Create contacts first
        var contact1 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batchupdate1@example.com" },
                    { "firstname", "BatchUpdate" },
                    { "lastname", "One" }
                }
            }
        });

        var contact2 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batchupdate2@example.com" },
                    { "firstname", "BatchUpdate" },
                    { "lastname", "Two" }
                }
            }
        });

        // Batch update
        var batchUpdateInput = new BatchInputSimplePublicObjectBatchInput
        {
            Inputs =
            [
                new SimplePublicObjectBatchInput
                {
                    Id = contact1!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "phone", "555-1111" }
                        }
                    }
                },
                new SimplePublicObjectBatchInput
                {
                    Id = contact2!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInput_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "phone", "555-2222" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Contacts.Batch.Update.PostAsync(batchUpdateInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("phone", "555-1111");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("phone", "555-2222");
    }

    [Fact]
    public async Task Can_batch_archive_contacts()
    {
        // Create contacts first
        var contact1 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batcharchive1@example.com" },
                    { "firstname", "BatchArchive" },
                    { "lastname", "One" }
                }
            }
        });

        var contact2 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batcharchive2@example.com" },
                    { "firstname", "BatchArchive" },
                    { "lastname", "Two" }
                }
            }
        });

        // Batch archive
        var batchArchiveInput = new BatchInputSimplePublicObjectId
        {
            Inputs =
            [
                new SimplePublicObjectId { Id = contact1!.Entity!.Id! },
                new SimplePublicObjectId { Id = contact2!.Entity!.Id! }
            ]
        };

        await _client.Crm.V3.Objects.Contacts.Batch.Archive.PostAsync(batchArchiveInput);

        // Verify contacts are archived (should throw exception or return not found)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Contacts[contact1.Entity!.Id!].GetAsync();
        });

        await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Contacts[contact2.Entity!.Id!].GetAsync();
        });
    }

    [Fact]
    public async Task Can_batch_upsert_contacts()
    {
        // Create one contact first
        var existingContact = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "batchupsert1@example.com" },
                    { "firstname", "BatchUpsert" },
                    { "lastname", "One" }
                }
            }
        });

        // Batch upsert: update existing and create new
        var batchUpsertInput = new BatchInputSimplePublicObjectBatchInputUpsert
        {
            Inputs =
            [
                new SimplePublicObjectBatchInputUpsert
                {
                    Id = existingContact!.Entity!.Id!,
                    Properties = new SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "phone", "555-3333" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputUpsert
                {
                    Properties = new SimplePublicObjectBatchInputUpsert_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "email", "batchupsert2@example.com" },
                            { "firstname", "BatchUpsert" },
                            { "lastname", "Two" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Contacts.Batch.Upsert.PostAsync(batchUpsertInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
        response.Results[0].Id.ShouldBe(existingContact.Entity!.Id);
        response.Results[0].Properties!.AdditionalData.ShouldContainKeyAndValue("phone", "555-3333");
        response.Results[1].Properties!.AdditionalData.ShouldContainKeyAndValue("email", "batchupsert2@example.com");
    }

    [Fact]
    public async Task Can_search_contacts()
    {
        // Create contacts to search
        await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "search1@example.com" },
                    { "firstname", "SearchTest" },
                    { "lastname", "One" }
                }
            }
        });

        await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "search2@example.com" },
                    { "firstname", "SearchTest" },
                    { "lastname", "Two" }
                }
            }
        });

        var searchRequest = new PublicObjectSearchRequest
        {
            FilterGroups =
            [
                new FilterGroup
                {
                    Filters =
                    [
                        new Filter
                        {
                            PropertyName = "firstname",
                            Operator = Filter_operator.EQ,
                            Value = "SearchTest"
                        }
                    ]
                }
            ],
            Properties = ["email", "firstname", "lastname"],
            Limit = 10
        };

        var searchResults = await _client.Crm.V3.Objects.Contacts.Search.PostAsync(searchRequest);

        searchResults.ShouldNotBeNull();
        searchResults.Results.ShouldNotBeNull();
        searchResults.Results.Count.ShouldBeGreaterThanOrEqualTo(2);
        searchResults.Results.All(r => r.Properties!.AdditionalData["firstname"] as string == "SearchTest").ShouldBeTrue();
    }

    [Fact]
    public async Task Can_merge_contacts()
    {
        var contact1 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "merge1@example.com" },
                    { "firstname", "Merge1" }
                }
            }
        });

        var contact2 = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "merge2@example.com" },
                    { "lastname", "Merge2" }
                }
            }
        });

        var mergeRequest = new PublicMergeInput
        {
            PrimaryObjectId = contact1!.Entity!.Id!,
            ObjectIdToMerge = contact2!.Entity!.Id!
        };

        var merged = await _client.Crm.V3.Objects.Contacts.Merge.PostAsync(mergeRequest);

        merged.ShouldNotBeNull();
        merged.Id.ShouldBe(contact1.Entity!.Id);
    }

    [Fact]
    public async Task Can_gdpr_delete_contact()
    {
        var contact = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "gdpr@example.com" },
                    { "firstname", "GDPR" }
                }
            }
        });

        await _client.Crm.V3.Objects.Contacts.GdprDelete.PostAsync(new PublicGdprDeleteInput
        {
            ObjectId = contact!.Entity!.Id!
        });

        await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Contacts[contact.Entity!.Id!].GetAsync();
        });
    }

    [Fact]
    public async Task Can_get_contact_with_associations()
    {
        var contact = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "assoc@example.com" },
                    { "firstname", "Assoc" }
                }
            }
        });

        var retrieved = await _client.Crm.V3.Objects.Contacts[contact!.Entity!.Id!].GetAsync(config =>
        {
            config.QueryParameters.Associations = ["companies", "deals"];
        });

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contact.Entity!.Id);
    }

    [Fact]
    public async Task Can_get_contact_with_properties_with_history()
    {
        var contact = await _client.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "history@example.com" },
                    { "firstname", "History" }
                }
            }
        });

        // Update to create history
        await _client.Crm.V3.Objects.Contacts[contact!.Entity!.Id!].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "firstname", "HistoryUpdated" }
                }
            }
        });

        var retrieved = await _client.Crm.V3.Objects.Contacts[contact.Entity!.Id!].GetAsync(config =>
        {
            config.QueryParameters.PropertiesWithHistory = ["firstname"];
        });

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contact.Entity!.Id);
        retrieved.PropertiesWithHistory.ShouldNotBeNull();
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
