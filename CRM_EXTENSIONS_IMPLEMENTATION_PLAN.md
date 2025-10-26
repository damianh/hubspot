# CRM Extensions Implementation Plan

**Date:** 2025-10-26
**Status:** Planning Phase

---

## Overview

CRM Extensions are specialized APIs that extend HubSpot CRM functionality with integrations for calling, video conferencing, CRM cards, and transcriptions. These APIs are distinct from the standard CRM object APIs.

---

## Current Status

### ✅ Already Implemented (via CrmExtensionsTests.cs)
- **Schemas API** - Custom object schema management
- **Imports API** - Data import operations
- **Exports API** - Data export operations
- **Timeline API** - Timeline event management

### ❌ Not Yet Implemented (True CRM Extensions)
The following 4 CRM Extension APIs need implementation:

1. **Calling Extensions V3** - `/crm/v3/extensions/calling/*`
2. **CRM Cards (Public App CRM Cards) V3** - `/crm/v3/extensions/cards/*`
3. **Video Conferencing Extensions V3** - `/crm/v3/extensions/videoconferencing/*`
4. **Transcriptions V3** - `/crm/v3/extensions/transcriptions/*`

---

## API Analysis

### 1. Calling Extensions V3

**Client:** `HubSpotCRMCallingExtensionsV3Client`
**Base Path:** `/crm/v3/extensions/calling`

**Endpoints:**
```
GET    /crm/v3/extensions/calling/{appId}/settings
PATCH  /crm/v3/extensions/calling/{appId}/settings
POST   /crm/v3/extensions/calling/{appId}/settings/ready
DELETE /crm/v3/extensions/calling/{appId}/settings
POST   /crm/v3/extensions/calling/{engagementId}/recordings/{recordingId}
GET    /crm/v3/extensions/calling/{engagementId}/recordings
```

**Purpose:** Manage calling integrations that appear in HubSpot CRM
- Register/configure calling apps
- Manage call recordings
- Link calls to CRM records

**Data Model:**
- Settings: App configuration, webhook URLs, calling options
- Recordings: Audio/video call recordings metadata

---

### 2. CRM Cards (Public App CRM Cards) V3

**Client:** `HubSpotCRMPublicAppCrmCardsV3Client`
**Base Path:** `/crm/v3/extensions/cards`

**Endpoints:**
```
GET    /crm/v3/extensions/cards/{appId}
POST   /crm/v3/extensions/cards/{appId}
PATCH  /crm/v3/extensions/cards/{appId}/{cardId}
DELETE /crm/v3/extensions/cards/{appId}/{cardId}
POST   /crm/v3/extensions/cards/sample-response
```

**Purpose:** Create custom cards that display on CRM records
- Custom data visualization
- External system integration
- Interactive actions within CRM

**Data Model:**
- Card definitions: Layout, display properties, actions
- Card fetch requests: Dynamic card content
- Card responses: Data to display

---

### 3. Video Conferencing Extensions V3

**Client:** `HubSpotCRMVideoConferencingExtensionV3Client`
**Base Path:** `/crm/v3/extensions/videoconferencing`

**Endpoints:**
```
GET    /crm/v3/extensions/videoconferencing/{appId}/settings
POST   /crm/v3/extensions/videoconferencing/{appId}/settings
PATCH  /crm/v3/extensions/videoconferencing/{appId}/settings
DELETE /crm/v3/extensions/videoconferencing/{appId}/settings
```

**Purpose:** Integrate video conferencing tools (Zoom, Teams, etc.)
- Register video conferencing providers
- Create meeting links within HubSpot
- Track meeting engagement

**Data Model:**
- Settings: Provider configuration, webhook URLs
- Meeting metadata: Links, participants, duration

---

### 4. Transcriptions V3

**Client:** `HubSpotCRMTranscriptionsV3Client`
**Base Path:** `/crm/v3/extensions/transcriptions`

**Endpoints:**
```
POST   /crm/v3/extensions/transcriptions/{engagementId}
GET    /crm/v3/extensions/transcriptions/{engagementId}
```

**Purpose:** Store and manage call/meeting transcriptions
- Upload transcription text
- Link transcriptions to calls/meetings
- Support AI-powered insights

**Data Model:**
- Transcription text: Full conversation text
- Metadata: Speakers, timestamps, confidence scores
- Associated engagement: Call or meeting record

---

## Implementation Strategy

### Phase 1: Data Models & Repositories

Create repositories to manage extension data:

1. **CallingExtensionRepository**
   - Store app settings
   - Manage recordings metadata
   - Track engagement associations

2. **CrmCardRepository**
   - Store card definitions per app
   - Handle card CRUD operations
   - Manage sample responses

3. **VideoConferencingRepository**
   - Store provider settings
   - Track configuration per app

4. **TranscriptionRepository**
   - Store transcription text
   - Associate with engagements
   - Handle metadata

### Phase 2: API Routes Implementation

Create API route handlers in `ApiRoutes.Extensions.cs`:

```csharp
public static partial class ApiRoutes
{
    public static void RegisterCrmExtensions(this RouteGroupBuilder group)
    {
        RegisterCallingExtensions(group);
        RegisterCrmCards(group);
        RegisterVideoConferencing(group);
        RegisterTranscriptions(group);
    }
    
    private static void RegisterCallingExtensions(RouteGroupBuilder group)
    {
        var calling = group.MapGroup("/crm/v3/extensions/calling");
        
        // GET /crm/v3/extensions/calling/{appId}/settings
        calling.MapGet("/{appId}/settings", async (string appId, CallingExtensionRepository repo) => 
        {
            var settings = await repo.GetSettingsAsync(appId);
            return settings != null ? Results.Ok(settings) : Results.NotFound();
        });
        
        // PATCH /crm/v3/extensions/calling/{appId}/settings
        calling.MapPatch("/{appId}/settings", async (string appId, JsonElement body, CallingExtensionRepository repo) =>
        {
            var settings = await repo.UpdateSettingsAsync(appId, body);
            return Results.Ok(settings);
        });
        
        // POST /crm/v3/extensions/calling/{appId}/settings/ready
        calling.MapPost("/{appId}/settings/ready", async (string appId, CallingExtensionRepository repo) =>
        {
            await repo.MarkAsReadyAsync(appId);
            return Results.NoContent();
        });
        
        // DELETE /crm/v3/extensions/calling/{appId}/settings
        calling.MapDelete("/{appId}/settings", async (string appId, CallingExtensionRepository repo) =>
        {
            await repo.DeleteSettingsAsync(appId);
            return Results.NoContent();
        });
        
        // POST /crm/v3/extensions/calling/{engagementId}/recordings/{recordingId}
        calling.MapPost("/{engagementId}/recordings/{recordingId}", async (
            string engagementId, 
            string recordingId, 
            JsonElement body,
            CallingExtensionRepository repo) =>
        {
            var recording = await repo.AddRecordingAsync(engagementId, recordingId, body);
            return Results.Created($"/crm/v3/extensions/calling/{engagementId}/recordings/{recordingId}", recording);
        });
        
        // GET /crm/v3/extensions/calling/{engagementId}/recordings
        calling.MapGet("/{engagementId}/recordings", async (string engagementId, CallingExtensionRepository repo) =>
        {
            var recordings = await repo.GetRecordingsAsync(engagementId);
            return Results.Ok(new { results = recordings });
        });
    }
    
    private static void RegisterCrmCards(RouteGroupBuilder group)
    {
        var cards = group.MapGroup("/crm/v3/extensions/cards");
        
        // GET /crm/v3/extensions/cards/{appId}
        cards.MapGet("/{appId}", async (string appId, CrmCardRepository repo) =>
        {
            var cardList = await repo.GetCardsAsync(appId);
            return Results.Ok(new { results = cardList });
        });
        
        // POST /crm/v3/extensions/cards/{appId}
        cards.MapPost("/{appId}", async (string appId, JsonElement body, CrmCardRepository repo) =>
        {
            var card = await repo.CreateCardAsync(appId, body);
            return Results.Created($"/crm/v3/extensions/cards/{appId}/{card.Id}", card);
        });
        
        // PATCH /crm/v3/extensions/cards/{appId}/{cardId}
        cards.MapPatch("/{appId}/{cardId}", async (string appId, string cardId, JsonElement body, CrmCardRepository repo) =>
        {
            var card = await repo.UpdateCardAsync(appId, cardId, body);
            return card != null ? Results.Ok(card) : Results.NotFound();
        });
        
        // DELETE /crm/v3/extensions/cards/{appId}/{cardId}
        cards.MapDelete("/{appId}/{cardId}", async (string appId, string cardId, CrmCardRepository repo) =>
        {
            await repo.DeleteCardAsync(appId, cardId);
            return Results.NoContent();
        });
        
        // POST /crm/v3/extensions/cards/sample-response
        cards.MapPost("/sample-response", async (JsonElement body) =>
        {
            // Return sample card response for testing
            return Results.Ok(new
            {
                results = new[]
                {
                    new
                    {
                        objectId = 12345,
                        title = "Sample Card",
                        properties = new[]
                        {
                            new { label = "Property 1", value = "Value 1" }
                        }
                    }
                }
            });
        });
    }
    
    private static void RegisterVideoConferencing(RouteGroupBuilder group)
    {
        var video = group.MapGroup("/crm/v3/extensions/videoconferencing");
        
        // GET /crm/v3/extensions/videoconferencing/{appId}/settings
        video.MapGet("/{appId}/settings", async (string appId, VideoConferencingRepository repo) =>
        {
            var settings = await repo.GetSettingsAsync(appId);
            return settings != null ? Results.Ok(settings) : Results.NotFound();
        });
        
        // POST /crm/v3/extensions/videoconferencing/{appId}/settings
        video.MapPost("/{appId}/settings", async (string appId, JsonElement body, VideoConferencingRepository repo) =>
        {
            var settings = await repo.CreateSettingsAsync(appId, body);
            return Results.Created($"/crm/v3/extensions/videoconferencing/{appId}/settings", settings);
        });
        
        // PATCH /crm/v3/extensions/videoconferencing/{appId}/settings
        video.MapPatch("/{appId}/settings", async (string appId, JsonElement body, VideoConferencingRepository repo) =>
        {
            var settings = await repo.UpdateSettingsAsync(appId, body);
            return Results.Ok(settings);
        });
        
        // DELETE /crm/v3/extensions/videoconferencing/{appId}/settings
        video.MapDelete("/{appId}/settings", async (string appId, VideoConferencingRepository repo) =>
        {
            await repo.DeleteSettingsAsync(appId);
            return Results.NoContent();
        });
    }
    
    private static void RegisterTranscriptions(RouteGroupBuilder group)
    {
        var transcriptions = group.MapGroup("/crm/v3/extensions/transcriptions");
        
        // POST /crm/v3/extensions/transcriptions/{engagementId}
        transcriptions.MapPost("/{engagementId}", async (
            string engagementId, 
            JsonElement body,
            TranscriptionRepository repo) =>
        {
            var transcription = await repo.CreateTranscriptionAsync(engagementId, body);
            return Results.Created($"/crm/v3/extensions/transcriptions/{engagementId}", transcription);
        });
        
        // GET /crm/v3/extensions/transcriptions/{engagementId}
        transcriptions.MapGet("/{engagementId}", async (string engagementId, TranscriptionRepository repo) =>
        {
            var transcription = await repo.GetTranscriptionAsync(engagementId);
            return transcription != null ? Results.Ok(transcription) : Results.NotFound();
        });
    }
}
```

### Phase 3: Integration Tests

Create comprehensive tests using the Kiota-generated clients:

```csharp
// In CrmExtensionsTests.cs or new file CrmExtensionsApiTests.cs

// Calling Extensions Tests
[Fact]
public async Task CallingExtensions_ConfigureSettings_ShouldSucceed()
{
    var client = new HubSpotCRMCallingExtensionsV3Client(_adapter);
    
    var settings = new CallingSettings
    {
        Name = "Test Calling App",
        Url = "https://example.com/call",
        // ... other properties
    };
    
    var result = await client.Crm.V3.Extensions.Calling[appId].Settings.PatchAsync(settings);
    result.ShouldNotBeNull();
}

[Fact]
public async Task CallingExtensions_AddRecording_ShouldSucceed()
{
    var client = new HubSpotCRMCallingExtensionsV3Client(_adapter);
    
    var recording = new CallRecording
    {
        Url = "https://example.com/recording.mp3",
        Duration = 300,
        // ... other properties
    };
    
    var result = await client.Crm.V3.Extensions.Calling[engagementId]
        .Recordings[recordingId].PostAsync(recording);
    result.ShouldNotBeNull();
}

// CRM Cards Tests
[Fact]
public async Task CrmCards_CreateCard_ShouldSucceed()
{
    var client = new HubSpotCRMPublicAppCrmCardsV3Client(_adapter);
    
    var card = new CardCreateRequest
    {
        Title = "Customer Data Card",
        Fetch = new CardFetchBody { /* ... */ },
        Display = new CardDisplayBody { /* ... */ }
    };
    
    var result = await client.Crm.V3.Extensions.CardsDev[appId].PostAsync(card);
    result.ShouldNotBeNull();
    result.Id.ShouldNotBeNullOrEmpty();
}

// Video Conferencing Tests
[Fact]
public async Task VideoConferencing_ConfigureProvider_ShouldSucceed()
{
    var client = new HubSpotCRMVideoConferencingExtensionV3Client(_adapter);
    
    var settings = new VideoConferencingSettings
    {
        Name = "Zoom Integration",
        // ... other properties
    };
    
    var result = await client.Crm.V3.Extensions.Settings[appId].PostAsync(settings);
    result.ShouldNotBeNull();
}

// Transcriptions Tests
[Fact]
public async Task Transcriptions_UploadTranscript_ShouldSucceed()
{
    var client = new HubSpotCRMTranscriptionsV3Client(_adapter);
    
    var transcript = new Transcription
    {
        Text = "This is the call transcription...",
        Confidence = 0.95,
        // ... other properties
    };
    
    var result = await client.Crm.V3.Extensions.Transcriptions[engagementId].PostAsync(transcript);
    result.ShouldNotBeNull();
}
```

---

## Implementation Order

### Priority 1: Foundation (1-2 hours)
1. Create repository interfaces and implementations
2. Create data models for each extension type
3. Add repository registrations to DI container

### Priority 2: API Routes (2-3 hours)
1. Implement `ApiRoutes.Extensions.cs`
2. Register all extension endpoints
3. Add proper error handling and validation

### Priority 3: Integration Tests (2-3 hours)
1. Add tests for Calling Extensions
2. Add tests for CRM Cards
3. Add tests for Video Conferencing
4. Add tests for Transcriptions
5. Validate all tests pass

### Priority 4: Documentation (30 minutes)
1. Update README with extensions information
2. Document usage examples
3. Add API coverage summary

---

## Data Structures

### Calling Extension Settings
```json
{
  "id": "app-123",
  "name": "My Calling App",
  "url": "https://example.com/widget",
  "height": 600,
  "width": 400,
  "isReady": true,
  "supportsCustomObjects": true,
  "createdAt": "2025-10-26T00:00:00Z",
  "updatedAt": "2025-10-26T00:00:00Z"
}
```

### Call Recording
```json
{
  "id": "recording-456",
  "engagementId": "engagement-789",
  "url": "https://example.com/recordings/456.mp3",
  "duration": 300,
  "transcript": "Optional transcript text...",
  "createdAt": "2025-10-26T00:00:00Z"
}
```

### CRM Card Definition
```json
{
  "id": "card-123",
  "appId": "app-456",
  "title": "Customer Insights",
  "fetch": {
    "targetUrl": "https://example.com/fetch-card",
    "objectTypes": [{"name": "contacts"}]
  },
  "display": {
    "properties": [
      {"name": "status", "label": "Status", "dataType": "STATUS"}
    ]
  },
  "actions": {
    "primary": {
      "type": "IFRAME",
      "label": "View Details",
      "url": "https://example.com/details"
    }
  }
}
```

### Video Conferencing Settings
```json
{
  "id": "app-789",
  "name": "Zoom Integration",
  "url": "https://zoom.us/create-meeting",
  "isReady": true,
  "createdAt": "2025-10-26T00:00:00Z"
}
```

### Transcription
```json
{
  "id": "transcript-101",
  "engagementId": "call-202",
  "text": "Full transcription text here...",
  "confidence": 0.95,
  "language": "en",
  "createdAt": "2025-10-26T00:00:00Z"
}
```

---

## Success Criteria

- ✅ All 4 CRM Extension APIs implemented
- ✅ Full CRUD operations for each API
- ✅ Integration tests using Kiota clients
- ✅ All tests passing
- ✅ Proper error handling
- ✅ Documentation updated

---

## Estimated Time: 6-8 hours

---

## Notes

- These are specialized integration APIs, not CRM object APIs
- They enable external apps to extend HubSpot functionality
- Each API has unique patterns (app-based settings, engagement associations)
- Tests should validate both basic CRUD and HubSpot-specific behaviors
- Consider mocking webhook callbacks for realistic testing scenarios
