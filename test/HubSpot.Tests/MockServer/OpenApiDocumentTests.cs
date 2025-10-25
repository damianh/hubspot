using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

public class OpenApiDocumentTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;

    public async ValueTask InitializeAsync()
    {
        var services = new ServiceCollection()
            .BuildServiceProvider();

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        _outputHelper.WriteLine(_server.Uri.ToString());
    }

    [Theory]
    [InlineData("/api-catalog-public/v1/apis/crm/v3/objects/companies")]
    [InlineData("/api-catalog-public/v1/apis/crm/v3/objects/contacts")]
    [InlineData("/api-catalog-public/v1/apis/crm/v3/objects/deals")]
    [InlineData("/api-catalog-public/v1/apis/crm/v3/objects/line_items")]
    [InlineData("/api-catalog-public/v1/apis/crm/v3/objects")]
    [InlineData("/api-catalog-public/v1/apis/marketing/v3/transactional")]
    public async Task Can_get_OpenAPI_documents(string path)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = _server.Uri
        };
        var response = await httpClient.GetAsync(path);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStringAsync();
    }

    public ValueTask DisposeAsync() => _server.DisposeAsync();
}