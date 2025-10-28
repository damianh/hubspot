# Test Coverage Improvement Plan for Kiota Clients

## Current State
- Tests exist for MockServer integration using Kiota clients
- Generated Kiota client code has low coverage (expected for generated code)
- Integration tests cover major workflows but not all API endpoints

## Recommendations

### 1. Exclude Generated Code from Coverage Metrics

Use the provided `coverlet.runsettings` file:

```bash
dotnet test --settings coverlet.runsettings --collect:"XPlat Code Coverage"
```

This excludes the auto-generated Kiota client code from coverage calculations.

### 2. Focus on Integration Test Coverage

The goal should be to test **API endpoint coverage**, not generated client code coverage.

#### Current Test Coverage Gaps

Based on the generated clients, here are APIs that may need more tests:

**CMS APIs:**
- ✓ Blog Posts (tested)
- ✓ Blog Authors (tested in CmsAdvancedTests)
- ⚠️ Blog Settings (limited)
- ⚠️ Domains (limited)
- ⚠️ HubDB (limited)
- ⚠️ Media Bridge (limited)
- ⚠️ Pages (limited)
- ⚠️ Site Search (limited)
- ⚠️ Source Code (limited)
- ⚠️ Tags (limited)
- ⚠️ URL Redirects (limited)

**CRM APIs:**
- ✓ Contacts (well tested)
- ✓ Companies (well tested)
- ✓ Deals (well tested)
- ✓ Line Items (well tested)
- ✓ Generic Objects (tested)
- ⚠️ Appointments (minimal)
- ⚠️ Feedback Submissions (minimal)
- ⚠️ Goals (minimal)
- ⚠️ Postal Mail (minimal)
- ⚠️ Quotes (minimal)

**Marketing APIs:**
- ✓ Transactional Emails (tested)
- ⚠️ Single Send (minimal)
- ⚠️ Marketing Emails (minimal)
- ⚠️ Marketing Events (minimal)

**Other APIs:**
- ✓ Webhooks (tested)
- ✓ Conversations (tested)
- ✓ Automation Actions (tested)
- ⚠️ Business Units (minimal)
- ⚠️ Calling Extensions (minimal)
- ⚠️ User Provisioning (limited)

### 3. Add More Integration Tests

Create tests for under-tested APIs. Use this template:

```csharp
// Example: CMS Pages Tests
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CmsPagesTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCMSPagesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCMSPagesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_page()
    {
        // Arrange
        var input = new PageInput
        {
            Name = "Test Page",
            Slug = "test-page",
            // Add more properties
        };

        // Act
        var created = await _client.Cms.V3.Pages.SitePages.PostAsync(input);

        // Assert
        created.ShouldNotBeNull();
        created.Id.ShouldNotBeNullOrEmpty();
        created.Name.ShouldBe("Test Page");
    }

    [Fact]
    public async Task Can_get_page_by_id()
    {
        // Create a page first, then retrieve it
    }

    [Fact]
    public async Task Can_update_page()
    {
        // Create, then update, then verify
    }

    [Fact]
    public async Task Can_delete_page()
    {
        // Create, then delete, then verify 404
    }

    [Fact]
    public async Task Can_list_pages()
    {
        // Create multiple pages, then list and verify
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
```

### 4. Measure API Coverage

Create a script to track which API endpoints are tested:

```powershell
# Count tested vs untested API endpoints
$generatedClients = Get-ChildItem "src/HubSpot.KiotaClient/Generated" -Recurse -Filter "*Client.cs"
$testFiles = Get-ChildItem "test/HubSpot.Tests/MockServer" -Filter "*Tests.cs"

# Compare and report gaps
```

### 5. Update Project Files

Add coverlet to the test project if not already present:

```xml
<ItemGroup>
  <PackageReference Include="coverlet.msbuild" Version="6.0.0">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

## Priority Order

1. **High Priority** - Exclude generated code from coverage (done)
2. **Medium Priority** - Add tests for commonly-used CMS APIs (Pages, Domains, HubDB)
3. **Medium Priority** - Add tests for additional CRM object types (Appointments, Quotes)
4. **Low Priority** - Add tests for less-common APIs

## Running Coverage Reports

```bash
# Generate coverage report
dotnet test --settings coverlet.runsettings --collect:"XPlat Code Coverage"

# Generate HTML report (requires reportgenerator)
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html

# Open the report
start coveragereport/index.html
```

## Expected Outcome

After implementing these recommendations:
- Generated Kiota code excluded from coverage metrics
- Coverage metrics focus on MockServer and custom code
- Better API endpoint test coverage
- Clearer visibility into which APIs are tested vs untested
