using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.PropertyDefinition;

/// <summary>
/// Repository for managing property definitions for HubSpot objects
/// </summary>
internal class PropertyDefinitionRepository
{
    private readonly ConcurrentDictionary<string, PropertyDefinition> _properties = new();
    private readonly ConcurrentDictionary<string, PropertyGroup> _groups = new();

    public record PropertyDefinition(
        string Name,
        string Label,
        string Type,
        string FieldType,
        string ObjectType,
        string? GroupName = null,
        string? Description = null,
        string[]? Options = null,
        bool Hidden = false,
        int DisplayOrder = 0,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default);

    public record PropertyGroup(
        string Name,
        string Label,
        string ObjectType,
        int DisplayOrder = 0,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default);

    public PropertyDefinitionRepository() => SeedDefaultProperties();

    private void SeedDefaultProperties()
    {
        var now = DateTime.UtcNow;

        // Contact properties
        AddProperty(new PropertyDefinition("email", "Email", "string", "text", "contacts", Description: "Email address", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("firstname", "First Name", "string", "text", "contacts", Description: "First name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("lastname", "Last Name", "string", "text", "contacts", Description: "Last name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("phone", "Phone Number", "string", "text", "contacts", Description: "Phone number", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("company", "Company Name", "string", "text", "contacts", Description: "Associated company", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("website", "Website", "string", "text", "contacts", Description: "Website URL", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("lifecyclestage", "Lifecycle Stage", "enumeration", "select", "contacts",
            Options: ["subscriber", "lead", "marketingqualifiedlead", "salesqualifiedlead", "opportunity", "customer", "evangelist", "other"
            ], CreatedAt: now, UpdatedAt: now));

        // Company properties
        AddProperty(new PropertyDefinition("name", "Name", "string", "text", "companies", Description: "Company name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("domain", "Domain", "string", "text", "companies", Description: "Company domain", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("industry", "Industry", "string", "text", "companies", Description: "Industry", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("phone", "Phone Number", "string", "text", "companies", Description: "Phone number", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("city", "City", "string", "text", "companies", Description: "City", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("state", "State", "string", "text", "companies", Description: "State/Region", CreatedAt: now, UpdatedAt: now));

        // Deal properties
        AddProperty(new PropertyDefinition("dealname", "Deal Name", "string", "text", "deals", Description: "Deal name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("amount", "Amount", "number", "number", "deals", Description: "Deal amount", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("dealstage", "Deal Stage", "enumeration", "select", "deals",
            Options: ["appointmentscheduled", "qualifiedtobuy", "presentationscheduled", "decisionmakerboughtin", "contractsent", "closedwon", "closedlost"
            ], CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("pipeline", "Pipeline", "enumeration", "select", "deals",
            Options: ["default"], CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("closedate", "Close Date", "datetime", "date", "deals", Description: "Expected close date", CreatedAt: now, UpdatedAt: now));

        // Ticket properties
        AddProperty(new PropertyDefinition("subject", "Subject", "string", "text", "tickets", Description: "Ticket subject", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("content", "Description", "string", "textarea", "tickets", Description: "Ticket description", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("hs_pipeline", "Pipeline", "enumeration", "select", "tickets",
            Options: ["0"], CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("hs_pipeline_stage", "Ticket Status", "enumeration", "select", "tickets",
            Options: ["1", "2", "3", "4"], CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("hs_ticket_priority", "Priority", "enumeration", "select", "tickets",
            Options: ["LOW", "MEDIUM", "HIGH"], CreatedAt: now, UpdatedAt: now));

        // Product properties
        AddProperty(new PropertyDefinition("name", "Name", "string", "text", "products", Description: "Product name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("price", "Price", "number", "number", "products", Description: "Product price", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("description", "Description", "string", "textarea", "products", Description: "Product description", CreatedAt: now, UpdatedAt: now));

        // Line Item properties
        AddProperty(new PropertyDefinition("name", "Name", "string", "text", "line_items", Description: "Line item name", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("quantity", "Quantity", "number", "number", "line_items", Description: "Quantity", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("price", "Price", "number", "number", "line_items", Description: "Unit price", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("amount", "Amount", "number", "number", "line_items", Description: "Total amount", CreatedAt: now, UpdatedAt: now));

        // Quote properties
        AddProperty(new PropertyDefinition("hs_title", "Title", "string", "text", "quotes", Description: "Quote title", CreatedAt: now, UpdatedAt: now));
        AddProperty(new PropertyDefinition("hs_expiration_date", "Expiration Date", "datetime", "date", "quotes", Description: "Expiration date", CreatedAt: now, UpdatedAt: now));
    }

    public PropertyDefinition? GetProperty(string objectType, string propertyName)
    {
        var key = GetKey(objectType, propertyName);
        return _properties.GetValueOrDefault(key);
    }

    public IReadOnlyList<PropertyDefinition> GetProperties(string objectType) => _properties.Values
            .Where(p => p.ObjectType == objectType)
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Name)
            .ToList();

    public PropertyDefinition CreateProperty(
        string objectType,
        string name,
        string label,
        string type,
        string fieldType,
        string? groupName = null,
        string? description = null,
        string[]? options = null)
    {
        var now = DateTime.UtcNow;
        var property = new PropertyDefinition(
            name,
            label,
            type,
            fieldType,
            objectType,
            groupName,
            description,
            options,
            false,
            _properties.Count,
            now,
            now);

        AddProperty(property);
        return property;
    }

    public PropertyDefinition? UpdateProperty(
        string objectType,
        string propertyName,
        string? label = null,
        string? description = null,
        string? groupName = null)
    {
        var existing = GetProperty(objectType, propertyName);
        if (existing == null)
        {
            return null;
        }

        var updated = existing with
        {
            Label = label ?? existing.Label,
            Description = description ?? existing.Description,
            GroupName = groupName ?? existing.GroupName,
            UpdatedAt = DateTime.UtcNow
        };

        AddProperty(updated);
        return updated;
    }

    public bool DeleteProperty(string objectType, string propertyName)
    {
        var key = GetKey(objectType, propertyName);
        return _properties.TryRemove(key, out _);
    }

    public PropertyGroup? GetGroup(string objectType, string groupName)
    {
        var key = GetKey(objectType, groupName);
        return _groups.GetValueOrDefault(key);
    }

    public IReadOnlyList<PropertyGroup> GetGroups(string objectType) => _groups.Values
            .Where(g => g.ObjectType == objectType)
            .OrderBy(g => g.DisplayOrder)
            .ThenBy(g => g.Name)
            .ToList();

    public PropertyGroup CreateGroup(
        string objectType,
        string name,
        string label,
        int displayOrder = 0)
    {
        var now = DateTime.UtcNow;
        var group = new PropertyGroup(name, label, objectType, displayOrder, now, now);

        var key = GetKey(objectType, name);
        _groups[key] = group;
        return group;
    }

    public bool DeleteGroup(string objectType, string groupName)
    {
        var key = GetKey(objectType, groupName);
        return _groups.TryRemove(key, out _);
    }

    public void Clear()
    {
        _properties.Clear();
        _groups.Clear();
        SeedDefaultProperties();
    }

    private void AddProperty(PropertyDefinition property)
    {
        var key = GetKey(property.ObjectType, property.Name);
        _properties[key] = property;
    }

    private static string GetKey(string objectType, string name) => $"{objectType}:{name}";
}
