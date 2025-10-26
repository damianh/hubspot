# CMS APIs Implementation Plan

## Summary
Implemented CMS API foundation with repositories and initial route files. Need to complete remaining route files, registration, and tests.

## Completed ✅
- 9 repository classes created
- 2 API route files created (Blog Posts, Blog Authors)
- Foundation for all CMS operations established

## Quick Implementation Plan

### Remaining API Routes (8 files) - ~2 hours
Each follows same pattern as BlogAuthors (simpler) or BlogPosts (complex with revisions):

1. **ApiRoutes.CmsBlogTags.cs** - Same as Authors
2. **ApiRoutes.CmsPages.cs** - Same as BlogPosts (with revisions)
3. **ApiRoutes.CmsBlogSettings.cs** - Simpler CRUD
4. **ApiRoutes.CmsDomains.cs** - Simple CRUD
5. **ApiRoutes.CmsUrlRedirects.cs** - Simple CRUD
6. **ApiRoutes.CmsHubDb.cs** - Table + Row operations
7. **ApiRoutes.CmsSourceCode.cs** - File operations
8. **ApiRoutes.CmsSiteSearch.cs** - Search operations
9. **ApiRoutes.CmsContentAudit.cs** - Read-only audit logs

### Registration in HubSpotMockServer.cs - ~30 min
Add to DI:
- BlogPostRepository
- BlogAuthorRepository
- BlogTagRepository
- PageRepository
- DomainRepository
- UrlRedirectRepository
- HubDbRepository
- SourceCodeRepository
- ContentAuditRepository

Register routes:
- RegisterCmsBlogPostsApi
- RegisterCmsBlogAuthorsApi
- RegisterCmsBlogTagsApi
- RegisterCmsPagesApi
- etc.

### Testing - ~3 hours
6 test files covering all functionality:
1. CmsBlogPostsTests.cs
2. CmsBlogAuthorsTests.cs
3. CmsBlogTagsTests.cs  
4. CmsPagesTests.cs
5. CmsConfigurationTests.cs
6. CmsAdvancedTests.cs

## Total Effort: ~5-6 hours

## Pattern for Remaining Files

Simple CRUD (Tags, Domains, Redirects, Settings):
- GET / - List with pagination
- GET /{id} - Get single
- POST / - Create
- PATCH /{id} - Update
- DELETE /{id} - Delete
- Batch operations

Complex (Pages):
- All CRUD
- Revisions endpoints
- Multi-language support

Specialized (HubDB, SourceCode, Search):
- Custom endpoints per API requirements

## Ready to proceed with efficient batch creation of remaining files.
