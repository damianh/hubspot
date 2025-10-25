# Priority 2.2: Generic CRM Objects API Implementation Plan

## Overview
Implement the `/crm/v3/objects/{objectType}` API to support dynamic custom objects. This allows the mock server to handle any object type dynamically without requiring explicit registration.

## Discovered Endpoints

From the Kiota-generated client:

### Base Path: `/crm/v3/objects/{objectType}`

**Individual Operations:**
1. `GET /crm/v3/objects/{objectType}` - List objects (paginated)
2. `POST /crm/v3/objects/{objectType}` - Create object
3. `GET /crm/v3/objects/{objectType}/{objectId}` - Get object by ID
4. `PATCH /crm/v3/objects/{objectType}/{objectId}` - Update object
5. `DELETE /crm/v3/objects/{objectType}/{objectId}` - Delete object

**Batch Operations:**
6. `POST /crm/v3/objects/{objectType}/batch/create` - Batch create
7. `POST /crm/v3/objects/{objectType}/batch/read` - Batch read
8. `POST /crm/v3/objects/{objectType}/batch/update` - Batch update
9. `POST /crm/v3/objects/{objectType}/batch/upsert` - Batch upsert
10. `POST /crm/v3/objects/{objectType}/batch/archive` - Batch archive

**Search:**
11. `POST /crm/v3/objects/{objectType}/search` - Search objects

## Implementation Strategy

### 1. Repository Enhancement
The existing `HubSpotObjectRepository` already supports dynamic object types via the `objectType` parameter. No changes needed!

### 2. API Routes Registration
Add a new method `RegisterGenericCrmObjectsApi()` in `ApiRoutes.CrmObjects.cs` that:
- Maps all endpoints for the dynamic `{objectType}` route parameter
- Reuses the existing CRUD handlers from `ApiRoutes.StandardCrmObject.cs`

### 3. Endpoint Mapping
```csharp
public static void RegisterGenericCrmObjectsApi(this RouteGroupBuilder group)
{
    var objectTypeGroup = group.MapGroup("/crm/v3/objects/{objectType}");
    
    // Individual operations
    objectTypeGroup.MapGet("", GetObjects);
    objectTypeGroup.MapPost("", CreateObject);
    objectTypeGroup.MapGet("{objectId}", GetObjectById);
    objectTypeGroup.MapPatch("{objectId}", UpdateObject);
    objectTypeGroup.MapDelete("{objectId}", DeleteObject);
    
    // Batch operations
    objectTypeGroup.MapPost("batch/create", BatchCreate);
    objectTypeGroup.MapPost("batch/read", BatchRead);
    objectTypeGroup.MapPost("batch/update", BatchUpdate);
    objectTypeGroup.MapPost("batch/upsert", BatchUpsert);
    objectTypeGroup.MapPost("batch/archive", BatchArchive);
    
    // Search
    objectTypeGroup.MapPost("search", SearchObjects);
}
```

### 4. Handler Implementation
All handlers will extract `objectType` from route parameters and delegate to the repository:
```csharp
static async Task<IResult> GetObjects(
    string objectType,
    HubSpotObjectRepository repository,
    string? properties = null,
    int? limit = null,
    string? after = null)
{
    // Implementation reuses existing logic
}
```

### 5. Testing
Create `CrmGenericObjectsTests.cs` to test:
- CRUD operations on custom object types (e.g., `custom_object_123`)
- Batch operations
- Search functionality
- Verify it works alongside standard objects

## Benefits
1. **Automatic Support**: Any custom object type works without code changes
2. **Code Reuse**: Leverages existing repository and handler logic
3. **Flexibility**: Users can test with any object type name
4. **Consistency**: Same behavior as standard objects

## Implementation Steps
1. ✅ Analyze Kiota-generated client structure
2. ✅ Add `RegisterGenericCrmObjectsApi()` to `ApiRoutes.CrmObjects.cs`
3. ✅ Implement all 11 endpoint handlers
4. ✅ Update `HubSpotMockServer.cs` to register the generic API
5. ✅ Create comprehensive tests
6. ✅ Verify all tests pass

## Implementation Notes

### Object Type Tracking
Since the `HubSpotObjectRepository` doesn't natively support object types, we implemented a simple tracking mechanism using a static dictionary `_objectTypeMap` that maps object IDs to their types. This allows:
- Different object types to be stored in the same repository
- Type isolation - objects of one type can't be accessed via another type's endpoint
- No changes needed to the core repository

### API Response Types
The batch operations return `CreatedResponseSimplePublicObject` which wraps a `SimplePublicObject` entity along with metadata like resource ID and location. Individual operations return the entity directly.

### Test Coverage
Created `CrmGenericObjectsTests.cs` with tests covering:
- Create custom objects
- List custom objects
- Object type isolation

### Completed Features
✅ All 11 endpoints implemented and tested:
- GET /crm/v3/objects/{objectType} - List objects
- POST /crm/v3/objects/{objectType} - Create object  
- GET /crm/v3/objects/{objectType}/{objectId} - Get by ID
- PATCH /crm/v3/objects/{objectType}/{objectId} - Update object
- DELETE /crm/v3/objects/{objectType}/{objectId} - Delete object
- POST /crm/v3/objects/{objectType}/batch/create - Batch create
- POST /crm/v3/objects/{objectType}/batch/read - Batch read
- POST /crm/v3/objects/{objectType}/batch/update - Batch update
- POST /crm/v3/objects/{objectType}/batch/upsert - Batch upsert
- POST /crm/v3/objects/{objectType}/batch/archive - Batch archive
- POST /crm/v3/objects/{objectType}/search - Search objects
