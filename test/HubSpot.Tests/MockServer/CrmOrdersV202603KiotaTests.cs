using DamianH.HubSpot.KiotaClient.CRM.Objects.V202603;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V202603.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Shouldly;

namespace DamianH.HubSpot.MockServer;

public sealed class CrmOrdersV202603KiotaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMObjectsV202603Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMObjectsV202603Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_order()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoSixZeroThree["orders"].PostAsync(
            new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object> { { "name", "Test Orders" } }
                }
            });

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Can_list_orders()
    {
        var result = await _client.Crm.Objects.TwoZeroTwoSixZeroThree["orders"].GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }
}
