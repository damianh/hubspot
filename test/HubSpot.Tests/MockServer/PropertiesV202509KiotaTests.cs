using DamianH.HubSpot.KiotaClient.CRM.Properties.V202509;
using DamianH.HubSpot.KiotaClient.CRM.Properties.V202509.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class PropertiesV202509KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMPropertiesV202509Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMPropertiesV202509Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_list_properties()
    {
        var result = await _client.Crm.Properties.TwoZeroTwoFiveZeroNine["contacts"].GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_create_property()
    {
        var result = await _client.Crm.Properties.TwoZeroTwoFiveZeroNine["contacts"].PostAsync(
            new PropertyCreate
            {
                Name = "test_v202509_prop",
                Label = "Test V202509 Property",
                Type = PropertyCreate_type.String,
                FieldType = PropertyCreate_fieldType.Text,
                GroupName = "contactinformation"
            });

        result.ShouldNotBeNull();
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }
}
