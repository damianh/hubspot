using DamianH.HubSpot.KiotaClient.Settings.UserProvisioning.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class UserProvisioningTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotSettingsUserProvisioningV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.BaseUri.ToString()
        };
        _client = new HubSpotSettingsUserProvisioningV3Client(adapter);
    }

    public async ValueTask DisposeAsync() 
        => await _server.DisposeAsync();

    [Fact]
    public async Task GetUsers_ReturnsUserList()
    {
        var users = await _client.Settings.V3.Users.EmptyPathSegment.GetAsync();

        users.ShouldNotBeNull();
        users.Results.ShouldNotBeNull();
        users.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetRoles_ReturnsRolesList()
    {
        var roles = await _client.Settings.V3.Users.Roles.GetAsync();

        roles.ShouldNotBeNull();
        roles.Results.ShouldNotBeNull();
        roles.Results.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetTeams_ReturnsTeamsList()
    {
        var teams = await _client.Settings.V3.Users.Teams.GetAsync();

        teams.ShouldNotBeNull();
        teams.Results.ShouldNotBeNull();
        teams.Results.ShouldNotBeEmpty();
    }
}
