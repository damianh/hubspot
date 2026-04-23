using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3;
using DamianH.HubSpot.KiotaClient.CRM.Contacts.V3.Models;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models;
using DamianH.HubSpot.MockServer;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot;

public class WebhookDeliveryTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMContactsV3Client _crmClient = null!;
    private HubSpotWebhooksWebhooksV3Client _webhookClient = null!;
    private WebhookReceiver _receiver = null!;

    private const string AppId = "12345";

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();

        var authProvider = new AnonymousAuthenticationProvider();
        var adapter = new HttpClientRequestAdapter(authProvider) { BaseUrl = _server.Uri.ToString() };
        _crmClient = new HubSpotCRMContactsV3Client(adapter);

        var webhookAdapter = new HttpClientRequestAdapter(authProvider)
        {
            BaseUrl = _server.BaseUri.ToString().TrimEnd('/')
        };
        _webhookClient = new HubSpotWebhooksWebhooksV3Client(webhookAdapter);

        _receiver = new WebhookReceiver();
        await _receiver.StartAsync();

        // Configure webhook settings pointing to our receiver
        await _webhookClient.Webhooks.V3[AppId].Settings.PutAsync(new SettingsChangeRequest
        {
            TargetUrl = _receiver.Url
        });
    }

    public async ValueTask DisposeAsync()
    {
        _receiver.Stop();
        await _server.DisposeAsync();
    }

    [Fact]
    [Obsolete]
    public async Task ContactCreation_DeliversCreationEvent()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        });

        var created = await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "email", "webhook@test.com" } }
            }
        });

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 3000);

        payload.ShouldNotBeNull();
        payload!.Count.ShouldBeGreaterThan(0);

        var evt = payload[0]!.AsObject();
        evt["subscriptionType"]!.GetValue<string>().ShouldBe("contact.creation");
        evt["objectId"]!.GetValue<string>().ShouldBe(created!.Entity!.Id);
    }

    [Fact]
    [Obsolete]
    public async Task ContactPropertyChange_DeliversPropertyChangeEvent()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactPropertyChange,
            PropertyName = "firstname",
            Active = true
        });

        var created = await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "firstname", "Alice" } }
            }
        });

        // Consume the creation call (no subscription for it)
        _receiver.Clear();

        await _crmClient.Crm.V3.Objects.Contacts[created!.Entity!.Id].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "firstname", "Bob" } }
            }
        });

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 3000);

        payload.ShouldNotBeNull();
        var evt = payload![0]!.AsObject();
        evt["propertyName"]!.GetValue<string>().ShouldBe("firstname");
        evt["propertyValue"]!.GetValue<string>().ShouldBe("Bob");
        evt["objectId"]!.GetValue<string>().ShouldBe(created.Entity.Id);
    }

    [Fact]
    [Obsolete]
    public async Task ContactDeletion_DeliversDeletionEvent()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactDeletion,
            Active = true
        });

        var created = await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "email", "delete@test.com" } }
            }
        });

        _receiver.Clear();

        await _crmClient.Crm.V3.Objects.Contacts[created!.Entity!.Id].DeleteAsync();

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 3000);

        payload.ShouldNotBeNull();
        var evt = payload![0]!.AsObject();
        evt["objectId"]!.GetValue<string>().ShouldBe(created.Entity.Id);
    }

    [Fact]
    [Obsolete]
    public async Task InactiveSubscription_DoesNotDeliver()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = false
        });

        await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "email", "inactive@test.com" } }
            }
        });

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 500);
        payload.ShouldBeNull();
    }

    [Fact]
    [Obsolete]
    public async Task PropertyChangeSubscription_WithNonMatchingPropertyName_DoesNotDeliver()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactPropertyChange,
            PropertyName = "lastname",
            Active = true
        });

        var created = await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "firstname", "Alice" } }
            }
        });
        _receiver.Clear();

        await _crmClient.Crm.V3.Objects.Contacts[created!.Entity!.Id].PatchAsync(new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object> { { "firstname", "Bob" } }  // not "lastname"
            }
        });

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 500);
        payload.ShouldBeNull();
    }

    [Fact]
    [Obsolete]
    public async Task MultipleActiveSubscriptions_AllFire()
    {
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        });
        await _webhookClient.Webhooks.V3[AppId].Subscriptions.PostAsync(new SubscriptionCreateRequest
        {
            EventType = SubscriptionCreateRequest_eventType.ContactCreation,
            Active = true
        });

        await _crmClient.Crm.V3.Objects.Contacts.PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "email", "multi@test.com" } }
            }
        });

        var payload = await _receiver.WaitForPayloadAsync(timeoutMs: 3000);

        payload.ShouldNotBeNull();
        payload!.Count.ShouldBe(2);
    }

    /// <summary>
    /// Minimal HTTP listener that captures incoming webhook POST payloads.
    /// </summary>
    private sealed class WebhookReceiver
    {
        private readonly HttpListener _listener = new();
        private readonly SemaphoreSlim _signal = new(0, 1);
        private JsonArray? _lastPayload;
        private Task? _listenTask;

        public string Url { get; private set; } = string.Empty;

        public async Task StartAsync()
        {
            // Pick a random available port
            var port = GetFreePort();
            Url = $"http://localhost:{port}/webhook/";
            _listener.Prefixes.Add(Url);
            _listener.Start();
            _listenTask = ListenAsync();
            await Task.Yield();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void Clear()
        {
            _lastPayload = null;
            // Drain the semaphore
            while (_signal.CurrentCount > 0)
            {
                _signal.Wait(0);
            }
        }

        public async Task<JsonArray?> WaitForPayloadAsync(int timeoutMs = 3000)
        {
            var signalled = await _signal.WaitAsync(timeoutMs);
            return signalled ? _lastPayload : null;
        }

        private async Task ListenAsync()
        {
            while (_listener.IsListening)
            {
                try
                {
                    var ctx = await _listener.GetContextAsync();
                    using var reader = new StreamReader(ctx.Request.InputStream);
                    var body = await reader.ReadToEndAsync();
                    _lastPayload = JsonSerializer.Deserialize<JsonArray>(body);
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Close();
                    _signal.Release();
                }
                catch (HttpListenerException)
                {
                    // Listener stopped
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }

        private static int GetFreePort()
        {
            using var listener = new System.Net.Sockets.TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
