# MockServer & Test Coverage Expansion

## TL;DR
> **Summary**: Expand MockServer routes and Kiota client tests to cover all API areas across V3, V202509, and V202603 versions, closing ~43 missing MockServer areas, adding V202603 route support, and backfilling tests for existing routes.
> **Estimated Effort**: XL (6 phases, each a single PR)

## Context
### Original Request
Expand MockServer and test coverage for the HubSpot .NET SDK across all API versions and object types.

### Key Findings
- **Standard CRM objects already registered**: The MockServer already registers ~30 CRM object types via `RegisterStandardCrmObject()` in `ApiRoutes.CrmObjects.cs` (contacts, companies, deals, tickets, products, quotes, calls, emails, meetings, notes, tasks, communications, postal_mail, feedback_submissions, goal_targets, appointments, leads, users, carts, orders, invoices, discounts, fees, taxes, commerce_payments, commerce_subscriptions, listings, contracts, courses, services, deal_splits, partner_clients, partner_services, transcriptions). Many of these lack Kiota client tests despite having working routes.
- **V202509 routes exist** at `/crm/objects/2025-09/{objectType}` with full CRUD+batch+search+merge+gdpr-delete — but NO tests exist for them.
- **V202603 routes are completely missing** from MockServer. The V202603 Kiota clients hit `/crm/objects/2026-03/{objectType}`. Key differences from V202509: no `gdpr-delete` endpoint, otherwise structurally identical.
- **Generic CRM object route** (`/crm/v3/objects/{objectType}`) handles custom objects and any unregistered type — this is a fallback.
- **Test pattern**: Each test class creates its own `HubSpotMockServer` instance, creates a typed Kiota client, exercises CRUD operations. See `CrmContactsKiotaTests.cs` for the canonical pattern (create, get, update, delete, list, batch ops, search, merge, gdpr-delete, associations, property history).
- **Non-CRM gaps**: CMS Pages, URL Mappings, Communication Preferences V4, Scheduler Meetings, Meta Origins, Marketing Campaigns (partial), Single Send V4 — these need dedicated route handlers and repositories.

## Objectives
### Core Objective
Achieve >90% API area coverage in both MockServer routes and Kiota client tests.

### Deliverables
- [ ] V3 Kiota client tests for all registered but untested CRM object types
- [ ] V202509 Kiota client tests for CRM objects
- [ ] V202603 MockServer routes + Kiota client tests for CRM objects
- [ ] Tests for existing but untested MockServer routes (Communication Preferences, Account Info V202509, Properties V202509, Associations V202509)
- [ ] MockServer routes + tests for missing non-CRM API areas (CMS Pages, URL Mappings, Scheduler, etc.)

### Definition of Done
- [ ] `dotnet test` passes with all new tests
- [ ] Every registered CRM object type has at least one Kiota client test
- [ ] V202603 routes exist and are tested for core CRM objects

### Guardrails (Must NOT)
- Do NOT cover V202609beta APIs (unstable)
- Do NOT modify generated Kiota client code
- Do NOT add raw HTTP tests where Kiota clients exist — prefer Kiota clients
- Do NOT change existing MockServer behavior — only add new routes/tests

## TODOs

### Phase 1: V3 CRM Object Test Backfill (tests only, no MockServer changes)
**Complexity: Small**
**Dependencies: None**

Many CRM object types already have working V3 MockServer routes via `RegisterStandardCrmObject()` but lack Kiota client tests. These follow the exact same pattern as `CrmContactsKiotaTests.cs`.

- [ ] 1.1 Add Kiota tests for Appointments V3
  **What**: Create test class following `CrmContactsKiotaTests` pattern — create, get, update, delete, list (minimum). Use `HubSpotCRMAppointmentsV3Client`.
  **Files**: `test/HubSpot.Tests/MockServer/CrmAppointmentsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.2 Add Kiota tests for Leads V3
  **What**: Same pattern as 1.1 for Leads.
  **Files**: `test/HubSpot.Tests/MockServer/CrmLeadsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.3 Add Kiota tests for Feedback Submissions V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmFeedbackSubmissionsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.4 Add Kiota tests for Commerce Payments V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommercePaymentsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.5 Add Kiota tests for Commerce Subscriptions V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommerceSubscriptionsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.6 Add Kiota tests for Deal Splits V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealSplitsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.7 Add Kiota tests for Goal Targets V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmGoalTargetsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.8 Add Kiota tests for Listings V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmListingsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.9 Add Kiota tests for Partner Clients V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPartnerClientsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.10 Add Kiota tests for Partner Services V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPartnerServicesKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.11 Add Kiota tests for Courses V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCoursesKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [ ] 1.12 Add Kiota tests for Postal Mail V3
  **What**: Same pattern as 1.1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPostalMailKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

### Phase 2: V202509 CRM Object Tests (tests only)
**Complexity: Medium**
**Dependencies: None (parallel with Phase 1)**

The V202509 routes at `/crm/objects/2025-09/{objectType}` already exist and handle all object types generically. Need Kiota client tests.

- [ ] 2.1 Add V202509 Kiota tests for Contacts
  **What**: Create test class using V202509 client. Test create, get, update, delete, list, batch create/read/update/upsert/archive, search, merge, gdpr-delete. Follow the Companies V202603 test pattern in `CrmCompaniesTests.cs` but targeting V202509.
  **Files**: `test/HubSpot.Tests/MockServer/CrmContactsV202509KiotaTests.cs`
  **Acceptance**: Full CRUD+batch+search tests pass via V202509 Kiota client

- [ ] 2.2 Add V202509 Kiota tests for Deals
  **What**: Same as 2.1 for Deals.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealsV202509KiotaTests.cs`
  **Acceptance**: Tests pass via V202509 Kiota client

- [ ] 2.3 Add V202509 Kiota tests for Tickets
  **What**: Same as 2.1 for Tickets.
  **Files**: `test/HubSpot.Tests/MockServer/CrmTicketsV202509KiotaTests.cs`
  **Acceptance**: Tests pass via V202509 Kiota client

- [ ] 2.4 Add V202509 tests for Account Info
  **What**: Test Account Info V202509 endpoints — these routes already exist in MockServer (`ApiRoutes.Account.cs`). Verify with V202509 Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/AccountInfoV202509KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 2.5 Add V202509 tests for Associations
  **What**: Test Associations V202509 endpoints using Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/AssociationsV202509KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 2.6 Add V202509 tests for Properties
  **What**: Test Properties V202509 endpoints using Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/PropertiesV202509KiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 3: V202603 MockServer Routes + Tests
**Complexity: Large**
**Dependencies: None (can start immediately, but benefits from Phase 2 patterns)**

V202603 Kiota clients hit `/crm/objects/2026-03/{objectType}`. No MockServer routes exist for this path. Structurally nearly identical to V202509 but: no `gdpr-delete` endpoint.

- [ ] 3.1 Create V202603 CRM objects route handler
  **What**: Create `ApiRoutes.CrmObjectsV202603.cs` by copying `ApiRoutes.CrmObjectsV202509.cs` and changing the path from `/crm/objects/2025-09/` to `/crm/objects/2026-03/`. Remove the `gdpr-delete` endpoint. Register in the application startup.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CrmObjectsV202603.cs`
  **Acceptance**: Route handler compiles and is registered

- [ ] 3.2 Register V202603 routes in application startup
  **What**: Add call to `RegisterCrmObjectsV202603(app)` alongside the existing `RegisterCrmObjectsV202509(app)` call.
  **Files**: The file that calls `RegisterCrmObjectsV202509` (find via grep for `RegisterCrmObjectsV202509`)
  **Acceptance**: Routes are registered at startup

- [ ] 3.3 Add V202603 Kiota tests for Contacts
  **What**: Test create, get, update, delete, list, batch ops, search, merge via V202603 Kiota client. No gdpr-delete test.
  **Files**: `test/HubSpot.Tests/MockServer/CrmContactsV202603KiotaTests.cs`
  **Acceptance**: Full CRUD+batch+search tests pass

- [ ] 3.4 Add V202603 Kiota tests for Deals
  **What**: Same as 3.3 for Deals.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 3.5 Add V202603 Kiota tests for Tickets
  **What**: Same as 3.3 for Tickets.
  **Files**: `test/HubSpot.Tests/MockServer/CrmTicketsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 3.6 Add V202603 Kiota tests for Products
  **What**: Same as 3.3 for Products.
  **Files**: `test/HubSpot.Tests/MockServer/CrmProductsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 4: Existing Route Test Backfill (tests only)
**Complexity: Small**
**Dependencies: None**

Several MockServer areas have routes but no tests.

- [ ] 4.1 Add Communication Preferences V3 tests
  **What**: Test subscription endpoints via Kiota client. Routes exist in `ApiRoutes.Subscriptions.cs`.
  **Files**: `test/HubSpot.Tests/MockServer/CommunicationPreferencesKiotaTests.cs`
  **Acceptance**: Tests pass for subscribe/unsubscribe/get status

- [ ] 4.2 Add CMS Pages tests
  **What**: Routes exist in `ApiRoutes.CmsPages.cs`. Test via Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/CmsPagesKiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 4.3 Add Scheduler Meetings tests
  **What**: Routes exist in `ApiRoutes.Scheduler.cs`. Repository exists in `Repositories/SchedulerMeeting/`. Test via Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/SchedulerMeetingsKiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 5: Missing Non-CRM MockServer Routes + Tests
**Complexity: Large**
**Dependencies: Phase 4 (understand patterns first)**

These API areas have no MockServer routes at all and need both route handlers and tests.

- [ ] 5.1 Add CMS URL Mappings routes + tests
  **What**: Create route handler for URL Mappings API. Follow pattern from `ApiRoutes.CmsUrlRedirects.cs` and `Repositories/UrlRedirect/`.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CmsUrlMappings.cs`, `src/HubSpot.MockServer/Repositories/UrlMapping/` (repository classes), `test/HubSpot.Tests/MockServer/CmsUrlMappingsKiotaTests.cs`
  **Acceptance**: CRUD tests pass via Kiota client

- [ ] 5.2 Add Communication Preferences V4 routes + tests
  **What**: Create V4 route handler for communication preferences. May differ from V3 — check generated client structure first.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CommunicationPreferencesV4.cs`, `test/HubSpot.Tests/MockServer/CommunicationPreferencesV4KiotaTests.cs`
  **Acceptance**: Tests pass via V4 Kiota client

- [ ] 5.3 Add Marketing Single Send V4 routes + tests
  **What**: Create route handler for Single Send V4 API. Check generated client for endpoint structure.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.MarketingSingleSendV4.cs`, `test/HubSpot.Tests/MockServer/MarketingSingleSendV4KiotaTests.cs`
  **Acceptance**: Tests pass via V4 Kiota client

- [ ] 5.4 Add Meta Origins routes + tests
  **What**: Create route handler for Origins API.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.MetaOrigins.cs`, `test/HubSpot.Tests/MockServer/MetaOriginsKiotaTests.cs`
  **Acceptance**: Tests pass via Kiota client

### Phase 6: Remaining CRM Object Types — V3 + V202603 Cross-Version Tests
**Complexity: Medium**
**Dependencies: Phase 1, Phase 3**

Add V202603 tests for the remaining CRM object types that were covered in V3 during Phase 1. These use the same V202603 routes added in Phase 3 but with different object type paths.

- [ ] 6.1 Add V202603 tests for Appointments, Leads, Feedback Submissions
  **What**: One test class per object type, using V202603 Kiota clients. Minimum: create, get, update, delete, list.
  **Files**: `test/HubSpot.Tests/MockServer/CrmAppointmentsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmLeadsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmFeedbackSubmissionsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 6.2 Add V202603 tests for Commerce objects (Payments, Subscriptions, Carts, Orders, Invoices)
  **What**: One test class per object type using V202603 Kiota clients.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommercePaymentsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCommerceSubscriptionsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCartsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmOrdersV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmInvoicesV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [ ] 6.3 Add V202603 tests for remaining object types (Discounts, Fees, Taxes, Listings, Contracts, Courses, Services)
  **What**: One test class per object type using V202603 Kiota clients.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDiscountsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmFeesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmTaxesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmListingsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmContractsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCoursesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmServicesV202603KiotaTests.cs`
  **Acceptance**: Tests pass

## Verification
- [ ] `dotnet test` passes with all new tests
- [ ] No regressions — all existing tests still pass
- [ ] Every CRM object type registered in `ApiRoutes.CrmObjects.cs` has at least one V3 Kiota test
- [ ] V202603 routes are registered and at least 4 core CRM object types (Companies, Contacts, Deals, Tickets) have V202603 tests
- [ ] V202509 has tests for at least Contacts, Deals, Tickets
