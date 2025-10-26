using DamianH.HubSpot.MockServer;
using Shouldly;
using System.Net.Http.Json;

namespace DamianH.HubSpot.Tests.MockServer;

public class CrmExtensionsTests : IAsyncLifetime
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

    // Schemas API Tests
    [Fact]
    public async Task Schemas_CreateObjectSchema_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "CustomProduct",
            labels = new { singular = "Custom Product", plural = "Custom Products" },
            primaryDisplayProperty = "name",
            properties = new[]
            {
                new
                {
                    name = "name",
                    label = "Product Name",
                    type = "string",
                    fieldType = "text"
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/schemas", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
        created.ShouldContainKey("name");
    }

    [Fact]
    public async Task Schemas_ListObjectSchemas_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/crm/v3/schemas");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task Schemas_GetObjectSchema_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "TestSchema",
            labels = new { singular = "Test", plural = "Tests" }
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/schemas", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var schemaId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/schemas/{schemaId}");
        getResponse.EnsureSuccessStatusCode();
        
        var schema = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        schema.ShouldNotBeNull();
        schema!["name"].ToString().ShouldBe("TestSchema");
    }

    [Fact]
    public async Task Schemas_UpdateObjectSchema_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "UpdateSchema",
            labels = new { singular = "Update", plural = "Updates" }
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/schemas", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var schemaId = created!["id"].ToString();

        var updateRequest = new
        {
            labels = new { singular = "Updated", plural = "Updateds" }
        };

        var updateResponse = await _httpClient.PatchAsJsonAsync($"/crm/v3/schemas/{schemaId}", updateRequest);
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Schemas_DeleteObjectSchema_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "DeleteSchema",
            labels = new { singular = "Delete", plural = "Deletes" }
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/schemas", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var schemaId = created!["id"].ToString();

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/schemas/{schemaId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    // Imports API Tests
    [Fact]
    public async Task Imports_CreateImport_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Contact Import",
            files = new[]
            {
                new
                {
                    fileName = "contacts.csv",
                    fileImportPage = new
                    {
                        hasHeader = true,
                        columnMappings = new[]
                        {
                            new { columnObjectTypeId = "0-1", columnName = "Email", propertyName = "email" }
                        }
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/imports", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Imports_ListImports_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/crm/v3/imports");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task Imports_GetImport_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Test Import"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/imports", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var importId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/imports/{importId}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Imports_CancelImport_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Cancel Import"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/imports", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var importId = created!["id"].ToString();

        var cancelResponse = await _httpClient.PostAsync($"/crm/v3/imports/{importId}/cancel", null);
        cancelResponse.EnsureSuccessStatusCode();
    }

    // Exports API Tests
    [Fact]
    public async Task Exports_CreateExport_ShouldSucceed()
    {
        var createRequest = new
        {
            exportType = "VIEW",
            exportName = "Contact Export",
            format = "CSV",
            objectType = "CONTACT"
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/exports/export/async", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Exports_GetExportStatus_ShouldSucceed()
    {
        var createRequest = new
        {
            exportType = "VIEW",
            exportName = "Test Export",
            format = "CSV",
            objectType = "CONTACT"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/exports/export/async", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var exportId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/exports/export/async/tasks/{exportId}/status");
        getResponse.EnsureSuccessStatusCode();
        
        var status = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        status.ShouldNotBeNull();
        status.ShouldContainKey("status");
    }

    // Timeline API Tests
    [Fact]
    public async Task Timeline_CreateEventTemplate_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Order Placed",
            applicationId = "app-123",
            eventTemplateType = "numeric"
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/timeline/events/templates", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Timeline_ListEventTemplates_ShouldSucceed()
    {
        var appId = "test-app";
        var response = await _httpClient.GetAsync($"/crm/v3/timeline/events/templates?appId={appId}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task Timeline_CreateEvent_ShouldSucceed()
    {
        var createRequest = new
        {
            eventTemplateId = "template-123",
            objectId = "contact-456",
            tokens = new
            {
                orderTotal = "100.00"
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/timeline/events", createRequest);
        response.EnsureSuccessStatusCode();
        
        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Timeline_GetEventById_ShouldSucceed()
    {
        var createRequest = new
        {
            eventTemplateId = "template-789",
            objectId = "contact-101"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/timeline/events", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var eventId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/timeline/events/{eventId}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Timeline_ListEventsByObjectType_ShouldSucceed()
    {
        var objectType = "contacts";
        var objectId = "12345";
        
        var response = await _httpClient.GetAsync($"/crm/v3/timeline/events/{objectType}/{objectId}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }
}
