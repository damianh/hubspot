using System.Net.Http.Json;

namespace DamianH.HubSpot.MockServer;

public class MarketingAndCommunicationsTests : IAsyncLifetime
{
    private HubSpotMockServer _mockServer = null!;

    public async ValueTask InitializeAsync() => _mockServer = await HubSpotMockServer.StartNew();

    public async ValueTask DisposeAsync() => await _mockServer.DisposeAsync();

    [Fact]
    public async Task MarketingEvents_CreateAndRetrieve_ShouldWork()
    {
        // This test validates that the Marketing Events API endpoints are registered
        // and respond correctly to basic HTTP requests
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        // Create a marketing event
        var createRequest = new
        {
            eventName = "Product Launch Webinar",
            eventType = "WEBINAR",
            startDateTime = DateTime.UtcNow.AddDays(7),
            endDateTime = DateTime.UtcNow.AddDays(7).AddHours(2),
            eventOrganizer = "Marketing Team",
            eventDescription = "Launch event for our new product"
        };

        var createResponse = await client.PostAsJsonAsync("/marketing/v3/marketing-events/events", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");

        // List marketing events
        var listResponse = await client.GetAsync("/marketing/v3/marketing-events/events");
        listResponse.EnsureSuccessStatusCode();
        var list = await listResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        list.ShouldNotBeNull();
        list.ShouldContainKey("results");
    }

    [Fact]
    public async Task MarketingEmails_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var createRequest = new
        {
            name = "Monthly Newsletter",
            subject = "Your Monthly Update",
            fromName = "Marketing Team",
            fromEmail = "marketing@example.com",
            state = "DRAFT"
        };

        var createResponse = await client.PostAsJsonAsync("/marketing/v3/emails", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task Campaigns_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var createRequest = new
        {
            name = "Q4 Product Launch Campaign",
            type = "EMAIL",
            subject = "New Product Available"
        };

        var createResponse = await client.PostAsJsonAsync("/marketing/v3/campaigns", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task SingleSend_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var createRequest = new
        {
            name = "Welcome Email",
            subject = "Welcome to Our Platform!",
            fromName = "Support Team",
            fromEmail = "support@example.com",
            state = "DRAFT"
        };

        var createResponse = await client.PostAsJsonAsync("/marketing/v4/singlesend", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
    }

    [Fact]
    public async Task SubscriptionsV3_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var email = "test@example.com";
        var createRequest = new
        {
            subscriptionId = "123",
            status = "SUBSCRIBED"
        };

        var createResponse = await client.PostAsJsonAsync($"/communication-preferences/v3/subscriptions/{email}", createRequest);
        createResponse.EnsureSuccessStatusCode();
        
        var getResponse = await client.GetAsync($"/communication-preferences/v3/subscriptions/{email}");
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task SubscriptionsV4_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var email = "test2@example.com";
        var createRequest = new
        {
            subscriptionId = "456",
            status = "SUBSCRIBED"
        };

        var createResponse = await client.PostAsJsonAsync($"/communication-preferences/v4/subscriptions/status/email/{email}", createRequest);
        createResponse.EnsureSuccessStatusCode();
        
        var getResponse = await client.GetAsync($"/communication-preferences/v4/subscriptions/status/email/{email}");
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task SubscriptionDefinitions_CreateAndRetrieve_ShouldWork()
    {
        using var client = new HttpClient { BaseAddress = _mockServer.Uri };

        var createRequest = new
        {
            name = "Marketing Newsletter",
            description = "Monthly marketing updates",
            isActive = true,
            isDefault = false,
            isInternal = false
        };

        var createResponse = await client.PostAsJsonAsync("/communication-preferences/v3/definitions", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");

        // List definitions
        var listResponse = await client.GetAsync("/communication-preferences/v3/definitions");
        listResponse.EnsureSuccessStatusCode();
        var list = await listResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        list.ShouldNotBeNull();
        list.ShouldContainKey("results");
    }
}
