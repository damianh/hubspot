namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotObjectRepository(HubSpotObjectIdGenerator idGenerator, TimeProvider timeProvider)
{
    private readonly Dictionary<HubSpotObjectId, HubSpotObjectData> _objectsById      = new();
    private readonly ReaderWriterLockSlim                           _readerWriterLock = new();

    public HubSpotObject Create(NewHubSpotObject newHubSpotObject)
    {
        _readerWriterLock.EnterWriteLock();
        
        var id  = idGenerator.Generate();
        var now = timeProvider.GetUtcNow();
        var data = new HubSpotObjectData
        {
            Id        = id.Value,
            CreatedAt = now,
            UpdatedAt = now
        };
        foreach (var property in newHubSpotObject.InitialProperties)
        {
            var propertyEntry = new PropertyValueEntryData
            {
                Timestamp   = now,
                Value       = property.Value,
                SourceId    = "mock",
                SourceLabel = "mock",
                SourceType  = "mock"
            };
            data.Properties.Add(property.Key, [propertyEntry]);
        }
        _objectsById.Add(id, data);
        
        var hubSpotObject = HubSpotObject.Load(
            data.Id,
            data.CreatedAt,
            data.UpdatedAt,
            data.Archived,
            data.ArchivedAt,
            data.Properties);
        
        _readerWriterLock.ExitWriteLock();
        
        return hubSpotObject;
    }

    public HubSpotObject Update(HubSpotObject hubSpotObject)
    {
        _readerWriterLock.EnterWriteLock();
        var id              = hubSpotObject.Id;
        var dirtyProperties = hubSpotObject.Properties.Where(p => p.Value.IsDirty).ToArray();
        var now             = timeProvider.GetUtcNow();
        if (dirtyProperties.Length == 0)
        {
            // No properties have been updated, return the current object
            var currentHubSpotObject = GetInternal(id);
            _readerWriterLock.ExitWriteLock();
            return currentHubSpotObject!;
        }
        var data = _objectsById[hubSpotObject.Id];
        foreach (var property in dirtyProperties)
        {
            var propertyEntry = new PropertyValueEntryData
            {
                Timestamp   = now,
                Value       = property.Value.NewValue!,
                SourceId    = "mock",
                SourceLabel = "mock",
                SourceType  = "mock"
            };
            if (!data.Properties.TryGetValue(property.Key, out var propertyEntries))
            {
                // No previous entries for this property, create a new list
                data.Properties.Add(property.Key, [propertyEntry]);
            }
            else
            {
                // Add a new entry to the existing list
                propertyEntries.Add(propertyEntry);
            }
        }
        data.UpdatedAt = timeProvider.GetUtcNow();

        var updateHubSpotObject = GetInternal(id);
        _readerWriterLock.ExitWriteLock();
        return updateHubSpotObject!;
    }

    public bool TryRead(HubSpotObjectId id, out HubSpotObject? hubSpotObject)
    {
        _readerWriterLock.EnterReadLock();
        var retrievedHubSpotObject = GetInternal(id);
        if (retrievedHubSpotObject is null)
        {
            hubSpotObject = null;
            _readerWriterLock.ExitReadLock();
            return false;
        }
        _readerWriterLock.ExitReadLock();
        hubSpotObject = retrievedHubSpotObject;
        return true;
    }
    
    private HubSpotObject? GetInternal(HubSpotObjectId id)
    {
        var found = _objectsById.TryGetValue(id, out var item);
        if (!found)
        {
            return null;
        }
        var hubSpotObject = HubSpotObject.Load(
            item!.Id,
            item.CreatedAt,
            item.UpdatedAt,
            item.Archived,
            item.ArchivedAt,
            item.Properties);
        return hubSpotObject;
    }

    private class HubSpotObjectData
    {
        public required int                                              Id         { get; init; }
        public          bool                                             Archived   { get; } = false;
        public          DateTimeOffset?                                  ArchivedAt { get; } = null;
        public required DateTimeOffset                                   CreatedAt  { get; init; }
        public required DateTimeOffset                                   UpdatedAt  { get; set; }
        public          Dictionary<string, List<PropertyValueEntryData>> Properties { get; } = new();
    }
}
