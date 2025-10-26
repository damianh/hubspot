using System.Net.Http.Json;
using System.Text;

namespace DamianH.HubSpot.MockServer;

public class ListsFilesEventsTests : IAsyncLifetime
{
    private HubSpotMockServer _mockServer = null!;
    private HttpClient _httpClient = null!;

    public async ValueTask InitializeAsync()
    {
        _mockServer = await HubSpotMockServer.StartNew();
        _httpClient = new HttpClient { BaseAddress = _mockServer.Uri };
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();
        await _mockServer.DisposeAsync();
    }

    // Lists API Tests
    [Fact]
    public async Task Lists_CreateList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "VIP Customers",
            processingType = "DYNAMIC",
            objectTypeId = "0-1", // Contacts
            filterBranch = new
            {
                filterBranchType = "AND",
                filters = new[]
                {
                    new
                    {
                        filterType = "PROPERTY",
                        property = "lifecyclestage",
                        operation = "EQ",
                        value = "customer"
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("listId");
    }

    [Fact]
    public async Task Lists_GetList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Test List",
            processingType = "STATIC",
            objectTypeId = "0-1"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var listId = created!["listId"].ToString();

        var getResponse = await _httpClient.GetAsync($"/crm/v3/lists/{listId}");
        getResponse.EnsureSuccessStatusCode();

        var list = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        list.ShouldNotBeNull();
        list!["listId"].ToString().ShouldBe(listId);
    }

    [Fact]
    public async Task Lists_UpdateList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Original List",
            processingType = "STATIC",
            objectTypeId = "0-1"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var listId = created!["listId"].ToString();

        var updateRequest = new
        {
            name = "Updated List"
        };

        var updateResponse = await _httpClient.PatchAsJsonAsync($"/crm/v3/lists/{listId}", updateRequest);
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Lists_DeleteList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Delete List",
            processingType = "STATIC",
            objectTypeId = "0-1"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var listId = created!["listId"].ToString();

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/lists/{listId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Lists_ListAll_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/crm/v3/lists");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("lists");
    }

    [Fact]
    public async Task Lists_AddMembersToList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Members List",
            processingType = "STATIC",
            objectTypeId = "0-1"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var listId = created!["listId"].ToString();

        var membersRequest = new
        {
            recordIds = new[] { "101", "102", "103" }
        };

        var addResponse = await _httpClient.PutAsJsonAsync($"/crm/v3/lists/{listId}/memberships/add", membersRequest);
        addResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Lists_RemoveMembersFromList_ShouldSucceed()
    {
        var createRequest = new
        {
            name = "Remove Members List",
            processingType = "STATIC",
            objectTypeId = "0-1"
        };

        var createResponse = await _httpClient.PostAsJsonAsync("/crm/v3/lists", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var listId = created!["listId"].ToString();

        var membersRequest = new
        {
            recordIds = new[] { "101" }
        };

        var removeResponse = await _httpClient.PutAsJsonAsync($"/crm/v3/lists/{listId}/memberships/remove", membersRequest);
        removeResponse.EnsureSuccessStatusCode();
    }

    // Files API Tests
    [Fact]
    public async Task Files_UploadFile_ShouldSucceed()
    {
        var fileContent = Encoding.UTF8.GetBytes("Test file content");
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(fileContent), "file", "test.txt");

        var response = await _httpClient.PostAsync("/files/v3/files", formData);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Files_GetFile_ShouldSucceed()
    {
        var fileContent = Encoding.UTF8.GetBytes("Get test content");
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(fileContent), "file", "gettest.txt");

        var createResponse = await _httpClient.PostAsync("/files/v3/files", formData);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var fileId = created!["id"].ToString();

        var getResponse = await _httpClient.GetAsync($"/files/v3/files/{fileId}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Files_ListFiles_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/files/v3/files");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task Files_UpdateFile_ShouldSucceed()
    {
        var fileContent = Encoding.UTF8.GetBytes("Update test content");
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(fileContent), "file", "updatetest.txt");

        var createResponse = await _httpClient.PostAsync("/files/v3/files", formData);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var fileId = created!["id"].ToString();

        var updateRequest = new
        {
            name = "Updated File Name"
        };

        var updateResponse = await _httpClient.PatchAsJsonAsync($"/files/v3/files/{fileId}", updateRequest);
        updateResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Files_DeleteFile_ShouldSucceed()
    {
        var fileContent = Encoding.UTF8.GetBytes("Delete test content");
        using var formData = new MultipartFormDataContent();
        formData.Add(new ByteArrayContent(fileContent), "file", "deletetest.txt");

        var createResponse = await _httpClient.PostAsync("/files/v3/files", formData);
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        var fileId = created!["id"].ToString();

        var deleteResponse = await _httpClient.DeleteAsync($"/files/v3/files/{fileId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    // Events API Tests
    [Fact]
    public async Task Events_CreateEvent_ShouldSucceed()
    {
        var createRequest = new
        {
            eventName = "page_view",
            email = "visitor@example.com",
            properties = new
            {
                page_url = "https://example.com/products",
                page_title = "Products"
            }
        };

        var response = await _httpClient.PostAsync("/events/v3/send",
            new StringContent(System.Text.Json.JsonSerializer.Serialize(createRequest), Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Events_SendBatchEvents_ShouldSucceed()
    {
        var batchRequest = new
        {
            inputs = new[]
            {
                new
                {
                    eventName = "purchase",
                    email = "customer1@example.com",
                    properties = new { amount = "99.99" }
                },
                new
                {
                    eventName = "purchase",
                    email = "customer2@example.com",
                    properties = new { amount = "149.99" }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/events/v3/send/batch", batchRequest);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Events_CreateCustomEvent_ShouldSucceed()
    {
        var createRequest = new
        {
            eventName = "custom_event_type",
            objectId = "contact-123",
            occurredAt = DateTime.UtcNow,
            properties = new
            {
                custom_property = "custom_value"
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/events/v3/events", createRequest);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Events_ListEvents_ShouldSucceed()
    {
        var response = await _httpClient.GetAsync("/events/v3/events?objectType=contact&objectId=123");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }
}
