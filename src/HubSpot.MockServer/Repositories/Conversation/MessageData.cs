namespace DamianH.HubSpot.MockServer.Repositories.Conversation;

internal class MessageData
{
    public string Id { get; set; } = string.Empty;
    public string ConversationId { get; set; } = string.Empty;
    public string Type { get; set; } = "MESSAGE";
    public string Text { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string SenderActorId { get; set; } = string.Empty;
    public string SenderType { get; set; } = "VISITOR";
}
