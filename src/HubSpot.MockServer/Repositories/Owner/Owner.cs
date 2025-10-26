namespace DamianH.HubSpot.MockServer.Repositories.Owner;

internal record Owner(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Type, // USER or TEAM
    DateTime CreatedAt = default,
    DateTime UpdatedAt = default);
