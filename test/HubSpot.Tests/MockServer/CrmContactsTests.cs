using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmContactsTests : IAsyncLifetime
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
    public async Task V3_Can_create_contact()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "test@example.com" },
                    { "firstname", "John" },
                    { "lastname", "Doe" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contact = created!.Entity!;

        contact.ShouldNotBeNull();
        contact.Id.ShouldNotBeNullOrEmpty();
        contact.Properties.ShouldNotBeNull();
        contact.Properties.AdditionalData.ShouldContainKeyAndValue("email", "test@example.com");
        contact.Properties.AdditionalData.ShouldContainKeyAndValue("firstname", "John");
        contact.Properties.AdditionalData.ShouldContainKeyAndValue("lastname", "Doe");
        contact.CreatedAt.ShouldNotBeNull();
        contact.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task V3_Can_get_contact_with_no_properties()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "jane@example.com" },
                    { "firstname", "Jane" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contactId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Contacts[contactId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contactId);
        retrieved.Properties.ShouldNotBeNull();
        retrieved.Properties.AdditionalData.Count.ShouldBe(0);
    }

    [Fact]
    public async Task V3_Can_get_contact_with_specified_properties()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "bob@example.com" },
                    { "firstname", "Bob" },
                    { "lastname", "Smith" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        var contactId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Contacts[contactId].GetAsync(config =>
        {
            config.QueryParameters.Properties = ["email", "firstname"];
        });

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(contactId);
        retrieved.Properties.ShouldNotBeNull();
        retrieved.Properties.AdditionalData.Count.ShouldBe(2);
        retrieved.Properties.AdditionalData.ShouldContainKeyAndValue("email", "bob@example.com");
        retrieved.Properties.AdditionalData.ShouldContainKeyAndValue("firstname", "Bob");
        retrieved.Properties.AdditionalData.ContainsKey("lastname").ShouldBeFalse();
    }

    [Fact]
    public async Task V3_Can_update_contact_with_changed_property()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "alice@example.com" },
                    { "firstname", "Alice" }
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
                    { "firstname", "Alicia" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Contacts[contactId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Id.ShouldBe(contactId);
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("firstname", "Alicia");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("email", "alice@example.com");
        updated.UpdatedAt.ShouldNotBe(updated.CreatedAt);
    }

    [Fact]
    public async Task V3_Can_update_contact_with_new_property()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "charlie@example.com" }
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
                    { "phone", "555-1234" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Contacts[contactId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Id.ShouldBe(contactId);
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("email", "charlie@example.com");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("phone", "555-1234");
    }

    [Fact]
    public async Task V3_Can_list_contacts()
    {
        var input1 = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "contact1@example.com" }
                }
            }
        };
        var input2 = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "contact2@example.com" }
                }
            }
        };

        await _client.Crm.V3.Objects.Contacts.PostAsync(input1);
        await _client.Crm.V3.Objects.Contacts.PostAsync(input2);

        var list = await _client.Crm.V3.Objects.Contacts.GetAsync(config =>
        {
            config.QueryParameters.Properties = ["email"];
        });

        list.ShouldNotBeNull();
        list.Results.ShouldNotBeNull();
        list.Results.Count.ShouldBeGreaterThanOrEqualTo(2);
        list.Results.Any(c => c.Properties.AdditionalData["email"] as string == "contact1@example.com").ShouldBeTrue();
        list.Results.Any(c => c.Properties.AdditionalData["email"] as string == "contact2@example.com").ShouldBeTrue();
    }

    [Fact]
    public async Task V3_Can_list_contacts_with_paging()
    {
        for (var i = 0; i < 15; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "email", $"contact{i}@example.com" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Contacts.PostAsync(input);
        }

        var page1 = await _client.Crm.V3.Objects.Contacts.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        page1.ShouldNotBeNull();
        page1.Results.ShouldNotBeNull();
        page1.Results.Count.ShouldBe(10);
        page1.Paging.ShouldNotBeNull();
        page1.Paging.Next.ShouldNotBeNull();

        var page2 = await _client.Crm.V3.Objects.Contacts.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
            config.QueryParameters.After = page1.Paging.Next.After;
        });

        page2.ShouldNotBeNull();
        page2.Results.ShouldNotBeNull();
        page2.Results.Count.ShouldBeGreaterThanOrEqualTo(5);
    }

    [Fact]
    public async Task V3_Can_archive_contact()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "todelete@example.com" }
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
    }

    [Fact]
    public async Task V3_Can_list_archived_contacts()
    {
        var input1 = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "archived1@example.com" }
                }
            }
        };
        var input2 = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "email", "archived2@example.com" }
                }
            }
        };

        var created1 = await _client.Crm.V3.Objects.Contacts.PostAsync(input1);
        var created2 = await _client.Crm.V3.Objects.Contacts.PostAsync(input2);

        await _client.Crm.V3.Objects.Contacts[created1!.Entity!.Id!].DeleteAsync();
        await _client.Crm.V3.Objects.Contacts[created2!.Entity!.Id!].DeleteAsync();

        var archivedList = await _client.Crm.V3.Objects.Contacts.GetAsync(config =>
        {
            config.QueryParameters.Archived = true;
            config.QueryParameters.Properties = ["email"];
        });

        archivedList.ShouldNotBeNull();
        archivedList.Results.ShouldNotBeNull();
        archivedList.Results.Count.ShouldBeGreaterThanOrEqualTo(2);
        archivedList.Results.Any(c => c.Properties.AdditionalData["email"] as string == "archived1@example.com").ShouldBeTrue();
        archivedList.Results.Any(c => c.Properties.AdditionalData["email"] as string == "archived2@example.com").ShouldBeTrue();
    }

    public ValueTask DisposeAsync() => _server.DisposeAsync();
}
