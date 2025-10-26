using DamianH.HubSpot.MockServer;
using Shouldly;
using System.Net.Http.Json;

namespace DamianH.HubSpot.Tests.MockServer;

public class AssociationsAndPropertiesTests : IAsyncLifetime
{
    private HubSpotMockServer _mockServer = null!;
    private HttpClient _httpClient = null!;

    public async ValueTask InitializeAsync()
    {
        _mockServer = await HubSpotMockServer.StartAsync();
        _httpClient = new HttpClient { BaseAddress = _mockServer.Uri };
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();
        await _mockServer.DisposeAsync();
    }

    // Associations V3 Tests
    [Fact]
    public async Task AssociationsV3_CreateAssociation_ShouldSucceed()
    {
        var fromObjectType = "contacts";
        var toObjectType = "companies";
        var fromObjectId = "contact-123";
        var toObjectId = "company-456";
        var associationTypeId = "1";

        var response = await _httpClient.PutAsync(
            $"/crm/v3/objects/{fromObjectType}/{fromObjectId}/associations/{toObjectType}/{toObjectId}/{associationTypeId}",
            null);
        
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AssociationsV3_ListAssociations_ShouldSucceed()
    {
        var objectType = "contacts";
        var objectId = "contact-123";
        var toObjectType = "companies";

        var response = await _httpClient.GetAsync($"/crm/v3/objects/{objectType}/{objectId}/associations/{toObjectType}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task AssociationsV3_DeleteAssociation_ShouldSucceed()
    {
        var fromObjectType = "contacts";
        var toObjectType = "companies";
        var fromObjectId = "contact-123";
        var toObjectId = "company-456";
        var associationTypeId = "1";

        var response = await _httpClient.DeleteAsync(
            $"/crm/v3/objects/{fromObjectType}/{fromObjectId}/associations/{toObjectType}/{toObjectId}/{associationTypeId}");
        
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task AssociationsV3_BatchCreate_ShouldSucceed()
    {
        var fromObjectType = "contacts";
        var toObjectType = "companies";

        var batchRequest = new
        {
            inputs = new[]
            {
                new
                {
                    from = new { id = "contact-1" },
                    to = new { id = "company-1" },
                    type = "contact_to_company"
                },
                new
                {
                    from = new { id = "contact-2" },
                    to = new { id = "company-2" },
                    type = "contact_to_company"
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"/crm/v3/associations/{fromObjectType}/{toObjectType}/batch/create", 
            batchRequest);
        
        response.EnsureSuccessStatusCode();
    }

    // Associations V4 Tests
    [Fact]
    public async Task AssociationsV4_CreateAssociations_ShouldSucceed()
    {
        var fromObjectType = "contacts";
        var toObjectType = "deals";

        var createRequest = new
        {
            inputs = new[]
            {
                new
                {
                    from = new { id = "contact-789" },
                    to = new { id = "deal-123" },
                    types = new[]
                    {
                        new { associationCategory = "HUBSPOT_DEFINED", associationTypeId = 3 }
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"/crm/v4/associations/{fromObjectType}/{toObjectType}/batch/create",
            createRequest);
        
        response.EnsureSuccessStatusCode();
    }

    // Properties V3 Tests
    [Fact]
    public async Task PropertiesV3_CreateProperty_ShouldSucceed()
    {
        var objectType = "contacts";
        var createRequest = new
        {
            name = "custom_field",
            label = "Custom Field",
            type = "string",
            fieldType = "text",
            groupName = "contactinformation"
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/properties/{objectType}", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("name");
    }

    [Fact]
    public async Task PropertiesV3_ListProperties_ShouldSucceed()
    {
        var objectType = "contacts";
        
        var response = await _httpClient.GetAsync($"/crm/v3/properties/{objectType}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task PropertiesV3_GetProperty_ShouldSucceed()
    {
        var objectType = "contacts";
        var propertyName = "email";
        
        var response = await _httpClient.GetAsync($"/crm/v3/properties/{objectType}/{propertyName}");
        response.EnsureSuccessStatusCode();
        
        var property = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        property.ShouldNotBeNull();
        property!["name"].ToString().ShouldBe(propertyName);
    }

    [Fact]
    public async Task PropertiesV3_UpdateProperty_ShouldSucceed()
    {
        var objectType = "contacts";
        var createRequest = new
        {
            name = "update_test",
            label = "Update Test",
            type = "string",
            fieldType = "text"
        };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/properties/{objectType}", createRequest);
        createResponse.EnsureSuccessStatusCode();

        var updateRequest = new
        {
            label = "Updated Label"
        };

        var updateResponse = await _httpClient.PatchAsJsonAsync(
            $"/crm/v3/properties/{objectType}/update_test",
            updateRequest);
        
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task PropertiesV3_DeleteProperty_ShouldSucceed()
    {
        var objectType = "contacts";
        var createRequest = new
        {
            name = "delete_test",
            label = "Delete Test",
            type = "string",
            fieldType = "text"
        };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/properties/{objectType}", createRequest);
        createResponse.EnsureSuccessStatusCode();

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/properties/{objectType}/delete_test");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PropertiesV3_CreatePropertyGroup_ShouldSucceed()
    {
        var objectType = "contacts";
        var createRequest = new
        {
            name = "custom_group",
            label = "Custom Group",
            displayOrder = 10
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/properties/{objectType}/groups", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("name");
    }

    [Fact]
    public async Task PropertiesV3_ListPropertyGroups_ShouldSucceed()
    {
        var objectType = "contacts";
        
        var response = await _httpClient.GetAsync($"/crm/v3/properties/{objectType}/groups");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    // Pipelines V3 Tests
    [Fact]
    public async Task PipelinesV3_CreatePipeline_ShouldSucceed()
    {
        var objectType = "deals";
        var createRequest = new
        {
            label = "Sales Pipeline",
            displayOrder = 1,
            stages = new[]
            {
                new
                {
                    label = "Qualified",
                    displayOrder = 0,
                    metadata = new { probability = "0.25" }
                },
                new
                {
                    label = "Closed Won",
                    displayOrder = 1,
                    metadata = new { probability = "1.0" }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/pipelines/{objectType}", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task PipelinesV3_ListPipelines_ShouldSucceed()
    {
        var objectType = "deals";
        
        var response = await _httpClient.GetAsync($"/crm/v3/pipelines/{objectType}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task PipelinesV3_GetPipeline_ShouldSucceed()
    {
        var objectType = "deals";
        var createRequest = new
        {
            label = "Test Pipeline",
            displayOrder = 1
        };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/pipelines/{objectType}", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var pipelineId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/pipelines/{objectType}/{pipelineId}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task PipelinesV3_UpdatePipeline_ShouldSucceed()
    {
        var objectType = "deals";
        var createRequest = new
        {
            label = "Update Pipeline",
            displayOrder = 1
        };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/pipelines/{objectType}", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var pipelineId = created!["id"].ToString();

        var updateRequest = new
        {
            label = "Updated Pipeline"
        };

        var updateResponse = await _httpClient.PatchAsJsonAsync(
            $"/crm/v3/pipelines/{objectType}/{pipelineId}",
            updateRequest);
        
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task PipelinesV3_DeletePipeline_ShouldSucceed()
    {
        var objectType = "deals";
        var createRequest = new
        {
            label = "Delete Pipeline",
            displayOrder = 1
        };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/pipelines/{objectType}", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var pipelineId = created!["id"].ToString();

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/pipelines/{objectType}/{pipelineId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    // Owners V3 Tests
    [Fact]
    public async Task OwnersV3_ListOwners_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/crm/v3/owners");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task OwnersV3_GetOwner_ShouldSucceed()
    {
        // First create an owner via the repository (this would normally be seeded)
        var createRequest = new
        {
            email = "owner@example.com",
            firstName = "Test",
            lastName = "Owner"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/owners", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var ownerId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/owners/{ownerId}");
        getResponse.EnsureSuccessStatusCode();
        
        var owner = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        owner.ShouldNotBeNull();
        owner!["id"].ToString().ShouldBe(ownerId);
    }
}
