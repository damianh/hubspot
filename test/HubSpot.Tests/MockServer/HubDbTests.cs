using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class HubDbTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCMSHubdbV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCMSHubdbV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task CreateTable_ReturnsTable()
    {
        var input = new HubDbTableV3Request
        {
            Name = "products",
            Label = "Products"
        };

        var result = await _client.Cms.V3.Hubdb.Tables.PostAsync(input);

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrEmpty();
        result.Name.ShouldBe("products");
    }
}
