using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

/// <summary>
/// Repository for managing associations between HubSpot objects
/// </summary>
internal class AssociationRepository
{
    private readonly ConcurrentDictionary<string, Association> _associations = new();
    private int _nextId = 1;

    public record Association(
        string Id,
        string FromObjectType,
        string FromObjectId,
        string ToObjectType,
        string ToObjectId,
        string AssociationTypeId,
        string? AssociationLabel = null);

    public record AssociationTypeDefinition(
        string Id,
        string Name,
        string? Category = null);

    // Standard HubSpot association types
    private static readonly Dictionary<string, AssociationTypeDefinition> _standardTypes = new()
    {
        // Contact to Company
        ["1"] = new("1", "Contact to Company (Primary)", "HUBSPOT_DEFINED"),
        ["2"] = new("2", "Company to Contact (Primary)", "HUBSPOT_DEFINED"),

        // Contact to Deal
        ["3"] = new("3", "Contact to Deal", "HUBSPOT_DEFINED"),
        ["4"] = new("4", "Deal to Contact", "HUBSPOT_DEFINED"),

        // Deal to Company
        ["5"] = new("5", "Deal to Company (Primary)", "HUBSPOT_DEFINED"),
        ["6"] = new("6", "Company to Deal (Primary)", "HUBSPOT_DEFINED"),

        // Deal to Line Item
        ["19"] = new("19", "Deal to Line Item", "HUBSPOT_DEFINED"),
        ["20"] = new("20", "Line Item to Deal", "HUBSPOT_DEFINED"),

        // Contact to Ticket
        ["15"] = new("15", "Contact to Ticket", "HUBSPOT_DEFINED"),
        ["16"] = new("16", "Ticket to Contact", "HUBSPOT_DEFINED"),

        // Company to Ticket
        ["25"] = new("25", "Company to Ticket", "HUBSPOT_DEFINED"),
        ["26"] = new("26", "Ticket to Company", "HUBSPOT_DEFINED"),

        // Quote to Deal
        ["63"] = new("63", "Quote to Deal", "HUBSPOT_DEFINED"),
        ["64"] = new("64", "Deal to Quote", "HUBSPOT_DEFINED"),

        // Quote to Line Item
        ["67"] = new("67", "Quote to Line Item", "HUBSPOT_DEFINED"),
        ["68"] = new("68", "Line Item to Quote", "HUBSPOT_DEFINED"),
    };

    private readonly ConcurrentDictionary<string, AssociationTypeDefinition> _customTypes = new();

    /// <summary>
    /// Create a new association between two objects
    /// </summary>
    public Association Create(
        string fromObjectType,
        string fromObjectId,
        string toObjectType,
        string toObjectId,
        string associationTypeId,
        string? associationLabel = null)
    {
        var key = GetKey(fromObjectType, fromObjectId, toObjectType, toObjectId, associationTypeId);

        var association = new Association(
            Interlocked.Increment(ref _nextId).ToString(),
            fromObjectType,
            fromObjectId,
            toObjectType,
            toObjectId,
            associationTypeId,
            associationLabel);

        _associations[key] = association;
        return association;
    }

    /// <summary>
    /// Create multiple associations in batch
    /// </summary>
    public IReadOnlyList<Association> CreateBatch(
        IEnumerable<(string fromObjectId, string toObjectId, string associationTypeId, string? label)> inputs,
        string fromObjectType,
        string toObjectType)
    {
        var results = new List<Association>();
        foreach (var (fromId, toId, typeId, label) in inputs)
        {
            results.Add(Create(fromObjectType, fromId, toObjectType, toId, typeId, label));
        }
        return results;
    }

    /// <summary>
    /// Get associations from an object to a specific object type
    /// </summary>
    public IReadOnlyList<Association> GetAssociations(
        string fromObjectType,
        string fromObjectId,
        string toObjectType) => _associations.Values
            .Where(a => a.FromObjectType == fromObjectType &&
                       a.FromObjectId == fromObjectId &&
                       a.ToObjectType == toObjectType)
            .ToList();

    /// <summary>
    /// Get associations for multiple objects in batch
    /// </summary>
    public Dictionary<string, IReadOnlyList<Association>> GetAssociationsBatch(
        IEnumerable<string> fromObjectIds,
        string fromObjectType,
        string toObjectType) => fromObjectIds.ToDictionary(
            id => id,
            id => GetAssociations(fromObjectType, id, toObjectType));

    /// <summary>
    /// Delete an association
    /// </summary>
    public bool Delete(
        string fromObjectType,
        string fromObjectId,
        string toObjectType,
        string toObjectId,
        string associationTypeId)
    {
        var key = GetKey(fromObjectType, fromObjectId, toObjectType, toObjectId, associationTypeId);
        return _associations.TryRemove(key, out _);
    }

    /// <summary>
    /// Delete multiple associations in batch
    /// </summary>
    public void DeleteBatch(
        IEnumerable<(string fromObjectId, string toObjectId, string associationTypeId)> inputs,
        string fromObjectType,
        string toObjectType)
    {
        foreach (var (fromId, toId, typeId) in inputs)
        {
            Delete(fromObjectType, fromId, toObjectType, toId, typeId);
        }
    }

    /// <summary>
    /// Delete all associations for an object (called when object is deleted)
    /// </summary>
    public void DeleteAllForObject(string objectType, string objectId)
    {
        var toRemove = _associations.Values
            .Where(a => (a.FromObjectType == objectType && a.FromObjectId == objectId) ||
                       (a.ToObjectType == objectType && a.ToObjectId == objectId))
            .ToList();

        foreach (var association in toRemove)
        {
            var key = GetKey(
                association.FromObjectType,
                association.FromObjectId,
                association.ToObjectType,
                association.ToObjectId,
                association.AssociationTypeId);
            _associations.TryRemove(key, out _);
        }
    }

    /// <summary>
    /// Get an association type definition
    /// </summary>
    public AssociationTypeDefinition? GetAssociationType(string associationTypeId) => _standardTypes.TryGetValue(associationTypeId, out var type)
            ? type
            : _customTypes.GetValueOrDefault(associationTypeId);

    /// <summary>
    /// Get all association type definitions for a pair of object types
    /// </summary>
    public IReadOnlyList<AssociationTypeDefinition> GetAssociationTypes(
        string fromObjectType,
        string toObjectType) =>
        // Return all standard + custom types (simplified - real HubSpot filters by object types)
        _standardTypes.Values
            .Concat(_customTypes.Values)
            .ToList();

    /// <summary>
    /// Create a custom association type definition
    /// </summary>
    public AssociationTypeDefinition CreateAssociationType(
        string name,
        string? category = null)
    {
        var id = (1000 + _customTypes.Count).ToString();
        var definition = new AssociationTypeDefinition(id, name, category ?? "USER_DEFINED");
        _customTypes[id] = definition;
        return definition;
    }

    /// <summary>
    /// Delete a custom association type
    /// </summary>
    public bool DeleteAssociationType(string associationTypeId)
    {
        // Can't delete standard types
        if (_standardTypes.ContainsKey(associationTypeId))
        {
            return false;
        }

        return _customTypes.TryRemove(associationTypeId, out _);
    }

    /// <summary>
    /// Clear all associations (for testing)
    /// </summary>
    public void Clear()
    {
        _associations.Clear();
        _customTypes.Clear();
        _nextId = 1;
    }

    private static string GetKey(
        string fromObjectType,
        string fromObjectId,
        string toObjectType,
        string toObjectId,
        string associationTypeId) => $"{fromObjectType}:{fromObjectId}:{toObjectType}:{toObjectId}:{associationTypeId}";
}
