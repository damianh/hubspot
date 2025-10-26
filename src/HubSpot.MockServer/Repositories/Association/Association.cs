namespace DamianH.HubSpot.MockServer.Repositories.Association;

internal record Association(
    string Id,
    string FromObjectType,
    string FromObjectId,
    string ToObjectType,
    string ToObjectId,
    string AssociationTypeId,
    string? AssociationLabel = null);
