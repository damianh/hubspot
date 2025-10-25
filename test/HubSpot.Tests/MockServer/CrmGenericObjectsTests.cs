using DamianH.HubSpot.KiotaClient.CRM.Objects.V3;
using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;
using DamianH.HubSpot.MockServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace HubSpot.Tests.MockServer;

public class CrmGenericObjectsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMObjectsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        var services = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMObjectsV3Client(requestAdapter);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }

    [Fact]
    public async Task Can_create_custom_object()
    {
        var customObjectType = "custom_pets";
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "name", "Fluffy" },
                    { "species", "Cat" },
                    { "age", "3" }
                }
            }
        };

        var result = await _client.Crm.V3.Objects[customObjectType].PostAsync(input);

        result.ShouldNotBeNull();
        result.Entity.ShouldNotBeNull();
        result.Entity!.Id.ShouldNotBeNullOrWhiteSpace();
        result.Entity.Properties.AdditionalData["name"].ShouldBe("Fluffy");
        result.Entity.Properties.AdditionalData["species"].ShouldBe("Cat");
        result.Entity.Properties.AdditionalData["age"].ShouldBe("3");
    }

    [Fact]
    public async Task Can_list_custom_objects()
    {
        var customObjectType = "custom_books";
        
        await _client.Crm.V3.Objects[customObjectType].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "title", "1984" },
                    { "author", "George Orwell" }
                }
            }
        });

        await _client.Crm.V3.Objects[customObjectType].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "title", "Brave New World" },
                    { "author", "Aldous Huxley" }
                }
            }
        });

        var result = await _client.Crm.V3.Objects[customObjectType].GetAsync();

        result.ShouldNotBeNull();
        result!.Results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Different_custom_object_types_are_isolated()
    {
        var type1 = "custom_apples";
        var type2 = "custom_oranges";

        await _client.Crm.V3.Objects[type1].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "variety", "Granny Smith" } }
            }
        });

        await _client.Crm.V3.Objects[type2].PostAsync(new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object> { { "variety", "Navel" } }
            }
        });

        var apples = await _client.Crm.V3.Objects[type1].GetAsync();
        var oranges = await _client.Crm.V3.Objects[type2].GetAsync();

        apples!.Results.Count.ShouldBe(1);
        oranges!.Results.Count.ShouldBe(1);
    }
}
