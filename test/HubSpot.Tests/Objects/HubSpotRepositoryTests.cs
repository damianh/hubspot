using DamianH.HubSpot.KiotaClient;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.Extensions.Time.Testing;

namespace DamianH.HubSpot.Objects;

public class HubSpotRepositoryTests
{
    private readonly HubSpotObjectRepository _sut;
    private readonly FakeTimeProvider _timeProvider;

    public HubSpotRepositoryTests()
    {
        var idGenerator = new HubSpotObjectIdGenerator();
        _timeProvider = new FakeTimeProvider();
        _sut = new HubSpotObjectRepository(idGenerator, _timeProvider);
    }

    [Fact]
    public void Can_create_hubspot_object()
    {
        var initialProperties = new Dictionary<string, string>
        {
            [PropertyNames.CrmCompany.Name] = "Foo",
            [PropertyNames.CrmCompany.Domain] = "example.com",
        };
        var newHubSpotObject = new NewHubSpotObject(initialProperties, Array.Empty<HubSpotAssociation>());
        var createdHubSpotObject = _sut.Create(newHubSpotObject);

        var found = _sut.TryRead(createdHubSpotObject.Id, out var readHubSpotObject);

        found.ShouldBeTrue();
        var now = _timeProvider.GetUtcNow();
        //readHubSpotObject!.Id.Value.ShouldNotBeNullOrEmpty();
        readHubSpotObject.Archived.ShouldBeFalse();
        readHubSpotObject.ArchivedAt.ShouldBeNull();
        readHubSpotObject.CreatedAt.ShouldBe(now);
        readHubSpotObject.UpdatedAt.ShouldBe(now);
        readHubSpotObject.Properties.Count.ShouldBe(2);
        readHubSpotObject.Properties[PropertyNames.CrmCompany.Name].CurrentValue.ShouldBe("Foo");
        readHubSpotObject.Properties[PropertyNames.CrmCompany.Domain].CurrentValue.ShouldBe("example.com");
    }

    [Fact]
    public void Can_update_hubspot_object_with_changed_property()
    {
        var initialProperties = new Dictionary<string, string>
        {
            [PropertyNames.CrmCompany.Name] = "Foo",
            [PropertyNames.CrmCompany.Domain] = "example.com",
        };
        var newHubSpotObject = new NewHubSpotObject(initialProperties, Array.Empty<HubSpotAssociation>());
        var createdHubSpotObject = _sut.Create(newHubSpotObject);

        _sut.TryRead(createdHubSpotObject.Id, out var originalHubSpotObject);

        originalHubSpotObject!.Properties[PropertyNames.CrmCompany.Name].NewValue = "Bar";
        _sut.Update(originalHubSpotObject);

        _sut.TryRead(createdHubSpotObject.Id, out var updatedHubSpotObject);

        updatedHubSpotObject!.ShouldSatisfyAll(
            x => x.Properties[PropertyNames.CrmCompany.Name].CurrentValue.ShouldBe("Bar"),
            x => x.Properties[PropertyNames.CrmCompany.Domain].CurrentValue.ShouldBe("example.com")
        );
    }

    [Fact]
    public void Can_update_hubspot_object_with_new_property()
    {
        var newPropertyName = "NewProperty";
        var initialProperties = new Dictionary<string, string>
        {
            [PropertyNames.CrmCompany.Name] = "Foo",
        };
        var newHubSpotObject = new NewHubSpotObject(initialProperties, Array.Empty<HubSpotAssociation>());
        var createdHubSpotObject = _sut.Create(newHubSpotObject);

        _sut.TryRead(createdHubSpotObject.Id, out var originalHubSpotObject);

        var hubSpotProperty = new HubSpotObjectProperty(newPropertyName, [])
        {
            NewValue = "Bar"
        };
        originalHubSpotObject!.Properties.Add(newPropertyName, hubSpotProperty);
        _sut.Update(originalHubSpotObject);

        _sut.TryRead(createdHubSpotObject.Id, out var updatedHubSpotObject);

        updatedHubSpotObject!.ShouldSatisfyAll(
            x => x.Properties.Count.ShouldBe(2),
            x => x.Properties[newPropertyName].CurrentValue.ShouldBe("Bar")
        );
    }
}
