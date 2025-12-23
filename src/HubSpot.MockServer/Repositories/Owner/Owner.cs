namespace DamianH.HubSpot.MockServer.Repositories.Owner;

internal record Owner(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Type, // PERSON or QUEUE
    DateTime CreatedAt = default,
    DateTime UpdatedAt = default,
    bool Archived = false,
    int? UserId = null,
    int? UserIdIncludingInactive = null,
    List<Team>? Teams = null);

internal record Team(
    string Id,
    string Name,
    bool Primary = false);
