# HubSpot Mock Server - Remaining APIs Implementation Plan

## Current Status
✅ Build: Passing  
✅ Core CRM Objects: Implemented  
✅ Associations: Implemented  
✅ Properties & Pipelines: Implemented  
✅ Marketing APIs: Implemented  
✅ Conversations: Implemented  
✅ CRM Extensions: Implemented  
✅ Automation: Implemented  
✅ Account/Settings: Implemented (Multicurrency, UserProvisioning, TaxRates)  
✅ Business Units: Implemented  
✅ Scheduler: Implemented  

## Missing API Implementations

### Phase 1: CMS APIs (12 API Groups) - HIGH PRIORITY
These are major content management APIs that are likely used frequently.

#### 1.1 CMS Blog APIs ✅ (PARTIAL - only Authors and Posts done)
- [x] `CMS.Authors` - Blog authors management
- [x] `CMS.Posts` - Blog posts management  
- [ ] `CMS.BlogSettings` - Blog configuration
- [ ] `CMS.Tags` - Blog tags management

**Estimated Effort**: 1 day
**Files to Create**:
- `ApiRoutes.CmsBlogSettings.cs`
- `ApiRoutes.CmsTags.cs`
- `Repositories/BlogSettingsRepository.cs`
- `Repositories/TagRepository.cs`
- Test files

#### 1.2 CMS Content & Pages (3 API Groups)
- [ ] `CMS.Pages` - Landing pages, website pages
- [ ] `CMS.SiteSearch` - Site search configuration
- [ ] `CMS.CmsContentAudit` - Content audit trails

**Estimated Effort**: 2 days
**Files to Create**:
- `ApiRoutes.CmsPages.cs`
- `ApiRoutes.CmsSiteSearch.cs`
- `ApiRoutes.CmsContentAudit.cs`
- Repository files
- Test files

#### 1.3 CMS Infrastructure (4 API Groups)
- [ ] `CMS.Domains` - Domain management
- [ ] `CMS.UrlRedirects` - URL redirect management
- [ ] `CMS.SourceCode` - Template/module source code
- [ ] `CMS.MediaBridge` - Media asset management

**Estimated Effort**: 2 days
**Files to Create**:
- `ApiRoutes.CmsDomains.cs`
- `ApiRoutes.CmsUrlRedirects.cs`
- `ApiRoutes.CmsSourceCode.cs`
- `ApiRoutes.CmsMediaBridge.cs`
- Repository files
- Test files

#### 1.4 CMS Database (1 API Group)
- [ ] `CMS.Hubdb` - HubDB tables and rows

**Estimated Effort**: 1 day
**Files to Create**:
- `ApiRoutes.CmsHubdb.cs`
- `Repositories/HubDbRepository.cs`
- Test files

**Phase 1 Total**: ~6 days for all CMS APIs

---

### Phase 2: Authentication & OAuth (1 API Group) - MEDIUM PRIORITY
Auth is important but the mock server may not need full OAuth flows.

- [ ] `Auth.Oauth` - OAuth token management

**Estimated Effort**: 1 day
**Files to Create**:
- `ApiRoutes.Auth.cs`
- `Repositories/OAuthRepository.cs`
- Test files

**Decision Needed**: Do we need full OAuth simulation or just token validation?

---

### Phase 3: Missing Settings APIs (if any) - LOW PRIORITY
Currently implemented:
- ✅ Multicurrency
- ✅ TaxRates
- ✅ UserProvisioning

Need to verify if there are other Settings APIs in the generated code.

**Estimated Effort**: TBD after investigation

---

## Implementation Priority Recommendation

### Option A: Complete Feature Coverage (Recommended)
Implement all CMS APIs to provide comprehensive mock server coverage.

**Order**:
1. **Week 1**: CMS Blog completion (Tags, BlogSettings) - 1 day
2. **Week 1-2**: CMS Content & Pages - 2 days
3. **Week 2-3**: CMS Infrastructure - 2 days
4. **Week 3**: CMS HubDB - 1 day
5. **Week 3-4**: Auth/OAuth - 1 day

**Total**: ~7 days of focused work

### Option B: Prioritize by Usage
Implement only the most commonly used CMS APIs first.

**Order**:
1. CMS Pages (most common)
2. CMS Domains & UrlRedirects
3. CMS HubDB
4. CMS Tags & BlogSettings
5. Others as needed

---

## Implementation Strategy

### For Each API Group:

1. **Analyze Generated Client**
   - Review `src/HubSpot.KiotaClient/Generated/{API}/` structure
   - Identify endpoints and models
   - Check for v3 and v202509 versions

2. **Create Repository**
   - Add `{Name}Repository.cs` in `Repositories/`
   - Implement in-memory storage
   - Handle CRUD operations

3. **Create API Routes**
   - Add `ApiRoutes.Cms{Name}.cs` or `ApiRoutes.{Name}.cs`
   - Map all endpoints from generated client
   - Use MapGroup pattern

4. **Register in HubSpotMockServer**
   - Add repository to DI
   - Call registration method

5. **Add Tests**
   - Create test file in `test/HubSpot.Tests/MockServer/`
   - Test basic CRUD operations
   - Test with both v3 and v202509 clients where applicable

6. **Validate**
   - Build solution
   - Run all tests
   - Ensure no regressions

---

## Testing Strategy

### Test Coverage Goals
- [ ] Each CMS API has basic CRUD tests
- [ ] Version compatibility (v3 vs v202509) tested
- [ ] Pagination tested where applicable
- [ ] Error scenarios tested (404, 400, etc.)

### Test Organization
```
test/HubSpot.Tests/MockServer/
├── CmsPagesTests.cs
├── CmsDomainsTests.cs
├── CmsHubDbTests.cs
├── CmsTagsTests.cs
├── CmsBlogSettingsTests.cs
├── CmsUrlRedirectsTests.cs
├── CmsSourceCodeTests.cs
├── CmsMediaBridgeTests.cs
├── CmsSiteSearchTests.cs
├── CmsContentAuditTests.cs
└── AuthOAuthTests.cs
```

---

## Repository Pattern

All new repositories should follow the established pattern:

```csharp
public class {Name}Repository
{
    private readonly ConcurrentDictionary<string, {Type}> _items = new();
    private int _nextId = 1;
    
    public {Type} Create({Type} item) { }
    public {Type}? Get(string id) { }
    public IEnumerable<{Type}> List() { }
    public {Type}? Update(string id, {Type} item) { }
    public bool Delete(string id) { }
}
```

---

## Questions to Address

1. **OAuth Scope**: Do we need full OAuth flows or just mock tokens?
2. **CMS Complexity**: Are there complex CMS features we can simplify?
3. **HubDB**: Does HubDB need query support or just basic CRUD?
4. **Media**: For MediaBridge, do we need actual file storage or just metadata?

---

## Success Criteria

✅ All generated clients have corresponding mock implementations  
✅ All tests passing  
✅ No compilation errors or warnings  
✅ Documentation updated  
✅ Test coverage >80% for new code  

---

## Next Steps

**Immediate Action**: Start with **CMS Blog completion** (Tags & BlogSettings)
- Low complexity
- Quick win
- Completes existing CMS blog functionality

**Command to Start**:
```bash
# Create the first set of files for CMS Tags
```

Would you like to proceed with implementing the CMS APIs?
