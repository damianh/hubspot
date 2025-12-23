namespace DamianH.HubSpot.MockServer.Repositories.PropertyDefinition;

internal record PropertyDefinition(
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
