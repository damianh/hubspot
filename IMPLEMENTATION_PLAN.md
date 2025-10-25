# HubSpot Mock Server - Implementation Plan

## Overview
This plan outlines the implementation of mock endpoints for all HubSpot APIs that have Kiota-generated clients. The mock server provides a test double for HubSpot APIs, allowing testing of Kiota clients without hitting the real HubSpot API.

## Current Status

### ✅ Completed
- **CRM Companies V3** (`/crm/v3/objects/companies`) - Fully implemented with comprehensive tests
  - List, Get, Create, Update, Archive operations
  - Property filtering
  - Pagination support
  - Archived filtering
- **CRM Contacts V3** (`/crm/v3/objects/contacts`) - Fully implemented with comprehensive tests
- **CRM Deals V3** (`/crm/v3/objects/0-3`) - Fully implemented with comprehensive tests  
- **CRM Line Items V3** (`/crm/v3/objects/line_items`) - Fully implemented with comprehensive tests
- **CRM Tickets V3** (`/crm/v3/objects/0-5`) - Registered via standard helper
- **CRM Products V3** (`/crm/v3/objects/0-7`) - Registered via standard helper
- **CRM Quotes V3** (`/crm/v3/objects/0-14`) - Registered via standard helper

### Infrastructure
- ✅ `HubSpotObjectRepository` - Generic repository for CRM objects
- ✅ `HubSpotObjectIdGenerator` - ID generation
- ✅ API Models (`SimplePublicObject`, `CollectionResponseSimplePublicObject`, etc.)
- ✅ MapGroup-based routing architecture
- ✅ Test fixtures and helpers
- ✅ **Refactoring Complete**: Extracted `RegisterStandardCrmObject` helper method
  - Drastically reduces code duplication
  - Makes adding new CRM object types trivial (1 line of code)
  - Separated into partial classes:
    - `ApiRoutes.StandardCrmObject.cs` - Helper method
    - `ApiRoutes.CrmObjects.cs` - Registration calls for all CRM objects

---

## Priority 1: Core CRM Objects (High Priority)
✅ **COMPLETED** - These are the most commonly used HubSpot objects.

### 1.1 CRM Contacts V3 (`/crm/v3/objects/contacts`) ✅
**Status:** Complete with full test coverage

### 1.2 CRM Deals V3 (`/crm/v3/objects/deals`) ✅
**Status:** Complete with full test coverage

### 1.3 CRM Line Items V3 (`/crm/v3/objects/line_items`) ✅
**Status:** Complete with full test coverage

---

## Priority 2: Generic CRM Objects API & Refactoring (Medium Priority)
✅ **REFACTORING COMPLETE** - 🎉 Massive code reduction achieved!

### 2.1 Extract Common CRM Object Registration Helper ✅
**Status:** Complete - Reduced ~800 lines to ~30 lines

**What Changed:**
- Created `RegisterStandardCrmObject()` helper method
- All standard CRM object APIs now use this single helper
- Adding new CRM object types requires only 1 line of code
- Organized into partial classes for better maintainability

**Additional Objects Added (via helper):**
- ✅ Tickets (`/crm/v3/objects/0-5`)
- ✅ Products (`/crm/v3/objects/0-7`)
- ✅ Quotes (`/crm/v3/objects/0-14`)

### 2.2 CRM Objects V3 (`/crm/v3/objects`) - Generic API
**Kiota Client:** `HubSpotCrmObjectsClient`  
**Generated from:** `https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects`

This is a **generic objects API** that works with any CRM object type (including custom objects).

**Endpoints:**
- `GET /crm/v3/objects/{objectType}` - List objects of any type
- `GET /crm/v3/objects/{objectType}/{objectId}` - Get object by ID
- `POST /crm/v3/objects/{objectType}` - Create object
- `PATCH /crm/v3/objects/{objectType}/{objectId}` - Update object
- `DELETE /crm/v3/objects/{objectType}/{objectId}` - Archive object

**Implementation:**
- Add dynamic routing with `{objectType}` parameter
- Route to `HubSpotObjectRepository` using the object type from the route
- This will automatically support:
  - Standard objects: companies, contacts, deals, tickets, etc.
  - Custom objects: any custom object type
- Add `RegisterCrmObjectsApi(WebApplication app)` method
- Create `CrmObjectsTests.cs`

**Estimated Effort:** 3-4 hours (slightly more complex due to dynamic routing)

---

## Priority 3: Marketing APIs (Medium Priority)

### 3.1 Marketing Transactional V3 (`/marketing/v3/transactional`)
**Kiota Client:** `HubSpotMarketingTransactionalClient`  
**Generated from:** `https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/transactional`

**Endpoints:**
- `POST /marketing/v3/transactional/single-send` - Send single transactional email
- `GET /marketing/v3/transactional/smtp-tokens` - Get SMTP tokens
- `POST /marketing/v3/transactional/smtp-tokens` - Create SMTP token

**Implementation:**
- Create `TransactionalEmailRepository` for managing email sends and SMTP tokens
- Add `RegisterMarketingTransactionalApi(WebApplication app)` method
- Create `MarketingTransactionalTests.cs`
- Store email send history (for verification in tests)

**New Models Needed:**
- `TransactionalEmailSendRequest`
- `TransactionalEmailSendResponse`
- `SmtpToken`

**Estimated Effort:** 4-5 hours (new domain, different patterns)

---

## Priority 4: Webhooks API (Medium Priority)

### 4.1 Webhooks V3 (`/webhooks/v3`)
**Kiota Client:** `HubSpotWebhooksWebhooksClient`  
**Generated from:** `https://api.hubspot.com/api-catalog-public/v1/apis/webhooks/v3`

**Endpoints:**
- `GET /webhooks/v3/{appId}/subscriptions` - List webhook subscriptions
- `GET /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Get subscription
- `POST /webhooks/v3/{appId}/subscriptions` - Create subscription
- `PATCH /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Update subscription
- `DELETE /webhooks/v3/{appId}/subscriptions/{subscriptionId}` - Delete subscription

**Implementation:**
- Create `WebhookSubscriptionRepository` for managing subscriptions
- Add `RegisterWebhooksApi(WebApplication app)` method
- Create `WebhooksTests.cs`
- Optionally: Add webhook event simulation capability

**New Models Needed:**
- `WebhookSubscription`
- `WebhookSubscriptionRequest`

**Estimated Effort:** 4-5 hours

---

## Priority 5: Additional CRM Objects (Lower Priority)
These can be implemented as needed, following the same pattern as Companies/Contacts/Deals.

### Standard CRM Objects
All follow the same pattern as Companies:

- **Tickets** (`/crm/v3/objects/tickets`)
- **Products** (`/crm/v3/objects/products`)
- **Quotes** (`/crm/v3/objects/quotes`)
- **Invoices** (`/crm/v3/objects/invoices`)
- **Calls** (`/crm/v3/objects/calls`)
- **Emails** (`/crm/v3/objects/emails`)
- **Meetings** (`/crm/v3/objects/meetings`)
- **Notes** (`/crm/v3/objects/notes`)
- **Tasks** (`/crm/v3/objects/tasks`)
- **Communications** (`/crm/v3/objects/communications`)
- **Postal Mail** (`/crm/v3/objects/postal_mail`)

**Estimated Effort per object:** 2-3 hours each

---

## Implementation Patterns & Best Practices

### Pattern 1: Standard CRM Object API
Used for: Companies, Contacts, Deals, Line Items, Tickets, etc.

```csharp
private static void RegisterCrm{ObjectType}Api(WebApplication app)
{
    var group = app.MapGroup("/crm/v3/objects/{objectType}")
        .WithTags("{ObjectType}");

    // List
    group.MapGet("", (repo, limit, after, archived, properties, ...) => { ... });
    
    // Get by ID
    group.MapGet("/{id}", (repo, id, properties, ...) => { ... });
    
    // Create
    group.MapPost("", (repo, input) => { ... });
    
    // Update
    group.MapPatch("/{id}", (repo, id, input) => { ... });
    
    // Archive
    group.MapDelete("/{id}", (repo, id) => { ... });
}
```

### Pattern 2: Non-Object APIs
Used for: Marketing, Webhooks, etc.

- Create dedicated repository classes (e.g., `TransactionalEmailRepository`)
- Create domain-specific models
- Register with dedicated MapGroup
- Follow similar testing patterns

### Testing Pattern
For each API implementation:

1. **Test File:** `{Area}{Feature}Tests.cs` (e.g., `CrmContactsTests.cs`)
2. **Test Coverage:**
   - Can create object
   - Can get object (with/without properties)
   - Can update object (existing property, new property)
   - Can list objects
   - Can list with pagination
   - Can archive object
   - Can list archived objects
3. **Use Test Fixture:** `IAsyncLifetime` for server setup/teardown

---

## Repository Enhancements

### Current Capabilities ✅
- Thread-safe CRUD operations
- Property management with history
- Pagination support
- Archive/unarchive
- Associations support

### Potential Future Enhancements
- **Search/Filter:** Add query filtering beyond just archived status
- **Sorting:** Add sort support for list operations
- **Batch Operations:** Add batch create/update/archive
- **Association Queries:** Add methods to query associations
- **Property History:** Expose property history in responses (when requested)
- **Validation:** Add property validation support

---

## File Organization

```
src/HubSpot.MockServer/
├── Apis/
│   ├── Crm/
│   │   ├── CrmCompaniesApi.cs      # ✅ Done
│   │   ├── CrmContactsApi.cs       # TODO
│   │   ├── CrmDealsApi.cs          # TODO
│   │   ├── CrmLineItemsApi.cs      # TODO
│   │   ├── CrmObjectsApi.cs        # TODO (generic)
│   │   └── ...                     # Other CRM objects
│   ├── Marketing/
│   │   └── MarketingTransactionalApi.cs  # TODO
│   ├── Webhooks/
│   │   └── WebhooksApi.cs          # TODO
│   └── Models/                     # ✅ Existing models
│       ├── SimplePublicObject.cs
│       ├── CollectionResponseSimplePublicObject.cs
│       └── ...
├── Objects/
│   ├── HubSpotObjectRepository.cs  # ✅ Done
│   ├── HubSpotObjectIdGenerator.cs # ✅ Done
│   └── ...
├── Repositories/                   # TODO: Non-CRM repositories
│   ├── TransactionalEmailRepository.cs
│   ├── WebhookSubscriptionRepository.cs
│   └── ...
├── HubSpotMockServer.cs            # Main server
└── ...

test/HubSpot.Tests/MockServer/
├── Crm/
│   ├── CrmCompaniesTests.cs        # ✅ Done
│   ├── CrmContactsTests.cs         # TODO
│   ├── CrmDealsTests.cs            # TODO
│   ├── CrmLineItemsTests.cs        # TODO
│   ├── CrmObjectsTests.cs          # TODO
│   └── ...
├── Marketing/
│   └── MarketingTransactionalTests.cs  # TODO
├── Webhooks/
│   └── WebhooksTests.cs            # TODO
└── ...
```

---

## Refactoring Opportunities

### 1. Extract CRM Object API Registration
Since most CRM objects follow the same pattern, create a helper method:

```csharp
private static void RegisterStandardCrmObjectApi(
    WebApplication app, 
    string objectType, 
    string displayName)
{
    var group = app.MapGroup($"/crm/v3/objects/{objectType}")
        .WithTags(displayName);
    
    // Shared implementation for all standard CRM objects
    // ...
}

// Then in StartNew:
RegisterStandardCrmObjectApi(app, "companies", "Companies");
RegisterStandardCrmObjectApi(app, "contacts", "Contacts");
RegisterStandardCrmObjectApi(app, "deals", "Deals");
// ...
```

**Benefits:**
- Reduces code duplication
- Ensures consistency across all CRM object APIs
- Easier to maintain and enhance

**Estimated Effort:** 2-3 hours

### 2. Move API Registration to Separate Files
Move each `RegisterXxxApi` method to its own file in `Apis/` folder:

```csharp
// Apis/Crm/CrmCompaniesApi.cs
public static class CrmCompaniesApi
{
    public static void Register(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/companies")
            .WithTags("Companies");
        // ... endpoint registrations
    }
}
```

**Benefits:**
- Better organization
- Easier to locate and modify specific APIs
- Reduces size of main `HubSpotMockServer.cs` file

**Estimated Effort:** 2 hours

---

## Suggested Implementation Order

### Phase 1: Core CRM Objects (Week 1)
1. ✅ CRM Companies (Done)
2. CRM Contacts
3. CRM Deals
4. CRM Line Items

**Total:** ~8-10 hours

### Phase 2: Generic Objects & Refactoring (Week 2)
1. Extract common CRM object registration helper
2. Move API registrations to separate files
3. Implement Generic CRM Objects API
4. Add additional standard CRM objects (Tickets, Products, etc.)

**Total:** ~12-15 hours

### Phase 3: Marketing & Webhooks (Week 3)
1. Marketing Transactional API
2. Webhooks API

**Total:** ~8-10 hours

### Phase 4: Polish & Enhancement (Ongoing)
1. Add more CRM object types as needed
2. Enhance repository capabilities (search, batch operations, etc.)
3. Add integration test suite against real HubSpot API
4. Performance testing

---

## Testing Strategy

### Unit Tests ✅
- Test each API endpoint independently
- Test Kiota clients against mock server
- Currently implemented for CRM Companies

### Integration Tests (Future)
- Test mock server against real HubSpot API
- Ensure response formats match
- Validate that Kiota clients work with both
- Use environment variable to toggle between mock and real API

### Contract Tests (Future)
- Use OpenAPI specs to validate mock server responses
- Ensure mock server matches OpenAPI contract
- Could use tools like Pact or Spring Cloud Contract

---

## Open Questions & Decisions

### 1. Should we implement batch operations?
HubSpot supports batch create/update/archive operations. Should we implement these?

**Recommendation:** Implement as needed. Start with individual operations, add batch later if tests require it.

### 2. Should we implement search endpoints?
HubSpot has dedicated search endpoints (`/crm/v3/objects/{objectType}/search`)

**Recommendation:** Add in Phase 4 if needed for testing complex queries.

### 3. How to handle associations in detail?
Current implementation supports storing associations but doesn't expose association queries.

**Recommendation:** Implement association endpoints (`/crm/v3/associations`) in Phase 4 if needed.

### 4. Should we support all query parameters from real API?
Some endpoints have many optional query parameters (e.g., `propertiesWithHistory`, `associations`).

**Recommendation:** Implement most common parameters. Add others as tests require them.

### 5. Should we validate property values?
Real HubSpot API validates property values (e.g., email format, date format).

**Recommendation:** Start with no validation. Add validation in Phase 4 if tests require it.

---

## Success Criteria

An API implementation is considered complete when:

1. ✅ All CRUD endpoints are implemented
2. ✅ Kiota client tests pass for all operations
3. ✅ Test coverage includes:
   - Create, Read, Update, Archive
   - Property filtering
   - Pagination
   - Archived filtering
4. ✅ Code follows established patterns
5. ✅ Documentation is updated

---

## Notes

- **Keep it simple:** Mock server should be "good enough" for testing, not a perfect HubSpot replica
- **Follow patterns:** Consistency across implementations is more important than perfect feature parity
- **Test-driven:** Implement based on what tests actually need
- **Incremental:** Add features incrementally as testing requirements grow
- **Reuse infrastructure:** Leverage existing `HubSpotObjectRepository` wherever possible

---

## Estimated Total Effort

- **Phase 1 (Core CRM):** 8-10 hours
- **Phase 2 (Generic + Refactor):** 12-15 hours
- **Phase 3 (Marketing + Webhooks):** 8-10 hours
- **Phase 4 (Polish):** Ongoing

**Total for Phases 1-3:** ~30-35 hours of development time

---

## Current Next Steps

1. **Implement CRM Contacts API** (~2-3 hours)
2. **Implement CRM Deals API** (~2-3 hours)
3. **Implement CRM Line Items API** (~2-3 hours)
4. **Refactor common code** (~2-3 hours)
5. **Implement Generic CRM Objects API** (~3-4 hours)

This plan provides a clear roadmap while maintaining flexibility to adjust based on actual testing needs.
