# Adding New API Tests - Practical Guide

## Current Status

You have **23 existing test files** covering **31 APIs** with **3,397 lines** of test code. These tests work correctly and can be used as templates.

## How to Add a New API Test

### Step 1: Identify the API Structure

Before writing tests, examine the generated client structure:

```powershell
# Example: Check CMS Posts structure
Get-ChildItem "src\HubSpot.KiotaClient\Generated\CMS\Posts\V3" -Recurse

# Look for:
# 1. The main client file (e.g., HubSpotCMSPostsV3Client.cs)
# 2. Request builders in subfolders
# 3. Models folder with request/response types
```

### Step 2: Find a Similar Existing Test

Match your new API to an existing test pattern:

**For CRM Objects (Contacts, Companies, Deals, Tickets, Products, Quotes):**
- Template: `CrmContactsTests.cs`, `CrmCompaniesTests.cs`, `CrmDealsTests.cs`
- Pattern: CRUD operations with SimplePublicObjectInput models

**For CMS Content (Blog Posts, Pages, Authors):**
- Template: Check `CmsAdvancedTests.cs` 
- Pattern: May use HTTP client directly or specific CMS models

**For Marketing (Events, Emails, Campaigns):**
- Template: `MarketingTransactionalTests.cs`
- Pattern: Specific marketing models

**For Settings/Configuration:**
- Template: `MulticurrencyTests.cs`, `TaxRatesTests.cs`
- Pattern: Simple models, less CRUD-focused

### Step 3: Copy and Adapt the Template

#### Example: Adding CRM Appointments Tests

```csharp
// 1. Check what the generated client is called
// Look in: src\HubSpot.KiotaClient\Generated\CRM\Appointments\

// 2. Copy CrmContactsTests.cs as template
// 3. Replace:
//    - Class name: CrmContactsTests → CrmAppointmentsTests
//    - Client type: HubSpotCRMContactsV3Client → HubSpotCRMAppointmentsV3Client
//    - Using statements: CRM.Contacts.V3 → CRM.Appointments.V3
//    - Property names: email, firstname, lastname → appointment-specific fields

using DamianH.HubSpot.KiotaClient.CRM.Appointments.V3;
using DamianH.HubSpot.KiotaClient.CRM.Appointments.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmAppointmentsTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMAppointmentsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMAppointmentsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_appointment()
    {
        // Check CRM.Appointments.V3.Models for actual model names
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToString("o") },
                    { "hs_appointment_title", "Sales Meeting" },
                    // Add other appointment-specific properties
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Appointments.PostAsync(input);
        var appointment = created!.Entity!;

        appointment.ShouldNotBeNull();
        appointment.Id.ShouldNotBeNullOrEmpty();
        // Add assertions
    }

    // Add more tests: Get, Update, Delete, List, Batch operations

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
```

### Step 4: Verify the API Path

The key is getting the correct path. Look at an existing test and the client structure:

**CRM Objects pattern:**
```csharp
_client.Crm.V3.Objects.{ObjectType}.PostAsync(input)
_client.Crm.V3.Objects.{ObjectType}[id].GetAsync()
```

**CMS pattern (varies):**
```csharp
// Some use Blogs:
_client.Cms.V3.Blogs.Posts.PostAsync(input)
_client.Cms.V3.Blogs.Authors.PostAsync(input)

// Others don't:
_client.Cms.V3.Pages.SitePages.PostAsync(input)
```

**Marketing pattern:**
```csharp
_client.Marketing.V3.{Feature}.PostAsync(input)
```

### Step 5: Find the Correct Property Names

Check the HubSpot API documentation or MockServer repository implementation:

```powershell
# Example: What properties does Appointments support?
Get-Content "src\HubSpot.MockServer\Repositories\**\*Repository.cs" | 
    Select-String "appointment" -Context 5
```

Or check the route handler:
```powershell
Get-Content "src\HubSpot.MockServer\Routes\ApiRoutes.*.cs" | 
    Select-String "appointments" -Context 10
```

### Step 6: Build and Run

```bash
# Build to check for compilation errors
dotnet build test\HubSpot.Tests\HubSpot.Tests.csproj

# Run the specific test
dotnet test --filter "ClassName=CrmAppointmentsTests"

# If it fails, check:
# 1. MockServer has the route implemented
# 2. Model names match the generated client
# 3. API path is correct
```

## Common Issues and Solutions

### Issue: Model Not Found
**Error:** `SimplePublicObjectInputForCreate could not be found`

**Solution:** Check the Models folder for the actual model names:
```powershell
Get-ChildItem "src\HubSpot.KiotaClient\Generated\CRM\Appointments\V3\Models" | Select-Object Name
```

### Issue: Wrong API Path
**Error:** `Does not contain a definition for 'SomeProperty'`

**Solution:** Examine the request builders:
```powershell
Get-ChildItem "src\HubSpot.KiotaClient\Generated\CRM\Appointments\V3" -Recurse -Filter "*RequestBuilder.cs"
```

Look at how they're nested and build the path accordingly.

### Issue: MockServer Not Implemented
**Error:** 404 or 405 errors when running test

**Solution:** 
1. Check if route exists in `src\HubSpot.MockServer\Routes\`
2. Check if repository exists in `src\HubSpot.MockServer\Repositories\`
3. Implement missing MockServer support first (see TEST_COVERAGE_STRATEGY.md)

## Priority APIs to Add

Based on analysis, here are the highest-value APIs to add tests for:

**High Priority CRM:**
- [ ] CRM Appointments
- [ ] CRM Invoices  
- [ ] CRM Feedback Submissions
- [ ] CRM Goal Targets
- [ ] CRM Users

**High Priority CMS:**
- [ ] CMS Pages (using Kiota client, not HTTP)
- [ ] CMS Tags
- [ ] CMS URL Redirects
- [ ] CMS Site Search
- [ ] CMS Source Code

**High Priority Marketing:**
- [ ] Marketing Events
- [ ] Marketing Emails  
- [ ] Campaigns

**High Priority Settings:**
- [ ] Pipelines
- [ ] Properties
- [ ] Schemas
- [ ] Owners

## Template Checklist

When creating a new test file:
- [ ] Copied from appropriate existing test
- [ ] Updated class name
- [ ] Updated client type and using statements
- [ ] Verified API path by checking generated client structure
- [ ] Verified model names in Models folder
- [ ] Updated property names to match API
- [ ] Builds without errors
- [ ] Runs successfully (MockServer implements the route)
- [ ] Added tests for: Create, Get, Update, Delete, List, Batch operations

## Quick Reference: Test File Locations

```
test/HubSpot.Tests/MockServer/
├── CrmContactsTests.cs          ← Best template for CRM objects
├── CrmCompaniesTests.cs          ← Alternative CRM template
├── CrmDealsTests.cs              ← Another CRM template
├── AdditionalCrmObjectsTests.cs  ← Products, Quotes, Tickets examples
├── MarketingTransactionalTests.cs ← Marketing template
├── MulticurrencyTests.cs         ← Settings template
├── WebhooksTests.cs              ← Webhooks template
└── [Your new test file].cs       ← Add here
```

## Next Steps

1. Pick one API from the priority list above
2. Find the most similar existing test file
3. Copy it and adapt following this guide
4. Build and fix any compilation errors
5. Run the test
6. If it fails, check MockServer implementation
7. Repeat for next API

Remember: **Start simple**. Get one CRUD test working first, then add batch operations, filters, etc.
