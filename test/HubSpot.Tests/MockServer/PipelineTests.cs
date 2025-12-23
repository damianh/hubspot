using DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3;
using DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class PipelineTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMPipelinesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMPipelinesV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task GetPipeline_ReturnsDefaultDealsPipeline()
    {
        var result = await _client.Crm.V3.Pipelines["deals"]["default"].GetAsync();

        result.ShouldNotBeNull();
        result.Id.ShouldBe("default");
        result.Label.ShouldBe("Sales Pipeline");
        result.Stages.ShouldNotBeNull();
        result.Stages.ShouldContain(s => s.Label == "Appointment Scheduled");
    }

    [Fact]
    public async Task CreatePipeline_ReturnsPipeline()
    {
        var input = new PipelineInput
        {
            Label = "Custom Sales Pipeline",
            DisplayOrder = 1
        };

        var result = await _client.Crm.V3.Pipelines["deals"].PostAsync(input);

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrEmpty();
        result.Label.ShouldBe("Custom Sales Pipeline");
    }
}
