using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmExtensionsIntegrationTests : IAsyncLifetime
{
    private HubSpotMockServer _mockServer = null!;
    private HttpClient _httpClient = null!;
    private HttpClientRequestAdapter _adapter = null!;

    public async ValueTask InitializeAsync()
    {
        _mockServer = await HubSpotMockServer.StartNew();
        _httpClient = new HttpClient { BaseAddress = _mockServer.Uri };
        _adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: _httpClient);
    }

    public async ValueTask DisposeAsync()
    {
        _adapter?.Dispose();
        _httpClient?.Dispose();
        await _mockServer.DisposeAsync();
    }

    #region Calling Extensions Tests

    [Fact]
    public async Task CallingExtensions_CreateSettings_ShouldSucceed()
    {
        var appId = "test-calling-app-1";
        var request = new
        {
            name = "Test Calling App",
            url = "https://example.com/calling-widget",
            height = 600,
            width = 400,
            isReady = false,
            supportsCustomObjects = true
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldBe(appId);
        result.GetProperty("name").GetString().ShouldBe("Test Calling App");
        result.GetProperty("url").GetString().ShouldBe("https://example.com/calling-widget");
        result.GetProperty("height").GetInt32().ShouldBe(600);
        result.GetProperty("width").GetInt32().ShouldBe(400);
        result.GetProperty("isReady").GetBoolean().ShouldBeFalse();
        result.GetProperty("supportsCustomObjects").GetBoolean().ShouldBeTrue();
        result.TryGetProperty("createdAt", out _).ShouldBeTrue();
        result.TryGetProperty("updatedAt", out _).ShouldBeTrue();
    }

    [Fact]
    public async Task CallingExtensions_GetSettings_ShouldSucceed()
    {
        var appId = "test-calling-app-2";
        var createRequest = new { name = "My Calling App", url = "https://example.com/widget" };

        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", createRequest);

        var response = await _httpClient.GetAsync($"/crm/v3/extensions/calling/{appId}/settings");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldBe(appId);
        result.GetProperty("name").GetString().ShouldBe("My Calling App");
    }

    [Fact]
    public async Task CallingExtensions_UpdateSettings_ShouldSucceed()
    {
        var appId = "test-calling-app-3";
        var createRequest = new { name = "Original Name", url = "https://example.com/original" };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", createRequest);

        var updateRequest = new { name = "Updated Name", height = 800 };
        var response = await _httpClient.PatchAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", updateRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("name").GetString().ShouldBe("Updated Name");
        result.GetProperty("height").GetInt32().ShouldBe(800);
    }

    [Fact]
    public async Task CallingExtensions_MarkAsReady_ShouldSucceed()
    {
        var appId = "test-calling-app-4";
        var createRequest = new { name = "App to Mark Ready", isReady = false };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", createRequest);

        var response = await _httpClient.PostAsync($"/crm/v3/extensions/calling/{appId}/settings/ready", null);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync($"/crm/v3/extensions/calling/{appId}/settings");
        var result = await getResponse.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("isReady").GetBoolean().ShouldBeTrue();
    }

    [Fact]
    public async Task CallingExtensions_DeleteSettings_ShouldSucceed()
    {
        var appId = "test-calling-app-5";
        var createRequest = new { name = "App to Delete" };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{appId}/settings", createRequest);

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/extensions/calling/{appId}/settings");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync($"/crm/v3/extensions/calling/{appId}/settings");
        getResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CallingExtensions_AddRecording_ShouldSucceed()
    {
        var engagementId = "engagement-123";
        var recordingId = "recording-456";
        var request = new
        {
            url = "https://example.com/recordings/456.mp3",
            duration = 300,
            transcript = "This is a test call recording transcript."
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"/crm/v3/extensions/calling/{engagementId}/recordings/{recordingId}",
            request);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldBe(recordingId);
        result.GetProperty("engagementId").GetString().ShouldBe(engagementId);
        result.GetProperty("url").GetString().ShouldBe("https://example.com/recordings/456.mp3");
        result.GetProperty("duration").GetInt32().ShouldBe(300);
    }

    [Fact]
    public async Task CallingExtensions_GetRecordings_ShouldSucceed()
    {
        var engagementId = "engagement-789";

        var recording1 = new { url = "https://example.com/rec1.mp3", duration = 100 };
        var recording2 = new { url = "https://example.com/rec2.mp3", duration = 200 };

        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{engagementId}/recordings/rec1", recording1);
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/calling/{engagementId}/recordings/rec2", recording2);

        var response = await _httpClient.GetAsync($"/crm/v3/extensions/calling/{engagementId}/recordings");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        var recordings = result.GetProperty("results");
        recordings.GetArrayLength().ShouldBe(2);
    }

    #endregion

    #region CRM Cards Tests

    [Fact]
    public async Task CrmCards_CreateCard_ShouldSucceed()
    {
        var appId = "test-card-app-1";
        var request = new
        {
            title = "Customer Insights Card",
            fetch = new
            {
                targetUrl = "https://example.com/fetch-card",
                objectTypes = new[] { new { name = "contacts" } }
            },
            display = new
            {
                properties = new[]
                {
                    new { name = "status", label = "Status", dataType = "STATUS" },
                    new { name = "value", label = "Value", dataType = "CURRENCY" }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/cards/{appId}", request);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldNotBeNullOrEmpty();
        result.GetProperty("appId").GetString().ShouldBe(appId);
        result.GetProperty("title").GetString().ShouldBe("Customer Insights Card");
    }

    [Fact]
    public async Task CrmCards_ListCards_ShouldSucceed()
    {
        var appId = "test-card-app-2";

        var card1 = new { title = "Card 1" };
        var card2 = new { title = "Card 2" };

        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/cards/{appId}", card1);
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/cards/{appId}", card2);

        var response = await _httpClient.GetAsync($"/crm/v3/extensions/cards/{appId}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        var cards = result.GetProperty("results");
        cards.GetArrayLength().ShouldBe(2);
    }

    [Fact]
    public async Task CrmCards_UpdateCard_ShouldSucceed()
    {
        var appId = "test-card-app-3";
        var createRequest = new { title = "Original Title" };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/cards/{appId}", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<JsonElement>();
        var cardId = created.GetProperty("id").GetString();

        var updateRequest = new { title = "Updated Title" };
        var response = await _httpClient.PatchAsJsonAsync($"/crm/v3/extensions/cards/{appId}/{cardId}", updateRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("title").GetString().ShouldBe("Updated Title");
    }

    [Fact]
    public async Task CrmCards_DeleteCard_ShouldSucceed()
    {
        var appId = "test-card-app-4";
        var createRequest = new { title = "Card to Delete" };

        var createResponse = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/cards/{appId}", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<JsonElement>();
        var cardId = created.GetProperty("id").GetString();

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/extensions/cards/{appId}/{cardId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        var listResponse = await _httpClient.GetAsync($"/crm/v3/extensions/cards/{appId}");
        var result = await listResponse.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("results").GetArrayLength().ShouldBe(0);
    }

    [Fact]
    public async Task CrmCards_GetSampleResponse_ShouldSucceed()
    {
        var request = new { objectId = 12345 };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/extensions/cards/sample-response", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.TryGetProperty("results", out var results).ShouldBeTrue();
        results.GetArrayLength().ShouldBeGreaterThan(0);
    }

    #endregion

    #region Video Conferencing Tests

    [Fact]
    public async Task VideoConferencing_CreateSettings_ShouldSucceed()
    {
        var appId = "test-video-app-1";
        var request = new
        {
            name = "Zoom Integration",
            url = "https://zoom.us/create-meeting",
            isReady = true
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings", request);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldBe(appId);
        result.GetProperty("name").GetString().ShouldBe("Zoom Integration");
        result.GetProperty("url").GetString().ShouldBe("https://zoom.us/create-meeting");
        result.GetProperty("isReady").GetBoolean().ShouldBeTrue();
    }

    [Fact]
    public async Task VideoConferencing_GetSettings_ShouldSucceed()
    {
        var appId = "test-video-app-2";
        var createRequest = new { name = "Teams Integration", url = "https://teams.microsoft.com" };

        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings", createRequest);

        var response = await _httpClient.GetAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("name").GetString().ShouldBe("Teams Integration");
    }

    [Fact]
    public async Task VideoConferencing_UpdateSettings_ShouldSucceed()
    {
        var appId = "test-video-app-3";
        var createRequest = new { name = "Original Video App", url = "https://example.com/video" };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings", createRequest);

        var updateRequest = new { name = "Updated Video App", isReady = true };
        var response = await _httpClient.PatchAsJsonAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings", updateRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("name").GetString().ShouldBe("Updated Video App");
        result.GetProperty("isReady").GetBoolean().ShouldBeTrue();
    }

    [Fact]
    public async Task VideoConferencing_DeleteSettings_ShouldSucceed()
    {
        var appId = "test-video-app-4";
        var createRequest = new { name = "Video App to Delete" };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings", createRequest);

        var deleteResponse = await _httpClient.DeleteAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        var getResponse = await _httpClient.GetAsync($"/crm/v3/extensions/videoconferencing/{appId}/settings");
        getResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    #endregion

    #region Transcriptions Tests

    [Fact]
    public async Task Transcriptions_CreateTranscription_ShouldSucceed()
    {
        var engagementId = "call-12345";
        var request = new
        {
            text = "This is a complete call transcription. The customer called about their account status.",
            confidence = 0.95,
            language = "en"
        };

        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/transcriptions/{engagementId}", request);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("id").GetString().ShouldNotBeNullOrEmpty();
        result.GetProperty("engagementId").GetString().ShouldBe(engagementId);
        result.GetProperty("text").GetString().ShouldContain("call transcription");
        result.GetProperty("confidence").GetDouble().ShouldBe(0.95, 0.01);
        result.GetProperty("language").GetString().ShouldBe("en");
    }

    [Fact]
    public async Task Transcriptions_GetTranscription_ShouldSucceed()
    {
        var engagementId = "call-67890";
        var createRequest = new
        {
            text = "Meeting transcription text here.",
            confidence = 0.92,
            language = "en-US"
        };

        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/transcriptions/{engagementId}", createRequest);

        var response = await _httpClient.GetAsync($"/crm/v3/extensions/transcriptions/{engagementId}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("engagementId").GetString().ShouldBe(engagementId);
        result.GetProperty("text").GetString().ShouldContain("Meeting transcription");
        result.GetProperty("confidence").GetDouble().ShouldBe(0.92, 0.01);
    }

    [Fact]
    public async Task Transcriptions_GetNonexistent_ShouldReturnNotFound()
    {
        var response = await _httpClient.GetAsync("/crm/v3/extensions/transcriptions/nonexistent-engagement");
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Transcriptions_OverwriteTranscription_ShouldSucceed()
    {
        var engagementId = "call-11111";

        var firstRequest = new { text = "First transcription", confidence = 0.8 };
        await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/transcriptions/{engagementId}", firstRequest);

        var secondRequest = new { text = "Second improved transcription", confidence = 0.95 };
        var response = await _httpClient.PostAsJsonAsync($"/crm/v3/extensions/transcriptions/{engagementId}", secondRequest);
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var getResponse = await _httpClient.GetAsync($"/crm/v3/extensions/transcriptions/{engagementId}");
        var result = await getResponse.Content.ReadFromJsonAsync<JsonElement>();
        result.GetProperty("text").GetString().ShouldBe("Second improved transcription");
        result.GetProperty("confidence").GetDouble().ShouldBe(0.95, 0.01);
    }

    #endregion
}
