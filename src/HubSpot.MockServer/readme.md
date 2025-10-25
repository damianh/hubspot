# HubSpot Mock Server

An easy to use mock server that implements a subset of HubSpot APIs
for use in testing with the HubSpot.Client library.

## Architecture

The mock server uses a **single WebApplication** with **MapGroup** routing to organize different API endpoints. This approach provides:

- **Simplicity**: Single process, single endpoint
- **Performance**: Lower resource overhead for tests
- **Ease of debugging**: All APIs in one application
- **Realistic**: Matches HubSpot's single endpoint model

All API route groups are registered in `HubSpotMockServer.cs` and share a common `HubSpotObjectRepository` for data storage.

## Usage

```csharp
await using var server = await HubSpotMockServer.StartNew(loggerFactory);

// Use server.Uri for all API calls
var client = new HubSpotClient(server.Uri);
```

## Implemented APIs

### CRM

#### Companies

Batch:

- [ ] Archive a batch of companies by ID `POST /crm/v3/objects/companies/batch/archive`
- [ ] Create a batch of companies `POST /crm/v3/objects/companies/batch/create`
- [ ] Read a batch of companies by ID `POST /crm/v3/objects/companies/batch/read`
- [ ] Update a batch of companies `POST /crm/v3/objects/companies/batch/update`

Basic:

- [x] List `GET /crm/v3/objects/companies`
- [x] Read `GET /crm/v3/objects/companies/{companyId}`
- [x] Create `POST /crm/v3/objects/companies`
- [x] Update `PATCH /crm/v3/objects/companies/{companyId}`
- [x] Archive `DELETE /crm/v3/objects/companies/{companyId}`

Public_Object:

- [ ] Merge `POST /crm/v3/objects/companies/merge`

Search:

- [ ] Search `POST /crm/v3/objects/companies/search`

## Adding New APIs

To add a new API endpoint group:

1. Create a new `Register{ApiName}Api(WebApplication app)` method in `HubSpotMockServer.cs`
2. Use `app.MapGroup("/your/api/path")` to create a route group
3. Add endpoints using `.MapGet()`, `.MapPost()`, etc.
4. Call the registration method from `StartNew()`

Example:

```csharp
private static void RegisterCrmContactsApi(WebApplication app)
{
    var group = app.MapGroup("/crm/v3/objects/contacts")
        .WithTags("Contacts");

    group.MapGet("", () => Results.Ok(new { results = Array.Empty<object>() }));
    // ... more endpoints
}
```
