using DamianH.HubSpot.KiotaClient.BusinessUnits.BusinessUnits.V3;
using DamianH.HubSpot.KiotaClient.BusinessUnits.BusinessUnits.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

// Note: The Kiota-generated client for BusinessUnits doesn't match the actual API structure
// The API supports POST/GET all operations but the generated client only has User/{userId} endpoint
public class BusinessUnitsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotBusinessUnitsBusinessUnitsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotBusinessUnitsBusinessUnitsV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    // TODO: Re-enable when Kiota client generation is fixed
    // [Fact]
    // public async Task CreateBusinessUnit_ReturnsCreatedUnit()
    // {
    //     var input = new BusinessUnitInput
    //     {
    //         Name = "EMEA Division",
    //         PublicLabel = "Europe, Middle East, and Africa",
    //         Domain = "emea.example.com"
    //     };
    //
    //     var result = await _client.BusinessUnits.V3.BusinessUnits.PostAsync(input);
    //
    //     result.ShouldNotBeNull();
    //     result.Id.ShouldNotBeNullOrEmpty();
    //     result.Name.ShouldBe("EMEA Division");
    //     result.PublicLabel.ShouldBe("Europe, Middle East, and Africa");
    // }
    //
    // [Fact]
    // public async Task GetAllBusinessUnits_ReturnsAllUnits()
    // {
    //     await _client.BusinessUnits.V3.BusinessUnits.PostAsync(new BusinessUnitInput { Name = "Unit 1" });
    //     await _client.BusinessUnits.V3.BusinessUnits.PostAsync(new BusinessUnitInput { Name = "Unit 2" });
    //
    //     var result = await _client.BusinessUnits.V3.BusinessUnits.GetAsync();
    //
    //     result.ShouldNotBeNull();
    //     result.Results.ShouldNotBeNull();
    //     result.Results.Count.ShouldBe(2);
    // }
}
