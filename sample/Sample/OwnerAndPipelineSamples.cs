using System.Net.Http.Json;
using DamianH.HubSpot.MockServer;

namespace DamianH.HubSpot.Sample;

/// <summary>
/// Demonstrates owner listing and pipeline management using the mock server.
/// </summary>
public class OwnerAndPipelineSamples : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HttpClient _httpClient = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _httpClient = new HttpClient { BaseAddress = _server.Uri };
    }

    public async ValueTask DisposeAsync()
    {
        _httpClient?.Dispose();
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task ListOwners_ReturnsPreSeededOwners()
    {
        // The mock server pre-seeds a set of owners out of the box
        var response = await _httpClient.GetAsync("/crm/v3/owners");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.ShouldNotBeNull();
        result.ShouldContainKey("results");
    }

    [Fact]
    public async Task GetOwnerById_ReturnsSingleOwner()
    {
        var response = await _httpClient.GetAsync("/crm/v3/owners/1");
        response.EnsureSuccessStatusCode();

        var owner = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        owner.ShouldNotBeNull();
        owner.ShouldContainKey("id");
        owner.ShouldContainKey("email");
    }

    [Fact]
    public async Task CreatePipeline_WithStages_ReturnsNewPipeline()
    {
        var pipeline = new
        {
            label = "Custom Sales Pipeline",
            displayOrder = 1,
            stages = new object[]
            {
                new { label = "Lead", displayOrder = 0, metadata = new { probability = "0.1" } },
                new { label = "Qualified", displayOrder = 1, metadata = new { probability = "0.3" } },
                new { label = "Proposal", displayOrder = 2, metadata = new { probability = "0.6" } },
                new { label = "Closed Won", displayOrder = 3, metadata = new { probability = "1.0", isClosed = "true" } }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/crm/v3/pipelines/deals", pipeline);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        created.ShouldNotBeNull();
        created.ShouldContainKey("id");
        created["label"].ToString().ShouldBe("Custom Sales Pipeline");
    }

    [Fact]
    public async Task GetDefaultDealsPipeline_HasPreSeededStages()
    {
        // The mock server comes with a pre-seeded default deals pipeline
        var response = await _httpClient.GetAsync("/crm/v3/pipelines/deals/default");
        response.EnsureSuccessStatusCode();

        var pipeline = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        pipeline.ShouldNotBeNull();
        pipeline.ShouldContainKey("id");
        pipeline.ShouldContainKey("stages");
    }
}
