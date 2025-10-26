# HubSpot Mock Server

A comprehensive in-memory mock server that implements HubSpot APIs for testing without external dependencies.

## Architecture

The mock server uses a **single WebApplication** with **MapGroup** routing organized into partial classes. This approach provides:

- **Simplicity**: Single process, single endpoint
- **Performance**: Lower resource overhead for tests
- **Maintainability**: API routes organized in partial classes by domain
- **Realistic**: Matches HubSpot's single endpoint model
- **Stateful**: In-memory repositories maintain state during test execution

API route groups are registered via partial `ApiRoutes` classes (e.g., `ApiRoutes.CrmObjects.cs`, `ApiRoutes.Marketing.cs`) and share common repositories for data storage.

## Usage

```csharp
// Start the mock server
await using var server = await HubSpotMockServer.StartNew();

// Create a client pointing to the mock server
var client = new HubSpotClient("any-token", server.BaseUri);

// Use the client as normal
var contact = await client.Crm.V3.Objects.Contacts.PostAsync(new() 
{
    Properties = new Dictionary<string, string> { ["email"] = "test@example.com" }
});
```

## Implemented APIs

### ✅ CRM APIs (Fully Implemented)

**Standard Objects** - Full CRUD, Batch, Search, Associations:
- Companies, Contacts, Deals, Line Items, Products, Tickets, Quotes, Goals
- Calls, Emails, Meetings, Notes, Tasks, Postal Mail, Communications

**Custom Objects**: Complete lifecycle including schema management

**Supporting APIs**:
- Associations (V3 & V4)
- Properties (definitions, groups)
- Pipelines (deals, tickets)
- Owners
- Imports & Exports

### ✅ Marketing APIs

- Transactional Email (single send)
- Marketing Events
- Marketing Emails
- Forms
- Campaigns

### ✅ Conversations APIs

- Visitor Identification
- Custom Channels

### ✅ Automation APIs

- Workflows

### ✅ CMS APIs

- Blog Posts, Authors, Settings
- Pages (landing, site)
- Domains
- URL Redirects
- HubDB Tables
- Source Code
- Site Search
- Tags
- Content Audit
- Media Bridge

### ✅ Business Management

- Webhooks (subscriptions, settings)
- Events (custom analytics events)
- Files (upload, management)
- Business Units
- Schemas (custom objects)
- Timeline (custom events)

### ✅ Settings & Configuration

- Account Info
- User Provisioning
- Multicurrency
- Tax Rates

### ✅ Extensions

- Calling Integration
- CRM Cards
- Video Conferencing
- Transcription

## Adding New APIs

### 1. Create a Partial Class

Create a new file `ApiRoutes.{Domain}.cs`:

```csharp
namespace DamianH.HubSpot.MockServer;

public partial class ApiRoutes
{
    public static void RegisterMyNewApi(WebApplication app)
    {
        var group = app.MapGroup("/my-api/v1");

        group.MapGet("/items", (MyRepository repo) => 
        {
            var items = repo.GetAll();
            return Results.Ok(new { results = items });
        });

        group.MapPost("/items", (MyRepository repo, MyItemInput input) => 
        {
            var created = repo.Create(input);
            return Results.Created($"/my-api/v1/items/{created.Id}", created);
        });
    }
}
```

### 2. Register in Main Server

In `HubSpotMockServer.cs`, add to the constructor:

```csharp
// Register API route groups
ApiRoutes.RegisterMyNewApi(app);
```

### 3. Add Repository (if needed)

Create a thread-safe repository:

```csharp
public class MyRepository
{
    private readonly ConcurrentDictionary<string, MyItem> _items = new();
    
    public MyItem Create(MyItemInput input)
    {
        var item = new MyItem { Id = Guid.NewGuid().ToString(), ... };
        _items.TryAdd(item.Id, item);
        return item;
    }
    
    public IEnumerable<MyItem> GetAll() => _items.Values;
}
```

### 4. Register Repository

In `HubSpotMockServer.cs` services:

```csharp
builder.Services.AddSingleton<MyRepository>();
```

## Design Patterns

### Repository Pattern
- All data stored in concurrent dictionaries for thread-safety
- Repositories injected via DI into route handlers
- Repositories simulate HubSpot's data relationships (e.g., associations)

### API Models
- Located in `Apis/Models/`
- Match HubSpot's API contracts
- Shared across multiple API versions where applicable

### Error Handling
- Returns proper HTTP status codes (400, 404, 409, etc.)
- Validates required fields and relationships
- Provides meaningful error messages

### Versioning
- Supports multiple API versions (V3, V4, V202509, etc.)
- Routes organized by version where needed
- Shared business logic across versions

## Testing

The mock server is extensively tested in `HubSpot.Tests`:
- Unit tests for repositories
- Integration tests for API endpoints
- Validation tests for generated Kiota clients
- Multi-version compatibility tests

Run tests:
```bash
dotnet test ../../../test/HubSpot.Tests/HubSpot.Tests.csproj
```

## Performance Considerations

- Uses in-memory storage (fast, ephemeral)
- Thread-safe concurrent collections
- Minimal middleware overhead
- Runs on random port to avoid conflicts
- Quick startup/shutdown for test isolation

## Limitations

- **Not persistent**: Data lost when server stops
- **No authentication**: Accepts any token
- **Simplified validation**: Basic validation only
- **No rate limiting**: Unlimited requests
- **No webhooks delivery**: Stores subscriptions but doesn't send events

These limitations are intentional for testing purposes.
