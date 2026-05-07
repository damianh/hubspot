using DamianH.HubSpot.KiotaClient.CMS.UrlMappings.V3;
using DamianH.HubSpot.KiotaClient.CMS.UrlMappings.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Shouldly;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer;

public sealed class CmsUrlMappingsKiotaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCMSUrlMappingsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCMSUrlMappingsV3Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_create_and_get_url_mapping()
    {
        var body = new UrlMapping
        {
            RoutePrefix = "/old-path",
            Destination = "https://example.com/new-path",
            RedirectStyle = 301
        };

        using var stream = await _client.UrlMappings.V3.UrlMappings.PostAsync(body);
        stream.ShouldNotBeNull();
        using var doc = await JsonDocument.ParseAsync(stream);
        var id = doc.RootElement.GetProperty("id").GetInt64();
        id.ShouldBeGreaterThan(0);

        var retrieved = await _client.UrlMappings.V3.UrlMappings[id].GetAsync();
        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(id);
        retrieved.RoutePrefix.ShouldBe("/old-path");
        retrieved.Destination.ShouldBe("https://example.com/new-path");
    }

    [Fact]
    public async Task Can_list_url_mappings()
    {
        var body1 = new UrlMapping { RoutePrefix = "/path-a", Destination = "https://example.com/a", RedirectStyle = 301 };
        var body2 = new UrlMapping { RoutePrefix = "/path-b", Destination = "https://example.com/b", RedirectStyle = 302 };

        using var s1 = await _client.UrlMappings.V3.UrlMappings.PostAsync(body1);
        using var s2 = await _client.UrlMappings.V3.UrlMappings.PostAsync(body2);

        var list = await _client.UrlMappings.V3.UrlMappings.GetAsync();
        list.ShouldNotBeNull();
        list.Count.ShouldBeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task Can_delete_url_mapping()
    {
        var body = new UrlMapping { RoutePrefix = "/to-delete", Destination = "https://example.com/dest", RedirectStyle = 301 };

        using var stream = await _client.UrlMappings.V3.UrlMappings.PostAsync(body);
        stream.ShouldNotBeNull();
        using var doc = await JsonDocument.ParseAsync(stream);
        var id = doc.RootElement.GetProperty("id").GetInt64();

        await _client.UrlMappings.V3.UrlMappings[id].DeleteAsync();

        await Should.ThrowAsync<Exception>(() => _client.UrlMappings.V3.UrlMappings[id].GetAsync());
    }
}
