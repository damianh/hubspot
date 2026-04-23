using DamianH.HubSpot.KiotaClient.CRM.Associations.V202509;
using DamianH.HubSpot.KiotaClient.CRM.Associations.V202509.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class AssociationsV202509KiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMAssociationsV202509Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMAssociationsV202509Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_batch_read_associations()
    {
        var result = await _client.Crm.Associations.TwoZeroTwoFiveZeroNine["contacts"]["companies"].Batch.Read.PostAsync(
            new BatchInputPublicFetchAssociationsBatchRequest
            {
                Inputs = [new PublicFetchAssociationsBatchRequest { Id = "contact-1" }]
            });

        result.ShouldNotBeNull();
        result.Status.ShouldNotBeNull();
    }
}
