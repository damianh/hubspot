using DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3;
using DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class OwnerTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCrmOwnersV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMCrmOwnersV3Client(adapter);
    }

    public async ValueTask DisposeAsync()
    {
        if (_server != null) await _server.DisposeAsync();
    }

    [Fact]
    public async Task GetOwnerById_ReturnsOwner()
    {
        var result = await _client.Crm.V3.Owners[1].GetAsync();

        result.ShouldNotBeNull();
        result.Id.ShouldBe("1");
        result.Email.ShouldBe("admin@example.com");
        result.FirstName.ShouldBe("Admin");
        result.LastName.ShouldBe("User");
        result.Type.ShouldBe(PublicOwner_type.PERSON);
        result.Archived.ShouldBe(false);
        result.UserId.ShouldBe(1001);
        result.UserIdIncludingInactive.ShouldBe(1001);
        result.CreatedAt.ShouldNotBeNull();
        result.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetOwnerById_WithTeams_ReturnsOwnerWithTeams()
    {
        var result = await _client.Crm.V3.Owners[2].GetAsync();

        result.ShouldNotBeNull();
        result.Id.ShouldBe("2");
        result.Email.ShouldBe("sales@example.com");
        result.Teams.ShouldNotBeNull();
        result.Teams.Count.ShouldBe(1);
        result.Teams[0].Id.ShouldBe("100");
        result.Teams[0].Name.ShouldBe("Sales Team");
        result.Teams[0].Primary.ShouldBe(true);
    }

    [Fact]
    public async Task GetAllOwners_ReturnsSeededOwners()
    {
        var result = await _client.Crm.V3.Owners.GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
        result.Results.Count.ShouldBeGreaterThanOrEqualTo(4);
    }

    [Fact]
    public async Task GetAllOwners_WithPagination_ReturnsLimitedResults()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Limit = 2;
        });

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
        result.Results.Count.ShouldBe(2);
        result.Paging.ShouldNotBeNull();
        result.Paging.Next.ShouldNotBeNull();
        result.Paging.Next.After.ShouldNotBeNullOrWhiteSpace();
        result.Paging.Next.Link.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetAllOwners_WithAfterCursor_ReturnsNextPage()
    {
        // Get first page
        var firstPage = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Limit = 2;
        });

        firstPage.ShouldNotBeNull();
        firstPage.Paging?.Next?.After.ShouldNotBeNull();

        // Get second page using cursor
        var secondPage = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.After = firstPage.Paging!.Next!.After;
            config.QueryParameters.Limit = 2;
        });

        secondPage.ShouldNotBeNull();
        secondPage.Results.ShouldNotBeNull();
        secondPage.Results.Count.ShouldBeGreaterThan(0);
        
        // Results should be different
        secondPage.Results[0].Id.ShouldNotBe(firstPage.Results![0].Id);
    }

    [Fact]
    public async Task GetAllOwners_FilterByEmail_ReturnsMatchingOwners()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Email = "sales";
        });

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
        result.Results.All(o => o.Email!.Contains("sales", StringComparison.OrdinalIgnoreCase)).ShouldBeTrue();
    }

    [Fact]
    public async Task GetAllOwners_FilterByArchived_ReturnsOnlyActiveOwners()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Archived = false;
        });

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
        result.Results.All(o => o.Archived == false).ShouldBeTrue();
    }

    [Fact]
    public async Task GetOwnerById_QueueType_ReturnsQueueOwner()
    {
        var result = await _client.Crm.V3.Owners[100].GetAsync();

        result.ShouldNotBeNull();
        result.Id.ShouldBe("100");
        result.Email.ShouldBe("sales-team@example.com");
        result.Type.ShouldBe(PublicOwner_type.QUEUE);
    }

    [Fact]
    public async Task PublicOwner_AllProperties_AreDeserialized()
    {
        var owner = await _client.Crm.V3.Owners[1].GetAsync();

        owner.ShouldNotBeNull();
        owner.Id.ShouldNotBeNull();
        owner.Email.ShouldNotBeNull();
        owner.FirstName.ShouldNotBeNull();
        owner.LastName.ShouldNotBeNull();
        owner.Type.ShouldNotBeNull();
        owner.Archived.ShouldNotBeNull();
        owner.CreatedAt.ShouldNotBeNull();
        owner.UpdatedAt.ShouldNotBeNull();
        owner.UserId.ShouldNotBeNull();
        owner.UserIdIncludingInactive.ShouldNotBeNull();
    }

    [Fact]
    public async Task PublicTeam_AllProperties_AreDeserialized()
    {
        var owner = await _client.Crm.V3.Owners[2].GetAsync();

        owner.ShouldNotBeNull();
        owner.Teams.ShouldNotBeNull();
        owner.Teams.Count.ShouldBeGreaterThan(0);
        
        var team = owner.Teams[0];
        team.Id.ShouldNotBeNull();
        team.Name.ShouldNotBeNull();
        team.Primary.ShouldNotBeNull();
    }

    [Fact]
    public async Task ForwardPaging_WithNextPage_IsDeserialized()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Limit = 2;
        });

        result.ShouldNotBeNull();
        result.Paging.ShouldNotBeNull();
        result.Paging.Next.ShouldNotBeNull();
    }

    [Fact]
    public async Task NextPage_AllProperties_AreDeserialized()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Limit = 2;
        });

        result.ShouldNotBeNull();
        result.Paging?.Next.ShouldNotBeNull();
        
        var nextPage = result.Paging!.Next;
        nextPage.After.ShouldNotBeNullOrWhiteSpace();
        nextPage.Link.ShouldNotBeNullOrWhiteSpace();
        nextPage.Link.ShouldContain("after=");
    }

    [Fact]
    public async Task GetAllOwners_LastPage_HasNoPaging()
    {
        var result = await _client.Crm.V3.Owners.GetAsync(config =>
        {
            config.QueryParameters.Limit = 100;
        });

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
        // If we get all results in one page, paging should be null
        if (result.Results.Count <= 100)
        {
            result.Paging.ShouldBeNull();
        }
    }
}
