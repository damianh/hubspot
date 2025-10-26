namespace DamianH.HubSpot.MockServer.Repositories.Conversation;

internal class ConversationData
{
    public string Id { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Status { get; set; } = "OPEN";
    public string ChannelId { get; set; } = string.Empty;
    public string InboxId { get; set; } = string.Empty;
    public string? AssignedTo { get; set; }
    public List<string> Participants { get; set; } = [];
    public DateTimeOffset LatestMessageTimestamp { get; set; }
}
