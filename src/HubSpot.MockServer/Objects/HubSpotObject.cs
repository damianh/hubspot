namespace DamianH.HubSpot.MockServer.Objects;

/*
internal class HubSpotProperties
{
    private readonly Dictionary<string, HubSpotObjectProperty> _properties = new();

    public HubSpotObjectProperty GetOrAdd(string name)
    {
        if(!_properties.TryGetValue(name, out var property))
        {
            property = new HubSpotObjectProperty(name, []);
            _properties.Add(name, property);
        }
        return property;
    }
    
    public IReadOnlyDictionary<string, HubSpotObjectProperty> Items => _properties;
}
*/

internal class HubSpotObject(
    HubSpotObjectId id,
    DateTimeOffset createdAt,
    DateTimeOffset updatedAt,
    bool archived = false,
    DateTimeOffset? archivedAt = null)
{
    private readonly Dictionary<string, HubSpotObjectProperty> _properties = new();

    public HubSpotObjectId Id => id;
    public IDictionary<string, HubSpotObjectProperty> Properties => _properties;
    public bool Archived => archived;
    public DateTimeOffset? ArchivedAt => archivedAt;
    public DateTimeOffset CreatedAt => createdAt;
    public DateTimeOffset UpdatedAt => updatedAt;

    public static HubSpotObject Load(
        int id,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt,
        bool archived,
        DateTimeOffset? archivedAt,
        Dictionary<string, List<PropertyValueEntryData>> properties)
    {
        var hubSpotObjectId = HubSpotObjectId.From(id);
        var hubSpotObject = new HubSpotObject(hubSpotObjectId, createdAt, updatedAt, archived, archivedAt);
        foreach (var property in properties)
        {
            var propertyEntries = property.Value.Select(s => new PropertyValueEntry
            {
                Timestamp = s.Timestamp,
                Value = s.Value,
                SourceId = s.SourceId,
                SourceLabel = s.SourceLabel,
                SourceType = s.SourceType
            }).ToArray();
            var hubSpotObjectProperty = new HubSpotObjectProperty(property.Key, propertyEntries);
            hubSpotObject._properties.Add(property.Key, hubSpotObjectProperty);
        }

        return hubSpotObject;
    }
}
