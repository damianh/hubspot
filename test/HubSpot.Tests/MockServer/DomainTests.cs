using DamianH.HubSpot.KiotaClient.CMS.Domains.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class DomainTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCMSDomainsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCMSDomainsV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task GetAllDomains_ReturnsEmptyList()
    {
        var result = await _client.Cms.V3.Domains.EmptyPathSegment.GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }
}
