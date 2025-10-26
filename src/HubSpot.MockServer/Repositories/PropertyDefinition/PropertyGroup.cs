namespace DamianH.HubSpot.MockServer.Repositories.PropertyDefinition;

internal record PropertyGroup(
    string Name,
    string Label,
    string ObjectType,
    int DisplayOrder = 0,
    DateTime CreatedAt = default,
    DateTime UpdatedAt = default);
