using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.CustomChannel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterConversationsApi(WebApplication app)
    {
        // Conversations Inbox & Messages API
        var conversations = app.MapGroup("/conversations/v3/conversations");

        conversations.MapGet("/", (
            ConversationRepository conversationRepo,
            string? status,
            int? limit,
            string? after) =>
        {
            var results = conversationRepo.ListConversations(status, limit, after);
            return Results.Ok(new
            {
                results = results.Select(ToConversationResponse),
                paging = new { next = results.Count >= (limit ?? 100)
                    ? new { after = results.Last().Id }
                    : null }
            });
        });

        conversations.MapGet("/{conversationId}", (
            ConversationRepository conversationRepo,
            string conversationId) =>
        {
            var conversation = conversationRepo.GetConversation(conversationId);
            return conversation == null
                ? Results.NotFound(new { message = $"Conversation {conversationId} not found" })
                : Results.Ok(ToConversationResponse(conversation));
        });

        conversations.MapPatch("/{conversationId}", async (
            ConversationRepository conversationRepo,
            string conversationId,
            HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var update = JsonSerializer.Deserialize<JsonElement>(body);

            ConversationData? conversation = null;

            if (update.TryGetProperty("status", out var statusProp))
            {
                var status = statusProp.GetString();
                conversation = conversationRepo.UpdateConversationStatus(conversationId, status ?? "OPEN");
            }

            if (update.TryGetProperty("assignedTo", out var assignedProp))
            {
                var ownerId = assignedProp.GetString();
                conversation = conversationRepo.AssignConversation(conversationId, ownerId ?? "");
            }

            return conversation == null
                ? Results.NotFound(new { message = $"Conversation {conversationId} not found" })
                : Results.Ok(ToConversationResponse(conversation));
        });

        conversations.MapPost("/{conversationId}/messages", async (
            ConversationRepository conversationRepo,
            string conversationId,
            HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var messageData = JsonSerializer.Deserialize<JsonElement>(body);

            var text = messageData.GetProperty("text").GetString() ?? "";
            var senderType = "VISITOR";
            var senderId = "visitor-default";
            var messageType = "MESSAGE";

            if (messageData.TryGetProperty("sender", out var sender))
            {
                if (sender.TryGetProperty("deliveryIdentifier", out var deliveryId))
                {
                    senderType = deliveryId.GetProperty("type").GetString() ?? "VISITOR";
                    senderId = deliveryId.GetProperty("value").GetString() ?? "visitor-default";
                }
            }

            if (messageData.TryGetProperty("type", out var typeProp))
            {
                messageType = typeProp.GetString() ?? "MESSAGE";
            }

            try
            {
                var message = conversationRepo.AddMessage(conversationId, text, senderType, senderId, messageType);
                return Results.Ok(ToMessageResponse(message));
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(new { message = $"Conversation {conversationId} not found" });
            }
        });

        conversations.MapGet("/{conversationId}/messages", (
            ConversationRepository conversationRepo,
            string conversationId,
            int? limit) =>
        {
            var messages = conversationRepo.ListMessages(conversationId, limit);
            return Results.Ok(new
            {
                results = messages.Select(ToMessageResponse),
                paging = new { }
            });
        });

        // Custom Channels API
        var channels = app.MapGroup("/conversations/v3/custom-channels");

        channels.MapPost("/", async(CustomChannelRepository channelRepo, HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var channelData = JsonSerializer.Deserialize<JsonElement>(body);

            var name = channelData.GetProperty("name").GetString() ?? "Unnamed Channel";
            var accountId = channelData.TryGetProperty("accountId", out var accId)
                ? accId.GetString()
                : null;

            var channel = channelRepo.CreateChannel(name, accountId);
            return Results.Ok(ToChannelResponse(channel));
        });

        channels.MapGet("/", (CustomChannelRepository channelRepo) =>
        {
            var results = channelRepo.ListChannels();
            return Results.Ok(new
            {
                results = results.Select(ToChannelResponse)
            });
        });

        channels.MapGet("/{channelId}", (
            CustomChannelRepository channelRepo,
            string channelId) =>
        {
            var channel = channelRepo.GetChannel(channelId);
            if (channel == null)
            {
                return Results.NotFound(new { message = $"Channel {channelId} not found" });
            }
            return Results.Ok(ToChannelResponse(channel));
        });

        channels.MapPatch("/{channelId}", async(
            CustomChannelRepository channelRepo,
            string channelId,
            HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var update = JsonSerializer.Deserialize<JsonElement>(body);

            string? name = null;
            bool? active = null;

            if (update.TryGetProperty("name", out var nameProp))
            {
                name = nameProp.GetString();
            }

            if (update.TryGetProperty("active", out var activeProp))
            {
                active = activeProp.GetBoolean();
            }

            var channel = channelRepo.UpdateChannel(channelId, name, active);
            if (channel == null)
            {
                return Results.NotFound(new { message = $"Channel {channelId} not found" });
            }

            return Results.Ok(ToChannelResponse(channel));
        });

        channels.MapDelete("/{channelId}", (
            CustomChannelRepository channelRepo,
            string channelId) =>
        {
            var deleted = channelRepo.DeleteChannel(channelId);
            return !deleted
                ? Results.NotFound(new { message = $"Channel {channelId} not found" })
                : Results.NoContent();
        });

        // Visitor Identification API
        var visitors = app.MapGroup("/conversations/v3/visitor-identification");

        visitors.MapPost("/tokens/create", async(
            VisitorIdentificationRepository visitorRepo,
            HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();

            string? email = null;
            if (!string.IsNullOrWhiteSpace(body))
            {
                var tokenData = JsonSerializer.Deserialize<JsonElement>(body);
                if (tokenData.TryGetProperty("email", out var emailProp))
                {
                    email = emailProp.GetString();
                }
            }

            var token = visitorRepo.GenerateToken(email);
            return Results.Ok(ToVisitorTokenResponse(token));
        });

        visitors.MapGet("/tokens/visitor/{visitorId}", (
            VisitorIdentificationRepository visitorRepo,
            string visitorId) =>
        {
            var token = visitorRepo.GetTokenByVisitorId(visitorId);
            if (token == null)
            {
                return Results.NotFound(new { message = $"Visitor {visitorId} not found" });
            }
            return Results.Ok(ToVisitorTokenResponse(token));
        });

        visitors.MapPost("/identify", async (
            VisitorIdentificationRepository visitorRepo,
            HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var identifyData = JsonSerializer.Deserialize<JsonElement>(body);

            var visitorId = identifyData.GetProperty("visitorId").GetString() ?? "";
            var contactId = identifyData.GetProperty("contactId").GetString() ?? "";

            visitorRepo.IdentifyVisitor(visitorId, contactId);

            return Results.Ok(new
            {
                visitorId,
                contactId,
                identified = true
            });
        });
    }

    private static object ToConversationResponse(ConversationData conversation) => new
    {
        id = conversation.Id,
        createdAt = conversation.CreatedAt,
        updatedAt = conversation.UpdatedAt,
        status = conversation.Status,
        channelId = conversation.ChannelId,
        inboxId = conversation.InboxId,
        assignedTo = conversation.AssignedTo,
        participants = conversation.Participants,
        latestMessageTimestamp = conversation.LatestMessageTimestamp
    };

    private static object ToMessageResponse(MessageData message) => new
    {
        id = message.Id,
        conversationId = message.ConversationId,
        type = message.Type,
        text = message.Text,
        createdAt = message.CreatedAt,
        sender = new
        {
            actorId = message.SenderActorId,
            deliveryIdentifier = new
            {
                type = message.SenderType,
                value = message.SenderActorId
            }
        }
    };

    private static object ToChannelResponse(CustomChannelData channel) => new
    {
        id = channel.Id,
        accountId = channel.AccountId,
        name = channel.Name,
        createdAt = channel.CreatedAt,
        active = channel.Active
    };

    private static object ToVisitorTokenResponse(VisitorTokenData token) => new
    {
        token = token.Token,
        visitorId = token.VisitorId,
        email = token.Email,
        createdAt = token.CreatedAt,
        expiresAt = token.ExpiresAt
    };
}
