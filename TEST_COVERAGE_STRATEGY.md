# Test Coverage Strategy - Integration Testing Approach

## Goal
**Ensure all Kiota-generated client functionality works correctly against the MockServer**

The purpose is to verify that:
1. Every API endpoint is implemented in the MockServer
2. All Kiota-generated client methods successfully call the MockServer
3. Request/response serialization works correctly
4. The generated clients can execute full workflows

## Current Coverage Analysis

### What Low Coverage Means
Low coverage over the generated Kiota clients indicates:
- **Missing MockServer API implementations** - Endpoints that don't exist yet
- **Untested API paths** - Client methods that haven't been called in tests
- **Incomplete integration** - Workflows that haven't been validated

### Coverage Metrics Should Track
1. **API Endpoint Coverage** - Which endpoints are implemented in MockServer
2. **Client Method Coverage** - Which generated client methods have been tested
3. **Workflow Coverage** - Which business scenarios are validated

## Strategy to Increase Coverage

### Phase 1: Audit Current State (Use Script)
```powershell
.\analyze-test-coverage.ps1
```

This shows which API clients have tests and which don't.

### Phase 2: Implement Missing MockServer Endpoints

For each untested API in the analysis:

1. **Check if MockServer has the route**
   ```powershell
   Get-ChildItem src\HubSpot.MockServer\Routes -Filter "ApiRoutes.*.cs" | Select-String "MapGet\|MapPost\|MapPatch\|MapDelete"
   ```

2. **If route is missing, add it**
   - Create/update route file in `src\HubSpot.MockServer\Routes\`
   - Implement the endpoint using existing patterns
   - Register in `HubSpotMockServer.cs`

3. **Add repository if needed**
   - Create repository in `src\HubSpot.MockServer\Repositories\{ApiName}\`
   - Implement data storage and CRUD operations

### Phase 3: Write Integration Tests

For each API, create comprehensive tests following this pattern:

```csharp
// Example structure (adjust model names based on actual generated code)
public class {ApiName}Tests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpot{ApiName}Client _client = null!;

    [Fact]
    public async Task Can_create_entity() { }

    [Fact]
    public async Task Can_get_entity_by_id() { }

    [Fact]
    public async Task Can_list_entities() { }

    [Fact]
    public async Task Can_update_entity() { }

    [Fact]
    public async Task Can_delete_entity() { }

    [Fact]
    public async Task Can_batch_create() { }

    [Fact]
    public async Task Can_batch_update() { }

    [Fact]
    public async Task Can_search_with_filters() { }
}
```

### Phase 4: Priority Order

Based on HubSpot API importance and usage:

**Tier 1 - Core CRM (High Priority)**
- CRM Contacts (✓ exists)
- CRM Companies (✓ exists) 
- CRM Deals (✓ exists)
- CRM Tickets
- CRM Products
- CRM Line Items (✓ exists)

**Tier 2 - Extended CRM**
- CRM Quotes
- CRM Invoices
- CRM Calls
- CRM Emails
- CRM Meetings
- CRM Tasks
- CRM Notes

**Tier 3 - CMS (High Priority)**
- CMS Pages
- CMS Blog Posts
- CMS Blog Authors
- CMS Domains
- CMS URL Redirects
- HubDB Tables

**Tier 4 - Marketing**
- Marketing Emails
- Marketing Events
- Campaigns
- Forms
- Workflows

**Tier 5 - Communication & Automation**
- Conversations
- Automation Actions (✓ exists)
- Sequences (✓ exists)
- Webhooks (✓ exists)

**Tier 6 - Settings & Configuration**
- Properties & Schemas
- Pipelines
- Owners
- Users
- Business Units

## Implementation Checklist

For each API:
- [ ] Check if Kiota client is generated
- [ ] Check if MockServer route exists
- [ ] Check if repository exists  
- [ ] Implement missing routes/repositories
- [ ] Create test file
- [ ] Test CRUD operations (Create, Read, Update, Delete)
- [ ] Test List/Search operations
- [ ] Test Batch operations
- [ ] Test associations (if applicable)
- [ ] Test error cases (404, validation errors)

## Measuring Progress

### Run Coverage Report
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Generate HTML Report
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html
start coveragereport/index.html
```

### Track Metrics
- **Overall Coverage %** - Should increase as more APIs are tested
- **Generated Client Coverage** - Shows which client methods are being exercised
- **MockServer Coverage** - Shows which endpoints are implemented
- **Test Count** - Number of integration tests

## Target Coverage Goals

**Short Term (3 months)**
- Tier 1 APIs: 90% coverage
- Tier 2 APIs: 60% coverage
- Overall: 50% coverage

**Medium Term (6 months)**
- Tier 1-2 APIs: 90% coverage
- Tier 3 APIs: 80% coverage
- Overall: 70% coverage

**Long Term (12 months)**
- All Tiers: 80%+ coverage
- Overall: 80%+ coverage

## Example: Adding CRM Tickets Support

### 1. Check if route exists
```powershell
Get-Content src\HubSpot.MockServer\Routes\*.cs | Select-String "tickets"
```

### 2. Create repository
```csharp
// src/HubSpot.MockServer/Repositories/Ticket/TicketRepository.cs
namespace DamianH.HubSpot.MockServer.Repositories.Ticket;

internal class TicketRepository
{
    private readonly CrmObjectRepository<Objects.Ticket> _repository = new();
    
    public Objects.Ticket Create(Objects.Ticket ticket) => _repository.Create(ticket);
    public Objects.Ticket? GetById(string id) => _repository.GetById(id);
    // ... etc
}
```

### 3. Add route
```csharp
// src/HubSpot.MockServer/Routes/ApiRoutes.CrmTickets.cs
internal static partial class ApiRoutes
{
    internal static void RegisterCrmTicketsApi(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/tickets");
        
        group.MapPost("/", ([FromServices] TicketRepository repository, 
            [FromBody] SimplePublicObjectInputForCreate input) =>
        {
            // Implementation
        });
        
        // ... more endpoints
    }
}
```

### 4. Register in HubSpotMockServer.cs
```csharp
builder.Services.AddSingleton<TicketRepository>();
ApiRoutes.RegisterCrmTicketsApi(app);
```

### 5. Create tests
```csharp
// test/HubSpot.Tests/MockServer/CrmTicketsTests.cs
public class CrmTicketsTests : IAsyncLifetime
{
    [Fact]
    public async Task Can_create_ticket() { }
    // ... more tests
}
```

### 6. Run and verify coverage increased

## Tools & Scripts

- **`analyze-test-coverage.ps1`** - Shows which APIs lack tests
- **Coverage Reports** - Show which code paths are exercised
- **MockServer Logs** - Help debug endpoint issues

## Success Criteria

✓ Every generated Kiota client can successfully call the MockServer
✓ All major workflows can be tested end-to-end
✓ Code coverage reflects real API implementation completeness
✓ New API additions automatically show in coverage reports
✓ Tests catch breaking changes in MockServer or generated clients

## Next Steps

1. Run `analyze-test-coverage.ps1` to see current state
2. Pick highest priority untested API from Tier 1
3. Implement MockServer support (route + repository)
4. Write integration tests
5. Verify coverage increased
6. Repeat for next API

The coverage percentage will naturally increase as you add MockServer endpoints and tests for each API.
