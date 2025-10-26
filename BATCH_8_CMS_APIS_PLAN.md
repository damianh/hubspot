# Batch 8: CMS APIs Implementation Plan

## Overview
Implement all CMS (Content Management System) APIs for the HubSpot Mock Server. CMS APIs manage blog posts, pages, domains, redirects, and other content-related functionality.

## CMS APIs to Implement (12 APIs)

### Priority 1: Blog & Content APIs (High Real-World Usage)
These are the most commonly used CMS features:

1. **Blog Posts** (`HubSpotCMSPostsV3Client`)
   - Create, read, update, delete blog posts
   - Multi-language support
   - Revision management
   - Scheduling and publishing
   - Client: `Generated/CMS/Posts/V3/HubSpotCMSPostsV3Client.cs`

2. **Blog Authors** (`HubSpotCMSAuthorsV3Client`)
   - Manage blog authors
   - Multi-language support
   - Batch operations
   - Client: `Generated/CMS/Authors/V3/HubSpotCMSAuthorsV3Client.cs`

3. **Blog Tags** (`HubSpotCMSTagsV3Client`)
   - Create and manage blog tags
   - Multi-language support
   - Batch operations
   - Client: `Generated/CMS/Tags/V3/HubSpotCMSTagsV3Client.cs`

4. **Pages** (`HubSpotCMSPagesV3Client`)
   - Landing pages, site pages
   - Multi-language support
   - Revision management
   - Client: `Generated/CMS/Pages/V3/HubSpotCMSPagesV3Client.cs`

### Priority 2: Configuration & Settings (Medium Usage)

5. **Blog Settings** (`HubSpotCMSBlogSettingsV3Client`)
   - Configure blog settings
   - Multi-language support
   - Client: `Generated/CMS/BlogSettings/V3/HubSpotCMSBlogSettingsV3Client.cs`

6. **Domains** (`HubSpotCMSDomainsV3Client`)
   - Manage website domains
   - SSL certificates
   - Client: `Generated/CMS/Domains/V3/HubSpotCMSDomainsV3Client.cs`

7. **URL Redirects** (`HubSpotCMSUrlRedirectsV3Client`)
   - Create and manage URL redirects
   - 301/302 redirects
   - Client: `Generated/CMS/UrlRedirects/V3/HubSpotCMSUrlRedirectsV3Client.cs`

### Priority 3: Advanced Features (Lower Usage)

8. **HubDB** (`HubSpotCMSHubdbV3Client`)
   - Database tables for CMS
   - Row and table management
   - Client: `Generated/CMS/Hubdb/V3/HubSpotCMSHubdbV3Client.cs`

9. **Source Code** (`HubSpotCMSSourceCodeV3Client`)
   - Manage templates, modules, CSS, JS
   - Extract and validate files
   - Client: `Generated/CMS/SourceCode/V3/HubSpotCMSSourceCodeV3Client.cs`

10. **Site Search** (`HubSpotCMSSiteSearchV3Client`)
    - Index and search site content
    - Client: `Generated/CMS/SiteSearch/V3/HubSpotCMSSiteSearchV3Client.cs`

11. **Media Bridge** (`HubSpotCMSMediaBridgeV1Client`)
    - External media integration
    - Client: `Generated/CMS/MediaBridge/V1/HubSpotCMSMediaBridgeV1Client.cs`

12. **Content Audit** (`HubSpotCMSCmsContentAuditV3Client`)
    - Audit logs for content changes
    - Client: `Generated/CMS/CmsContentAudit/V3/HubSpotCMSCmsContentAuditV3Client.cs`

## Implementation Strategy

### Phase 1: Repository Layer
Create specialized repositories for CMS objects:

1. **BlogPostRepository.cs**
   - Store blog posts with metadata
   - Handle revisions and versions
   - Support multi-language variants
   - Scheduling/publishing states

2. **BlogAuthorRepository.cs**
   - Manage author profiles
   - Author metadata

3. **BlogTagRepository.cs**
   - Tag management
   - Tag associations

4. **PageRepository.cs**
   - Landing/site pages
   - Similar structure to blog posts

5. **DomainRepository.cs**
   - Domain configurations
   - SSL settings

6. **UrlRedirectRepository.cs**
   - Redirect rules
   - Source/destination mapping

7. **HubDbRepository.cs**
   - Table definitions
   - Row data storage

8. **SourceCodeRepository.cs**
   - File storage (templates, CSS, JS)
   - File metadata

9. **ContentAuditRepository.cs**
   - Audit log entries
   - Change tracking

### Phase 2: API Models
Create API models in `src/HubSpot.MockServer/Apis/Models/`:

1. **CmsBlogPost.cs** - Blog post data structure
2. **CmsBlogAuthor.cs** - Author data structure
3. **CmsBlogTag.cs** - Tag data structure
4. **CmsPage.cs** - Page data structure
5. **CmsDomain.cs** - Domain configuration
6. **CmsUrlRedirect.cs** - Redirect rule
7. **CmsHubDbTable.cs** - HubDB table schema
8. **CmsHubDbRow.cs** - HubDB row data
9. **CmsSourceCodeFile.cs** - Source file metadata
10. **CmsContentAuditEntry.cs** - Audit log entry

### Phase 3: API Routes
Create route handlers in partial classes:

1. **ApiRoutes.CmsBlogPosts.cs**
   - CRUD operations
   - Batch operations
   - Multi-language operations
   - Revision management
   - Schedule/publish endpoints

2. **ApiRoutes.CmsBlogAuthors.cs**
   - CRUD and batch operations
   - Multi-language support

3. **ApiRoutes.CmsBlogTags.cs**
   - CRUD and batch operations
   - Multi-language support

4. **ApiRoutes.CmsPages.cs**
   - CRUD operations
   - Batch operations
   - Multi-language operations
   - Revision management

5. **ApiRoutes.CmsBlogSettings.cs**
   - Settings management
   - Multi-language settings

6. **ApiRoutes.CmsDomains.cs**
   - Domain CRUD operations

7. **ApiRoutes.CmsUrlRedirects.cs**
   - Redirect CRUD operations

8. **ApiRoutes.CmsHubDb.cs**
   - Table and row management
   - Publish/draft operations

9. **ApiRoutes.CmsSourceCode.cs**
   - File management
   - Extract/validate operations

10. **ApiRoutes.CmsSiteSearch.cs**
    - Index and search operations

11. **ApiRoutes.CmsMediaBridge.cs**
    - Media integration endpoints

12. **ApiRoutes.CmsContentAudit.cs**
    - Audit log retrieval

### Phase 4: Registration
Update `HubSpotMockServer.cs`:
- Register repositories in DI
- Register API routes with MapGroup

### Phase 5: Testing
Create comprehensive tests:

1. **CmsBlogPostsTests.cs**
   - Test all CRUD operations
   - Test batch operations
   - Test multi-language features
   - Test revisions
   - Test scheduling

2. **CmsBlogAuthorsTests.cs**
   - Test author management

3. **CmsBlogTagsTests.cs**
   - Test tag management

4. **CmsPagesTests.cs**
   - Test page management
   - Similar to blog posts

5. **CmsConfigurationTests.cs**
   - Test domains, redirects, settings

6. **CmsAdvancedTests.cs**
   - Test HubDB
   - Test source code
   - Test site search
   - Test content audit

## Implementation Order

### Step 1: Foundation (Blog Posts - Most Important)
1. Create `BlogPostRepository.cs`
2. Create blog post models
3. Create `ApiRoutes.CmsBlogPosts.cs`
4. Create `CmsBlogPostsTests.cs`
5. Verify client integration

### Step 2: Blog Supporting Features
1. Blog Authors (repository, models, routes, tests)
2. Blog Tags (repository, models, routes, tests)
3. Blog Settings (repository, models, routes, tests)

### Step 3: Pages
1. Create `PageRepository.cs`
2. Create page models
3. Create `ApiRoutes.CmsPages.cs`
4. Create `CmsPagesTests.cs`

### Step 4: Configuration
1. Domains (repository, models, routes, tests)
2. URL Redirects (repository, models, routes, tests)

### Step 5: Advanced Features
1. HubDB (repository, models, routes, tests)
2. Source Code (repository, models, routes, tests)
3. Site Search (repository, models, routes, tests)
4. Media Bridge (repository, models, routes, tests)
5. Content Audit (repository, models, routes, tests)

## Key Design Patterns

### 1. Multi-Language Support
Many CMS objects support multiple languages:
```csharp
public class CmsMultiLanguageVariant
{
    public string Id { get; set; }
    public string Language { get; set; }
    public string MasterContentId { get; set; }
    public Dictionary<string, object> Content { get; set; }
}
```

### 2. Revision Management
Blog posts and pages have revision tracking:
```csharp
public class CmsRevision
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Dictionary<string, object> Content { get; set; }
}
```

### 3. Publishing States
Content can be in different states:
```csharp
public enum CmsPublishState
{
    Draft,
    Scheduled,
    Published,
    Archived
}
```

### 4. HubDB Table/Row Structure
```csharp
public class HubDbTable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<HubDbColumn> Columns { get; set; }
    public bool Published { get; set; }
}

public class HubDbRow
{
    public string Id { get; set; }
    public Dictionary<string, object> Values { get; set; }
}
```

## Testing Strategy

### Test Coverage Requirements
1. **CRUD Operations** - All standard operations
2. **Batch Operations** - Where supported
3. **Multi-Language** - Language variants
4. **Revisions** - Version management
5. **Publishing** - State transitions
6. **Search/Filter** - Query operations
7. **Error Cases** - Validation, not found, etc.

### Integration Tests
- Test against actual Kiota clients
- Verify request/response formats
- Test pagination
- Test error responses

## Expected Outcomes

After implementation:
1. ✅ All 12 CMS API clients functional
2. ✅ Full CRUD support for blog posts, authors, tags
3. ✅ Multi-language support working
4. ✅ Revision management working
5. ✅ Page management functional
6. ✅ Domain and redirect management working
7. ✅ HubDB tables and rows operational
8. ✅ Source code management functional
9. ✅ Comprehensive test coverage
10. ✅ Mock server supports full CMS workflow

## Estimated Effort

- **Priority 1** (Blog & Content): ~4-6 hours
- **Priority 2** (Configuration): ~2-3 hours
- **Priority 3** (Advanced): ~3-4 hours
- **Total**: ~9-13 hours

## Next Steps After CMS

After CMS APIs are complete, remaining major API categories:
1. **Analytics & Reports** - Tracking and reporting
2. **Integrations** - Third-party integrations
3. **Settings** - Account/portal settings
4. **OAuth** - Authentication flows
5. **Any remaining specialized APIs**

---
**Status**: Ready to begin implementation
**Priority**: Medium-High (CMS is commonly used in real-world scenarios)
