using DamianH.HubSpot.KiotaClient.CMS.Tags.V3;
using DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CmsTagsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCMSTagsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCMSTagsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_tag()
    {
        var input = new Tag
        {
            Name = "product-launch"
        };

        var created = await _client.Cms.V3.Blogs.Tags.PostAsync(input);

        created.ShouldNotBeNull();
        created.Id.ShouldNotBeNullOrEmpty();
        created.Name.ShouldBe("product-launch");
    }

    [Fact]
    public async Task Can_get_tag_by_id()
    {
        var input = new Tag
        {
            Name = "customer-success"
        };

        var created = await _client.Cms.V3.Blogs.Tags.PostAsync(input);
        var tagId = created!.Id!;

        var retrieved = await _client.Cms.V3.Blogs.Tags[tagId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(tagId);
        retrieved.Name.ShouldBe("customer-success");
    }

    [Fact]
    public async Task Can_update_tag()
    {
        var input = new Tag
        {
            Name = "original-tag"
        };

        var created = await _client.Cms.V3.Blogs.Tags.PostAsync(input);
        var tagId = created!.Id!;

        var updateInput = new Tag
        {
            Name = "updated-tag"
        };

        var updated = await _client.Cms.V3.Blogs.Tags[tagId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Name.ShouldBe("updated-tag");
    }

    [Fact]
    public async Task Can_list_tags()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new Tag
            {
                Name = $"tag-{i}"
            };
            await _client.Cms.V3.Blogs.Tags.PostAsync(input);
        }

        var response = await _client.Cms.V3.Blogs.Tags.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_delete_tag()
    {
        var input = new Tag
        {
            Name = "tag-to-delete"
        };

        var created = await _client.Cms.V3.Blogs.Tags.PostAsync(input);
        var tagId = created!.Id!;

        await _client.Cms.V3.Blogs.Tags[tagId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Cms.V3.Blogs.Tags[tagId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_batch_create_tags()
    {
        var batchInput = new BatchInputTag
        {
            Inputs =
            [
                new Tag { Name = "batch-tag-1" },
                new Tag { Name = "batch-tag-2" }
            ]
        };

        var response = await _client.Cms.V3.Blogs.Tags.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
