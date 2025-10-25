# Batch 5: Conversations APIs Implementation

## Overview
Implement the Conversations APIs to enable testing of chat, messaging, and visitor identification features in HubSpot.

## APIs to Implement (3 APIs)

### 1. Conversations Inbox & Messages
**Endpoint:** `/conversations/v3/conversations/*`
**Client:** `HubSpot.KiotaClient.Generated.Conversations.ConversationsInbox&Messages`

**Key Features:**
- List conversations/threads
- Get conversation details
- Send messages in conversations
- Update conversation status (open, closed)
- Assign conversations to agents/teams
- Conversation participants
- Message types (chat, email, etc.)

### 2. Custom Channels
**Endpoint:** `/conversations/v3/custom-channels/*`  
**Client:** `HubSpot.KiotaClient.Generated.Conversations.CustomChannels`

**Key Features:**
- Create custom conversation channels
- Configure channel settings
- Channel authentication
- Channel status management

### 3. Visitor Identification
**Endpoint:** `/conversations/v3/visitor-identification/*`
**Client:** `HubSpot.KiotaClient.Generated.Conversations.VisitorIdentification`

**Key Features:**
- Generate visitor identification tokens
- Identify visitors
- Link visitors to contacts
- Visitor tracking

## Implementation Plan

### Phase 1: Repository Layer (45 minutes)

#### ConversationRepository
```csharp
public class ConversationRepository
{
    // Core conversation storage
    - Dictionary<string, Conversation> _conversations
    - Dictionary<string, List<Message>> _conversationMessages
    - Dictionary<string, List<string>> _conversationParticipants
    
    // Methods
    - CreateConversation(status, channelId, inboxId)
    - GetConversation(conversationId)
    - ListConversations(filters)
    - UpdateConversationStatus(conversationId, status)
    - AddMessage(conversationId, message)
    - ListMessages(conversationId)
    - AssignConversation(conversationId, ownerId)
}
```

#### CustomChannelRepository
```csharp
public class CustomChannelRepository
{
    // Channel storage
    - Dictionary<string, CustomChannel> _channels
    
    // Methods
    - CreateChannel(name, settings)
    - GetChannel(channelId)
    - ListChannels()
    - UpdateChannel(channelId, settings)
    - DeleteChannel(channelId)
}
```

#### VisitorIdentificationRepository
```csharp
public class VisitorIdentificationRepository
{
    // Token and visitor mapping
    - Dictionary<string, VisitorToken> _visitorTokens
    - Dictionary<string, string> _visitorToContact // visitorId -> contactId
    
    // Methods
    - GenerateToken(email)
    - ValidateToken(token)
    - IdentifyVisitor(token, contactId)
    - GetVisitorContact(visitorId)
}
```

### Phase 2: API Routes (60 minutes)

#### Create: ApiRoutes.Conversations.cs
```csharp
public static class ApiRoutesConversations
{
    public static void RegisterConversationsApi(
        WebApplication app,
        ConversationRepository conversationRepo,
        CustomChannelRepository channelRepo,
        VisitorIdentificationRepository visitorRepo)
    {
        var conversations = app.MapGroup("/conversations/v3/conversations");
        // GET /conversations/v3/conversations
        // GET /conversations/v3/conversations/{conversationId}
        // PATCH /conversations/v3/conversations/{conversationId}
        // POST /conversations/v3/conversations/{conversationId}/messages
        // GET /conversations/v3/conversations/{conversationId}/messages
        
        var channels = app.MapGroup("/conversations/v3/custom-channels");
        // POST /conversations/v3/custom-channels
        // GET /conversations/v3/custom-channels
        // GET /conversations/v3/custom-channels/{channelId}
        // PATCH /conversations/v3/custom-channels/{channelId}
        // DELETE /conversations/v3/custom-channels/{channelId}
        
        var visitors = app.MapGroup("/conversations/v3/visitor-identification");
        // POST /conversations/v3/visitor-identification/tokens/create
        // GET /conversations/v3/visitor-identification/tokens/visitor/{visitorId}
    }
}
```

### Phase 3: Tests (30 minutes)

#### Create: ConversationsTests.cs
```csharp
public class ConversationsTests
{
    [Fact] // Create and retrieve conversation
    [Fact] // List conversations
    [Fact] // Send message in conversation
    [Fact] // Update conversation status
    [Fact] // Create custom channel
    [Fact] // Generate visitor token
    [Fact] // Identify visitor
}
```

## API Models Reference

### Conversation Object
```json
{
  "id": "123",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z",
  "status": "OPEN",
  "channelId": "channel-1",
  "inboxId": "inbox-1",
  "assignedTo": "owner-123",
  "participants": ["visitor-1", "agent-1"],
  "latestMessageTimestamp": "2024-01-01T00:00:00Z"
}
```

### Message Object
```json
{
  "id": "msg-123",
  "conversationId": "123",
  "type": "MESSAGE",
  "text": "Hello!",
  "createdAt": "2024-01-01T00:00:00Z",
  "sender": {
    "actorId": "visitor-1",
    "deliveryIdentifier": {
      "type": "VISITOR",
      "value": "visitor-1"
    }
  }
}
```

### Custom Channel Object
```json
{
  "id": "channel-123",
  "accountId": "account-1",
  "name": "My Custom Channel",
  "createdAt": "2024-01-01T00:00:00Z",
  "active": true
}
```

### Visitor Token Object
```json
{
  "token": "token-abc123",
  "visitorId": "visitor-123",
  "createdAt": "2024-01-01T00:00:00Z",
  "expiresAt": "2024-01-02T00:00:00Z"
}
```

## Implementation Order

1. **ConversationRepository** - Core conversation/message storage
2. **CustomChannelRepository** - Channel management
3. **VisitorIdentificationRepository** - Visitor tokens and identification
4. **ApiRoutes.Conversations.cs** - Route registration
5. **ConversationsTests.cs** - Test coverage

## Estimated Time
- Repositories: 45 minutes
- API Routes: 60 minutes  
- Tests: 30 minutes
- **Total: ~2.5 hours**

## Success Criteria
- ✅ All 3 conversation API clients work against mock server
- ✅ Can create conversations and send messages
- ✅ Can create and manage custom channels
- ✅ Can generate and use visitor identification tokens
- ✅ All tests pass
- ✅ Build passes

## Related Files
- Repositories: `src/HubSpot.MockServer/Repositories/`
- Routes: `src/HubSpot.MockServer/ApiRoutes/ApiRoutes.Conversations.cs`
- Tests: `test/HubSpot.Tests/MockServer/ConversationsTests.cs`
