# Account & Settings APIs - Complete Implementation Plan

## Overview
This document outlines the complete implementation plan for all Account and Settings APIs in the HubSpot Mock Server.

## APIs to Implement

### Account APIs
1. **Account Info V3** (`HubSpotAccountAccountInfoV3Client`)
   - GET `/account/v3/api-usage/daily` - Get daily API usage
   - GET `/account/v3/api-usage/daily/private-apps` - Get private apps daily usage
   - GET `/account-info/v3/details` - Get account details

2. **Account Info V202509** (`HubSpotAccountAccountInfoV202509Client`)
   - Similar endpoints with updated API version

3. **Audit Logs V3** (`HubSpotAccountAuditLogsV3Client`)
   - GET `/account-info/v3/audit-logs/activity` - Get activity audit logs
   - GET `/account-info/v3/audit-logs/login` - Get login audit logs
   - GET `/account-info/v3/audit-logs/security` - Get security audit logs

### Settings APIs
1. **Multicurrency V3** (`HubSpotSettingsMulticurrencyV3Client`)
   - Company Currency:
     - GET `/settings/v3/currencies/company-currency` - Get company currency
     - PUT `/settings/v3/currencies/company-currency` - Update company currency
   
   - Currencies:
     - GET `/settings/v3/currencies/codes` - Get supported currency codes
     - POST `/settings/v3/currencies/add-currency` - Add a currency
     - PUT `/settings/v3/currencies/update-visibility` - Update currency visibility
   
   - Exchange Rates:
     - GET `/settings/v3/currencies/exchange-rates` - List exchange rates
     - POST `/settings/v3/currencies/exchange-rates` - Create exchange rate
     - PUT `/settings/v3/currencies/exchange-rates/{exchangeRateId}` - Update exchange rate
     - DELETE `/settings/v3/currencies/exchange-rates/{exchangeRateId}` - Delete exchange rate
     - GET `/settings/v3/currencies/exchange-rates/current` - Get current exchange rates
     - POST `/settings/v3/currencies/exchange-rates/batch/create` - Batch create
     - POST `/settings/v3/currencies/exchange-rates/batch/read` - Batch read
     - POST `/settings/v3/currencies/exchange-rates/batch/update` - Batch update
   
   - Central FX Rates:
     - GET `/settings/v3/currencies/central-fx-rates/information` - Get central FX info
     - GET `/settings/v3/currencies/central-fx-rates/unsupported-currencies` - Get unsupported currencies

2. **User Provisioning V3** (`HubSpotSettingsUserProvisioningV3Client`)
   - Users:
     - GET `/settings/v3/users` - List users
     - POST `/settings/v3/users` - Create user
     - GET `/settings/v3/users/{userId}` - Get user by ID (supports email/id)
     - PUT `/settings/v3/users/{userId}` - Update user
     - DELETE `/settings/v3/users/{userId}` - Remove user
   
   - Roles:
     - GET `/settings/v3/users/roles` - List available roles/permissions
   
   - Teams:
     - GET `/settings/v3/users/teams` - List teams

3. **Tax Rates V1** (`HubSpotSettingsTaxRatesV1Client`)
   - GET `/tax-rates/v1/tax-rates` - List tax rate groups
   - GET `/tax-rates/v1/tax-rates/{taxRateGroupId}` - Get tax rate group

## Implementation Phases

### Phase 1: Repository & Data Models (30 min)
**Goal:** Create repositories and data models for Account & Settings data

#### Tasks:
1. Create `AccountInfoRepository.cs`
   - Account details storage (account ID, portal ID, name, timezone, etc.)
   - API usage tracking (daily usage counters)
   - Audit logs storage (activity, login, security events)

2. Create `CurrencyRepository.cs`
   - Company currency settings
   - Supported currencies list
   - Exchange rates storage
   - Central FX rates information

3. Create `UserProvisioningRepository.cs`
   - User accounts (id, email, first name, last name, roles, teams)
   - Roles/permissions definitions
   - Teams storage

4. Create `TaxRateRepository.cs`
   - Tax rate groups storage
   - Tax rates within groups

5. Create data model classes in `Objects/` folder:
   - `AccountDetail.cs`
   - `ApiUsageData.cs`
   - `AuditLogEntry.cs`
   - `Currency.cs`
   - `ExchangeRate.cs`
   - `UserAccount.cs`
   - `TaxRateGroup.cs`

**Validation:**
- All repositories compile
- Data models are properly structured

---

### Phase 2: Account APIs Implementation (45 min)
**Goal:** Implement all Account API endpoints

#### Tasks:
1. Create `ApiRoutes.Account.cs` partial class

2. Implement `RegisterAccountInfoV3Api()`
   - GET `/account/v3/api-usage/daily` → Return daily API usage stats
   - GET `/account/v3/api-usage/daily/private-apps` → Return private apps usage
   - GET `/account-info/v3/details` → Return account details

3. Implement `RegisterAccountInfoV202509Api()`
   - Similar to V3 but with 202509 version paths

4. Implement `RegisterAuditLogsV3Api()`
   - GET `/account-info/v3/audit-logs/activity` → Return activity logs with pagination
   - GET `/account-info/v3/audit-logs/login` → Return login logs with pagination
   - GET `/account-info/v3/audit-logs/security` → Return security logs with pagination

5. Register all Account APIs in `HubSpotMockServer.cs`

**Validation:**
- Build succeeds
- All Account endpoints are registered

---

### Phase 3: Multicurrency API Implementation (60 min)
**Goal:** Implement complete Multicurrency settings API

#### Tasks:
1. Create `ApiRoutes.Multicurrency.cs` partial class

2. Implement Company Currency endpoints:
   - GET `/settings/v3/currencies/company-currency`
   - PUT `/settings/v3/currencies/company-currency`

3. Implement Currencies Management:
   - GET `/settings/v3/currencies/codes`
   - POST `/settings/v3/currencies/add-currency`
   - PUT `/settings/v3/currencies/update-visibility`

4. Implement Exchange Rates (full CRUD):
   - GET `/settings/v3/currencies/exchange-rates` (list with pagination)
   - POST `/settings/v3/currencies/exchange-rates` (create)
   - GET `/settings/v3/currencies/exchange-rates/{id}` (get)
   - PUT `/settings/v3/currencies/exchange-rates/{id}` (update)
   - DELETE `/settings/v3/currencies/exchange-rates/{id}` (delete)
   - GET `/settings/v3/currencies/exchange-rates/current` (current rates)

5. Implement Batch Operations:
   - POST `/settings/v3/currencies/exchange-rates/batch/create`
   - POST `/settings/v3/currencies/exchange-rates/batch/read`
   - POST `/settings/v3/currencies/exchange-rates/batch/update`

6. Implement Central FX Rates:
   - GET `/settings/v3/currencies/central-fx-rates/information`
   - GET `/settings/v3/currencies/central-fx-rates/unsupported-currencies`

7. Register Multicurrency API in `HubSpotMockServer.cs`

**Validation:**
- Build succeeds
- All Multicurrency endpoints registered

---

### Phase 4: User Provisioning API Implementation (45 min)
**Goal:** Implement User Provisioning settings API

#### Tasks:
1. Create `ApiRoutes.UserProvisioning.cs` partial class

2. Implement User CRUD:
   - GET `/settings/v3/users` (list with pagination, filter by email)
   - POST `/settings/v3/users` (create user)
   - GET `/settings/v3/users/{userId}` (get by ID or email via query param)
   - PUT `/settings/v3/users/{userId}` (update user)
   - DELETE `/settings/v3/users/{userId}` (remove user)

3. Implement Roles endpoint:
   - GET `/settings/v3/users/roles` (list available permission sets)

4. Implement Teams endpoint:
   - GET `/settings/v3/users/teams` (list teams)

5. Register User Provisioning API in `HubSpotMockServer.cs`

**Validation:**
- Build succeeds
- All User Provisioning endpoints registered

---

### Phase 5: Tax Rates API & Testing (60 min)
**Goal:** Implement Tax Rates API and comprehensive tests for all Account/Settings APIs

#### Tasks:
1. Create `ApiRoutes.TaxRates.cs` partial class

2. Implement Tax Rates:
   - GET `/tax-rates/v1/tax-rates` (list with pagination)
   - GET `/tax-rates/v1/tax-rates/{id}` (get by ID)

3. Register Tax Rates API in `HubSpotMockServer.cs`

4. Create comprehensive test files:
   - `test/HubSpot.Tests/MockServer/AccountInfoTests.cs`
   - `test/HubSpot.Tests/MockServer/AuditLogsTests.cs`
   - `test/HubSpot.Tests/MockServer/MulticurrencyTests.cs`
   - `test/HubSpot.Tests/MockServer/UserProvisioningTests.cs`
   - `test/HubSpot.Tests/MockServer/TaxRatesTests.cs`

5. Test coverage for each API:
   - **AccountInfo**: Test getting account details, API usage stats
   - **AuditLogs**: Test retrieving activity, login, security logs
   - **Multicurrency**: Test CRUD operations for currencies and exchange rates, batch operations
   - **UserProvisioning**: Test user CRUD, roles, teams
   - **TaxRates**: Test listing and getting tax rate groups

6. Run all tests and fix any issues

**Validation:**
- All tests pass
- Build succeeds
- Full coverage of Account & Settings APIs

---

## Summary

### Total Estimated Time: 4 hours

### Deliverables:
1. ✅ 5 new repository classes
2. ✅ 7+ data model classes
3. ✅ 5 API route partial classes
4. ✅ 30+ endpoint implementations
5. ✅ 5 comprehensive test files
6. ✅ All tests passing
7. ✅ Full Account & Settings API coverage

### APIs Covered:
- Account Info (V3 + V202509)
- Audit Logs
- Multicurrency Settings
- User Provisioning
- Tax Rates

### Key Features:
- Full CRUD operations where applicable
- Batch operations for exchange rates
- Pagination support
- Query parameter support (filtering, idProperty)
- Comprehensive test coverage
- Proper error handling
- Repository pattern for data management
