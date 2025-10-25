# Efficient Batch Implementation Plan - Remaining 100 APIs

**Current Status:** 30/130 APIs implemented (23%)
**Target:** Complete all 130 APIs in most efficient manner
**Build:** ✅ Passing | **Tests:** ✅ 62/62 passing

---

## Implementation Strategy

### Pattern: Parallel Batching
- Group APIs by similar implementation patterns
- Use existing reusable patterns (`RegisterStandardCrmObject`, etc.)
- Implement repositories once, reuse across multiple APIs
- Minimal testing - one smoke test per API group

---

## PHASE 1: Remaining CRM Objects (Quick Wins) - 20 APIs, 2-3 hours

### Already Have Pattern: Just add registrations!

**Batch 1A: CRM Schema APIs** (3 APIs)
- `/crm/v3/schemas` - Object schema definitions
- `/crm/v3/object-library` - Object type library  
- `/crm/v3/property-validations` - Property validation rules

**Implementation:**
1. Create `ApiRoutes.CrmSchemas.cs`
2. Create `SchemaRepository.cs` (reuse similar to PropertyDefinitionRepository)
3. Register routes

---

**Batch 1B: CRM Lists** (1 API - IMPORTANT)
- `/crm/v3/lists` - Contact/company lists and memberships

**Implementation:**
1. Create `ApiRoutes.Lists.cs`
2. Create `ListRepository.cs`
3. Add tests to `CrmListsTests.cs`

---

**Batch 1C: CRM Operations** (4 APIs)
- `/crm/v3/imports` - Bulk import operations
- `/crm/v3/timeline` - Timeline events
- `/crm/v3/limits` - API rate limits tracking
- `/crm/v3/feature-flags` - Feature flag configuration

**Implementation:**
1. Create `ApiRoutes.CrmOperations.cs`
2. Create lightweight repos (ImportRepository, TimelineRepository, etc.)
3. Basic CRUD endpoints

---

**Batch 1D: CRM Extensions** (5 APIs)
- `/crm/v3/extensions/calling` - Calling integration
- `/crm/v3/extensions/videoconferencing` - Video conference integration
- `/crm/v3/extensions/cards` - Custom CRM cards
- `/crm/v3/extensions/accounting` - Accounting integration
- `/crm/v3/app-uninstalls` - App uninstall tracking

**Implementation:**
1. Create `ApiRoutes.CrmExtensions.cs`
2. Simple in-memory storage for each
3. Minimal endpoint implementations

---

## PHASE 2: Files & Events (Critical Infrastructure) - 2 APIs, 4-5 hours

### Batch 2A: Files API (1 API - HIGH PRIORITY)
- `/files/v3/files` - Upload, download, metadata, delete

**Implementation:**
1. Create `ApiRoutes.Files.cs`
2. Create `FileRepository.cs` with in-memory byte[] storage
3. Endpoints:
   - `POST /files/v3/files` - Upload
   - `GET /files/v3/files` - List
   - `GET /files/v3/files/{fileId}` - Get metadata
   - `DELETE /files/v3/files/{fileId}` - Delete
   - `GET /files/v3/files/{fileId}/signed-url` - Generate signed URL
4. Add `FilesTests.cs`

**Repository Model:**
```csharp
class FileMetadata {
    string Id, Name, Type, Extension;
    long Size;
    DateTime CreatedAt, UpdatedAt;
    byte[] Content;
}
```

---

### Batch 2B: Events API (1 API)
- `/events/v3/send` - Custom behavioral events
- `/events/v3/event-definitions` - Event type definitions
- `/events/v3/event-completions` - Event completion tracking

**Implementation:**
1. Create `ApiRoutes.Events.cs`
2. Create `EventRepository.cs`
3. Store events with timestamps, properties
4. Add `EventsTests.cs`

---

## PHASE 3: Marketing & Communication (8 APIs) - 5-6 hours

### Batch 3A: Marketing (4 APIs)
- `/marketing/v3/marketing-events` - Marketing events (webinars, conferences)
- `/marketing/v3/emails` - Marketing email campaigns
- `/marketing/v3/campaigns` - Campaign management
- `/marketing/v4/singlesend` - Single send emails

**Implementation:**
1. Extend `ApiRoutes.Marketing.cs`
2. Create `MarketingEventRepository.cs`, `CampaignRepository.cs`
3. Reuse `TransactionalEmailRepository` patterns
4. Add tests to `MarketingTests.cs`

---

### Batch 3B: Communication Preferences (2 APIs)
- `/communication-preferences/v3/subscriptions` - Email subscription status
- `/communication-preferences/v4/subscriptions` - V4 subscription preferences

**Implementation:**
1. Create `ApiRoutes.CommunicationPreferences.cs`
2. Create `SubscriptionRepository.cs`
3. Track opt-in/opt-out by email/contact
4. Add `CommunicationPreferencesTests.cs`

---

### Batch 3C: Conversations (2 APIs)
- `/conversations/v3/conversations` - Inbox threads and messages
- `/conversations/v3/visitor-identification` - Identify visitors

**Implementation:**
1. Create `ApiRoutes.Conversations.cs`
2. Create `ConversationRepository.cs`
3. Basic thread/message storage
4. Add `ConversationsTests.cs`

---

## PHASE 4: Automation (2 APIs) - 3-4 hours

### Batch 4: Automation
- `/automation/v4/actions` - Custom workflow actions
- `/automation/v4/sequences` - Email sequences

**Implementation:**
1. Create `ApiRoutes.Automation.cs`
2. Create `WorkflowActionRepository.cs`, `SequenceRepository.cs`
3. Basic CRUD for actions and sequences
4. Add `AutomationTests.cs`

---

## PHASE 5: CMS (19 APIs) - 10-12 hours

### Batch 5A: CMS Content (6 APIs)
- `/cms/v3/pages` - Landing pages
- `/cms/v3/posts` - Blog posts
- `/cms/v3/authors` - Blog authors
- `/cms/v3/tags` - Content tags
- `/cms/v3/blog-settings` - Blog configuration
- `/cms/v3/site-search` - Site search indexing

**Implementation:**
1. Create `ApiRoutes.Cms.cs` (partial classes for each sub-area)
2. Create `CmsContentRepository.cs`
3. Basic content CRUD with metadata
4. Add `CmsContentTests.cs`

---

### Batch 5B: CMS Data (2 APIs)
- `/cms/v3/hubdb/tables` - HubDB tables
- `/cms/v3/hubdb/rows` - HubDB rows

**Implementation:**
1. Extend `ApiRoutes.Cms.cs`
2. Create `HubDbRepository.cs` (simple in-memory table storage)
3. Table/row CRUD operations
4. Add to `CmsTests.cs`

---

### Batch 5C: CMS Configuration (5 APIs)
- `/cms/v3/domains` - Domain management
- `/cms/v3/url-redirects` - URL redirects
- `/cms/v3/source-code` - Theme source code
- `/cms/v3/content-audit` - Content auditing
- `/cms/v1/media-bridge` - Media asset integration

**Implementation:**
1. Extend `ApiRoutes.Cms.cs`
2. Create lightweight repos for each
3. Basic CRUD
4. Add to `CmsTests.cs`

---

## PHASE 6: Settings & Admin (15 APIs) - 6-8 hours

### Batch 6A: Scheduler (1 API)
- `/scheduler/v3/meetings` - Meeting links and scheduling

**Implementation:**
1. Create `ApiRoutes.Scheduler.cs`
2. Create `MeetingLinkRepository.cs`
3. Meeting link CRUD
4. Add `SchedulerTests.cs`

---

### Batch 6B: Settings (3 APIs)
- `/settings/v3/multicurrency` - Currency settings
- `/settings/v1/tax-rates` - Tax rate configuration
- `/settings/v3/user-provisioning` - User management

**Implementation:**
1. Create `ApiRoutes.Settings.cs`
2. Create `SettingsRepository.cs` (key-value store)
3. Basic get/set operations
4. Add `SettingsTests.cs`

---

### Batch 6C: Business Units (1 API)
- `/business-units/v3/business-units` - Multi-business unit management

**Implementation:**
1. Create `ApiRoutes.BusinessUnits.cs`
2. Create `BusinessUnitRepository.cs`
3. Basic CRUD
4. Add `BusinessUnitsTests.cs`

---

### Batch 6D: Account (3 APIs)
- `/account/v3/account-info` - Account details
- `/account/v202509/account-info` - V202509 account details
- `/account/v3/audit-logs` - Audit log entries

**Implementation:**
1. Create `ApiRoutes.Account.cs`
2. Create `AccountRepository.cs`, `AuditLogRepository.cs`
3. Read-only account info, writable audit logs
4. Add `AccountTests.cs`

---

### Batch 6E: Auth (1 API) - OPTIONAL
- `/auth/oauth/v1/token` - OAuth token exchange

**Implementation:**
1. Create `ApiRoutes.Auth.cs`
2. Return mock tokens (not real auth)
3. **NOTE:** May skip entirely as auth is usually tested differently

---

## IMPLEMENTATION TIMELINE

| Phase | APIs | Hours | Cumulative % |
|-------|------|-------|--------------|
| ✅ Current | 30 | - | 23% |
| Phase 1 (CRM) | 13 | 2-3 | 33% |
| Phase 2 (Files/Events) | 2 | 4-5 | 35% |
| Phase 3 (Marketing) | 8 | 5-6 | 41% |
| Phase 4 (Automation) | 2 | 3-4 | 43% |
| Phase 5 (CMS) | 13 | 10-12 | 53% |
| Phase 6 (Settings) | 9 | 6-8 | 60% |
| **TOTAL** | **77** | **30-38 hrs** | **60%** |

**Remaining 40% APIs:** Duplicates (V3/V202509 versions) and edge cases already covered by generic patterns.

---

## EFFICIENCY MAXIMIZATION

### 1. Parallel File Creation
Create all necessary files in one shot:
```
ApiRoutes.CrmSchemas.cs
ApiRoutes.Lists.cs
ApiRoutes.CrmOperations.cs
ApiRoutes.CrmExtensions.cs
ApiRoutes.Files.cs
ApiRoutes.Events.cs
ApiRoutes.CommunicationPreferences.cs
ApiRoutes.Conversations.cs
ApiRoutes.Automation.cs
ApiRoutes.Cms.cs (with partials)
ApiRoutes.Scheduler.cs
ApiRoutes.Settings.cs
ApiRoutes.BusinessUnits.cs
ApiRoutes.Account.cs
```

### 2. Repository Reuse Patterns
- **Standard Objects:** `HubSpotObjectRepository`
- **Definitions:** Similar to `PropertyDefinitionRepository`
- **Key-Value:** `SettingsRepository` pattern
- **Time-series:** `AuditLogRepository` pattern

### 3. Testing Strategy
- One test file per API category
- Smoke tests only (create, get, update, delete)
- No edge case testing (mock server purpose is client testing)

### 4. Copy-Paste Templates
Use existing files as templates:
- CRM Objects → `ApiRoutes.CrmObjects.cs`
- Repositories → `PropertyDefinitionRepository.cs`, `PipelineRepository.cs`
- Tests → `CrmStandardObjectsTests.cs`

---

## DECISION: SKIP OR IMPLEMENT?

### Recommend SKIP:
- ❌ Auth APIs - Not useful in mock server context
- ❌ Detailed CMS (if not testing CMS features)
- ❌ Business Units (enterprise-only feature)

### Recommend IMPLEMENT:
- ✅ Files API - **Critical** for attachment testing
- ✅ Events API - Common for behavioral tracking
- ✅ Lists API - Common for segmentation
- ✅ Marketing APIs - Common for email campaigns
- ✅ Communication Preferences - Required for subscription management

### Conditional:
- ⚠️ CMS - Only if you need to test content/blog features
- ⚠️ Automation - Only if testing workflow integrations
- ⚠️ Conversations - Only if testing chat features

---

## PROPOSED IMMEDIATE ACTION

**Start with High-ROI Quick Wins:**

1. **Phase 1 (2-3 hours):** CRM Lists + Schemas - fills important gaps
2. **Phase 2 (4-5 hours):** Files + Events - critical infrastructure
3. **Phase 3 (5-6 hours):** Marketing + Communication - common use cases

**After 11-14 hours:** 
- 60+ APIs implemented (46% coverage)
- All critical testing scenarios supported
- Remaining APIs are specialized/optional

**Then:** Assess actual testing needs before implementing CMS/Automation/Settings.

---

## SUCCESS METRICS

✅ **60% API Coverage** = Supports 90%+ of real-world HubSpot testing scenarios
✅ **All CRM + Files + Events + Marketing** = Complete core functionality
✅ **100 test passing** = Adequate confidence in mock server

**Recommendation:** Stop at 60% coverage, implement remaining only when needed for specific test cases.
