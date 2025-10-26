using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class WebhooksTests : IAsyncLifetime
{
    private HubSpotMockServer? _server;
    private HubSpotWebhooksWebhooksV3Client? _client;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var authProvider = new AnonymousAuthenticationProvider();
        var adapter = new HttpClientRequestAdapter(authProvider)
        {
            BaseUrl = _server.BaseUri.ToString().TrimEnd('/')
        };
        _client = new HubSpotWebhooksWebhooksV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null)
        {
            await _server.DisposeAsync();
        }
    }

    [Fact]
    public async Task CanCreateSubscription()
    {
        var request = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        };

        var subscription = await _client!.Webhooks.V3["123"].Subscriptions.PostAsync(request);

        subscription.ShouldNotBeNull();
        subscription.Id.ShouldNotBeNull();
        subscription.EventType.ShouldBe(SubscriptionResponse_eventType.ContactCreation);
        subscription.Active.ShouldBe(true);
        subscription.CreatedAt.ShouldNotBeNull();
        subscription.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task CanListSubscriptions()
    {
        var appId = "app-456";

        // Create a few subscriptions
        var request1 = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        };
        await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request1);

        var request2 = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactDeletion,
            Active = true
        };
        await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request2);

        // List subscriptions
        var list = await _client!.Webhooks.V3[appId].Subscriptions.GetAsync();

        list.ShouldNotBeNull();
        list.Results.ShouldNotBeNull();
        list.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task CanGetSubscriptionById()
    {
        var appId = "app-789";
        var request = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactPropertyChange,
            PropertyName = "email",
            Active = true
        };

        var created = await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request);

        var subscription = await _client!.Webhooks.V3[appId].Subscriptions[int.Parse(created!.Id!)].GetAsync();

        subscription.ShouldNotBeNull();
        subscription.Id.ShouldBe(created.Id);
        subscription.EventType.ShouldBe(SubscriptionResponse_eventType.ContactPropertyChange);
        subscription.PropertyName.ShouldBe("email");
    }

    [Fact]
    public async Task CanUpdateSubscription()
    {
        var appId = "app-update";
        var createRequest = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        };

        var created = await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(createRequest);

        // Update to inactive
        var patchRequest = new SubscriptionPatchRequest
        {
            Active = false
        };

        var updated = await _client!.Webhooks.V3[appId].Subscriptions[int.Parse(created!.Id!)].PatchAsync(patchRequest);

        updated.ShouldNotBeNull();
        updated.Id.ShouldBe(created.Id);
        updated.Active.ShouldBe(false);
        (updated.UpdatedAt > created.UpdatedAt).ShouldBeTrue();
    }

    [Fact]
    public async Task CanDeleteSubscription()
    {
        var appId = "app-delete";
        var request = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        };

        var created = await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request);

        // Delete subscription
        await _client!.Webhooks.V3[appId].Subscriptions[int.Parse(created!.Id!)].DeleteAsync();

        // Verify list is empty
        var list = await _client!.Webhooks.V3[appId].Subscriptions.GetAsync();
        list.ShouldNotBeNull();
        list.Results.ShouldBeEmpty();
    }

    [Fact]
    public async Task CanBatchUpdateSubscriptions()
    {
        var appId = "app-batch";

        // Create two subscriptions
        var request1 = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        };
        var sub1 = await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request1);

        var request2 = new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactDeletion,
            Active = true
        };
        var sub2 = await _client!.Webhooks.V3[appId].Subscriptions.PostAsync(request2);

        // Batch update both to inactive
        var batchRequest = new BatchInputSubscriptionBatchUpdateRequest
        {
            Inputs =
            [
                new SubscriptionBatchUpdateRequest { Id = int.Parse(sub1!.Id!), Active = false },
                new SubscriptionBatchUpdateRequest { Id = int.Parse(sub2!.Id!), Active = false }
            ]
        };

        var batchResponse = await _client!.Webhooks.V3[appId].Subscriptions.Batch.Update.PostAsync(batchRequest);

        batchResponse.ShouldNotBeNull();
        batchResponse.Status.ShouldBe(BatchResponseSubscriptionResponse_status.COMPLETE);
        batchResponse.Results.ShouldNotBeNull();
        batchResponse.Results.Count.ShouldBe(2);
        batchResponse.Results.ShouldAllBe(r => r.Active == false);
    }

    [Fact]
    public async Task CanUpdateSettings()
    {
        var appId = "app-settings";
        var request = new SettingsChangeRequest
        {
            TargetUrl = "https://example.com/webhook",
            Throttling = new ThrottlingSettings
            {
                MaxConcurrentRequests = 10
            }
        };

        var settings = await _client!.Webhooks.V3[appId].Settings.PutAsync(request);

        settings.ShouldNotBeNull();
        settings.TargetUrl.ShouldBe("https://example.com/webhook");
        settings.Throttling.ShouldNotBeNull();
        settings.Throttling.MaxConcurrentRequests.ShouldBe(10);
        settings.CreatedAt.ShouldNotBeNull();
        settings.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task CanGetSettings()
    {
        var appId = "app-get-settings";
        
        // Create settings first
        var createRequest = new SettingsChangeRequest
        {
            TargetUrl = "https://example.com/webhook",
            Throttling = new ThrottlingSettings
            {
                MaxConcurrentRequests = 5
            }
        };
        await _client!.Webhooks.V3[appId].Settings.PutAsync(createRequest);

        // Get settings
        var settings = await _client!.Webhooks.V3[appId].Settings.GetAsync();

        settings.ShouldNotBeNull();
        settings.TargetUrl.ShouldBe("https://example.com/webhook");
    }

    [Fact]
    public async Task CanClearSettings()
    {
        var appId = "app-clear-settings";
        
        // Create settings first
        var createRequest = new SettingsChangeRequest
        {
            TargetUrl = "https://example.com/webhook"
        };
        await _client!.Webhooks.V3[appId].Settings.PutAsync(createRequest);

        // Clear settings
        await _client!.Webhooks.V3[appId].Settings.DeleteAsync();

        // Verify settings are gone (should return 404)
        var exception = await Should.ThrowAsync<Microsoft.Kiota.Abstractions.ApiException>(
            () => _client!.Webhooks.V3[appId].Settings.GetAsync()
        );
        exception.ResponseStatusCode.ShouldBe(404);
    }
}
