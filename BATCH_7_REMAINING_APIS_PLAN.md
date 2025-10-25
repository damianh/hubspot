# Batch 7: Remaining APIs Implementation Plan

**Date:** 2025-10-25
**Current Progress:** 33/130+ APIs (25%)

---

## 📊 COMPREHENSIVE API INVENTORY

### ✅ ALREADY IMPLEMENTED (33 APIs)

#### CRM Objects (29)
1. Companies (V3 + V202509) ✅
2. Contacts (V3 + V202509) ✅
3. Deals (V3 + V202509) ✅
4. Tickets (V3 + V202509) ✅
5. Products (V3 + V202509) ✅
6. Line Items (V3 + V202509) ✅
7. Quotes (V3 + V202509) ✅
8. Calls (V3 + V202509) ✅
9. Emails (V3 + V202509) ✅
10. Meetings (V3 + V202509) ✅
11. Notes (V3 + V202509) ✅
12. Tasks (V3 + V202509) ✅
13. Communications (V3 + V202509) ✅
14. Postal Mail (V3 + V202509) ✅
15. Feedback Submissions (V3) ✅
16. Goals (V3) ✅

#### Metadata & Core CRM (8)
17. Associations V3 ✅
18. Associations V4 ✅
19. Associations Schema ✅
20. Properties ✅
21. Pipelines ✅
22. Owners ✅
23. Lists ✅
24. Schemas ✅

#### Marketing & Communication (6)
25. Marketing Events V3 Beta ✅
26. Marketing Emails (Transactional Single Send) ✅
27. Communication Preferences (Subscriptions) ✅
28. Events V3 ✅
29. Files V3 ✅
30. Webhooks V3 ✅

#### Conversations & Other (3)
31. Conversations (Inbox & Messages) ✅
32. Custom Channels ✅
33. Visitor Identification ✅

---

## 🔴 REMAINING HIGH-VALUE APIs (Priority Order)

### **BATCH 7A: Data Operations (3 APIs, ~4 hours)**
**Why First:** Critical for bulk operations, commonly used in real scenarios

1. **Imports API** (`/crm/v3/imports`)
   - Import files (contacts, companies, etc.)
   - Check import status
   - Get import errors
   - **Repository:** ✅ ImportRepository exists, check implementation
   - **Routes:** ✅ ApiRoutes.Imports.cs exists
   - **Time:** 1 hour (verify/complete)

2. **Exports API** (`/crm/v3/exports`)
   - Export objects
   - Check export status
   - Download export files
   - **Repository:** NEW ExportRepository
   - **Routes:** NEW ApiRoutes.Exports.cs
   - **Time:** 2 hours

3. **Timeline Events API** (`/crm/v3/timeline`)
   - Create timeline events
   - Custom event templates
   - Event details
   - **Repository:** ✅ TimelineRepository exists
   - **Routes:** ✅ ApiRoutes.Timeline.cs exists
   - **Time:** 1 hour (verify/complete)

---

### **BATCH 7B: Commerce Objects (8 APIs, ~6 hours)**
**Why:** E-commerce integration testing, medium priority

4. **Carts** (`/crm/v3/objects/carts`)
5. **Orders** (`/crm/v3/objects/orders`)
6. **Invoices** (`/crm/v3/objects/invoices`)
7. **Discounts** (`/crm/v3/objects/discounts`)
8. **Fees** (`/crm/v3/objects/fees`)
9. **Taxes** (`/crm/v3/objects/taxes`)
10. **Commerce Payments** (`/crm/v3/objects/commerce_payments`)
11. **Commerce Subscriptions** (`/crm/v3/objects/commerce_subscriptions`)

**Implementation:**
- Use `StandardCrmObject` helper for rapid implementation
- Single repository: `HubSpotObjectRepository` (already exists)
- Routes in: `ApiRoutes.CrmObjects.cs` (extend existing)
- **Time:** ~45 min each = 6 hours total

---

### **BATCH 7C: Specialized CRM Objects (10 APIs, ~7 hours)**
**Why:** Less common but needed for specific use cases

12. **Leads** (`/crm/v3/objects/leads`)
13. **Listings** (`/crm/v3/objects/listings`)
14. **Contracts** (`/crm/v3/objects/contracts`)
15. **Courses** (`/crm/v3/objects/courses`)
16. **Services** (`/crm/v3/objects/services`)
17. **Deal Splits** (`/crm/v3/objects/deal_splits`)
18. **Goal Targets** (`/crm/v3/objects/goal_targets`)
19. **Partner Clients** (`/crm/v3/objects/partner_clients`)
20. **Partner Services** (`/crm/v3/objects/partner_services`)
21. **Appointments** (`/crm/v3/objects/appointments`)

**Implementation:**
- Use `StandardCrmObject` helper
- Single repository: `HubSpotObjectRepository`
- Routes in: `ApiRoutes.CrmObjects.cs`
- **Time:** ~40 min each = 7 hours total

---

### **BATCH 7D: Marketing APIs (5 APIs, ~8 hours)**
**Why:** Marketing automation & campaigns

22. **Marketing Campaigns** (`/marketing/v3/campaigns`)
    - CRUD campaigns
    - **Repository:** ✅ CampaignRepository exists
    - **Routes:** In ApiRoutes.Marketing.cs
    - **Time:** 1 hour (verify/complete)

23. **Marketing Forms** (`/marketing/v3/forms`)
    - Form submissions
    - Form definitions
    - **Repository:** NEW FormRepository
    - **Routes:** Extend ApiRoutes.Marketing.cs
    - **Time:** 2 hours

24. **Marketing Single Send** (`/marketing/v3/emails/single-send`)
    - Single email campaigns
    - **Repository:** ✅ SingleSendRepository exists
    - **Routes:** In ApiRoutes.Marketing.cs
    - **Time:** 1 hour (verify/complete)

25. **Marketing Events** (`/marketing/v3/marketing-events`)
    - Full implementation (not beta)
    - Webinars, conferences
    - **Repository:** ✅ MarketingEventRepository exists
    - **Routes:** In ApiRoutes.Marketing.cs
    - **Time:** 2 hours (verify/complete)

26. **Transactional Email** (Public API)
    - Already implemented via TransactionalEmailRepository ✅
    - **Time:** 1 hour (verify)

---

### **BATCH 7E: Automation & Workflows (3 APIs, ~6 hours)**
**Why:** Workflow testing scenarios

27. **Workflow Actions** (`/automation/v4/actions`)
    - Custom workflow actions
    - Execute actions
    - **Repository:** NEW WorkflowActionRepository
    - **Routes:** NEW ApiRoutes.Automation.cs
    - **Time:** 2 hours

28. **Automation V4** (`/automation/v4`)
    - Workflow management
    - **Repository:** NEW WorkflowRepository
    - **Routes:** Extend ApiRoutes.Automation.cs
    - **Time:** 2 hours

29. **Sequences** (`/automation/v4/sequences`)
    - Email sequences
    - Enrollment
    - **Repository:** NEW SequenceRepository
    - **Routes:** Extend ApiRoutes.Automation.cs
    - **Time:** 2 hours

---

### **BATCH 7F: CRM Extensions (8 APIs, ~10 hours)**
**Why:** Advanced CRM integrations

30. **Custom Objects** (`/crm/v3/objects/custom_objects`)
    - User-defined objects
    - **Repository:** NEW CustomObjectRepository
    - **Routes:** NEW ApiRoutes.CustomObjects.cs
    - **Time:** 3 hours

31. **Object Library** (`/crm/v4/objects`)
    - Object metadata
    - **Repository:** Extend SchemaRepository
    - **Routes:** Extend ApiRoutes.Schemas.cs
    - **Time:** 1 hour

32. **Calling Extensions** (`/crm/v3/extensions/calling`)
    - Call tracking
    - **Repository:** NEW CallingExtensionRepository
    - **Routes:** NEW ApiRoutes.Extensions.cs
    - **Time:** 1 hour

33. **Video Conferencing** (`/crm/v3/extensions/videoconferencing`)
    - Meeting integrations
    - **Repository:** NEW VideoConferencingRepository
    - **Routes:** Extend ApiRoutes.Extensions.cs
    - **Time:** 1 hour

34. **Public App CRM Cards** (`/crm/v3/extensions/cards`)
    - Custom cards
    - **Repository:** NEW CrmCardRepository
    - **Routes:** Extend ApiRoutes.Extensions.cs
    - **Time:** 1 hour

35. **Property Validations** (`/crm/v3/properties/validations`)
    - Validation rules
    - **Repository:** Extend PropertyDefinitionRepository
    - **Routes:** Extend ApiRoutes.Properties.cs
    - **Time:** 1 hour

36. **Feature Flags** (`/crm/v3/feature-flags`)
    - Feature toggles
    - **Repository:** NEW FeatureFlagRepository
    - **Routes:** NEW ApiRoutes.FeatureFlags.cs
    - **Time:** 1 hour

37. **Limits Tracking** (`/crm/v3/rate-limits`)
    - API limits monitoring
    - **Repository:** NEW LimitsRepository
    - **Routes:** NEW ApiRoutes.Limits.cs
    - **Time:** 1 hour

---

### **BATCH 7G: CMS APIs (13 APIs, ~15 hours)**
**Why:** Content management (OPTIONAL - only if testing CMS)

38. **CMS Pages** (`/cms/v3/pages`)
39. **CMS Blog Posts** (`/cms/v3/blogs/posts`)
40. **CMS Site Pages** (`/cms/v3/pages/site-pages`)
41. **CMS Landing Pages** (`/cms/v3/pages/landing-pages`)
42. **CMS Templates** (`/cms/v3/templates`)
43. **CMS Modules** (`/cms/v3/modules`)
44. **CMS Themes** (`/cms/v3/themes`)
45. **CMS Layouts** (`/cms/v3/layouts`)
46. **CMS Partials** (`/cms/v3/partials`)
47. **HubDB Tables** (`/cms/v3/hubdb/tables`)
48. **HubDB Rows** (`/cms/v3/hubdb/tables/{tableId}/rows`)
49. **CMS Domains** (`/cms/v3/domains`)
50. **CMS URL Redirects** (`/cms/v3/url-redirects`)

**Implementation:**
- **Repository:** NEW CmsRepository (complex)
- **Routes:** NEW ApiRoutes.Cms.cs
- **Time:** ~15 hours total

---

### **BATCH 7H: Other Specialized APIs (8 APIs, ~10 hours)**
**Why:** Low priority, niche use cases

51. **Users API** (`/crm/v3/users`)
    - User management
    - **Repository:** Extend OwnerRepository
    - **Routes:** Extend ApiRoutes.Owners.cs
    - **Time:** 1 hour

52. **Transcriptions** (`/crm/v3/objects/transcriptions`)
    - Call transcriptions
    - **Repository:** Use HubSpotObjectRepository
    - **Routes:** Extend ApiRoutes.CrmObjects.cs
    - **Time:** 1 hour

53. **App Uninstalls** (`/crm/v3/app-uninstalls`)
    - Track uninstalls
    - **Repository:** NEW AppUninstallRepository
    - **Routes:** NEW ApiRoutes.AppUninstalls.cs
    - **Time:** 1 hour

54. **Scheduler** (`/scheduler/v3`)
    - Meeting scheduling
    - **Repository:** NEW SchedulerRepository
    - **Routes:** NEW ApiRoutes.Scheduler.cs
    - **Time:** 2 hours

55. **Business Units** (`/business-units/v3`)
    - Multi-business unit
    - **Repository:** NEW BusinessUnitRepository
    - **Routes:** NEW ApiRoutes.BusinessUnits.cs
    - **Time:** 2 hours

56. **Settings** (`/settings/*`)
    - Various settings endpoints
    - **Repository:** NEW SettingsRepository
    - **Routes:** NEW ApiRoutes.Settings.cs
    - **Time:** 2 hours

57. **Account Info** (`/account-info/v3`)
    - Account details
    - **Repository:** NEW AccountRepository
    - **Routes:** NEW ApiRoutes.Account.cs
    - **Time:** 1 hour

58. **Auth APIs** (`/oauth/*`)
    - Authentication (mock only)
    - **Repository:** NEW AuthRepository
    - **Routes:** NEW ApiRoutes.Auth.cs
    - **Time:** 1 hour (minimal)

---

## 🎯 RECOMMENDED IMPLEMENTATION ORDER

### **Phase 1: Quick Wins** (Verify existing, ~4 hours)
- [ ] Verify Imports implementation
- [ ] Verify Timeline implementation
- [ ] Verify Marketing Campaigns implementation
- [ ] Verify Marketing Single Send implementation
- [ ] Verify Marketing Events (full version)

**Result:** 38/130 APIs (29%), 4 hours

---

### **Phase 2: Commerce & Specialized Objects** (~13 hours)
- [ ] Batch 7B: Commerce objects (8 APIs)
- [ ] Batch 7C: Specialized CRM objects (10 APIs)

**Result:** 56/130 APIs (43%), 17 hours total

---

### **Phase 3: Marketing & Automation** (~14 hours)
- [ ] Batch 7D: Marketing APIs (5 APIs)
- [ ] Batch 7E: Automation & Workflows (3 APIs)

**Result:** 64/130 APIs (49%), 31 hours total

---

### **Phase 4: Extensions & Advanced** (~10 hours)
- [ ] Batch 7F: CRM Extensions (8 APIs)

**Result:** 72/130 APIs (55%), 41 hours total

---

### **Phase 5: CMS (OPTIONAL)** (~15 hours)
- [ ] Batch 7G: CMS APIs (13 APIs)

**Result:** 85/130 APIs (65%), 56 hours total

---

### **Phase 6: Specialized (LOW PRIORITY)** (~10 hours)
- [ ] Batch 7H: Other APIs (8 APIs)

**Result:** 93/130 APIs (72%), 66 hours total

---

## 🚀 IMMEDIATE NEXT STEPS

### Step 1: Verify Existing Implementations (2 hours)
Check these 5 APIs that have repositories/routes but may need completion:

```powershell
# Verify these files
src/HubSpot.MockServer/ApiRoutes.Imports.cs
src/HubSpot.MockServer/ApiRoutes.Timeline.cs
src/HubSpot.MockServer/ApiRoutes.Marketing.cs
src/HubSpot.MockServer/Repositories/ImportRepository.cs
src/HubSpot.MockServer/Repositories/TimelineRepository.cs
src/HubSpot.MockServer/Repositories/CampaignRepository.cs
src/HubSpot.MockServer/Repositories/SingleSendRepository.cs
```

### Step 2: Implement Exports API (2 hours)
Critical for data export testing.

### Step 3: Batch Commerce Objects (6 hours)
Quick StandardCrmObject implementations for e-commerce.

---

## 📊 TOTAL REMAINING WORK

| Phase | APIs | Hours | Priority |
|-------|------|-------|----------|
| Phase 1 (Verify) | 5 | 4 | HIGH |
| Phase 2 (Commerce+Specialized) | 18 | 13 | HIGH |
| Phase 3 (Marketing+Automation) | 8 | 14 | MEDIUM |
| Phase 4 (Extensions) | 8 | 10 | MEDIUM |
| Phase 5 (CMS) | 13 | 15 | LOW |
| Phase 6 (Other) | 8 | 10 | LOW |
| **TOTAL** | **60** | **66** | - |

---

## 💡 EFFICIENCY STRATEGIES

1. **Batch Similar APIs** - Do all commerce objects together
2. **Reuse StandardCrmObject** - Most CRM objects follow same pattern
3. **Copy-Paste Templates** - Repositories follow same CRUD pattern
4. **Minimal Testing** - One smoke test per API group
5. **Focus on High-Value** - Skip CMS/Settings unless needed

---

## ✅ DECISION POINT

**Question:** Which phase should we tackle next?

**Recommendation:** **Phase 1 (Verify) + Exports API** = 6 hours
- Ensures existing code is complete
- Adds critical Exports functionality
- Quick wins before tackling larger batches

**Alternative:** **Phase 2 (Commerce + Specialized)** = 13 hours
- Completes all CRM object types
- Brings coverage to 43%
- Good stopping point for most use cases

---

## WAIT FOR USER DECISION

Which batch should we implement next?
1. **Phase 1** - Verify + Exports (6 hours, low risk)
2. **Phase 2** - Commerce + Specialized CRM (13 hours, 43% coverage)
3. **Phase 3** - Marketing + Automation (14 hours, 49% coverage)
4. **Custom** - Pick specific APIs
