# Next Priority APIs Implementation Plan

**Date:** 2025-10-26  
**Context:** After completing automation/workflows, identify and implement next priority APIs

---

## 📊 CURRENT STATUS SUMMARY

### Implemented APIs: 99+ implementations (74% coverage)

**Already Complete:**
- ✅ All 36 CRM Standard Objects (V3 + V202509) - 72 implementations
- ✅ CRM Core APIs (Associations, Properties, Pipelines, Owners, Lists, Schemas) - 11 implementations
- ✅ Data Operations (Imports, Exports, Timeline) - 3 implementations
- ✅ Files & Events - 2 implementations
- ✅ Marketing APIs (Events, Emails, Campaigns, Single Send, Transactional) - 5 implementations
- ✅ Communication Preferences/Subscriptions (V3, V4) - 2 implementations
- ✅ Webhooks - 1 implementation
- ✅ Conversations (Inbox, Custom Channels, Visitor ID) - 3 implementations
- ✅ **Automation/Workflows (NEW)** - 2 implementations
  - ✅ Automation Actions V4
  - ✅ Automation Sequences V4

**Total Generated Clients:** 130 unique client types

---

## 🔍 GAP ANALYSIS

### Generated But NOT Implemented (31 clients)

#### **CRM Extensions & Advanced Features** (8 clients)
1. ❌ `CRMCustomObjectsV3Client` - Custom object type management
2. ❌ `CRMCustomObjectsV202509Client` - Custom object type management (V202509)
3. ❌ `CRMObjectLibraryV3Client` - Object library/catalog management
4. ❌ `CRMCallingExtensionsV3Client` - Calling extension SDK
5. ❌ `CRMVideoConferencingExtensionV3Client` - Video conferencing extension SDK
6. ❌ `CRMPublicAppCrmCardsV3Client` - CRM cards for apps
7. ❌ `CRMPropertyValidationsV3Client` - Property validation rules
8. ❌ `CRMPublicAppFeatureFlagsV3V3Client` - Feature flags API
9. ❌ `CRMLimitsTrackingV3Client` - API limits tracking
10. ❌ `CRMAppUninstallsV3Client` - App uninstall tracking

#### **CMS Content Management** (9 clients)
11. ❌ `CMSPagesV3Client` - CMS pages
12. ❌ `CMSPostsV3Client` - Blog posts
13. ❌ `CMSSourceCodeV3Client` - Templates, modules, layouts
14. ❌ `CMSDomainsV3Client` - Domain management
15. ❌ `CMSUrlRedirectsV3Client` - URL redirects
16. ❌ `CMSHubdbV3Client` - HubDB tables & rows
17. ❌ `CMSAuthorsV3Client` - Blog authors
18. ❌ `CMSTagsV3Client` - Blog tags
19. ❌ `CMSBlogSettingsV3Client` - Blog settings
20. ❌ `CMSCmsContentAuditV3Client` - Content audit
21. ❌ `CMSMediaBridgeV1Client` - Media bridge
22. ❌ `CMSSiteSearchV3Client` - Site search

#### **Other Specialized APIs** (9 clients)
23. ❌ `SchedulerMeetingsV3Client` - Meeting scheduler/booking
24. ❌ `BusinessUnitsBusinessUnitsV3Client` - Business units
25. ❌ `SettingsMulticurrencyV3Client` - Multi-currency settings
26. ❌ `SettingsTaxRatesV1Client` - Tax rate settings
27. ❌ `SettingsUserProvisioningV3Client` - User provisioning (SCIM)
28. ❌ `AccountAccountInfoV3Client` - Account information
29. ❌ `AccountAccountInfoV202509Client` - Account information (V202509)
30. ❌ `AccountAuditLogsV3Client` - Audit logs
31. ❌ `AuthOauthV1Client` - OAuth authentication

#### **Events (Additional)** (2 clients)
32. ❌ `EventsManageEventDefinitionsV3Client` - Event definition management
33. ❌ `EventsSendEventCompletionsV3Client` - Event completions

---

## 🎯 PRIORITIZATION FRAMEWORK

### Tier 1: High Value / Real-World Coverage (PRIORITY)
APIs commonly used in testing scenarios, high ROI

### Tier 2: Medium Value / Niche Use Cases
APIs used in specific scenarios, moderate ROI

### Tier 3: Low Value / Rarely Tested
APIs rarely needed for testing, low ROI

---

## 📋 RECOMMENDED IMPLEMENTATION PRIORITIES

### **PRIORITY 1: High-Value Missing APIs** ⭐⭐⭐

#### 1.1 Scheduler/Meetings API
**Client:** `SchedulerMeetingsV3Client`

**Use Case:** Meeting booking links, calendar scheduling
**Complexity:** Medium
**Estimated Time:** 2-3 hours
**Test Coverage:** Meeting booking flows

**Endpoints:**
- `GET /scheduler/v3/meetings/meeting-links` - List meeting links
- `GET /scheduler/v3/meetings/meeting-links/{meetingLinkId}` - Get link details
- `POST /scheduler/v3/meetings/meeting-links` - Create meeting link
- `PATCH /scheduler/v3/meetings/meeting-links/{meetingLinkId}` - Update link
- `DELETE /scheduler/v3/meetings/meeting-links/{meetingLinkId}` - Delete link

**Repository:** `MeetingSchedulerRepository`
**Models:** MeetingLink, AvailabilitySettings, BookingRules

---

#### 1.2 Event Definitions & Completions APIs
**Clients:** 
- `EventsManageEventDefinitionsV3Client`
- `EventsSendEventCompletionsV3Client`

**Use Case:** Custom behavioral event tracking (currently only basic events are supported)
**Complexity:** Low-Medium
**Estimated Time:** 1-2 hours
**Test Coverage:** Event analytics workflows

**Endpoints:**
- `GET /events/v3/event-definitions` - List event definitions
- `POST /events/v3/event-definitions` - Create event definition
- `GET /events/v3/event-definitions/{eventName}` - Get definition
- `PATCH /events/v3/event-definitions/{eventName}` - Update definition
- `DELETE /events/v3/event-definitions/{eventName}` - Delete definition
- `POST /events/v3/send/event-completions` - Send event completion

**Enhancement:** Add to existing `EventRepository`

---

#### 1.3 Business Units API
**Client:** `BusinessUnitsBusinessUnitsV3Client`

**Use Case:** Multi-tenant HubSpot portals, enterprise accounts
**Complexity:** Low-Medium
**Estimated Time:** 1.5-2 hours
**Test Coverage:** Enterprise multi-brand scenarios

**Endpoints:**
- `GET /business-units/v3/business-units` - List business units
- `GET /business-units/v3/business-units/{businessUnitId}` - Get business unit
- `POST /business-units/v3/business-units` - Create business unit
- `PATCH /business-units/v3/business-units/{businessUnitId}` - Update business unit

**Repository:** `BusinessUnitsRepository`
**Models:** BusinessUnit, BusinessUnitSettings

---

#### 1.4 Account Information API
**Clients:**
- `AccountAccountInfoV3Client`
- `AccountAccountInfoV202509Client`

**Use Case:** Portal/account details, branding, timezone
**Complexity:** Low
**Estimated Time:** 1 hour
**Test Coverage:** Account configuration tests

**Endpoints:**
- `GET /account-info/v3/details` - Get account details
- `GET /account-info/v3/api-usage/daily` - Get API usage
- `GET /account-info/v3/api-usage/daily/{date}` - Get usage for date

**Repository:** `AccountRepository` (simple, mostly static data)
**Models:** AccountInfo, ApiUsage

---

**PRIORITY 1 SUBTOTAL:** 4 API groups, ~7-9 hours

---

### **PRIORITY 2: CRM Extensions** ⭐⭐

#### 2.1 Custom Objects API (Management)
**Clients:**
- `CRMCustomObjectsV3Client`
- `CRMCustomObjectsV202509Client`

**Use Case:** Create/manage custom object SCHEMAS (not instances - those use generic objects API)
**Complexity:** Medium
**Estimated Time:** 3-4 hours
**Test Coverage:** Custom object schema management

**Note:** This is DIFFERENT from the generic objects API already implemented. This manages the object TYPE definitions themselves.

**Endpoints:**
- `GET /crm/v3/schemas` - List custom object schemas
- `POST /crm/v3/schemas` - Create custom object schema
- `GET /crm/v3/schemas/{objectType}` - Get schema
- `PATCH /crm/v3/schemas/{objectType}` - Update schema
- `DELETE /crm/v3/schemas/{objectType}` - Archive schema
- `POST /crm/v3/schemas/{objectType}/associations` - Define associations

**Repository:** `CustomObjectSchemaRepository`
**Models:** ObjectSchema, PropertyDefinition, AssociationDefinition

---

#### 2.2 Calling Extensions API
**Client:** `CRMCallingExtensionsV3Client`

**Use Case:** Third-party calling integrations (Twilio, etc.)
**Complexity:** Medium-High
**Estimated Time:** 3-4 hours
**Test Coverage:** Calling integration tests

**Endpoints:**
- `GET /crm/v3/extensions/calling/{appId}/settings` - Get settings
- `POST /crm/v3/extensions/calling/{appId}/settings` - Create/update settings
- `PATCH /crm/v3/extensions/calling/{appId}/settings` - Update settings
- `DELETE /crm/v3/extensions/calling/{appId}/settings` - Delete settings

**Repository:** `CallingExtensionsRepository`
**Models:** CallingSettings, CallingProvider

---

#### 2.3 CRM Cards API
**Client:** `CRMPublicAppCrmCardsV3Client`

**Use Case:** Custom cards on CRM records
**Complexity:** Medium
**Estimated Time:** 2-3 hours
**Test Coverage:** Custom UI extension tests

**Endpoints:**
- `GET /crm/v3/extensions/cards/{appId}` - List cards
- `POST /crm/v3/extensions/cards/{appId}` - Create card
- `GET /crm/v3/extensions/cards/{appId}/{cardId}` - Get card
- `PATCH /crm/v3/extensions/cards/{appId}/{cardId}` - Update card
- `DELETE /crm/v3/extensions/cards/{appId}/{cardId}` - Delete card

**Repository:** `CrmCardsRepository`
**Models:** CrmCard, CardDefinition

---

**PRIORITY 2 SUBTOTAL:** 3 API groups, ~8-11 hours

---

### **PRIORITY 3: Settings & Configuration** ⭐

#### 3.1 Multi-Currency Settings
**Client:** `SettingsMulticurrencyV3Client`

**Use Case:** Multi-currency pricing/quotes
**Complexity:** Low-Medium
**Estimated Time:** 1.5-2 hours
**Test Coverage:** Multi-currency deal/quote tests

**Endpoints:**
- `GET /settings/v3/currencies` - List enabled currencies
- `POST /settings/v3/currencies` - Add currency
- `GET /settings/v3/currencies/{currencyCode}` - Get currency
- `PATCH /settings/v3/currencies/{currencyCode}` - Update exchange rate
- `DELETE /settings/v3/currencies/{currencyCode}` - Remove currency

**Repository:** `CurrencyRepository`
**Models:** Currency, ExchangeRate

---

#### 3.2 Audit Logs API
**Client:** `AccountAuditLogsV3Client`

**Use Case:** Audit trail for compliance/security
**Complexity:** Low-Medium
**Estimated Time:** 1.5-2 hours
**Test Coverage:** Audit logging tests

**Endpoints:**
- `GET /account/v3/audit-logs` - Query audit logs
- `GET /account/v3/audit-logs/{eventId}` - Get log entry

**Repository:** `AuditLogRepository`
**Models:** AuditLogEntry, AuditEvent

---

**PRIORITY 3 SUBTOTAL:** 2 API groups, ~3-4 hours

---

### **PRIORITY 4: CMS APIs** (DEFER)

**Rationale:** CMS APIs are rarely needed for SDK/API testing scenarios. Most testing focuses on CRM, marketing automation, and data operations. CMS is content-focused.

**Clients to DEFER:**
- `CMSPagesV3Client`
- `CMSPostsV3Client`
- `CMSSourceCodeV3Client`
- `CMSDomainsV3Client`
- `CMSUrlRedirectsV3Client`
- `CMSHubdbV3Client`
- `CMSAuthorsV3Client`
- `CMSTagsV3Client`
- `CMSBlogSettingsV3Client`
- `CMSCmsContentAuditV3Client`
- `CMSMediaBridgeV1Client`
- `CMSSiteSearchV3Client`

**Total:** 12 API clients
**Estimated Time:** 15-20 hours
**Value:** Low (unless specifically testing CMS integrations)

**Decision:** Implement only if specific test cases require CMS functionality

---

### **PRIORITY 5: Other Specialized APIs** (DEFER)

**Low-priority niche APIs:**
- `CRMVideoConferencingExtensionV3Client` - Video conferencing integrations
- `CRMPropertyValidationsV3Client` - Property validation rules
- `CRMPublicAppFeatureFlagsV3V3Client` - Feature flags
- `CRMLimitsTrackingV3Client` - Rate limit tracking
- `CRMAppUninstallsV3Client` - App lifecycle tracking
- `CRMObjectLibraryV3Client` - Object library/catalog
- `SettingsTaxRatesV1Client` - Tax configuration
- `SettingsUserProvisioningV3Client` - SCIM user provisioning
- `AuthOauthV1Client` - OAuth flows (complex, low test value)

**Total:** 9 API clients
**Estimated Time:** 12-18 hours
**Value:** Very Low

**Decision:** Implement on-demand only

---

## 📊 IMPLEMENTATION ROADMAP

### Phase 1: High-Value APIs (RECOMMENDED) ⭐⭐⭐
**Time:** 7-9 hours  
**APIs:** 4 groups
**Coverage Increase:** 74% → 78%

1. ✅ Automation (DONE)
2. ⏭️ Scheduler/Meetings API (2-3 hours)
3. ⏭️ Event Definitions & Completions (1-2 hours)
4. ⏭️ Business Units API (1.5-2 hours)
5. ⏭️ Account Information API (1 hour)

**Outcome:** Core testing scenarios covered at ~78%

---

### Phase 2: CRM Extensions (OPTIONAL) ⭐⭐
**Time:** 8-11 hours  
**APIs:** 3 groups
**Coverage Increase:** 78% → 82%

1. Custom Objects Schema Management (3-4 hours)
2. Calling Extensions (3-4 hours)
3. CRM Cards (2-3 hours)

**Outcome:** Advanced CRM integration testing supported

---

### Phase 3: Settings & Config (OPTIONAL) ⭐
**Time:** 3-4 hours  
**APIs:** 2 groups
**Coverage Increase:** 82% → 84%

1. Multi-Currency Settings (1.5-2 hours)
2. Audit Logs (1.5-2 hours)

**Outcome:** Enterprise configuration testing supported

---

### Phase 4+: Deferred APIs
**Time:** 27-38 hours  
**APIs:** 21 groups
**Coverage Increase:** 84% → 100%

**Not Recommended** - Defer until specific test cases require them

---

## 🎯 RECOMMENDED APPROACH

### Option A: Complete Phase 1 Only ✅ RECOMMENDED
**Time:** 7-9 hours  
**Coverage:** 78%  
**Value:** High - covers vast majority of real-world testing

**Deliverables:**
- Scheduler/Meetings API
- Event Definitions & Completions
- Business Units API
- Account Information API

**Decision:** STOP after Phase 1 unless specific tests require more

---

### Option B: Complete Phases 1 + 2
**Time:** 15-20 hours  
**Coverage:** 82%  
**Value:** Medium - adds CRM extension testing

**Additional Deliverables:**
- Custom Objects Schema Management
- Calling Extensions
- CRM Cards

**Decision:** Only if CRM extensions are actively being tested

---

### Option C: Complete Phases 1 + 2 + 3
**Time:** 18-24 hours  
**Coverage:** 84%  
**Value:** Medium-Low - adds configuration testing

**Additional Deliverables:**
- Multi-Currency Settings
- Audit Logs

**Decision:** Only for comprehensive enterprise testing scenarios

---

## 💡 IMPLEMENTATION STRATEGY

### Efficient Batch Implementation (Phase 1)

**Session 1: Scheduler API (2-3 hours)**
1. Create `MeetingSchedulerRepository`
2. Implement `ApiRoutes.Scheduler.cs`
3. Add `SchedulerMeetingsTests.cs`
4. Verify with Kiota client

**Session 2: Events Enhancement (1-2 hours)**
1. Enhance `EventRepository` with definitions
2. Implement event definition endpoints in `ApiRoutes.Events.cs`
3. Add event completions endpoint
4. Extend `EventsTests.cs`

**Session 3: Business Units (1.5-2 hours)**
1. Create `BusinessUnitsRepository`
2. Implement `ApiRoutes.BusinessUnits.cs`
3. Add `BusinessUnitsTests.cs`
4. Verify with Kiota client

**Session 4: Account Info (1 hour)**
1. Create `AccountRepository` (simple/static)
2. Implement `ApiRoutes.Account.cs`
3. Add `AccountInfoTests.cs`
4. Verify with Kiota client

**Total Time:** 5.5-8 hours for all Phase 1 APIs

---

## 📈 EXPECTED OUTCOMES

### After Phase 1 Completion
- **API Coverage:** 78% (up from 74%)
- **Implemented APIs:** 103+ (up from 99)
- **Real-World Coverage:** 97%+ of testing scenarios
- **Test Count:** ~145+ tests (up from 137)

### Value Proposition
- ✅ Meeting scheduling flows
- ✅ Custom event analytics
- ✅ Multi-tenant scenarios
- ✅ Account configuration

### Stopping Criteria
**STOP after Phase 1 if:**
- No failing tests require additional APIs
- Test scenarios are adequately covered
- ROI diminishes for remaining APIs

---

## 🚀 NEXT ACTIONS

### Immediate (START WITH PHASE 1)

1. **Implement Scheduler/Meetings API** (2-3 hours)
   - Most commonly used of the missing APIs
   - High value for calendar integration testing

2. **Enhance Events API** (1-2 hours)
   - Quick win, extends existing implementation
   - Enables custom event definition testing

3. **Implement Business Units API** (1.5-2 hours)
   - Enterprise/multi-brand testing
   - Medium complexity

4. **Implement Account Info API** (1 hour)
   - Simple, quick win
   - Useful for portal configuration tests

5. **REASSESS** after Phase 1
   - Evaluate if additional APIs are needed
   - Review test coverage gaps
   - Decide on Phase 2/3

---

## 📝 SUCCESS CRITERIA

### Definition of Done (Phase 1)
- ✅ All 4 API groups implemented with full CRUD
- ✅ Repositories created and registered
- ✅ Routes registered in `HubSpotMockServer`
- ✅ Comprehensive test coverage for each API
- ✅ All tests passing (100% pass rate)
- ✅ Documentation updated

### Quality Gates
- Build passes (0 errors)
- All tests pass (100%)
- Kiota client integration verified
- Repository isolation confirmed

---

## 🎯 FINAL RECOMMENDATION

**PROCEED WITH PHASE 1 IMPLEMENTATION**

**Rationale:**
1. High-value APIs with clear testing use cases
2. Manageable time investment (7-9 hours)
3. Increases coverage to 78% (excellent for testing)
4. Covers most real-world testing scenarios
5. Low risk, high reward

**Defer Phases 2-4 until:**
- Specific test cases require them
- Value proposition becomes clear
- Time permits for completeness

---

**Status:** Ready to begin Phase 1 implementation  
**Next Step:** Implement Scheduler/Meetings API
