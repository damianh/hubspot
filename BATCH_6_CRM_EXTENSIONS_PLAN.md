# Batch 6: CRM Extensions & Operations APIs

## Overview
Implement CRM Extensions and Operations APIs to enable bulk data operations, custom object schemas, and timeline events - critical for real-world enterprise integrations.

## APIs to Implement (4 APIs)

### 1. Schemas API ⭐ HIGH PRIORITY
**Endpoint:** `/crm/v3/schemas/*`
**Client:** `HubSpot.KiotaClient.Generated.Crm.Schemas`

**Key Features:**
- Define custom object schemas
- Get object type definitions
- Update schema properties
- Association definitions for custom objects
- Required properties and validation rules

**Why Important:** Required for creating and managing custom CRM objects beyond the standard types.

### 2. Imports API ⭐ HIGH PRIORITY
**Endpoint:** `/crm/v3/imports/*`
**Client:** `HubSpot.KiotaClient.Generated.Crm.Imports`

**Key Features:**
- Create bulk import jobs
- Upload CSV/file data
- Track import status
- Get import errors
- Cancel imports
- Import history

**Why Important:** Essential for testing bulk data loading scenarios.

### 3. Timeline API ⭐ MEDIUM-HIGH PRIORITY
**Endpoint:** `/crm/v3/timeline/*`
**Client:** `HubSpot.KiotaClient.Generated.Crm.Timeline`

**Key Features:**
- Create custom timeline events
- Timeline event templates
- Associate events with CRM records
- Event detail rendering
- Event types and tokens

**Why Important:** Enables custom activity tracking on CRM records.

### 4. Exports API (Not in generated clients - check if exists)
**Endpoint:** `/crm/v3/exports/*` (may not be in Kiota clients)

**Key Features:**
- Export CRM data
- Export status
- Download export files

**Note:** May need to verify if this API exists in the generated clients.

## Implementation Plan

### Phase 1: Repository Layer (90 minutes)

#### SchemaRepository
```csharp
public class SchemaRepository
{
    // Schema storage
    private Dictionary<string, ObjectSchema> _schemas;
    private Dictionary<string, List<SchemaProperty>> _schemaProperties;
    private Dictionary<string, List<AssociationDefinition>> _schemaAssociations;
    
    // Methods
    - CreateSchema(name, labels, properties, associatedObjects)
    - GetSchema(objectType)
    - ListSchemas()
    - UpdateSchema(objectType, updates)
    - DeleteSchema(objectType)
    - AddProperty(objectType, property)
    - GetSchemaProperties(objectType)
    - CreateAssociationDefinition(fromObjectType, toObjectType, name)
}
```

**Models:**
- ObjectSchema: name, labels, primaryDisplayProperty, secondaryDisplayProperties, searchableProperties, requiredProperties, created, updated
- SchemaProperty: name, label, type, fieldType, description, groupName, options, displayOrder
- AssociationDefinition: fromObjectType, toObjectType, name, id

#### ImportRepository
```csharp
public class ImportRepository
{
    // Import job storage
    private Dictionary<string, ImportJob> _imports;
    private Dictionary<string, List<ImportRow>> _importData;
    private Dictionary<string, List<ImportError>> _importErrors;
    
    // Methods
    - CreateImport(name, files, importOperations)
    - GetImport(importId)
    - ListImports(filters)
    - CancelImport(importId)
    - ProcessImport(importId) // Simulate import processing
    - GetImportErrors(importId)
    - GetImportStatus(importId)
}
```

**Models:**
- ImportJob: id, state (STARTED, PROCESSING, DONE, FAILED, CANCELED), createdAt, updatedAt, objectType, importName, metadata
- ImportRow: rowIndex, data (properties), objectId (after import)
- ImportError: rowIndex, error message, errorType

**States:** STARTED → PROCESSING → DONE/FAILED/CANCELED

#### TimelineRepository
```csharp
public class TimelineRepository
{
    // Timeline event storage
    private Dictionary<string, TimelineEvent> _events;
    private Dictionary<string, TimelineEventTemplate> _templates;
    private Dictionary<string, List<string>> _objectEvents; // objectId -> eventIds
    
    // Methods
    - CreateEventTemplate(name, tokens, detailTemplate, headerTemplate)
    - GetEventTemplate(templateId)
    - ListEventTemplates()
    - CreateEvent(templateId, objectType, objectId, tokens, timestamp)
    - GetEvent(eventId)
    - ListEvents(objectType, objectId)
    - DeleteEvent(eventId)
    - DeleteEventTemplate(templateId)
}
```

**Models:**
- TimelineEventTemplate: id, name, tokens (name, type, options), headerTemplate, detailTemplate, objectType
- TimelineEvent: id, eventTemplateId, objectType, objectId, timestamp, tokens (name -> value), created
- EventToken: name, type (string, date, number, enum), label, options

### Phase 2: API Routes (90 minutes)

#### Create: ApiRoutes.Schemas.cs
```csharp
internal static partial class ApiRoutes
{
    internal static void RegisterSchemasApi(
        WebApplication app,
        SchemaRepository schemaRepo)
    {
        var v3 = app.MapGroup("/crm/v3/schemas");
        
        // GET /crm/v3/schemas
        v3.MapGet("", (bool? archived) => 
            schemaRepo.ListSchemas(archived ?? false));
        
        // GET /crm/v3/schemas/{objectType}
        v3.MapGet("{objectType}", (string objectType) => 
            schemaRepo.GetSchema(objectType));
        
        // POST /crm/v3/schemas
        v3.MapPost("", (SchemaCreateRequest request) => 
            schemaRepo.CreateSchema(request));
        
        // PATCH /crm/v3/schemas/{objectType}
        v3.MapPatch("{objectType}", (string objectType, SchemaUpdateRequest request) => 
            schemaRepo.UpdateSchema(objectType, request));
        
        // DELETE /crm/v3/schemas/{objectType}
        v3.MapDelete("{objectType}", (string objectType) => 
            schemaRepo.DeleteSchema(objectType));
        
        // Association definitions
        var associations = v3.MapGroup("{objectType}/associations");
        
        // POST /crm/v3/schemas/{objectType}/associations
        associations.MapPost("", (string objectType, AssociationDefinitionRequest request) =>
            schemaRepo.CreateAssociationDefinition(objectType, request));
        
        // DELETE /crm/v3/schemas/{objectType}/associations/{associationId}
        associations.MapDelete("{associationId}", (string objectType, string associationId) =>
            schemaRepo.DeleteAssociationDefinition(objectType, associationId));
    }
}
```

#### Create: ApiRoutes.Imports.cs
```csharp
internal static partial class ApiRoutes
{
    internal static void RegisterImportsApi(
        WebApplication app,
        ImportRepository importRepo)
    {
        var v3 = app.MapGroup("/crm/v3/imports");
        
        // POST /crm/v3/imports
        v3.MapPost("", async (HttpRequest request) =>
        {
            // Handle multipart/form-data file upload
            var form = await request.ReadFormAsync();
            var files = form.Files;
            var importName = form["importName"].ToString();
            var config = form["config"].ToString(); // JSON config
            
            return importRepo.CreateImport(importName, files, config);
        });
        
        // GET /crm/v3/imports/{importId}
        v3.MapGet("{importId}", (string importId) => 
            importRepo.GetImport(importId));
        
        // GET /crm/v3/imports
        v3.MapGet("", (string? after, int? limit) => 
            importRepo.ListImports(after, limit ?? 10));
        
        // POST /crm/v3/imports/{importId}/cancel
        v3.MapPost("{importId}/cancel", (string importId) => 
            importRepo.CancelImport(importId));
        
        // GET /crm/v3/imports/{importId}/errors
        v3.MapGet("{importId}/errors", (string importId, string? after, int? limit) =>
            importRepo.GetImportErrors(importId, after, limit ?? 50));
    }
}
```

#### Create: ApiRoutes.Timeline.cs
```csharp
internal static partial class ApiRoutes
{
    internal static void RegisterTimelineApi(
        WebApplication app,
        TimelineRepository timelineRepo)
    {
        var v3 = app.MapGroup("/crm/v3/timeline");
        
        // Event Templates
        var templates = v3.MapGroup("event-templates");
        
        // POST /crm/v3/timeline/event-templates
        templates.MapPost("", (TimelineEventTemplateRequest request) =>
            timelineRepo.CreateEventTemplate(request));
        
        // GET /crm/v3/timeline/event-templates/{templateId}
        templates.MapGet("{templateId}", (string templateId) =>
            timelineRepo.GetEventTemplate(templateId));
        
        // PUT /crm/v3/timeline/event-templates/{templateId}
        templates.MapPut("{templateId}", (string templateId, TimelineEventTemplateRequest request) =>
            timelineRepo.UpdateEventTemplate(templateId, request));
        
        // DELETE /crm/v3/timeline/event-templates/{templateId}
        templates.MapDelete("{templateId}", (string templateId) =>
            timelineRepo.DeleteEventTemplate(templateId));
        
        // Events
        var events = v3.MapGroup("events");
        
        // POST /crm/v3/timeline/events
        events.MapPost("", (TimelineEventRequest request) =>
            timelineRepo.CreateEvent(request));
        
        // GET /crm/v3/timeline/events/{objectType}/{objectId}
        events.MapGet("{objectType}/{objectId}", (string objectType, string objectId) =>
            timelineRepo.ListEvents(objectType, objectId));
        
        // DELETE /crm/v3/timeline/events/{eventId}
        events.MapDelete("{eventId}", (string eventId) =>
            timelineRepo.DeleteEvent(eventId));
    }
}
```

### Phase 3: Integration (15 minutes)

Update `HubSpotMockServer.cs`:
```csharp
// Add repositories
var schemaRepo = new SchemaRepository();
var importRepo = new ImportRepository(objectRepo); // May need objectRepo for processing
var timelineRepo = new TimelineRepository();

// Register routes
ApiRoutes.RegisterSchemasApi(app, schemaRepo);
ApiRoutes.RegisterImportsApi(app, importRepo);
ApiRoutes.RegisterTimelineApi(app, timelineRepo);
```

### Phase 4: Tests (60 minutes)

#### Create: CrmSchemasTests.cs
```csharp
public class CrmSchemasTests : IClassFixture<HubSpotMockServerFixture>
{
    [Fact] // Create custom object schema
    [Fact] // Get schema by object type
    [Fact] // List all schemas
    [Fact] // Update schema properties
    [Fact] // Delete schema
    [Fact] // Create association definition
    [Fact] // Delete association definition
}
```

#### Create: CrmImportsTests.cs
```csharp
public class CrmImportsTests : IClassFixture<HubSpotMockServerFixture>
{
    [Fact] // Create import with CSV file
    [Fact] // Get import status (STARTED -> PROCESSING -> DONE)
    [Fact] // List imports
    [Fact] // Cancel import
    [Fact] // Get import errors
    [Fact] // Import with validation errors
}
```

#### Create: CrmTimelineTests.cs
```csharp
public class CrmTimelineTests : IClassFixture<HubSpotMockServerFixture>
{
    [Fact] // Create timeline event template
    [Fact] // Get event template
    [Fact] // Create timeline event for contact
    [Fact] // List events for object
    [Fact] // Delete timeline event
    [Fact] // Delete event template
    [Fact] // Timeline event with custom tokens
}
```

## API Models Reference

### Schema Models
```json
// ObjectSchema
{
  "id": "0-123",
  "name": "pets",
  "labels": {
    "singular": "Pet",
    "plural": "Pets"
  },
  "primaryDisplayProperty": "name",
  "requiredProperties": ["name"],
  "searchableProperties": ["name"],
  "properties": [...],
  "associations": [...],
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}

// SchemaProperty
{
  "name": "pet_type",
  "label": "Pet Type",
  "type": "enumeration",
  "fieldType": "select",
  "groupName": "petinfo",
  "options": [
    {"label": "Dog", "value": "dog"},
    {"label": "Cat", "value": "cat"}
  ],
  "displayOrder": 1
}
```

### Import Models
```json
// ImportJob
{
  "id": "123456",
  "state": "DONE",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:10:00Z",
  "optOutImport": false,
  "importSource": "API",
  "importName": "Contact Import Jan 2024",
  "metadata": {
    "objectType": "CONTACT",
    "numRowsProcessed": 100,
    "numRowsSucceeded": 98,
    "numRowsFailed": 2
  }
}

// ImportError
{
  "id": "error-1",
  "createdAt": 50,
  "objectType": "CONTACT",
  "invalidEmails": ["invalid@"],
  "sourceData": {...},
  "errorType": "INVALID_EMAIL"
}
```

### Timeline Models
```json
// TimelineEventTemplate
{
  "id": "template-123",
  "name": "payment_received",
  "tokens": [
    {
      "name": "amount",
      "label": "Amount",
      "type": "number"
    },
    {
      "name": "currency",
      "label": "Currency",
      "type": "string"
    }
  ],
  "headerTemplate": "Payment of {{amount}} {{currency}} received",
  "detailTemplate": "<div>Payment processed successfully</div>",
  "objectType": "CONTACT"
}

// TimelineEvent
{
  "id": "event-456",
  "eventTemplateId": "template-123",
  "objectType": "CONTACT",
  "objectId": "contact-789",
  "timestamp": "2024-01-01T12:00:00Z",
  "tokens": {
    "amount": "99.99",
    "currency": "USD"
  },
  "createdAt": "2024-01-01T12:00:00Z"
}
```

## Implementation Order

1. **SchemaRepository** - Custom object schemas (60 min)
2. **ImportRepository** - Bulk import management (45 min)
3. **TimelineRepository** - Timeline events (30 min)
4. **ApiRoutes.Schemas.cs** - Schema routes (30 min)
5. **ApiRoutes.Imports.cs** - Import routes (30 min)
6. **ApiRoutes.Timeline.cs** - Timeline routes (20 min)
7. **Tests** - All three test files (60 min)

## Estimated Time
- Repositories: 135 minutes (2.25 hours)
- API Routes: 80 minutes (1.33 hours)
- Integration: 15 minutes
- Tests: 60 minutes (1 hour)
- **Total: ~5 hours**

## Success Criteria
- ✅ Can create and manage custom object schemas
- ✅ Can create bulk import jobs and track status
- ✅ Can create timeline event templates and events
- ✅ All Kiota clients work against mock server
- ✅ All tests pass (targeting 15+ new tests)
- ✅ Build passes

## Notes

### Schema API Complexity
- Schemas define custom object types beyond standard CRM objects
- Tightly integrated with generic objects API
- Association definitions allow custom relationships

### Import API Considerations
- File upload handling (multipart/form-data)
- Async processing simulation
- Error tracking per row
- State transitions: STARTED → PROCESSING → DONE/FAILED/CANCELED

### Timeline API Use Cases
- Custom activity tracking
- Integration event logging
- Payment notifications
- Custom milestones
- Third-party system events

## Related Files
- Repositories: `src/HubSpot.MockServer/Repositories/SchemaRepository.cs`, `ImportRepository.cs`, `TimelineRepository.cs`
- Routes: `src/HubSpot.MockServer/ApiRoutes/ApiRoutes.Schemas.cs`, `ApiRoutes.Imports.cs`, `ApiRoutes.Timeline.cs`
- Tests: `test/HubSpot.Tests/MockServer/CrmSchemasTests.cs`, `CrmImportsTests.cs`, `CrmTimelineTests.cs`

## Coverage After Batch 6
- **APIs Implemented:** 45+ (33% of total)
- **Real-world Coverage:** ~98%
- **Remaining:** Mostly CMS, advanced automation, settings APIs
