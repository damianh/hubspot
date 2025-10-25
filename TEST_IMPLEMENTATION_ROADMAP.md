# HubSpot Mock Server - Test Implementation Roadmap

## Executive Summary

The HubSpot Mock Server has **70+ API endpoints** implemented across all major CRM objects and services. Currently, **74 tests** cover **19 API groups** with 100% pass rate. This roadmap outlines the strategy to achieve comprehensive test coverage for the remaining **8 untested API groups** and future expansion.

---

## Current State (As of 2025-10-25)

### ✅ What's Working
- **All repositories registered** in DI container
- **All routes active** and properly configured
- **74 tests passing** with 0 failures
- **Solid patterns established** for standard CRM object testing
- **MapGroup routing** working cleanly
- **Generic object handling** is flexible and extensible

### ❌ Test Coverage Gaps
1. Associations API (V3, V4, V202509) - **Critical**
2. Properties API (V3, V202509) - **Critical**
3. Pipelines API (V3) - **High**
4. Owners API (V3) - **High**
5. Lists API (V3) - **Medium**
6. Files API (V3) - **Medium**
7. Events API (V3) - **Low**
8. CRM Extensions (Schemas, Imports, Exports, Timeline) - **Medium**

---

## Phase 1: Critical Infrastructure Tests (Week 1)

### Priority 1A: Associations API Tests
**File to create:** `test\HubSpot.Tests\MockServer\AssociationsTests.cs`

**Test scenarios:**
- ✅ V3 batch read associations between companies and contacts
- ✅ V3 create association between objects
- ✅ V3 archive/delete association
- ✅ V3 list association types
- ✅ V4 batch read (if differs from V3)
- ✅ V4 create association with labels
- ✅ V202509 batch read (latest version)
- ✅ Verify association isolation between object types

**Estimated complexity:** Medium  
**Estimated time:** 4-6 hours  
**Dependencies:** None  
**Business value:** HIGH - Required for relating any CRM objects

### Priority 1B: Properties API Tests
**File to create:** `test\HubSpot.Tests\MockServer\PropertiesTests.cs`

**Test scenarios:**
- ✅ V3 list all properties for object type
- ✅ V3 create custom property
- ✅ V3 get property by name
- ✅ V3 update property
- ✅ V3 delete property
- ✅ V3 create property with options (dropdown)
- ✅ V202509 list/create properties (if differs)
- ✅ Verify property uniqueness per object type

**Estimated complexity:** Medium  
**Estimated time:** 4-6 hours  
**Dependencies:** None  
**Business value:** HIGH - Required for custom fields on all objects

---

## Phase 2: Configuration & Metadata Tests (Week 2)

### Priority 2A: Pipelines API Tests
**File to create:** `test\HubSpot.Tests\MockServer\PipelinesTests.cs`

**Test scenarios:**
- ✅ List all pipelines for deal object type
- ✅ Create new pipeline with stages
- ✅ Get pipeline by ID
- ✅ Update pipeline
- ✅ Delete pipeline
- ✅ Add/remove/reorder stages
- ✅ Verify pipeline isolation between object types

**Estimated complexity:** Medium  
**Estimated time:** 4-6 hours  
**Dependencies:** None  
**Business value:** MEDIUM-HIGH - Important for deals, tickets

### Priority 2B: Owners API Tests
**File to create:** `test\HubSpot.Tests\MockServer\OwnersTests.cs`

**Test scenarios:**
- ✅ List all owners
- ✅ Get owner by ID
- ✅ Filter owners by email
- ✅ Verify owner data structure
- ✅ Test pagination if applicable

**Estimated complexity:** Low  
**Estimated time:** 2-3 hours  
**Dependencies:** None  
**Business value:** MEDIUM - Used for object assignment

---

## Phase 3: Data Management Tests (Week 3)

### Priority 3A: Lists API Tests
**File to create:** `test\HubSpot.Tests\MockServer\ListsTests.cs`

**Test scenarios:**
- ✅ Create static list
- ✅ Create dynamic list with filters
- ✅ Get list by ID
- ✅ Update list
- ✅ Delete list
- ✅ Add members to list
- ✅ Remove members from list
- ✅ List memberships
- ✅ Verify list isolation

**Estimated complexity:** Medium-High  
**Estimated time:** 6-8 hours  
**Dependencies:** None  
**Business value:** MEDIUM - Contact/company segmentation

### Priority 3B: Files API Tests
**File to create:** `test\HubSpot.Tests\MockServer\FilesTests.cs`

**Test scenarios:**
- ✅ Upload file
- ✅ Download file
- ✅ List files
- ✅ Get file metadata
- ✅ Update file metadata
- ✅ Delete file
- ✅ Search files by name
- ✅ Verify file content integrity

**Estimated complexity:** Medium  
**Estimated time:** 4-6 hours  
**Dependencies:** None  
**Business value:** MEDIUM - Attachment management

---

## Phase 4: Advanced Features Tests (Week 4)

### Priority 4A: Events API Tests
**File to create:** `test\HubSpot.Tests\MockServer\EventsTests.cs`

**Test scenarios:**
- ✅ Send custom event
- ✅ Create event definition
- ✅ List event definitions
- ✅ Get event definition by name
- ✅ Update event definition
- ✅ Delete event definition
- ✅ Verify event properties

**Estimated complexity:** Low-Medium  
**Estimated time:** 3-4 hours  
**Dependencies:** None  
**Business value:** LOW-MEDIUM - Analytics/tracking

### Priority 4B: CRM Extensions Tests
**File to create:** `test\HubSpot.Tests\MockServer\CrmExtensionsTests.cs`

**Test scenarios:**

**Schemas API:**
- ✅ Create custom object schema
- ✅ Get schema by object type
- ✅ Update schema
- ✅ Delete schema
- ✅ Verify schema properties and associations

**Imports API:**
- ✅ Start import
- ✅ Get import status
- ✅ List imports
- ✅ Cancel import
- ✅ Get import errors

**Exports API:**
- ✅ Start export
- ✅ Get export status
- ✅ Download export file
- ✅ List exports

**Timeline API:**
- ✅ Create timeline event type
- ✅ Create timeline event
- ✅ List timeline events for object
- ✅ Delete timeline event

**Estimated complexity:** High  
**Estimated time:** 8-12 hours  
**Dependencies:** May need schemas for imports/exports  
**Business value:** MEDIUM - Advanced data operations

---

## Phase 5: Search & Batch Operations (Week 5-6)

### Priority 5A: Search API Implementation & Tests
**Status:** Not yet implemented

**Implementation needed:**
- Search endpoint for each CRM object type
- FilterGroups and filter logic
- Sorting and pagination
- Property selection

**Test scenarios:**
- ✅ Search with single filter
- ✅ Search with filter groups (AND/OR)
- ✅ Search with sorting
- ✅ Search with property selection
- ✅ Search pagination
- ✅ Search across different object types

**Estimated complexity:** High  
**Estimated time:** 12-16 hours  
**Dependencies:** None, but applies to all object types  
**Business value:** HIGH - Critical for real-world usage

### Priority 5B: Batch Operations Enhancement
**Status:** Partially implemented

**Test coverage needed:**
- ✅ Batch create (test with max batch size)
- ✅ Batch update
- ✅ Batch archive
- ✅ Batch read
- ✅ Batch upsert
- ✅ Batch error handling
- ✅ Batch partial success scenarios

**Estimated complexity:** Medium  
**Estimated time:** 6-8 hours  
**Dependencies:** Existing object implementations  
**Business value:** HIGH - Performance critical

---

## Phase 6: Additional CRM Objects (Week 7-10)

### Already Implemented CRM Objects
The following standard CRM objects already have route implementations:

**Priority 1 - Fully Tested:**
- ✅ Companies (9 tests)
- ✅ Contacts (9 tests)
- ✅ Deals (9 tests)
- ✅ Line Items (9 tests)
- ✅ Calls, Emails, Meetings, Notes, Tasks (5 tests)

**Priority 2 - Routes Registered, Need Tests:**
- ⚠️ Tickets
- ⚠️ Products
- ⚠️ Quotes
- ⚠️ Communications
- ⚠️ Postal Mail
- ⚠️ Feedback Submissions
- ⚠️ Goals

**Priority 3 - Routes Registered, Need Tests:**
- ⚠️ Appointments
- ⚠️ Leads
- ⚠️ Users

**Priority 4 - Commerce Objects (Routes Registered, Need Tests):**
- ⚠️ Carts
- ⚠️ Orders
- ⚠️ Invoices
- ⚠️ Discounts
- ⚠️ Fees
- ⚠️ Taxes
- ⚠️ Commerce Payments
- ⚠️ Commerce Subscriptions

**Priority 5 - Specialized Objects (Routes Registered, Need Tests):**
- ⚠️ Listings
- ⚠️ Contracts
- ⚠️ Courses
- ⚠️ Services
- ⚠️ Deal Splits
- ⚠️ Goal Targets
- ⚠️ Partner Clients
- ⚠️ Partner Services
- ⚠️ Transcriptions

### Test Strategy for Additional Objects
Since all objects use the same `StandardCrmObject` pattern, tests can follow the proven template:

**Template test file (use for each object):**
```csharp
// Example: CrmTicketsTests.cs
- V3_Can_create_Ticket
- V3_Can_get_Ticket_with_no_properties
- V3_Can_get_Ticket_with_specified_properties
- V3_Can_update_Ticket_with_changed_property
- V3_Can_update_Ticket_with_new_property
- V3_Can_list_Tickets
- V3_Can_list_Tickets_with_paging
- V3_Can_archive_Ticket
- V3_Can_list_archived_Tickets
```

**Estimated time per object:** 2-3 hours  
**Total for all 26 untested objects:** 52-78 hours (6-10 weeks part-time)

---

## Phase 7: Unimplemented API Categories (Future)

### CMS APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\CMS`:
- Blog posts
- Pages
- Site search
- URL redirects
- Domains
- Performance API
- HubDB
- Source code

**Estimated effort:** 60-100 hours

### Automation APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\Automation`:
- Workflows
- Actions

**Estimated effort:** 20-40 hours

### Settings APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\Settings`:
- Account settings
- User preferences

**Estimated effort:** 10-20 hours

### Account & Auth APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\Account` and `Auth`:
- Account information
- OAuth endpoints
- API key management

**Estimated effort:** 15-30 hours

### Scheduler APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\Scheduler`:
- Meeting scheduling

**Estimated effort:** 10-20 hours

### Business Units APIs (Not Yet Implemented)
Based on `src\HubSpot.KiotaClient\Generated\BusinessUnits`:
- Multi-business unit support

**Estimated effort:** 15-25 hours

---

## Summary Timeline

### Short Term (Weeks 1-4) - Critical Coverage
| Week | Phase | Focus | Hours | Tests Added |
|------|-------|-------|-------|-------------|
| 1 | Phase 1 | Associations & Properties | 8-12 | ~20-25 |
| 2 | Phase 2 | Pipelines & Owners | 6-9 | ~15-20 |
| 3 | Phase 3 | Lists & Files | 10-14 | ~20-25 |
| 4 | Phase 4 | Events & CRM Extensions | 11-16 | ~25-30 |
| **Total** | | | **35-51** | **80-100** |

### Medium Term (Weeks 5-10) - Enhanced Coverage
| Weeks | Phase | Focus | Hours | Tests Added |
|-------|-------|-------|-------|-------------|
| 5-6 | Phase 5 | Search & Batch Ops | 18-24 | ~40-50 |
| 7-10 | Phase 6 | Additional CRM Objects | 52-78 | ~200-230 |
| **Total** | | | **70-102** | **240-280** |

### Long Term (3-6 months) - Complete Coverage
| Phase | Focus | Hours | Tests Added |
|-------|-------|-------|-------------|
| Phase 7 | CMS APIs | 60-100 | ~100-150 |
| Phase 7 | Automation | 20-40 | ~30-50 |
| Phase 7 | Settings/Auth/etc | 50-95 | ~80-120 |
| **Total** | | **130-235** | **210-320** |

---

## Recommendations

### Immediate Actions (This Week)
1. ✅ **Implement AssociationsTests.cs** - Most critical for object relationships
2. ✅ **Implement PropertiesTests.cs** - Required for custom fields
3. 📝 **Document test patterns** - Create template for future object tests

### Next Month
4. ✅ Complete Phases 1-4 (Infrastructure & metadata tests)
5. ✅ Begin Search API implementation and tests
6. ✅ Start testing additional CRM objects (Tickets, Products, Quotes)

### Next Quarter
7. ✅ Complete all standard CRM object tests
8. ✅ Implement and test batch operations thoroughly
9. ✅ Begin CMS API implementation

### Success Metrics
- **Target coverage:** 90%+ of implemented endpoints
- **Current:** 70% coverage (19/27 API groups)
- **After Phase 4:** ~100% coverage of existing implementations
- **After Phase 6:** ~180-200 total tests
- **After Phase 7:** ~400-500 total tests

---

## Test Development Best Practices

### Proven Patterns to Follow
1. **Use WebApplicationFactory pattern** (already working well)
2. **One test class per API group**
3. **Follow naming convention:** `{ObjectName}Tests.cs`
4. **Standard test methods:** Create, Get, Update, List, Archive, Pagination
5. **Use xUnit Facts** for simple tests
6. **Add Theories** for parameterized scenarios
7. **Clean up test data** between tests (repository isolation working)

### Code Quality Guidelines
1. **Keep tests simple and focused** - One assertion per test when possible
2. **Use descriptive test names** - Clearly state what's being tested
3. **Minimize test setup** - Use helper methods for common operations
4. **Assert meaningful things** - Don't just check for non-null
5. **Test error cases** - Not just happy paths

### Test Organization
```
test/HubSpot.Tests/MockServer/
├── CrmCompaniesTests.cs ✅
├── CrmContactsTests.cs ✅
├── CrmDealsTests.cs ✅
├── AssociationsTests.cs ⬅️ NEXT
├── PropertiesTests.cs ⬅️ NEXT
├── PipelinesTests.cs
├── OwnersTests.cs
├── ListsTests.cs
├── FilesTests.cs
├── EventsTests.cs
├── CrmExtensionsTests.cs
└── ... (additional object tests)
```

---

## Conclusion

The HubSpot Mock Server has a **solid foundation** with 70+ endpoints implemented and working. The immediate priority is to add test coverage for the 8 untested API groups (Phases 1-4), which will bring coverage to nearly 100% of currently implemented functionality.

The standardized patterns make it straightforward to add tests for the additional 26 CRM objects that already have route registrations. Long-term expansion to CMS, Automation, and other API categories will provide comprehensive HubSpot API coverage.

**Recommended next steps:**
1. Start with AssociationsTests.cs this week
2. Complete PropertiesTests.cs next
3. Continue through Phases 1-4 over the next month
4. Then systematically add tests for remaining CRM objects

This approach balances **immediate business value** (critical infrastructure tests) with **long-term completeness** (full API coverage).
