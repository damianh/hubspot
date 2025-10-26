using DamianH.HubSpot.KiotaClient.Account.AuditLogs.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class AuditLogsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotAccountAuditLogsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartAsync();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.BaseUri.ToString()
        };
        _client = new HubSpotAccountAuditLogsV3Client(adapter);
    }

    public async ValueTask DisposeAsync() 
        => await _server.DisposeAsync();

    [Fact]
    public async Task GetActivityLogs_ReturnsActivityAuditLogs()
    {
        var logs = await _client.AccountInfo.V3.Activity.AuditLogs.GetAsync();

        logs.ShouldNotBeNull();
        logs.Results.ShouldNotBeNull();
        logs.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetLoginLogs_ReturnsLoginAuditLogs()
    {
        var logs = await _client.AccountInfo.V3.Activity.Login.GetAsync();

        logs.ShouldNotBeNull();
        logs.Results.ShouldNotBeNull();
        logs.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetSecurityLogs_ReturnsSecurityAuditLogs()
    {
        var logs = await _client.AccountInfo.V3.Activity.Security.GetAsync();

        logs.ShouldNotBeNull();
        logs.Results.ShouldNotBeNull();
        logs.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetActivityLogs_WithPagination_ReturnsPagedResults()
    {
        var logs = await _client.AccountInfo.V3.Activity.AuditLogs.GetAsync(config =>
        {
            config.QueryParameters.Limit = 2;
        });

        logs.ShouldNotBeNull();
        logs.Results.ShouldNotBeNull();
        logs.Results.Count.ShouldBeLessThanOrEqualTo(2);
    }
}
