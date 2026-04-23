# MockServer & Test Coverage Expansion

## TL;DR
> **Summary**: Expand MockServer routes and Kiota client tests to cover all API areas across V3, V202509, and V202603 versions, closing ~43 missing MockServer areas, adding V202603 route support, and backfilling tests for existing routes.
> **Estimated Effort**: XL

## Context
### Original Request
Expand MockServer and test coverage for the HubSpot .NET SDK across all API versions and object types.

### Key Findings
- **Standard CRM objects already registered**: The MockServer already registers ~30 CRM object types via `RegisterStandardCrmObject()` in `ApiRoutes.CrmObjects.cs` (contacts, companies, deals, tickets, products, quotes, calls, emails, meetings, notes, tasks, communications, postal_mail, feedback_submissions, goal_targets, appointments, leads, users, carts, orders, invoices, discounts, fees, taxes, commerce_payments, commerce_subscriptions, listings, contracts, courses, services, deal_splits, partner_clients, partner_services, transcriptions). Many of these lack Kiota client tests despite having working routes.
- **V202509 routes exist** at `/crm/objects/2025-09/{objectType}` with full CRUD+batch+search+merge+gdpr-delete — but NO tests exist for them.
- **V202603 routes** at `/crm/objects/2026-03/{objectType}` were added in `ApiRoutes.CrmObjectsV202603.cs` and registered in `HubSpotMockServer.cs`. Structurally identical to V202509 but without `gdpr-delete`.
- **Generic CRM object route** (`/crm/v3/objects/{objectType}`) handles custom objects and any unregistered type — this is a fallback.
- **Test pattern**: Each test class creates its own `HubSpotMockServer` instance, creates a typed Kiota client, exercises CRUD operations. See `CrmContactsKiotaTests.cs` for the canonical pattern (create, get, update, delete, list, batch ops, search, merge, gdpr-delete, associations, property history).
- **Non-CRM gaps**: CMS Pages, URL Mappings, Communication Preferences V4, Scheduler Meetings, Meta Origin, Marketing Single Send V4 — these need dedicated route handlers and repositories.

## Objectives
### Core Objective
Achieve >90% API area coverage in both MockServer routes and Kiota client tests.

### Deliverables
- [ ] V3 Kiota client tests for all registered but untested CRM object types
- [ ] V202509 Kiota client tests for CRM objects
- [ ] V202603 Kiota client tests for CRM objects
- [ ] Tests for existing but untested MockServer routes (Communication Preferences, Account Info V202509, Properties V202509, Associations V202509)
- [ ] MockServer routes + tests for missing non-CRM API areas (CMS URL Mappings, Comm Prefs V4, Single Send V4, Meta Origins)

### Definition of Done
- [ ] `dotnet build -c Release` passes with 0 errors
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

- [x] 1 Add Kiota tests for Appointments V3
  **What**: Create test class following `CrmContactsKiotaTests` pattern — create, get, update, delete, list (minimum). Use `HubSpotCRMAppointmentsV3Client`.
  **Files**: `test/HubSpot.Tests/MockServer/CrmAppointmentsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 2 Add Kiota tests for Leads V3
  **What**: Same pattern as task 1 for Leads.
  **Files**: `test/HubSpot.Tests/MockServer/CrmLeadsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 3 Add Kiota tests for Feedback Submissions V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmFeedbackSubmissionsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 4 Add Kiota tests for Commerce Payments V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommercePaymentsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 5 Add Kiota tests for Commerce Subscriptions V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommerceSubscriptionsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 6 Add Kiota tests for Deal Splits V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealSplitsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 7 Add Kiota tests for Goal Targets V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmGoalTargetsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 8 Add Kiota tests for Listings V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmListingsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 9 Add Kiota tests for Partner Clients V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPartnerClientsKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 10 Add Kiota tests for Partner Services V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPartnerServicesKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 11 Add Kiota tests for Courses V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCoursesKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

- [x] 12 Add Kiota tests for Postal Mail V3
  **What**: Same pattern as task 1.
  **Files**: `test/HubSpot.Tests/MockServer/CrmPostalMailKiotaTests.cs`
  **Acceptance**: Tests pass exercising CRUD+list via Kiota client

### Phase 2: V202509 CRM Object Tests (tests only)
**Complexity: Medium**
**Dependencies: None (parallel with Phase 1)**

The V202509 routes at `/crm/objects/2025-09/{objectType}` already exist and handle all object types generically. Need Kiota client tests.

- [x] 13 Add V202509 Kiota tests for Contacts
  **What**: Create test class using V202509 client. Test create, get, update, delete, list, batch create/read/update/upsert/archive, search, merge, gdpr-delete. Follow the Companies V202603 test pattern in `CrmCompaniesTests.cs` but targeting V202509.
  **Files**: `test/HubSpot.Tests/MockServer/CrmContactsV202509KiotaTests.cs`
  **Acceptance**: Full CRUD+batch+search tests pass via V202509 Kiota client

- [x] 14 Add V202509 Kiota tests for Deals
  **What**: Same as task 13 for Deals.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealsV202509KiotaTests.cs`
  **Acceptance**: Tests pass via V202509 Kiota client

- [x] 15 Add V202509 Kiota tests for Tickets
  **What**: Same as task 13 for Tickets.
  **Files**: `test/HubSpot.Tests/MockServer/CrmTicketsV202509KiotaTests.cs`
  **Acceptance**: Tests pass via V202509 Kiota client

- [x] 16 Add V202509 tests for Account Info
  **What**: Test Account Info V202509 endpoints — these routes already exist in MockServer (`ApiRoutes.Account.cs`). Verify with V202509 Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/AccountInfoV202509KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 17 Add V202509 tests for Associations
  **What**: Test Associations V202509 endpoints using Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/AssociationsV202509KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 18 Add V202509 tests for Properties
  **What**: Test Properties V202509 endpoints using Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/PropertiesV202509KiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 3: V202603 MockServer Routes + Tests
**Complexity: Large**
**Dependencies: None**

V202603 routes at `/crm/objects/2026-03/{objectType}` were already created in `src/HubSpot.MockServer/Routes/ApiRoutes.CrmObjectsV202603.cs` and registered. Now need Kiota client tests.

- [x] 19 Create V202603 CRM objects route handler
  **What**: Created `ApiRoutes.CrmObjectsV202603.cs` — reuses V202509 handlers at path `/crm/objects/2026-03/`. No gdpr-delete.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CrmObjectsV202603.cs`
  **Acceptance**: Route handler compiles and is registered ✓

- [x] 20 Register V202603 routes in application startup
  **What**: Added `RegisterCrmObjectsV202603(app)` call in `HubSpotMockServer.cs`.
  **Files**: `src/HubSpot.MockServer/HubSpotMockServer.cs`
  **Acceptance**: Routes are registered at startup ✓

- [x] 21 Add V202603 Kiota tests for Contacts
  **What**: Test create, get, update, delete, list, batch ops, search, merge via V202603 Kiota client. No gdpr-delete test.
  **Files**: `test/HubSpot.Tests/MockServer/CrmContactsV202603KiotaTests.cs`
  **Acceptance**: Full CRUD+batch+search tests pass

- [x] 22 Add V202603 Kiota tests for Deals
  **What**: Same as task 21 for Deals.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDealsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 23 Add V202603 Kiota tests for Tickets
  **What**: Same as task 21 for Tickets.
  **Files**: `test/HubSpot.Tests/MockServer/CrmTicketsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 24 Add V202603 Kiota tests for Products
  **What**: Same as task 21 for Products.
  **Files**: `test/HubSpot.Tests/MockServer/CrmProductsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 4: Existing Route Test Backfill (tests only)
**Complexity: Small**
**Dependencies: None**

Several MockServer areas have routes but no tests.

- [x] 25 Add Communication Preferences V3 tests
  **What**: Test subscription endpoints via Kiota client. Routes exist in `ApiRoutes.Subscriptions.cs`.
  **Files**: `test/HubSpot.Tests/MockServer/CommunicationPreferencesKiotaTests.cs`
  **Acceptance**: Tests pass for subscribe/unsubscribe/get status

- [x] 26 Add CMS Pages tests
  **What**: Routes exist in `ApiRoutes.CmsPages.cs`. Test via Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/CmsPagesKiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 27 Add Scheduler Meetings tests
  **What**: Routes exist in `ApiRoutes.Scheduler.cs`. Repository exists in `Repositories/SchedulerMeeting/`. Test via Kiota client.
  **Files**: `test/HubSpot.Tests/MockServer/SchedulerMeetingsKiotaTests.cs`
  **Acceptance**: Tests pass

### Phase 5: Missing Non-CRM MockServer Routes + Tests
**Complexity: Large**
**Dependencies: Phase 4 (understand patterns first)**

These API areas have no MockServer routes at all and need both route handlers and tests.

- [x] 28 Add CMS URL Mappings routes + tests
  **What**: Create route handler for URL Mappings API. Follow pattern from `ApiRoutes.CmsUrlRedirects.cs` and `Repositories/UrlRedirect/`.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CmsUrlMappings.cs`, `src/HubSpot.MockServer/Repositories/UrlMapping/` (repository classes), `test/HubSpot.Tests/MockServer/CmsUrlMappingsKiotaTests.cs`
  **Acceptance**: CRUD tests pass via Kiota client

- [x] 29 Add Communication Preferences V4 routes + tests
  **What**: Create V4 route handler for communication preferences. May differ from V3 — check generated client structure first.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.CommunicationPreferencesV4.cs`, `test/HubSpot.Tests/MockServer/CommunicationPreferencesV4KiotaTests.cs`
  **Acceptance**: Tests pass via V4 Kiota client

- [x] 30 Add Marketing Single Send V4 routes + tests
  **What**: Create route handler for Single Send V4 API. Check generated client for endpoint structure.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.MarketingSingleSendV4.cs`, `test/HubSpot.Tests/MockServer/MarketingSingleSendV4KiotaTests.cs`
  **Acceptance**: Tests pass via V4 Kiota client

- [x] 31 Add Meta Origins routes + tests
  **What**: Create route handler for Origins API.
  **Files**: `src/HubSpot.MockServer/Routes/ApiRoutes.MetaOrigins.cs`, `test/HubSpot.Tests/MockServer/MetaOriginsKiotaTests.cs`
  **Acceptance**: Tests pass via Kiota client

### Phase 6: Remaining CRM Object Types — V202603 Cross-Version Tests
**Complexity: Medium**
**Dependencies: Phase 1, Phase 3**

Add V202603 tests for the remaining CRM object types. These use the V202603 routes from Phase 3.

- [x] 32 Add V202603 tests for Appointments, Leads, Feedback Submissions
  **What**: One test class per object type, using V202603 Kiota clients. Minimum: create, get, update, delete, list.
  **Files**: `test/HubSpot.Tests/MockServer/CrmAppointmentsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmLeadsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmFeedbackSubmissionsV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 33 Add V202603 tests for Commerce objects (Payments, Subscriptions, Carts, Orders, Invoices)
  **What**: One test class per object type using V202603 Kiota clients.
  **Files**: `test/HubSpot.Tests/MockServer/CrmCommercePaymentsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCommerceSubscriptionsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCartsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmOrdersV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmInvoicesV202603KiotaTests.cs`
  **Acceptance**: Tests pass

- [x] 34 Add V202603 tests for remaining object types (Discounts, Fees, Taxes, Listings, Contracts, Courses, Services)
  **What**: One test class per object type using V202603 Kiota clients.
  **Files**: `test/HubSpot.Tests/MockServer/CrmDiscountsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmFeesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmTaxesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmListingsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmContractsV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmCoursesV202603KiotaTests.cs`, `test/HubSpot.Tests/MockServer/CrmServicesV202603KiotaTests.cs`
  **Acceptance**: Tests pass

## Verification
- [ ] `dotnet build -c Release` passes with 0 errors and 0 warnings in non-test projects
- [ ] No regressions — all existing tests still pass
- [ ] Every CRM object type registered in `ApiRoutes.CrmObjects.cs` has at least one V3 Kiota test
- [ ] V202603 routes are registered and at least 4 core CRM object types (Companies, Contacts, Deals, Tickets) have V202603 tests
- [ ] V202509 has tests for at least Contacts, Deals, Tickets
