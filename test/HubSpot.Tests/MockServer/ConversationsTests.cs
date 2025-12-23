namespace DamianH.HubSpot.MockServer;

public class ConversationsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;

    public async ValueTask InitializeAsync()
        => _server = await HubSpotMockServer.StartNew();

    public async ValueTask DisposeAsync()
        => await _server.DisposeAsync();

    [Fact]
    public async Task ListConversations_ShouldReturnEmpty_Initially()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };
        var response = await httpClient.GetAsync("/conversations/v3/conversations");

        response.IsSuccessStatusCode.ShouldBeTrue();

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain("results");
    }

    [Fact]
    public async Task CreateCustomChannel_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        var channelJson = @"{
            ""name"": ""My Custom Channel"",
            ""accountId"": ""test-account-123""
        }";

        var content = new StringContent(channelJson, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/conversations/v3/custom-channels", content);

        response.IsSuccessStatusCode.ShouldBeTrue();

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.ShouldContain("My Custom Channel");
        responseBody.ShouldContain("id");
    }

    [Fact]
    public async Task ListCustomChannels_ShouldReturnCreatedChannels()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        // Create a channel first
        var channelJson = @"{""name"": ""Test Channel""}";
        var content = new StringContent(channelJson, System.Text.Encoding.UTF8, "application/json");
        var createResponse = await httpClient.PostAsync("/conversations/v3/custom-channels", content);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();

        // List channels
        var listResponse = await httpClient.GetAsync("/conversations/v3/custom-channels");
        listResponse.IsSuccessStatusCode.ShouldBeTrue();

        var responseBody = await listResponse.Content.ReadAsStringAsync();
        responseBody.ShouldContain("Test Channel");
        responseBody.ShouldContain("results");
    }

    [Fact]
    public async Task UpdateCustomChannel_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        // Create a channel
        var channelJson = @"{""name"": ""Original Name""}";
        var content = new StringContent(channelJson, System.Text.Encoding.UTF8, "application/json");
        var createResponse = await httpClient.PostAsync("/conversations/v3/custom-channels", content);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();

        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createDoc = System.Text.Json.JsonDocument.Parse(createBody);
        var channelId = createDoc.RootElement.GetProperty("id").GetString()!;

        // Update the channel
        var updateJson = @"{""name"": ""Updated Name"", ""active"": false}";
        var updateContent = new StringContent(updateJson, System.Text.Encoding.UTF8, "application/json");
        var updateRequest = new HttpRequestMessage(HttpMethod.Patch, $"/conversations/v3/custom-channels/{channelId}")
        {
            Content = updateContent
        };

        var updateResponse = await httpClient.SendAsync(updateRequest);
        updateResponse.IsSuccessStatusCode.ShouldBeTrue();

        var updateBody = await updateResponse.Content.ReadAsStringAsync();
        updateBody.ShouldContain("Updated Name");
        updateBody.ShouldContain("\"active\":false");
    }

    [Fact]
    public async Task DeleteCustomChannel_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        // Create a channel
        var channelJson = @"{""name"": ""To Be Deleted""}";
        var content = new StringContent(channelJson, System.Text.Encoding.UTF8, "application/json");
        var createResponse = await httpClient.PostAsync("/conversations/v3/custom-channels", content);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();

        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createDoc = System.Text.Json.JsonDocument.Parse(createBody);
        var channelId = createDoc.RootElement.GetProperty("id").GetString()!;

        // Delete the channel
        var deleteResponse = await httpClient.DeleteAsync($"/conversations/v3/custom-channels/{channelId}");
        deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        // Verify it's deleted
        var getResponse = await httpClient.GetAsync($"/conversations/v3/custom-channels/{channelId}");
        getResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GenerateVisitorToken_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        var tokenJson = @"{""email"": ""visitor@example.com""}";
        var content = new StringContent(tokenJson, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/conversations/v3/visitor-identification/tokens/create", content);

        response.IsSuccessStatusCode.ShouldBeTrue();

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.ShouldContain("token");
        responseBody.ShouldContain("visitorId");
        responseBody.ShouldContain("visitor@example.com");
    }

    [Fact]
    public async Task GetVisitorToken_ByVisitorId_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        // Generate a token first
        var tokenJson = @"{""email"": ""test@example.com""}";
        var content = new StringContent(tokenJson, System.Text.Encoding.UTF8, "application/json");
        var createResponse = await httpClient.PostAsync("/conversations/v3/visitor-identification/tokens/create", content);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();

        var createBody = await createResponse.Content.ReadAsStringAsync();
        var createDoc = System.Text.Json.JsonDocument.Parse(createBody);
        var visitorId = createDoc.RootElement.GetProperty("visitorId").GetString()!;

        // Get the token by visitor ID
        var getResponse = await httpClient.GetAsync($"/conversations/v3/visitor-identification/tokens/visitor/{visitorId}");
        getResponse.IsSuccessStatusCode.ShouldBeTrue();

        var getBody = await getResponse.Content.ReadAsStringAsync();
        getBody.ShouldContain("test@example.com");
        getBody.ShouldContain("token");
    }

    [Fact]
    public async Task IdentifyVisitor_ShouldSucceed()
    {
        var httpClient = new HttpClient { BaseAddress = _server.Uri };

        var identifyJson = @"{
            ""visitorId"": ""visitor-123"",
            ""contactId"": ""contact-456""
        }";

        var content = new StringContent(identifyJson, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/conversations/v3/visitor-identification/identify", content);

        response.IsSuccessStatusCode.ShouldBeTrue();

        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.ShouldContain("visitor-123");
        responseBody.ShouldContain("contact-456");
        responseBody.ShouldContain("\"identified\":true");
    }
}
