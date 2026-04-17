namespace DamianH.HubSpot.MockServer.Webhooks;

internal abstract record WebhookEvent(string ObjectType, string ObjectId, DateTimeOffset OccurredAt);

internal sealed record ObjectCreatedEvent(
    string ObjectType,
    string ObjectId,
    DateTimeOffset OccurredAt,
    IReadOnlyDictionary<string, string?> Properties)
    : WebhookEvent(ObjectType, ObjectId, OccurredAt);

internal sealed record ObjectPropertyChangedEvent(
    string ObjectType,
    string ObjectId,
    DateTimeOffset OccurredAt,
    string PropertyName,
    string? OldValue,
    string? NewValue)
    : WebhookEvent(ObjectType, ObjectId, OccurredAt);

internal sealed record ObjectDeletedEvent(
    string ObjectType,
    string ObjectId,
    DateTimeOffset OccurredAt)
    : WebhookEvent(ObjectType, ObjectId, OccurredAt);
