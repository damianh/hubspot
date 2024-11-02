using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit.Abstractions;

namespace DamianH.HubSpot.MockServer;

public class OpenApiDocumentTests(ITestOutputHelper outputHelper) : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;

    public async Task InitializeAsync()
    {
        var services = new ServiceCollection()
            .AddLogging(logging => logging.AddXUnit(outputHelper))
            .BuildServiceProvider();

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        outputHelper.WriteLine(_server.Uri.ToString());
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

    public  Task                      DisposeAsync() => _server.DisposeAsync().AsTask();
}