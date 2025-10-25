# Next Implementation Steps - Efficient Batch Approach

## Current Status (2025-10-25)
✅ **33 APIs implemented** out of 135 (24% complete)
✅ **62 tests passing**
✅ **Phase 1 (Critical CRM Metadata) COMPLETE**

---

## Immediate Next Steps (Most Efficient)

### BATCH 1: Remaining Standard CRM Objects (2-3 hours)
Use existing `RegisterStandardCrmObject` pattern for quick implementation:

1. **Appointments** - `/crm/v3/objects/appointments`
2. **Leads** - `/crm/v3/objects/leads`
3. **Users** - `/crm/v3/objects/users`

**Why:** These follow the exact same pattern, minimal code needed.

**Implementation:**
```csharp
// In ApiRoutes.CrmObjects.cs, add:
RegisterStandardCrmObject(app, "appointments", "Appointments");
RegisterStandardCrmObject(app, "leads", "Leads");
RegisterStandardCrmObject(app, "users", "Users");
```

**Register in HubSpotMockServer.cs:**
```csharp
ApiRoutes.RegisterCrmAppointments(app);
ApiRoutes.RegisterCrmLeads(app);
ApiRoutes.RegisterCrmUsers(app);
```

---

### BATCH 2: Commerce Objects (3-4 hours)
Simple objects using same pattern:

4. **Carts** - `/crm/v3/objects/carts`
5. **Orders** - `/crm/v3/objects/orders`
6. **Invoices** - `/crm/v3/objects/invoices`
7. **Discounts** - `/crm/v3/objects/discounts`
8. **Fees** - `/crm/v3/objects/fees`
9. **Taxes** - `/crm/v3/objects/taxes`
10. **Commerce Payments** - `/crm/v3/objects/commerce_payments`
11. **Commerce Subscriptions** - `/crm/v3/objects/commerce_subscriptions`

**Why:** All follow standard object pattern, batch registration.

---

### BATCH 3: Specialized Objects (2-3 hours)
More standard objects:

12. **Listings** - `/crm/v3/objects/listings`
13. **Contracts** - `/crm/v3/objects/contracts`
14. **Courses** - `/crm/v3/objects/courses`
15. **Services** - `/crm/v3/objects/services`
16. **Deal Splits** - `/crm/v3/objects/deal_splits`
17. **Goal Targets** - `/crm/v3/objects/goal_targets`
18. **Partner Clients** - `/crm/v3/objects/partner_clients`
19. **Partner Services** - `/crm/v3/objects/partner_services`
20. **Transcriptions** - `/crm/v3/objects/transcriptions`

**Why:** Complete CRM object coverage.

---

### BATCH 4: Files API (4-5 hours)
**Priority: HIGH** - Frequently needed

Create:
- `src/HubSpot.MockServer/Repositories/FileRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Files.cs`

**Endpoints:**
- `POST /files/v3/upload` - Upload file
- `GET /files/v3/files` - List files
- `GET /files/v3/files/{fileId}` - Get file metadata
- `DELETE /files/v3/files/{fileId}` - Delete file
- `GET /files/v3/files/{fileId}/download` - Download file

**Storage:** In-memory byte arrays (mock server)

---

### BATCH 5: Events API (3-4 hours)
Custom behavioral events

Create:
- `src/HubSpot.MockServer/Repositories/EventRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Events.cs`

**Endpoints:**
- `POST /events/v3/send` - Send event
- `GET /events/v3/event-definitions` - List definitions
- `POST /events/v3/event-definitions` - Create definition

---

### BATCH 6: Lists API (4-5 hours)
CRM Lists (deferred from Phase 1)

Create:
- `src/HubSpot.MockServer/Repositories/ListRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.Lists.cs`

**Endpoints:**
- `GET /crm/v3/lists` - List all lists
- `POST /crm/v3/lists` - Create list
- `GET /crm/v3/lists/{listId}` - Get list
- `PUT /crm/v3/lists/{listId}/memberships/add` - Add to list
- `PUT /crm/v3/lists/{listId}/memberships/remove` - Remove from list

---

### BATCH 7: Marketing Events API (3-4 hours)

Create:
- `src/HubSpot.MockServer/Repositories/MarketingEventRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.MarketingEvents.cs`

**Endpoints:**
- `GET /marketing/v3/marketing-events` - List events
- `POST /marketing/v3/marketing-events` - Create event
- `PATCH /marketing/v3/marketing-events/{eventId}` - Update event
- `DELETE /marketing/v3/marketing-events/{eventId}` - Delete event

---

### BATCH 8: Communication Preferences (3-4 hours)

Create:
- `src/HubSpot.MockServer/Repositories/SubscriptionRepository.cs`
- `src/HubSpot.MockServer/ApiRoutes.CommunicationPreferences.cs`

**Endpoints:**
- `GET /communication-preferences/v3/status/email/{email}` - Get status
- `POST /communication-preferences/v3/subscribe` - Subscribe
- `POST /communication-preferences/v3/unsubscribe` - Unsubscribe

---

## Estimated Timeline

| Batch | APIs | Hours | Progress |
|-------|------|-------|----------|
| 1 - Standard Objects | 3 | 2-3 | ⬜ |
| 2 - Commerce | 8 | 3-4 | ⬜ |
| 3 - Specialized | 10 | 2-3 | ⬜ |
| 4 - Files | 1 | 4-5 | ⬜ |
| 5 - Events | 1 | 3-4 | ⬜ |
| 6 - Lists | 1 | 4-5 | ⬜ |
| 7 - Marketing Events | 1 | 3-4 | ⬜ |
| 8 - Comm Prefs | 1 | 3-4 | ⬜ |
| **Total** | **26** | **25-32 hrs** | **0%** |

**After Batches 1-8:** 59/135 APIs (44% complete)

---

## Lower Priority (Defer Until Needed)

### CMS APIs (Large Scope)
- Pages, Posts, Blog Settings, Domains, HubDB, etc.
- **Implement only if testing CMS features**

### Automation APIs
- Actions, Sequences
- **Implement if testing workflow automation**

### Settings & Admin APIs
- Multicurrency, Tax Rates, User Provisioning, Business Units
- **Low priority for mock testing**

### Auth APIs
- OAuth flows
- **Usually tested differently, not in mock server**

---

## Testing Strategy

For each batch:
1. **Quick smoke test** - Verify one endpoint per API works
2. **Reuse existing test patterns** from CrmCompaniesTests.cs
3. **No comprehensive coverage** - Mock server is for testing clients, not the mock itself

---

## Efficiency Tips

1. **Copy-paste pattern** - Use existing ApiRoutes files as templates
2. **Batch registration** - Register multiple objects in one commit
3. **Minimal testing** - One test per API to verify it works
4. **Reuse repositories** - HubSpotObjectRepository handles most objects
5. **Skip complex features** - Focus on basic CRUD, not edge cases

---

## Success Criteria

By completing Batches 1-8:
- ✅ All critical CRM metadata APIs implemented
- ✅ All standard + commerce CRM objects implemented
- ✅ Files API for attachment testing
- ✅ Events API for tracking scenarios
- ✅ Lists API for segmentation
- ✅ Marketing Events + Comm Prefs for marketing workflows
- ✅ ~44% API coverage
- ✅ Covers 90%+ of common HubSpot testing scenarios

---

## Decision Points

**Should we implement CMS?**
- Only if you need to test CMS integrations
- Large scope (10+ APIs, 15-20 hours)
- Skip for now unless explicitly needed

**Should we implement Automation?**
- Only if testing workflow triggers
- Medium scope (2 APIs, 6-8 hours)
- Can add later if needed

**Recommendation:** Focus on completing Batches 1-8 first, then reassess based on actual testing needs.
