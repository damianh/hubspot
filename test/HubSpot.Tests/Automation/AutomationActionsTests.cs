using DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4;
using DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class AutomationActionsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotAutomationActionsV4V4Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        var services = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };

        _client = new HubSpotAutomationActionsV4V4Client(requestAdapter);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }

    [Fact]
    public async Task CompleteCallbacks_ReturnsNoContent()
    {
        var request = new BatchInputCallbackCompletionBatchRequest
        {
            Inputs = new List<CallbackCompletionBatchRequest>
            {
                new CallbackCompletionBatchRequest
                {
                    CallbackId = "callback-1"
                }
            }
        };

        // Should not throw
        await _client.Automation.V4.Actions.Callbacks.Complete.PostAsync(request);
    }
}
