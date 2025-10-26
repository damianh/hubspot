using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class SchemaRepository
{
    private readonly ConcurrentDictionary<string, ObjectSchema> _schemas = new();
    private readonly ConcurrentDictionary<string, List<SchemaProperty>> _properties = new();
    private readonly ConcurrentDictionary<string, List<AssociationDefinition>> _associations = new();

    public ObjectSchema CreateSchema(
        string name,
        Dictionary<string, string> labels,
        string primaryDisplayProperty,
        List<string>? requiredProperties = null,
        List<string>? searchableProperties = null,
        List<string>? secondaryDisplayProperties = null)
    {
        var schema = new ObjectSchema
        {
            Id = $"0-{GenerateSchemaId()}",
            Name = name,
            Labels = labels,
            PrimaryDisplayProperty = primaryDisplayProperty,
            RequiredProperties = requiredProperties ?? [],
            SearchableProperties = searchableProperties ?? [],
            SecondaryDisplayProperties = secondaryDisplayProperties ?? [],
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Archived = false
        };

        _schemas[name] = schema;
        _properties[name] = [];
        _associations[name] = [];

        return schema;
    }

    public ObjectSchema? GetSchema(string objectTypeOrId)
    {
        // Try by name first
        if (_schemas.TryGetValue(objectTypeOrId, out var schema))
        {
            return schema;
        }

        // Try by ID
        return _schemas.Values.FirstOrDefault(s => s.Id == objectTypeOrId);
    }

    public List<ObjectSchema> ListSchemas(bool archived = false)
    {
        return _schemas.Values
            .Where(s => s.Archived == archived)
            .OrderBy(s => s.Name)
            .ToList();
    }

    public ObjectSchema? UpdateSchema(string objectTypeOrId, Dictionary<string, string>? labels = null, 
        string? primaryDisplayProperty = null,
        List<string>? requiredProperties = null,
        List<string>? searchableProperties = null,
        List<string>? secondaryDisplayProperties = null)
    {
        // Try by name first
        if (!_schemas.TryGetValue(objectTypeOrId, out var schema))
        {
            // Try by ID
            schema = _schemas.Values.FirstOrDefault(s => s.Id == objectTypeOrId);
            if (schema == null)
            {
                return null;
            }
        }

        if (labels != null)
        {
            schema.Labels = labels;
        }

        if (primaryDisplayProperty != null)
        {
            schema.PrimaryDisplayProperty = primaryDisplayProperty;
        }

        if (requiredProperties != null)
        {
            schema.RequiredProperties = requiredProperties;
        }

        if (searchableProperties != null)
        {
            schema.SearchableProperties = searchableProperties;
        }

        if (secondaryDisplayProperties != null)
        {
            schema.SecondaryDisplayProperties = secondaryDisplayProperties;
        }

        schema.UpdatedAt = DateTimeOffset.UtcNow;

        return schema;
    }

    public bool DeleteSchema(string objectTypeOrId)
    {
        // Try by name first
        if (!_schemas.TryGetValue(objectTypeOrId, out var schema))
        {
            // Try by ID
            schema = _schemas.Values.FirstOrDefault(s => s.Id == objectTypeOrId);
            if (schema == null)
            {
                return false;
            }
        }

        schema.Archived = true;
        schema.UpdatedAt = DateTimeOffset.UtcNow;
        return true;
    }

    public SchemaProperty AddProperty(string objectType, string name, string label, string type, 
        string fieldType, string? groupName = null, string? description = null,
        List<PropertyOption>? options = null, int? displayOrder = null)
    {
        if (!_properties.ContainsKey(objectType))
        {
            _properties[objectType] = [];
        }

        var property = new SchemaProperty
        {
            Name = name,
            Label = label,
            Type = type,
            FieldType = fieldType,
            GroupName = groupName ?? "default",
            Description = description,
            Options = options ?? [],
            DisplayOrder = displayOrder ?? _properties[objectType].Count,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Archived = false
        };

        _properties[objectType].Add(property);

        return property;
    }

    public List<SchemaProperty> GetProperties(string objectType)
    {
        return _properties.TryGetValue(objectType, out var props) 
            ? props.Where(p => !p.Archived).ToList() 
            : [];
    }

    public AssociationDefinition CreateAssociationDefinition(string fromObjectType, 
        string toObjectType, string name, string? label = null)
    {
        if (!_associations.ContainsKey(fromObjectType))
        {
            _associations[fromObjectType] = [];
        }

        var associationId = GenerateAssociationId();
        var definition = new AssociationDefinition
        {
            Id = associationId.ToString(),
            FromObjectType = fromObjectType,
            ToObjectType = toObjectType,
            Name = name,
            Label = label ?? name,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _associations[fromObjectType].Add(definition);

        return definition;
    }

    public List<AssociationDefinition> GetAssociationDefinitions(string objectType)
    {
        return _associations.TryGetValue(objectType, out var defs) ? defs : [];
    }

    public bool DeleteAssociationDefinition(string objectType, string associationId)
    {
        if (!_associations.TryGetValue(objectType, out var defs))
        {
            return false;
        }

        var def = defs.FirstOrDefault(d => d.Id == associationId);
        if (def == null)
        {
            return false;
        }

        defs.Remove(def);
        return true;
    }

    private static int _schemaIdCounter;
    private static int GenerateSchemaId() => Interlocked.Increment(ref _schemaIdCounter);

    private static int _associationIdCounter = 1000;
    private static int GenerateAssociationId() => Interlocked.Increment(ref _associationIdCounter);
}

public class ObjectSchema
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Dictionary<string, string> Labels { get; set; }
    public required string PrimaryDisplayProperty { get; set; }
    public List<string> RequiredProperties { get; set; } = [];
    public List<string> SearchableProperties { get; set; } = [];
    public List<string> SecondaryDisplayProperties { get; set; } = [];
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool Archived { get; set; }
}

public class SchemaProperty
{
    public required string Name { get; init; }
    public required string Label { get; set; }
    public required string Type { get; init; }
    public required string FieldType { get; init; }
    public string GroupName { get; set; } = "default";
    public string? Description { get; set; }
    public List<PropertyOption> Options { get; set; } = [];
    public int DisplayOrder { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool Archived { get; set; }
}

public class PropertyOption
{
    public required string Label { get; init; }
    public required string Value { get; init; }
    public int? DisplayOrder { get; init; }
    public bool Hidden { get; init; }
}

public class AssociationDefinition
{
    public required string Id { get; init; }
    public required string FromObjectType { get; init; }
    public required string ToObjectType { get; init; }
    public required string Name { get; init; }
    public required string Label { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}
