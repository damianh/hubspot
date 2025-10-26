# Account & Settings APIs Implementation Plan

## Overview
Implement mock server endpoints and tests for HubSpot Account and Settings APIs. These APIs provide account-level information, audit logs, and configuration settings.

## APIs to Implement

### 1. Account APIs

#### 1.1 Account Info V3
**Client**: `HubSpotAccountAccountInfoV3Client`
**Base Path**: `/account-info/v3`

**Endpoints**:
- `GET /account-info/v3/details` - Get account details (portal information, timezone, currencies, data hosting)
- `GET /account-info/v3/api-usage/daily/private-apps` - Get daily API usage for private apps

**Models**:
- `PortalInformationResponse` - Account details response
- API usage models

#### 1.2 Account Info V202509
**Client**: `HubSpotAccountAccountInfoV202509Client`
**Base Path**: `/account-info/2025-09`

**Endpoints**:
- `GET /account-info/2025-09/details` - Get account details (newer version)
- `GET /account-info/2025-09/api-usage/daily/private-apps` - API usage (newer version)

#### 1.3 Audit Logs V3
**Client**: `HubSpotAccountAuditLogsV3Client`
**Base Path**: `/account-info/v3/activity`

**Endpoints**:
- `GET /account-info/v3/activity/audit-logs` - Get audit logs (Enterprise only)
  - Query params: actingUserId, after, limit, occurredAfter, occurredBefore, sort
- `GET /account-info/v3/activity/login` - Get login activity logs
- `GET /account-info/v3/activity/security` - Get security activity logs

**Models**:
- `CollectionResponsePublicApiUserActionEventForwardPaging` - Audit logs collection
- `PublicApiUserActionEvent` - Individual audit log event

### 2. Settings APIs

#### 2.1 Multicurrency V3
**Client**: `HubSpotSettingsMulticurrencyV3Client`
**Base Path**: `/settings/v3/currencies`

**Endpoints**:
- `GET /settings/v3/currencies/codes` - Get available currency codes
- `GET /settings/v3/currencies/company-currency` - Get company default currency
- `PUT /settings/v3/currencies/company-currency` - Update company currency
- `GET /settings/v3/currencies/exchange-rates` - Get exchange rates
- `POST /settings/v3/currencies/exchange-rates/batch/create` - Create multiple exchange rates
- `POST /settings/v3/currencies/exchange-rates/batch/update` - Update multiple exchange rates
- `GET /settings/v3/currencies/exchange-rates/current/{fromCurrencyCode}/{toCurrencyCode}` - Get current rate
- `PUT /settings/v3/currencies/exchange-rates/{fromCurrencyCode}/{toCurrencyCode}` - Update exchange rate
- `GET /settings/v3/currencies/central-fx-rates` - Get central FX rates
- `POST /settings/v3/currencies/central-fx-rates/archive` - Archive central FX rate
- `POST /settings/v3/currencies/central-fx-rates/import` - Import central FX rates
- `PUT /settings/v3/currencies/central-fx-rates/update` - Update central FX rate

**Models**:
- Currency codes, exchange rates, central FX rates models

#### 2.2 Tax Rates V1
**Client**: `HubSpotSettingsTaxRatesV1Client`
**Base Path**: `/tax-rates/v1/tax-rates`

**Endpoints**:
- `GET /tax-rates/v1/tax-rates` - List tax rates
- `POST /tax-rates/v1/tax-rates` - Create tax rate
- `GET /tax-rates/v1/tax-rates/{taxRateId}` - Get tax rate
- `PATCH /tax-rates/v1/tax-rates/{taxRateId}` - Update tax rate
- `DELETE /tax-rates/v1/tax-rates/{taxRateId}` - Archive tax rate

**Models**:
- Tax rate models

#### 2.3 User Provisioning V3
**Client**: `HubSpotSettingsUserProvisioningV3Client`
**Base Path**: `/settings/v3/users`

**Endpoints**:
- `GET /settings/v3/users` - List users
- `POST /settings/v3/users` - Create user
- `GET /settings/v3/users/{userId}` - Get user
- `PUT /settings/v3/users/{userId}` - Update user
- `DELETE /settings/v3/users/{userId}` - Remove user
- `GET /settings/v3/users/{userId}/roles` - Get user roles
- `PUT /settings/v3/users/{userId}/roles` - Update user roles
- `GET /settings/v3/users/roles` - List available roles
- `GET /settings/v3/users/teams` - List teams
- `POST /settings/v3/users/teams` - Create team
- `GET /settings/v3/users/teams/{teamId}` - Get team
- `PUT /settings/v3/users/teams/{teamId}` - Update team
- `DELETE /settings/v3/users/teams/{teamId}` - Archive team
- `GET /settings/v3/users/teams/{teamId}/users` - Get team members
- `PUT /settings/v3/users/teams/{teamId}/users` - Update team members

**Models**:
- User, Role, Team models

## Implementation Strategy

### Phase 1: Infrastructure & Simple APIs (Account Info)
**Priority: High** | **Complexity: Low-Medium**

1. Create `ApiRoutes.Account.cs` for Account APIs
2. Create `ApiRoutes.Settings.cs` for Settings APIs
3. Implement Account Info V3 endpoints (simple GET endpoints)
4. Implement Account Info V202509 endpoints
5. Create simple in-memory data stores for account settings
6. Create tests in `AccountInfoTests.cs`

**Time estimate**: 2-3 hours

### Phase 2: Audit Logs
**Priority: Medium** | **Complexity: Medium**

1. Implement Audit Logs V3 endpoints
2. Create audit log event generator/storage
3. Implement filtering and pagination
4. Create tests in `AuditLogsTests.cs`

**Time estimate**: 2-3 hours

### Phase 3: Multicurrency Settings
**Priority: Medium** | **Complexity: Medium-High**

1. Implement currency codes endpoint (simple reference data)
2. Implement company currency GET/PUT
3. Implement exchange rates CRUD operations
4. Implement batch operations for exchange rates
5. Implement central FX rates endpoints
6. Create tests in `MulticurrencyTests.cs`

**Time estimate**: 3-4 hours

### Phase 4: Tax Rates
**Priority: Low-Medium** | **Complexity: Low-Medium**

1. Implement tax rates CRUD endpoints
2. Create simple tax rate storage
3. Create tests in `TaxRatesTests.cs`

**Time estimate**: 1-2 hours

### Phase 5: User Provisioning
**Priority: Medium** | **Complexity: High**

1. Implement users CRUD endpoints
2. Implement user roles endpoints
3. Implement teams CRUD endpoints
4. Implement team membership endpoints
5. Create hierarchical storage for users/roles/teams
6. Create tests in `UserProvisioningTests.cs`

**Time estimate**: 4-5 hours

## Data Model Design

### Account Info Store
```csharp
public class AccountInfoStore
{
    public PortalInformation AccountDetails { get; set; }
    public Dictionary<string, DailyApiUsage> ApiUsage { get; set; }
}
```

### Audit Log Store
```csharp
public class AuditLogStore
{
    private List<AuditLogEvent> _events;
    
    public IEnumerable<AuditLogEvent> GetLogs(
        string? actingUserId = null,
        DateTime? occurredAfter = null,
        DateTime? occurredBefore = null,
        string? sort = null,
        int limit = 100,
        string? after = null);
}
```

### Currency Store
```csharp
public class CurrencyStore
{
    public List<CurrencyCode> AvailableCodes { get; set; }
    public string CompanyCurrency { get; set; }
    public Dictionary<string, Dictionary<string, ExchangeRate>> ExchangeRates { get; set; }
    public List<CentralFxRate> CentralFxRates { get; set; }
}
```

### Tax Rate Store
```csharp
public class TaxRateStore
{
    private Dictionary<string, TaxRate> _taxRates;
    
    public IEnumerable<TaxRate> List();
    public TaxRate? Get(string id);
    public TaxRate Create(TaxRate taxRate);
    public TaxRate Update(string id, TaxRate taxRate);
    public void Archive(string id);
}
```

### User Provisioning Store
```csharp
public class UserProvisioningStore
{
    private Dictionary<string, User> _users;
    private Dictionary<string, Role> _roles;
    private Dictionary<string, Team> _teams;
    private Dictionary<string, List<string>> _userRoles;
    private Dictionary<string, List<string>> _teamMembers;
    
    // User methods
    public IEnumerable<User> ListUsers();
    public User? GetUser(string userId);
    public User CreateUser(User user);
    public User UpdateUser(string userId, User user);
    public void RemoveUser(string userId);
    
    // Role methods
    public IEnumerable<Role> ListRoles();
    public IEnumerable<Role> GetUserRoles(string userId);
    public void UpdateUserRoles(string userId, IEnumerable<string> roleIds);
    
    // Team methods
    public IEnumerable<Team> ListTeams();
    public Team? GetTeam(string teamId);
    public Team CreateTeam(Team team);
    public Team UpdateTeam(string teamId, Team team);
    public void ArchiveTeam(string teamId);
    public IEnumerable<User> GetTeamMembers(string teamId);
    public void UpdateTeamMembers(string teamId, IEnumerable<string> userIds);
}
```

## Testing Strategy

### Test Files to Create
1. `AccountInfoTests.cs` - Account info endpoints (both V3 and V202509)
2. `AuditLogsTests.cs` - Audit logs with filtering and pagination
3. `MulticurrencyTests.cs` - Currency and exchange rate operations
4. `TaxRatesTests.cs` - Tax rate CRUD operations
5. `UserProvisioningTests.cs` - User, role, and team management

### Test Coverage Requirements
- All CRUD operations
- Pagination and filtering
- Batch operations (where applicable)
- Error cases (404, 400, etc.)
- Both API versions (where applicable)

## File Structure

```
src/HubSpot.MockServer/
├── ApiRoutes.Account.cs          # NEW - Account API routes
├── ApiRoutes.Settings.cs         # NEW - Settings API routes
├── Stores/
│   ├── AccountInfoStore.cs       # NEW - Account info storage
│   ├── AuditLogStore.cs          # NEW - Audit log storage
│   ├── CurrencyStore.cs          # NEW - Currency/exchange rate storage
│   ├── TaxRateStore.cs           # NEW - Tax rate storage
│   └── UserProvisioningStore.cs  # NEW - User/role/team storage

test/HubSpot.Tests/MockServer/
├── AccountInfoTests.cs           # NEW
├── AuditLogsTests.cs             # NEW
├── MulticurrencyTests.cs         # NEW
├── TaxRatesTests.cs              # NEW
└── UserProvisioningTests.cs      # NEW
```

## Implementation Order (Recommended)

1. **Phase 1**: Account Info (simple, good starting point)
2. **Phase 4**: Tax Rates (simple CRUD, builds on patterns)
3. **Phase 2**: Audit Logs (adds complexity with filtering)
4. **Phase 3**: Multicurrency (complex with batch operations)
5. **Phase 5**: User Provisioning (most complex with relationships)

## Notes

- **Account/Settings APIs are simpler than CRM APIs** - They're mostly configuration data, not complex object graphs
- **No batch operations required** (except multicurrency) - Simpler to implement
- **Less interdependency** - Each API is relatively standalone
- **Read-heavy APIs** - Most are GET operations with some PUT/POST
- **Audit Logs are Enterprise only** - Should return appropriate error for non-enterprise accounts (consider adding enterprise flag to mock server config)
- **User Provisioning has relationships** - Users → Roles, Teams → Users (similar to CRM associations but simpler)

## Potential Issues

1. **Timezone handling** - Account info includes timezone settings
2. **Currency formatting** - Need to handle different currency formats
3. **Audit log event types** - Many different event types to consider
4. **User permissions** - Role-based access control might be complex
5. **Team hierarchies** - Teams might have parent-child relationships

## Success Criteria

- [ ] All Account Info endpoints implemented and tested
- [ ] All Audit Logs endpoints implemented and tested
- [ ] All Multicurrency endpoints implemented and tested
- [ ] All Tax Rates endpoints implemented and tested
- [ ] All User Provisioning endpoints implemented and tested
- [ ] All tests passing
- [ ] No compilation errors
- [ ] Proper error handling (404, 400, etc.)
- [ ] Pagination working correctly
- [ ] Both API versions (V3 and V202509) working for Account Info

## Estimated Total Time
**12-17 hours** for complete implementation and testing of all Account & Settings APIs.

This is significantly less complex than CRM APIs due to simpler data models and fewer interdependencies.
