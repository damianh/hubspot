using DamianH.HubSpot.KiotaClient.CRM.Schemas.V3;
using DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class SchemaTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMSchemasV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMSchemasV3Client(adapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task CreateSchema_ReturnsSchema()
    {
        var input = new ObjectSchemaEgg
        {
            Name = "pets",
            Labels = new ObjectTypeDefinitionLabels
            {
                Singular = "Pet",
                Plural = "Pets"
            },
            PrimaryDisplayProperty = "pet_name",
            RequiredProperties = new List<string> { "pet_name" }
        };

        var result = await _client.CrmObjectSchemas.V3.Schemas.PostAsync(input);

        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrEmpty();
        result.Name.ShouldBe("pets");
        result.Labels?.Singular.ShouldBe("Pet");
    }
}
