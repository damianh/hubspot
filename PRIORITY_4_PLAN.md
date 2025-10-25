# Priority 4: Webhooks API Implementation Plan

## Overview
Implement the Webhooks V3 API (`/webhooks/v3`) to support webhook subscription management. This allows the mock server to handle webhook subscription CRUD operations.

## Discovered Endpoints

From the Kiota-generated client analysis:

### Base Path: `/webhooks/v3/{appId}`

**Subscription Operations:**
1. `GET /webhooks/v3/{appId}/subscriptions` - List webhook subscriptions (paginated)
2. `POST /webhooks/v3/{appId}/subscriptions` - Create webhook subscription
3. `GET /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Get subscription by ID
4. `PATCH /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Update subscription
5. `DELETE /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Delete subscription

**Batch Operations:**
6. `POST /webhooks/v3/{appId}/subscriptions/batch/update` - Batch update subscriptions

**Settings:**
7. `GET /webhooks/v3/{appId}/settings` - Get webhook settings
8. `PUT /webhooks/v3/{appId}/settings` - Update webhook settings
9. `DELETE /webhooks/v3/{appId}/settings` - Clear webhook settings

## Implementation Strategy

### 1. Create Repository
Create `WebhookRepository` to manage:
- Webhook subscriptions per app ID
- Webhook settings per app ID
- Thread-safe operations

### 2. Models (reuse Kiota-generated)
All models already exist in the Kiota-generated client - need to analyze the client to identify exact model names.

### 3. API Routes Registration
Create `ApiRoutes.Webhooks.cs` (partial class) with:
- `RegisterWebhooksApi()` method
- Subscription CRUD handlers
- Batch update handler
- Settings handlers

### 4. Testing
Create `WebhooksTests.cs` to test:
- Create subscription
- List subscriptions
- Get subscription by ID
- Update subscription
- Delete subscription
- Batch update subscriptions
- Get/Update/Clear settings

## Implementation Steps
1. ✅ Analyze Kiota-generated client structure to identify models
2. ✅ Create `WebhookRepository`
3. ✅ Create `ApiRoutes.Webhooks.cs` (partial class)
4. ✅ Implement subscription endpoints
5. ✅ Implement batch operations
6. ✅ Implement settings endpoints
7. ✅ Update `HubSpotMockServer.cs` to register Webhooks API
8. ✅ Create comprehensive tests
9. ⚠️ Tests are failing with 400 errors - likely enum serialization issue similar to Marketing API

## Current Status
- ✅ Core implementation complete
- ✅ Repository, API routes, and tests all created
- ✅ 9 tests created covering all endpoints
- ✅ Build successful
- ✅ **All tests passing!**
- ✅ Enum serialization issue resolved by creating simple DTO models and mapping event types
- ✅ **Priority 4 Complete!**

## Next Steps
- Debug the 400 error in subscription creation
- Likely need to ensure enum values are properly serialized as strings (similar fix to Marketing API)
- Verify JSON serialization configuration is applied to webhook endpoints

## Repository Design

```csharp
public class WebhookRepository
{
    // Subscriptions per app ID
    ConcurrentDictionary<string, ConcurrentDictionary<string, SubscriptionResponse>> _subscriptions
    
    // Settings per app ID
    ConcurrentDictionary<string, SettingsResponse> _settings
    
    // Subscriptions
    CreateSubscription(appId, request) -> SubscriptionResponse
    GetSubscription(appId, subscriptionId) -> SubscriptionResponse?
    ListSubscriptions(appId) -> CollectionResponseSubscriptionResponse
    UpdateSubscription(appId, subscriptionId, request) -> SubscriptionResponse
    DeleteSubscription(appId, subscriptionId) -> bool
    BatchUpdateSubscriptions(appId, requests) -> BatchResponseSubscriptionResponse
    
    // Settings
    GetSettings(appId) -> SettingsResponse?
    UpdateSettings(appId, settings) -> SettingsResponse
    ClearSettings(appId) -> bool
}
```

## Notes
- Subscriptions are scoped per app ID
- Settings are also scoped per app ID
- Keep implementation simple and focused on testing needs
- No actual webhook delivery - just subscription management
