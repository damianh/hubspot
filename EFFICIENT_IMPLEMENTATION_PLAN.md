# Efficient Implementation Plan for Remaining HubSpot APIs

## Strategy Overview

The implementation will follow a **pattern-based approach** to maximize efficiency:

1. **Reuse existing patterns**: `StandardCrmObject` for object APIs, shared repositories
2. **Batch implementation**: Group similar APIs together
3. **Parallel development**: Focus on independent APIs that can be implemented simultaneously
4. **Minimal testing**: Only test representative samples, not every variant

---

## Phase 1: Critical CRM APIs (Week 1)
**Goal**: Enable real-world testing scenarios with associations and properties

### 1.1 Associations API ⭐⭐⭐⭐⭐ CRITICAL
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/AssociationRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Associations.cs`
- `test/HubSpot.Tests/MockServer/AssociationsTests.cs`

**Endpoints:**
- V3: `/crm/v3/associations/{fromObjectType}/{toObjectType}/batch/read`
- V3: `/crm/v3/associations/{fromObjectType}/{toObjectType}/batch/create`
- V3: `/crm/v3/associations/{fromObjectType}/{toObjectType}/batch/archive`
- V3: `/crm/v3/associations/{fromObjectId}/{toObjectType}`
- V4: `/crm/v4/associations/{fromObjectType}/{toObjectType}/batch/read`
- V4: `/crm/v4/associations/{fromObjectType}/{toObjectType}/batch/create`
- V4: `/crm/v4/associations/{fromObjectType}/{toObjectType}/batch/labels`
- V202509: Similar to V4

**Implementation Notes:**
- Store associations as: `(fromObjectType, fromId, toObjectType, toId, associationTypeId)`
- Support bidirectional lookup
- Default association types: COMPANY_TO_CONTACT, CONTACT_TO_DEAL, etc.

**Estimated Time**: 2 days

---

### 1.2 Properties API ⭐⭐⭐⭐ HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/PropertyDefinitionRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Properties.cs`
- `test/HubSpot.Tests/MockServer/PropertiesTests.cs`

**Endpoints:**
- V3: `/crm/v3/properties/{objectType}` (GET all, POST create)
- V3: `/crm/v3/properties/{objectType}/{propertyName}` (GET, PATCH, DELETE)
- V3: `/crm/v3/properties/{objectType}/groups` (Property groups)
- V202509: Similar endpoints

**Implementation Notes:**
- Store property definitions with metadata (type, fieldType, options, etc.)
- Validate property values against definitions during object creation/update
- Support default properties + custom properties

**Estimated Time**: 1.5 days

---

### 1.3 Owners API ⭐⭐⭐⭐ HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/OwnerRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Owners.cs`
- `test/HubSpot.Tests/MockServer/OwnersTests.cs`

**Endpoints:**
- V3: `/crm/v3/owners` (GET all)
- V3: `/crm/v3/owners/{ownerId}` (GET by ID)

**Implementation Notes:**
- Simple repository with Owner model (id, email, firstName, lastName, type: USER/TEAM)
- Seed with 2-3 default owners on startup
- Read-only for mock (no create/update)

**Estimated Time**: 0.5 days

---

### 1.4 Pipelines API ⭐⭐⭐⭐ HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/PipelineRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Pipelines.cs`
- `test/HubSpot.Tests/MockServer/PipelinesTests.cs`

**Endpoints:**
- V3: `/crm/v3/pipelines/{objectType}` (GET all, POST create)
- V3: `/crm/v3/pipelines/{objectType}/{pipelineId}` (GET, PATCH, DELETE)
- V3: `/crm/v3/pipelines/{objectType}/{pipelineId}/stages` (Stages CRUD)

**Implementation Notes:**
- Support deals and tickets pipelines (most common)
- Each pipeline has stages with displayOrder
- Seed with default "Sales Pipeline" and "Support Pipeline"

**Estimated Time**: 1 day

---

**Phase 1 Total**: 5 days

---

## Phase 2: Supporting CRM APIs (Week 2)

### 2.1 Lists API ⭐⭐⭐ MEDIUM-HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/ListRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Lists.cs`
- `test/HubSpot.Tests/MockServer/ListsTests.cs`

**Endpoints:**
- V3: `/crm/v3/lists` (GET all, POST create)
- V3: `/crm/v3/lists/{listId}` (GET, PATCH, DELETE)
- V3: `/crm/v3/lists/{listId}/memberships` (Add/remove members)

**Implementation Notes:**
- Simple static lists (not dynamic filtering)
- Store list metadata + member IDs
- Support contacts and companies lists

**Estimated Time**: 1.5 days

---

### 2.2 Files API ⭐⭐⭐⭐ HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/FileRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Files.cs`
- `test/HubSpot.Tests/MockServer/FilesTests.cs`

**Endpoints:**
- V3: `/files/v3/files` (GET all, POST upload)
- V3: `/files/v3/files/{fileId}` (GET, PATCH, DELETE)
- V3: `/files/v3/files/{fileId}/signed-url` (Generate signed URL)

**Implementation Notes:**
- Store files in-memory as byte arrays
- Return mock URLs for file access
- Support basic metadata (name, extension, size)

**Estimated Time**: 1.5 days

---

### 2.3 Schemas API ⭐⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/SchemaRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Schemas.cs`
- `test/HubSpot.Tests/MockServer/SchemasTests.cs`

**Endpoints:**
- V3: `/crm/v3/schemas` (GET all, POST create)
- V3: `/crm/v3/schemas/{objectType}` (GET, PATCH, DELETE)
- V3: `/crm/v3/schemas/{objectType}/associations` (Manage associations)

**Implementation Notes:**
- Define custom object type schemas
- Integrate with PropertyDefinitionRepository
- Work with GenericCrmObjectsApi

**Estimated Time**: 1.5 days

---

### 2.4 Association Schemas API ⭐⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/AssociationTypeRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.AssociationSchemas.cs`

**Endpoints:**
- V3: `/crm/v3/associations/{fromObjectType}/{toObjectType}/labels` (CRUD)
- V4: `/crm/v4/associations/{fromObjectType}/{toObjectType}/definitions` (CRUD)

**Implementation Notes:**
- Define custom association types/labels
- Integrate with AssociationRepository

**Estimated Time**: 1 day

---

**Phase 2 Total**: 5.5 days

---

## Phase 3: Commerce & Specialized Objects (Week 3 - Part 1)
**Goal**: Quick wins using StandardCrmObject pattern

### 3.1 Commerce Objects ⭐⭐ LOW (Batch Implementation)
**Files to Create:**
- Update `src/HubSpot.MockServer/ApiRoutes.CrmObjects.cs` with new registrations

**Objects to Add** (all follow StandardCrmObject pattern):
1. Carts (`/crm/v3/objects/carts`, `/crm/v202509/objects/carts`)
2. Orders (`/crm/v3/objects/orders`, `/crm/v202509/objects/orders`)
3. Invoices (`/crm/v3/objects/invoices`, `/crm/v202509/objects/invoices`)
4. Discounts (`/crm/v3/objects/discounts`, `/crm/v202509/objects/discounts`)
5. Fees (`/crm/v3/objects/fees`, `/crm/v202509/objects/fees`)
6. Taxes (`/crm/v3/objects/taxes`, `/crm/v202509/objects/taxes`)
7. Commerce Payments (`/crm/v3/objects/commerce_payments`, `/crm/v202509/objects/commerce_payments`)
8. Commerce Subscriptions (`/crm/v3/objects/commerce_subscriptions`, `/crm/v202509/objects/commerce_subscriptions`)

**Implementation:**
```csharp
// In ApiRoutes.CrmObjects.cs - just add these calls:
RegisterStandardCrmObject(app, "carts", "Carts", "cartId");
RegisterStandardCrmObject(app, "orders", "Orders", "orderId");
RegisterStandardCrmObject(app, "invoices", "Invoices", "invoiceId");
// ... etc for all 8 objects
```

**Also add V202509 routes:**
```csharp
RegisterStandardCrmObjectV202509(app, "carts", "Carts (V202509)", "cartId");
// ... etc
```

**Estimated Time**: 0.5 days (all 8 objects)

---

### 3.2 Specialized Objects ⭐ LOW (Batch Implementation)
**Files to Update:**
- `src/HubSpot.MockServer/ApiRoutes.CrmObjects.cs`

**Objects to Add:**
1. Leads (`/crm/v3/objects/leads`, `/crm/v202509/objects/leads`)
2. Listings (`/crm/v3/objects/listings`, `/crm/v202509/objects/listings`)
3. Contracts (`/crm/v3/objects/contracts`, `/crm/v202509/objects/contracts`)
4. Courses (`/crm/v3/objects/courses`, `/crm/v202509/objects/courses`)
5. Services (`/crm/v3/objects/services`, `/crm/v202509/objects/services`)
6. Partner Clients (`/crm/v3/objects/partner_clients`, `/crm/v202509/objects/partner_clients`)
7. Partner Services (`/crm/v3/objects/partner_services`, `/crm/v202509/objects/partner_services`)
8. Users (`/crm/v3/objects/users`, `/crm/v202509/objects/users`)
9. Appointments (`/crm/v3/objects/appointments`, `/crm/v202509/objects/appointments`)
10. Transcriptions (`/crm/v3/objects/transcriptions`)
11. Deal Splits (`/crm/v3/objects/deal_splits`)

**Implementation**: Same pattern as commerce objects

**Estimated Time**: 0.5 days (all 11 objects)

---

**Phase 3 Total**: 1 day

---

## Phase 4: Marketing APIs (Week 3 - Part 2)

### 4.1 Marketing Events API ⭐⭐⭐ MEDIUM-HIGH
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/MarketingEventRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Marketing.Events.cs`
- `test/HubSpot.Tests/MockServer/MarketingEventsTests.cs`

**Endpoints:**
- V3: `/marketing/v3/marketing-events/events` (CRUD)
- V3: `/marketing/v3/marketing-events/events/{eventId}/attendees` (Manage attendees)
- V3: `/marketing/v3/marketing-events/events/{eventId}/complete` (Mark complete)
- V3: `/marketing/v3/marketing-events/events/{eventId}/cancel` (Cancel event)

**Estimated Time**: 1.5 days

---

### 4.2 Campaigns API ⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/CampaignRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Marketing.Campaigns.cs`

**Endpoints:**
- V3: `/marketing/v3/campaigns` (GET all)
- V3: `/marketing/v3/campaigns/{campaignId}` (GET)

**Implementation Notes:**
- Read-only for mock (campaigns are usually created in UI)
- Seed with 1-2 example campaigns

**Estimated Time**: 0.5 days

---

### 4.3 Single Send API V4 ⭐⭐ MEDIUM
**Files to Update:**
- `src/HubSpot.MockServer/ApiRoutes.Marketing.cs` (add V4 alongside existing transactional)

**Endpoints:**
- V4: `/marketing/v4/emails/send` (Send single email)
- V4: `/marketing/v4/emails/send/status/{emailId}` (Get status)

**Estimated Time**: 0.5 days

---

**Phase 4 Total**: 2.5 days

---

## Phase 5: Communication & Events (Week 4 - Part 1)

### 5.1 Communication Preferences API ⭐⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/CommunicationPreferencesRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.CommunicationPreferences.cs`
- `test/HubSpot.Tests/MockServer/CommunicationPreferencesTests.cs`

**Endpoints:**
- V3: `/communication-preferences/v3/subscriptions` (Subscription types)
- V3: `/communication-preferences/v3/status/email/{email}` (Get/update status)
- V3: `/communication-preferences/v3/subscribe` (Opt-in)
- V3: `/communication-preferences/v3/unsubscribe` (Opt-out)

**Estimated Time**: 1.5 days

---

### 5.2 Events API ⭐⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/EventRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Events.cs`
- `test/HubSpot.Tests/MockServer/EventsTests.cs`

**Endpoints:**
- V3: `/events/v3/events` (POST custom behavioral events)
- V3: `/events/v3/events/batch` (POST batch events)

**Implementation Notes:**
- Store events with timestamp, eventName, properties
- Associate with contacts/companies via objectId

**Estimated Time**: 1 day

---

**Phase 5 Total**: 2.5 days

---

## Phase 6: Automation (Week 4 - Part 2)

### 6.1 Automation/Workflows API ⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/WorkflowRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Automation.cs`
- `test/HubSpot.Tests/MockServer/AutomationTests.cs`

**Endpoints:**
- V4: `/automation/v4/actions/{appId}` (Custom actions CRUD)
- V4: `/automation/v4/actions/{appId}/{actionId}/functions` (Action functions)
- V4: `/automation/v4/actions/{appId}/{actionId}/execute` (Execute action)

**Implementation Notes:**
- Simplified workflow execution (just store execution history)
- Support custom action registration and callbacks

**Estimated Time**: 2 days

---

**Phase 6 Total**: 2 days

---

## Phase 7: Extensions & Integrations (Week 5 - Optional)

### 7.1 Calling Extensions ⭐ LOW
**Files to Create:**
- `src/HubSpot.MockServer/ApiRoutes.Extensions.Calling.cs`

**Endpoints:**
- V3: `/crm/v3/extensions/calling/{appId}/settings` (Settings CRUD)

**Estimated Time**: 0.5 days

---

### 7.2 Video Conferencing Extension ⭐ LOW
**Files to Create:**
- `src/HubSpot.MockServer/ApiRoutes.Extensions.VideoConferencing.cs`

**Endpoints:**
- V3: `/crm/v3/extensions/videoconferencing/settings` (Settings CRUD)

**Estimated Time**: 0.5 days

---

### 7.3 CRM Cards ⭐ LOW
**Files to Create:**
- `src/HubSpot.MockServer/ApiRoutes.Extensions.Cards.cs`

**Endpoints:**
- V3: `/crm/v3/extensions/cards/{appId}` (Cards CRUD)

**Estimated Time**: 0.5 days

---

**Phase 7 Total**: 1.5 days

---

## Phase 8: Conversations (Week 6 - Optional)

### 8.1 Conversations API ⭐⭐ MEDIUM
**Files to Create:**
- `src/HubSpot.MockServer/Repositories/ConversationRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Conversations.cs`

**Endpoints:**
- V3: `/conversations/v3/conversations/threads` (Thread CRUD)
- V3: `/conversations/v3/conversations/threads/{threadId}/messages` (Messages)

**Estimated Time**: 1.5 days

---

### 8.2 Visitor Identification ⭐ LOW
**Files to Create:**
- `src/HubSpot.MockServer/ApiRoutes.VisitorIdentification.cs`

**Endpoints:**
- V3: `/conversations/v3/visitor-identification/tokens/create` (Token generation)

**Estimated Time**: 0.5 days

---

**Phase 8 Total**: 2 days

---

## Phase 9: Remaining Services (As Needed)

### 9.1 Imports API ⭐⭐ MEDIUM
**Estimated Time**: 1.5 days

### 9.2 Scheduler API ⭐ LOW-MEDIUM
**Estimated Time**: 1 day

### 9.3 Business Units API ⭐ LOW
**Estimated Time**: 1 day

### 9.4 Settings APIs ⭐ LOW
- User Provisioning
- Multicurrency Settings
**Estimated Time**: 1 day

### 9.5 Account APIs ⭐ LOW
**Estimated Time**: 0.5 days

### 9.6 CMS APIs (Large Scope) ⭐ LOW
- Blog Posts, Pages, Settings, Templates
**Estimated Time**: 3-4 days (if needed)

---

## Implementation Summary

### Critical Path (Phases 1-2): ~10.5 days
- Associations ⭐⭐⭐⭐⭐
- Properties ⭐⭐⭐⭐
- Owners ⭐⭐⭐⭐
- Pipelines ⭐⭐⭐⭐
- Lists ⭐⭐⭐
- Files ⭐⭐⭐⭐
- Schemas ⭐⭐⭐
- Association Schemas ⭐⭐⭐

### Quick Wins (Phase 3): ~1 day
- All Commerce Objects (8) using StandardCrmObject
- All Specialized Objects (11) using StandardCrmObject

### Marketing & Events (Phases 4-5): ~5 days
- Marketing Events, Campaigns, Single Send V4
- Communication Preferences
- Events API

### Automation (Phase 6): ~2 days
- Workflows/Automation API

### Optional (Phases 7-9): ~6-10 days
- Extensions, Conversations, Remaining services

---

## Total Estimated Time

- **MVP (Phases 1-3)**: ~11.5 days (~2.5 weeks)
- **Full Coverage (Phases 1-6)**: ~18.5 days (~4 weeks)
- **Complete (All Phases)**: ~25-30 days (~5-6 weeks)

---

## Efficiency Tactics

### 1. Pattern Reuse
- StandardCrmObject pattern → 19 objects in ~1 day
- Repository pattern → consistent data access
- Shared route registration helpers

### 2. Parallel Implementation
Independent APIs can be built simultaneously:
- Associations + Properties (different developers)
- Files + Lists (different developers)
- All commerce objects at once (copy-paste)

### 3. Minimal Testing
- Test 1 representative from each group
- E.g., test Companies (V3), assume Contacts (V3) works
- Test Carts, assume Orders/Invoices work

### 4. Code Generation Where Possible
- Could generate StandardCrmObject registrations from config/JSON
- Example:
```json
[
  {"route": "carts", "tag": "Carts", "idParam": "cartId"},
  {"route": "orders", "tag": "Orders", "idParam": "orderId"}
]
```

### 5. Incremental Delivery
- Deliver in phases, test critical path first
- Don't block on optional APIs

---

## Next Steps (Immediate Actions)

### Start with Phase 1.1: Associations API
This is the most critical missing piece. Most real-world HubSpot scenarios require associations.

**Steps:**
1. Create `AssociationRepository.cs`
2. Create `ApiRoutes.Associations.cs` with V3/V4/V202509 routes
3. Add basic tests in `AssociationsTests.cs`
4. Validate with generated Kiota clients

**Shall I proceed with implementing the Associations API?**
