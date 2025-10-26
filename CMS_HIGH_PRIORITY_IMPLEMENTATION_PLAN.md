# CMS High-Priority APIs Implementation Plan

## Overview
Implement the remaining high-priority CMS APIs in the mock server to enable comprehensive testing of HubSpot CMS functionality.

## Current Status

### ✅ Already Implemented
1. **CMS Tags** (`ApiRoutes.CmsTags`) - Blog tags API
2. **Blog Settings** (`ApiRoutes.CmsBlogSettings`) - Blog configuration API

### ✅ Repositories Already Created
- BlogPostRepository.cs
- BlogAuthorRepository.cs  
- BlogTagRepository.cs (used by Tags API)
- PageRepository.cs
- DomainRepository.cs
- UrlRedirectRepository.cs
- HubDbRepository.cs
- SourceCodeRepository.cs
- ContentAuditRepository.cs
- BlogSettingsRepository.cs (used by Blog Settings API)

### 📦 CMS Clients Available (12 total)
Based on `src/HubSpot.KiotaClient/Generated/CMS/`:
1. **Authors** - Blog authors management
2. **BlogSettings** - ✅ Already implemented
3. **CmsContentAudit** - Content audit logs
4. **Domains** - Domain configuration
5. **Hubdb** - Database tables and rows
6. **MediaBridge** - Media asset management
7. **Pages** - CMS pages
8. **Posts** - Blog posts
9. **SiteSearch** - Site search configuration
10. **SourceCode** - File system and templates
11. **Tags** - ✅ Already implemented
12. **UrlRedirects** - URL redirect management

## Implementation Plan

### Phase 1: Blog Content APIs (Highest Priority)
These are the most commonly used CMS features.

#### 1.1 Blog Posts API ⭐⭐⭐
**File**: `ApiRoutes.CmsBlogPosts.cs`
**Repository**: BlogPostRepository (already created)
**Client**: `HubSpotCMSPostsV3Client`

**Endpoints**:
- `GET /cms/v3/blogs/posts` - List posts
- `GET /cms/v3/blogs/posts/{objectId}` - Get post
- `POST /cms/v3/blogs/posts` - Create post
- `PATCH /cms/v3/blogs/posts/{objectId}` - Update post
- `DELETE /cms/v3/blogs/posts/{objectId}` - Archive post
- `POST /cms/v3/blogs/posts/batch/create` - Batch create
- `POST /cms/v3/blogs/posts/batch/read` - Batch read
- `POST /cms/v3/blogs/posts/batch/update` - Batch update
- `POST /cms/v3/blogs/posts/batch/archive` - Batch archive
- `GET /cms/v3/blogs/posts/{objectId}/revisions` - Get revisions
- `GET /cms/v3/blogs/posts/{objectId}/revisions/{revisionId}` - Get specific revision
- `POST /cms/v3/blogs/posts/multi-language/create-language-variation` - Create translation
- `POST /cms/v3/blogs/posts/multi-language/update-languages` - Update languages

**Test File**: `CmsBlogPostsTests.cs`

#### 1.2 Blog Authors API ⭐⭐⭐
**File**: `ApiRoutes.CmsBlogAuthors.cs`
**Repository**: BlogAuthorRepository (already created)
**Client**: `HubSpotCMSAuthorsV3Client`

**Endpoints**:
- `GET /cms/v3/blogs/authors` - List authors
- `GET /cms/v3/blogs/authors/{objectId}` - Get author
- `POST /cms/v3/blogs/authors` - Create author
- `PATCH /cms/v3/blogs/authors/{objectId}` - Update author
- `DELETE /cms/v3/blogs/authors/{objectId}` - Archive author
- `POST /cms/v3/blogs/authors/batch/create` - Batch create
- `POST /cms/v3/blogs/authors/batch/read` - Batch read
- `POST /cms/v3/blogs/authors/batch/update` - Batch update
- `POST /cms/v3/blogs/authors/batch/archive` - Batch archive

**Test File**: `CmsBlogAuthorsTests.cs`

### Phase 2: Pages and Domains (High Priority)

#### 2.1 Pages API ⭐⭐⭐
**File**: `ApiRoutes.CmsPages.cs`
**Repository**: PageRepository (already created)
**Client**: `HubSpotCMSPagesV3Client`

**Endpoints**:
- `GET /cms/v3/pages/site-pages` - List pages
- `GET /cms/v3/pages/site-pages/{objectId}` - Get page
- `POST /cms/v3/pages/site-pages` - Create page
- `PATCH /cms/v3/pages/site-pages/{objectId}` - Update page
- `DELETE /cms/v3/pages/site-pages/{objectId}` - Archive page
- `POST /cms/v3/pages/site-pages/batch/create` - Batch create
- `POST /cms/v3/pages/site-pages/batch/read` - Batch read
- `POST /cms/v3/pages/site-pages/batch/update` - Batch update
- `POST /cms/v3/pages/site-pages/batch/archive` - Batch archive
- Landing pages and other page types as needed

**Test File**: `CmsPagesTests.cs`

#### 2.2 Domains API ⭐⭐
**File**: `ApiRoutes.CmsDomains.cs`
**Repository**: DomainRepository (already created)
**Client**: `HubSpotCMSDomainsV3Client`

**Endpoints**:
- `GET /cms/v3/domains` - List domains
- `GET /cms/v3/domains/{domainId}` - Get domain

**Test File**: `CmsDomainsTests.cs`

#### 2.3 URL Redirects API ⭐⭐
**File**: `ApiRoutes.CmsUrlRedirects.cs`
**Repository**: UrlRedirectRepository (already created)
**Client**: `HubSpotCMSUrlRedirectsV3Client`

**Endpoints**:
- `GET /cms/v3/url-redirects` - List redirects
- `GET /cms/v3/url-redirects/{urlRedirectId}` - Get redirect
- `POST /cms/v3/url-redirects` - Create redirect
- `PATCH /cms/v3/url-redirects/{urlRedirectId}` - Update redirect
- `DELETE /cms/v3/url-redirects/{urlRedirectId}` - Delete redirect

**Test File**: `CmsUrlRedirectsTests.cs`

### Phase 3: Advanced CMS Features (Medium Priority)

#### 3.1 HubDB API ⭐⭐
**File**: `ApiRoutes.CmsHubDb.cs`
**Repository**: HubDbRepository (already created)
**Client**: `HubSpotCMSHubdbV3Client`

**Endpoints**:
- `GET /cms/v3/hubdb/tables` - List tables
- `GET /cms/v3/hubdb/tables/{tableIdOrName}` - Get table
- `POST /cms/v3/hubdb/tables` - Create table
- `PATCH /cms/v3/hubdb/tables/{tableIdOrName}` - Update table
- `DELETE /cms/v3/hubdb/tables/{tableIdOrName}` - Archive table
- `GET /cms/v3/hubdb/tables/{tableIdOrName}/rows` - List rows
- `GET /cms/v3/hubdb/tables/{tableIdOrName}/rows/{rowId}` - Get row
- `POST /cms/v3/hubdb/tables/{tableIdOrName}/rows` - Create row
- `PATCH /cms/v3/hubdb/tables/{tableIdOrName}/rows/{rowId}` - Update row
- `DELETE /cms/v3/hubdb/tables/{tableIdOrName}/rows/{rowId}` - Delete row
- `POST /cms/v3/hubdb/tables/{tableIdOrName}/rows/batch/create` - Batch operations
- `POST /cms/v3/hubdb/tables/{tableIdOrName}/publish` - Publish table

**Test File**: `CmsHubDbTests.cs`

#### 3.2 Source Code API ⭐⭐
**File**: `ApiRoutes.CmsSourceCode.cs`
**Repository**: SourceCodeRepository (already created)
**Client**: `HubSpotCMSSourceCodeV3Client`

**Endpoints**:
- `GET /cms/v3/source-code/{environment}/content` - List files
- `GET /cms/v3/source-code/{environment}/content/{path}` - Get file
- `POST /cms/v3/source-code/{environment}/content/{path}` - Create/upload file
- `PUT /cms/v3/source-code/{environment}/content/{path}` - Replace file
- `DELETE /cms/v3/source-code/{environment}/content/{path}` - Delete file
- `POST /cms/v3/source-code/{environment}/validate/{path}` - Validate file

**Test File**: `CmsSourceCodeTests.cs`

### Phase 4: Search and Audit (Lower Priority)

#### 4.1 Site Search API ⭐
**File**: `ApiRoutes.CmsSiteSearch.cs`
**Repository**: Create `SiteSearchRepository.cs`
**Client**: `HubSpotCMSSiteSearchV3Client`

**Endpoints**:
- `GET /cms/v3/site-search/search` - Perform search
- `GET /cms/v3/site-search/indexed-data` - Get indexed content

**Test File**: `CmsSiteSearchTests.cs`

#### 4.2 Content Audit API ⭐
**File**: `ApiRoutes.CmsContentAudit.cs`
**Repository**: ContentAuditRepository (already created)
**Client**: `HubSpotCMSCmsContentAuditV3Client`

**Endpoints**:
- `GET /cms/v3/audit-logs/{objectType}/{objectId}` - Get audit logs

**Test File**: `CmsContentAuditTests.cs`

### Phase 5: Media Bridge (Optional/Low Priority)

#### 5.1 Media Bridge API ⭐
**File**: `ApiRoutes.CmsMediaBridge.cs`
**Repository**: Create `MediaBridgeRepository.cs` if needed
**Client**: `HubSpotCMSMediaBridgeV3Client`

**Note**: Media Bridge is a specialized API for integrating external media services. May not be needed for most testing scenarios.

## Implementation Strategy

### Pattern to Follow
Each API route file follows this structure (based on existing CmsTags implementation):

```csharp
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static partial class CmsXxx
    {
        public static void RegisterCmsXxxV3Api(WebApplication app)
        {
            var group = app.MapGroup("/cms/v3/...");
            
            // Standard CRUD endpoints
            group.MapGet("/", (XxxRepository repo, ...) => { });
            group.MapPost("/", (XxxRepository repo, ...) => { });
            group.MapGet("/{id}", (XxxRepository repo, string id) => { });
            group.MapPatch("/{id}", (XxxRepository repo, string id, ...) => { });
            group.MapDelete("/{id}", (XxxRepository repo, string id) => { });
            
            // Batch endpoints if applicable
            group.MapPost("/batch/create", ...);
            group.MapPost("/batch/read", ...);
            group.MapPost("/batch/update", ...);
            group.MapPost("/batch/archive", ...);
        }
    }
}
```

### Test Pattern
Each test file follows this structure (based on existing CmsTagsTests):

```csharp
using DamianH.HubSpot.MockServer;
using Xunit;
using Xunit.Abstractions;

namespace DamianH.HubSpot.Tests.MockServer;

public class CmsXxxTests(ITestOutputHelper output) : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCMSXxxV3Client _client = null!;

    public async Task InitializeAsync()
    {
        _server = await HubSpotMockServer.StartAsync();
        _client = new HubSpotCMSXxxV3Client(_server.BaseUri.ToString());
    }

    public async Task DisposeAsync()
    {
        await _server.DisposeAsync();
    }

    [Fact] public async Task CanCreate() { }
    [Fact] public async Task CanGet() { }
    [Fact] public async Task CanUpdate() { }
    [Fact] public async Task CanDelete() { }
    [Fact] public async Task CanList() { }
    [Fact] public async Task CanBatchCreate() { }
    // etc.
}
```

## Registration Steps

For each API implemented, update `HubSpotMockServer.cs`:

1. **Add repository to services** (if new):
```csharp
.AddSingleton<XxxRepository>()
```

2. **Register API routes**:
```csharp
// Register CMS APIs
ApiRoutes.CmsBlogPosts.RegisterCmsBlogPostsV3Api(app);
ApiRoutes.CmsBlogAuthors.RegisterCmsBlogAuthorsV3Api(app);
ApiRoutes.CmsPages.RegisterCmsPagesV3Api(app);
// etc.
```

## Execution Order

### Sprint 1: Core Blog Features (Most Used)
1. Blog Posts API + Tests (1.1)
2. Blog Authors API + Tests (1.2)
3. Run all tests to verify

### Sprint 2: Pages and Configuration
1. Pages API + Tests (2.1)
2. Domains API + Tests (2.2)
3. URL Redirects API + Tests (2.3)
4. Run all tests to verify

### Sprint 3: Advanced Features
1. HubDB API + Tests (3.1)
2. Source Code API + Tests (3.2)
3. Run all tests to verify

### Sprint 4: Search and Audit (If Time)
1. Site Search API + Tests (4.1)
2. Content Audit API + Tests (4.2)
3. Run all tests to verify

### Sprint 5: Media Bridge (Optional)
1. Media Bridge API + Tests (5.1)

## Success Criteria

✅ All 12 CMS client types have corresponding mock API implementations
✅ Each API has comprehensive test coverage
✅ All tests pass
✅ Mock server can handle standard CRUD operations for all CMS objects
✅ Batch operations work correctly
✅ Multi-language/revision support for blog posts
✅ HubDB table and row operations work
✅ Source code file management works

## Notes

- Repositories are already created for most APIs, reducing implementation effort
- Focus on implementing routes and tests
- Follow existing patterns from CmsTags and CmsBlogSettings
- Blog Posts has the most complex requirements (revisions, multi-language)
- HubDB has two-level hierarchy (tables -> rows) requiring nested routes
- Source Code has environment parameter and path-based routing

## Estimated Effort

- Phase 1: ~2-3 hours (Blog Posts is most complex)
- Phase 2: ~2 hours
- Phase 3: ~2-3 hours (HubDB has complex structure)
- Phase 4: ~1 hour
- Phase 5: ~1 hour (optional)

**Total: ~8-10 hours for complete CMS API coverage**
