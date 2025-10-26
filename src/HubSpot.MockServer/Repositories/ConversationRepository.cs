using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class ConversationRepository
{
    private readonly ConcurrentDictionary<string, ConversationData> _conversations = new();
    private readonly ConcurrentDictionary<string, List<MessageData>> _conversationMessages = new();
    private long _nextConversationId = 1;
    private long _nextMessageId = 1;

    public ConversationData CreateConversation(string? channelId = null, string? inboxId = null, string? status = null)
    {
        var id = Interlocked.Increment(ref _nextConversationId).ToString();
        var now = DateTimeOffset.UtcNow;

        var conversation = new ConversationData
        {
            Id = id,
            CreatedAt = now,
            UpdatedAt = now,
            Status = status ?? "OPEN",
            ChannelId = channelId ?? "default-channel",
            InboxId = inboxId ?? "default-inbox",
            Participants = [],
            LatestMessageTimestamp = now
        };

        _conversations[id] = conversation;
        _conversationMessages[id] = [];

        return conversation;
    }

    public ConversationData? GetConversation(string conversationId) => _conversations.GetValueOrDefault(conversationId);

    public List<ConversationData> ListConversations(string? status = null, int? limit = null, string? after = null)
    {
        var conversations = _conversations.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(status))
        {
            conversations = conversations.Where(c => c.Status == status);
        }

        if (!string.IsNullOrEmpty(after))
        {
            conversations = conversations.Where(c => string.Compare(c.Id, after, StringComparison.Ordinal) > 0);
        }

        conversations = conversations.OrderBy(c => c.CreatedAt);

        if (limit.HasValue)
        {
            conversations = conversations.Take(limit.Value);
        }

        return conversations.ToList();
    }

    public ConversationData? UpdateConversationStatus(string conversationId, string status)
    {
        if (!_conversations.TryGetValue(conversationId, out var conversation))
        {
            return null;
        }

        conversation.Status = status;
        conversation.UpdatedAt = DateTimeOffset.UtcNow;

        return conversation;
    }

    public ConversationData? AssignConversation(string conversationId, string ownerId)
    {
        if (!_conversations.TryGetValue(conversationId, out var conversation))
        {
            return null;
        }

        conversation.AssignedTo = ownerId;
        conversation.UpdatedAt = DateTimeOffset.UtcNow;

        return conversation;
    }

    public MessageData AddMessage(string conversationId, string text, string senderType, string senderId, string? messageType = null)
    {
        if (!_conversations.TryGetValue(conversationId, out var conversation))
        {
            throw new InvalidOperationException($"Conversation {conversationId} not found");
        }

        var messageId = Interlocked.Increment(ref _nextMessageId).ToString();
        var now = DateTimeOffset.UtcNow;

        var message = new MessageData
        {
            Id = messageId,
            ConversationId = conversationId,
            Type = messageType ?? "MESSAGE",
            Text = text,
            CreatedAt = now,
            SenderActorId = senderId,
            SenderType = senderType
        };

        if (!_conversationMessages.ContainsKey(conversationId))
        {
            _conversationMessages[conversationId] = [];
        }

        _conversationMessages[conversationId].Add(message);

        // Update conversation
        conversation.LatestMessageTimestamp = now;
        conversation.UpdatedAt = now;

        if (!conversation.Participants.Contains(senderId))
        {
            conversation.Participants.Add(senderId);
        }

        return message;
    }

    public List<MessageData> ListMessages(string conversationId, int? limit = null)
    {
        if (!_conversationMessages.TryGetValue(conversationId, out var messages))
        {
            return [];
        }

        var result = messages.OrderBy(m => m.CreatedAt).AsEnumerable();

        if (limit.HasValue)
        {
            result = result.Take(limit.Value);
        }

        return result.ToList();
    }

    public void DeleteConversation(string conversationId)
    {
        _conversations.TryRemove(conversationId, out _);
        _conversationMessages.TryRemove(conversationId, out _);
    }

    public void Clear()
    {
        _conversations.Clear();
        _conversationMessages.Clear();
        _nextConversationId = 1;
        _nextMessageId = 1;
    }
}

public class ConversationData
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

public class MessageData
{
    public string Id { get; set; } = string.Empty;
    public string ConversationId { get; set; } = string.Empty;
    public string Type { get; set; } = "MESSAGE";
    public string Text { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string SenderActorId { get; set; } = string.Empty;
    public string SenderType { get; set; } = "VISITOR";
}
