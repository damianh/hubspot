# Priority 3: Marketing Transactional API Implementation Plan

## Overview
Implement the Marketing Transactional Single Send API (`/marketing/v3/transactional`) to support transactional email sending and SMTP token management.

## Discovered Endpoints

### 1. Single Email Send
**POST /marketing/v3/transactional/single-email/send**
- Send a single transactional email asynchronously
- Request: `PublicSingleSendRequestEgg` (emailId, message, contactProperties, customProperties)
- Response: `EmailSendStatusView` (statusId, status, sendResult, timestamps)

### 2. SMTP Tokens Management
**GET /marketing/v3/transactional/smtp-tokens**
- Query multiple SMTP API tokens
- Query params: campaignName, emailCampaignId, limit, after
- Response: `CollectionResponseSmtpApiTokenViewForwardPaging`

**POST /marketing/v3/transactional/smtp-tokens**
- Create a new SMTP API token
- Request: `SmtpApiTokenRequestEgg`
- Response: `SmtpApiTokenView`

**GET /marketing/v3/transactional/smtp-tokens/{tokenId}**
- Get a specific SMTP token by ID
- Response: `SmtpApiTokenView`

**DELETE /marketing/v3/transactional/smtp-tokens/{tokenId}**
- Delete/archive an SMTP token
- No response body

**PUT /marketing/v3/transactional/smtp-tokens/{tokenId}**
- Reset password for an SMTP token
- Response: `SmtpApiTokenView` (with new password)

## Implementation Strategy

### 1. Create Repository
Create `TransactionalEmailRepository` to manage:
- Email send requests and their status
- SMTP tokens storage and management
- Thread-safe operations

### 2. Models (reuse Kiota-generated)
All models already exist in the Kiota-generated client:
- `PublicSingleSendRequestEgg`
- `EmailSendStatusView`
- `SmtpApiTokenView`
- `SmtpApiTokenRequestEgg`
- `CollectionResponseSmtpApiTokenViewForwardPaging`

### 3. API Routes Registration
Create `ApiRoutes.Marketing.cs` with:
- `RegisterMarketingTransactionalApi()` method
- Email send endpoint handler
- SMTP token CRUD handlers

### 4. Testing
Create `MarketingTransactionalTests.cs` to test:
- Send transactional email
- Create SMTP token
- List SMTP tokens
- Get SMTP token by ID
- Reset SMTP token password
- Delete SMTP token

## Implementation Steps
1. ✅ Analyze Kiota-generated client structure
2. ✅ Create `TransactionalEmailRepository`
3. ✅ Create `ApiRoutes.Marketing.cs` (partial class)
4. ✅ Implement email send endpoint
5. ✅ Implement SMTP token endpoints
6. ✅ Update `HubSpotMockServer.cs` to register Marketing API
7. ✅ Create comprehensive tests
8. ⚠️ Tests failing due to enum serialization issue - needs debugging

## Current Status
- Core implementation complete
- Repository, API routes, and tests all created
- 6 tests created: Send email, Create/List/Get/Delete SMTP tokens
- Build successful
- Tests failing with enum parsing error - EmailSendStatusView_status and EmailSendStatusView_sendResult enums not being parsed correctly
- Need to investigate enum value naming/casing in responses

## Repository Design

```csharp
public class TransactionalEmailRepository
{
    // Email Sends
    ConcurrentDictionary<string, EmailSendStatusView> _emailSends
    
    SendEmail(request) -> EmailSendStatusView
    GetEmailSendStatus(statusId) -> EmailSendStatusView?
    
    // SMTP Tokens
    ConcurrentDictionary<string, SmtpApiTokenView> _smtpTokens
    
    CreateSmtpToken(request) -> SmtpApiTokenView
    GetSmtpToken(tokenId) -> SmtpApiTokenView?
    ListSmtpTokens(filters) -> IEnumerable<SmtpApiTokenView>
    ResetSmtpTokenPassword(tokenId) -> SmtpApiTokenView
    DeleteSmtpToken(tokenId) -> bool
}
```

## Notes
- Email sends are fire-and-forget (immediately return COMPLETED status for simplicity)
- SMTP tokens include password generation
- Token passwords are only returned once (on creation or reset)
- Keep implementation simple and focused on testing needs
