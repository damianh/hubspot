# HubSpot CLI Tool

## TL;DR
> **Summary**: Build a `hubspot` CLI tool as a dotnet global/local tool using System.CommandLine and the existing KiotaClient. Uses the **generic Objects V3 client** (`/crm/v3/objects/{objectType}`) to implement all CRM CRUD commands with a single code path, avoiding duplicated per-object-type wiring.
> **Estimated Effort**: Large

## Context

### Original Request
Design and plan a `hubspot` CLI tool as a new project in `tool/HubSpot.Cli/` that supports all major CRM operations, multiple output formats, flexible authentication, and scriptable usage patterns.

### Key Findings

1. **Generic Objects V3 Client is the key abstraction.** The KiotaClient has `HubSpotCRMObjectsV3Client` at `CRM.Objects.V3` which exposes `/crm/v3/objects/{objectType}` — a single generic client that works for ALL CRM object types (companies, contacts, deals, etc.). This means we do NOT need to wire up 15+ typed clients. One client handles list, get, create, update, delete, and search for any object type by passing the object type name as a path parameter.

2. **Model types live in `DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models`** — includes `SimplePublicObjectInputForCreate`, `SimplePublicObjectInput`, `PublicObjectSearchRequest`, `Filter`, `FilterGroup`, `CollectionResponseSimplePublicObjectWithAssociations`, etc. These are shared across all object types when using the generic client.

3. **Separate clients exist for non-CRUD APIs:** Properties (`CRM.Properties.V3`), Pipelines (`CRM.Pipelines.V3`), Owners (`CRM.CrmOwners.V3`), Associations (`CRM.Associations.V3`), Schemas (`CRM.Schemas.V3`) — each needs its own specific wiring.

4. **Kiota adapter pattern:** Create `HttpClientRequestAdapter` with an `IAuthenticationProvider`, set `BaseUrl`, pass to client constructor. The adapter handles auth headers, serialization, HTTP execution.

5. **Project conventions:**
   - Target: `net10.0`
   - Root namespace pattern: `DamianH.HubSpot.*`
   - `src/Directory.Build.props` applies MinVer, NuGet metadata, analyzers to `src/` projects
   - The `tool/` directory does NOT have a `Directory.Build.props` — tool projects set `IsPackable=false`
   - Solution uses `.slnx` format

6. **CI/CD:** Both `ci.yml` and `release.yml` build/test/pack `hubspot.slnx`. The CLI project needs to be added to the solution and will be packed as a dotnet tool automatically.

7. **Properties are passed as `AdditionalData`** — a `Dictionary<string, object>` on the `_properties` wrapper classes. The CLI will parse `key=value` pairs from the command line.

## Architecture Decisions

### AD1: Use the Generic Objects V3 Client
Use `HubSpotCRMObjectsV3Client` for ALL CRM object CRUD commands. This lets us register one set of command handlers that work for companies, contacts, deals, tickets, etc. — just varying the `objectType` string. This is far better than wiring 15+ typed clients.

### AD2: System.CommandLine 2.0
Use `System.CommandLine` (latest 2.0.x-beta or stable). It provides:
- Declarative command/option/argument definitions
- Auto-generated help text
- Tab completion support
- Middleware pipeline for cross-cutting concerns (auth, output formatting)
- Testable `InvocationContext` pattern

### AD3: Output formatting as middleware
Global `--output` option processed by a shared formatter. Commands return a result object; the formatter serializes to JSON/table/CSV based on the flag. This keeps command handlers clean.

### AD4: Authentication as middleware
A `HubSpotAuthMiddleware` resolves the token from `--token` flag → `HUBSPOT_ACCESS_TOKEN` env var → `~/.hubspot/config.json`, then configures the `IRequestAdapter` before the command handler runs.

### AD5: Separate CLI project in `tool/HubSpot.Cli/`
The project lives alongside the existing `tool/Generate/` project. It references `src/HubSpot.KiotaClient/` directly. It does NOT reference MockServer. It gets its own `Directory.Build.props` for tool-specific settings or inherits from a root-level one.

### AD6: CRM object types as data, not code
Define a static list of known CRM object types (companies, contacts, deals, tickets, etc.) and generate subcommands from that list at startup. Adding a new object type = adding one string to the list.

## Objectives

### Core Objective
Ship a functional `hubspot` CLI tool that covers CRM CRUD operations, is packable as a dotnet tool, and is extensible for future API categories.

### Deliverables
- [x] New project `tool/HubSpot.Cli/HubSpot.Cli.csproj` packable as a dotnet tool
- [x] CRM object CRUD commands (list, get, create, update, delete, search) for all standard object types
- [x] CRM support commands (associations, properties, pipelines, owners)
- [x] Authentication (flag, env var, config file)
- [x] Output formatting (JSON, table, CSV)
- [x] Integration into solution and CI

### Definition of Done
- [x] `dotnet build tool/HubSpot.Cli/HubSpot.Cli.csproj -c Release` succeeds
- [x] `dotnet run --project tool/HubSpot.Cli -- crm companies list --help` shows correct help text
- [x] `dotnet run --project tool/HubSpot.Cli -- --help` shows all top-level commands
- [x] `dotnet pack tool/HubSpot.Cli/HubSpot.Cli.csproj` produces a tool package
- [x] All existing tests still pass (`dotnet test hubspot.slnx`)

### Guardrails (Must NOT)
- Must NOT modify any files in `src/HubSpot.KiotaClient/` (auto-generated)
- Must NOT reference `HubSpot.MockServer` from the CLI project
- Must NOT break existing CI pipeline
- Must NOT hardcode API keys or secrets

## TODOs

### Phase 1: Project Scaffolding + Infrastructure

- [x] 1. **Create the CLI project file**
  **What**: Create `tool/HubSpot.Cli/HubSpot.Cli.csproj` with dotnet tool configuration, System.CommandLine dependency, and KiotaClient project reference.
  **Files**: `tool/HubSpot.Cli/HubSpot.Cli.csproj` (create)
  **Details**:
  ```xml
  <Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
      <OutputType>Exe</OutputType>
      <TargetFramework>net10.0</TargetFramework>
      <RootNamespace>DamianH.HubSpot.Cli</RootNamespace>
      <ImplicitUsings>enable</ImplicitUsings>
      <Nullable>enable</Nullable>
      <IsPackable>true</IsPackable>
      <PackAsTool>true</PackAsTool>
      <ToolCommandName>hubspot</ToolCommandName>
      <PackageId>DamianH.HubSpot.Cli</PackageId>
      <Description>CLI tool for interacting with the HubSpot API</Description>
      <!-- Suppress CS1591 (missing XML docs) for CLI tool code -->
      <NoWarn>$(NoWarn);CS1591</NoWarn>
    </PropertyGroup>
    <ItemGroup>
      <PackageReference Include="System.CommandLine" Version="2.0.0-beta5.25306.1" />
      <PackageReference Include="Microsoft.Kiota.Http.HttpClientLibrary" Version="1.20.1" />
    </ItemGroup>
    <ItemGroup>
      <ProjectReference Include="..\..\src\HubSpot.KiotaClient\HubSpot.KiotaClient.csproj" />
    </ItemGroup>
  </Project>
  ```
  Note: Verify latest `System.CommandLine` version at build time. The `2.0.0-beta5.*` series is the latest prerelease. Also consider `System.CommandLine.NamingConventionBinder` for handler binding if needed.
  **Acceptance**: `dotnet restore tool/HubSpot.Cli/HubSpot.Cli.csproj` succeeds

- [x] 2. **Add CLI project to the solution**
  **What**: Update `hubspot.slnx` to include the new project under the `/tool/` folder.
  **Files**: `hubspot.slnx` (modify)
  **Details**: Add `<Project Path="tool/HubSpot.Cli/HubSpot.Cli.csproj" />` to the `/tool/` folder section.
  **Acceptance**: `dotnet build hubspot.slnx` builds all projects including the CLI

- [x] 3. **Create Program.cs with root command structure**
  **What**: Entry point that builds the root `RootCommand` with global options (`--token`, `--output`, `--verbose`, `--base-url`, `--quiet`) and registers the `crm` subcommand group.
  **Files**: `tool/HubSpot.Cli/Program.cs` (create)
  **Details**:
  - Define `RootCommand` with description "HubSpot CLI — interact with the HubSpot API from the command line"
  - Add global options:
    - `--token` (`Option<string?>`) — "HubSpot API access token"
    - `--output` (`Option<OutputFormat>`) with enum `{Json, Table, Csv}`, default `Json`
    - `--verbose` (`Option<bool>`) — "Enable verbose output"
    - `--base-url` (`Option<string?>`) — "Override API base URL (e.g., for mock server)"
    - `--quiet` (`Option<bool>`) — "Suppress non-essential output"
  - Register `CrmCommand.Create()` as subcommand
  - Call `rootCommand.InvokeAsync(args)`
  **Acceptance**: `dotnet run --project tool/HubSpot.Cli -- --help` shows help with global options

- [x] 4. **Create OutputFormat enum and output formatting infrastructure**
  **What**: Create the `OutputFormat` enum and `IOutputFormatter` interface with JSON, table, and CSV implementations.
  **Files**:
  - `tool/HubSpot.Cli/Output/OutputFormat.cs` (create)
  - `tool/HubSpot.Cli/Output/IOutputFormatter.cs` (create)
  - `tool/HubSpot.Cli/Output/JsonOutputFormatter.cs` (create)
  - `tool/HubSpot.Cli/Output/TableOutputFormatter.cs` (create)
  - `tool/HubSpot.Cli/Output/CsvOutputFormatter.cs` (create)
  **Details**:
  - `OutputFormat` enum: `Json`, `Table`, `Csv`
  - `IOutputFormatter` interface:
    ```csharp
    interface IOutputFormatter
    {
        void WriteObject(object obj, TextWriter writer);
        void WriteCollection(IEnumerable<object> items, TextWriter writer, string[]? columns = null);
        void WriteError(string message, TextWriter writer);
    }
    ```
  - `JsonOutputFormatter`: Uses `System.Text.Json.JsonSerializer` with `WriteIndented = true`. For Kiota model objects, extract `AdditionalData` properties and standard fields (Id, CreatedAt, UpdatedAt) into a clean JSON structure.
  - `TableOutputFormatter`: Renders a simple ASCII table. Columns derived from property keys. Uses `string.PadRight` alignment.
  - `CsvOutputFormatter`: Standard CSV output with header row. Quote values containing commas.
  - Factory method: `OutputFormatterFactory.Create(OutputFormat format) => format switch { ... }`
  **Acceptance**: Unit-testable formatters that produce correct output for sample data

- [x] 5. **Create authentication infrastructure**
  **What**: Build `HubSpotAuth` class that resolves the access token from the priority chain and creates a configured `IRequestAdapter`.
  **Files**:
  - `tool/HubSpot.Cli/Auth/HubSpotAuth.cs` (create)
  - `tool/HubSpot.Cli/Auth/ConfigFile.cs` (create)
  **Details**:
  - `HubSpotAuth.ResolveToken(string? flagToken)`:
    1. If `flagToken` is not null/empty, return it
    2. Check `HUBSPOT_ACCESS_TOKEN` environment variable
    3. Check `~/.hubspot/config.json` for `{ "accessToken": "..." }`
    4. If none found, write error to stderr and return null
  - `HubSpotAuth.CreateAdapter(string token, string? baseUrl)`:
    - Create `HttpClientRequestAdapter` with `BaseBearerTokenAuthenticationProvider` using a `TokenProvider` that returns the resolved token
    - Set `BaseUrl` to `baseUrl ?? "https://api.hubapi.com"`
    - Return the adapter
  - `ConfigFile` model: Simple record with `string? AccessToken` property, deserialized from `~/.hubspot/config.json`
  **Acceptance**: Token resolution works for all three sources; adapter is properly configured

- [x] 6. **Create the shared CommandContext infrastructure**
  **What**: Build a `CliContext` class that encapsulates the resolved adapter, output formatter, and verbosity settings. This is built once per invocation and passed to command handlers.
  **Files**: `tool/HubSpot.Cli/CliContext.cs` (create)
  **Details**:
  ```csharp
  sealed class CliContext
  {
      public required IRequestAdapter Adapter { get; init; }
      public required IOutputFormatter Formatter { get; init; }
      public required bool Verbose { get; init; }
      public required bool Quiet { get; init; }
      public required TextWriter Out { get; init; }   // stdout
      public required TextWriter Error { get; init; }  // stderr
  }
  ```
  - Factory method `CliContext.FromInvocation(InvocationContext ctx)` that:
    1. Resolves token via `HubSpotAuth`
    2. Creates adapter with resolved token and base URL
    3. Creates formatter from `--output` option
    4. Returns configured `CliContext`
  - If token resolution fails, write error to stderr and set exit code to 1
  **Acceptance**: `CliContext` correctly initializes from `InvocationContext` global options

### Phase 2: CRM Object Commands (using generic Objects V3 client)

- [x] 7. **Create the CRM command group**
  **What**: Create `crm` subcommand that contains object-type subcommands generated from a list.
  **Files**: `tool/HubSpot.Cli/Commands/CrmCommand.cs` (create)
  **Details**:
  - Static method `CrmCommand.Create()` returns a `Command("crm", "CRM operations")`
  - Iterates over a static list of known object types and calls `CrmObjectCommand.Create(objectType)` for each:
    ```csharp
    static readonly string[] ObjectTypes =
    [
        "companies", "contacts", "deals", "tickets", "line_items",
        "products", "quotes", "calls", "emails", "meetings",
        "notes", "tasks", "postal_mail", "communications",
        "leads", "invoices", "orders", "carts", "fees", "discounts",
        "contracts", "appointments", "feedback_submissions", "goal_targets",
    ];
    ```
  - Also registers specialized sub-commands: `associations`, `properties`, `pipelines`, `owners`, `schemas`
  **Acceptance**: `hubspot crm --help` lists all object types and specialized commands

- [x] 8. **Create the generic CRM object command factory**
  **What**: Build `CrmObjectCommand.Create(string objectType)` that generates a complete subcommand with `list`, `get`, `create`, `update`, `delete`, `search` sub-commands — all using `HubSpotCRMObjectsV3Client`.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/CrmObjectCommand.cs` (create)
  **Details**:
  - `CrmObjectCommand.Create("companies")` → `Command("companies", "Manage companies")` with sub-commands:
    - `list` — list objects with pagination
    - `get` — get single object by ID
    - `create` — create with properties
    - `update` — update by ID with properties
    - `delete` — delete by ID
    - `search` — search with filters
  - Each sub-command shares common options via helper methods
  **Acceptance**: `hubspot crm companies --help` shows all 6 sub-commands

- [x] 9. **Implement the `list` command handler**
  **What**: Handler for `hubspot crm <type> list` that calls the generic Objects V3 list endpoint.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/ListHandler.cs` (create)
  **Details**:
  - Options: `--limit` (int, default 10), `--properties` (string[], comma-separated), `--after` (string, pagination cursor), `--archived` (bool)
  - Creates `HubSpotCRMObjectsV3Client`, navigates to `.Crm.V3.Objects[objectType].GetAsync(q => { ... })`
  - Maps response `CollectionResponseSimplePublicObjectWithAssociations` to output:
    - Extracts `Results` list → for each object, flatten `Id`, `Properties.AdditionalData`, `CreatedAt`, `UpdatedAt`
    - If `Paging?.Next?.After` exists, show "Next page cursor: ..." on stderr (if not quiet)
  - Passes result list to `CliContext.Formatter.WriteCollection()`
  **Acceptance**: `hubspot crm companies list --limit 5 --properties name,domain` returns formatted output

- [x] 10. **Implement the `get` command handler**
  **What**: Handler for `hubspot crm <type> get <id>`.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/GetHandler.cs` (create)
  **Details**:
  - Arguments: `<id>` (required string)
  - Options: `--properties` (string[]), `--associations` (string[])
  - Calls `.Crm.V3.Objects[objectType][id].GetAsync(q => { ... })`
  - Maps `SimplePublicObjectWithAssociations` to output
  - Passes single object to `CliContext.Formatter.WriteObject()`
  **Acceptance**: `hubspot crm companies get 123 --properties name` returns the object

- [x] 11. **Implement the `create` command handler**
  **What**: Handler for `hubspot crm <type> create --properties key=value,key=value`.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/CreateHandler.cs` (create)
  **Details**:
  - Options: `--properties` or `-p` (string[], parsed as `key=value` pairs)
  - Builds `SimplePublicObjectInputForCreate` with `Properties.AdditionalData` populated from parsed pairs
  - Calls `.Crm.V3.Objects[objectType].PostAsync(input)`
  - Maps `CreatedResponseSimplePublicObject` (which has `.Entity`) to output
  - Exit code 0 on success
  **Acceptance**: `hubspot crm companies create --properties name=Acme,domain=acme.com` returns created object

- [x] 12. **Implement the `update` command handler**
  **What**: Handler for `hubspot crm <type> update <id> --properties key=value`.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/UpdateHandler.cs` (create)
  **Details**:
  - Arguments: `<id>` (required string)
  - Options: `--properties` (string[], key=value pairs)
  - Builds `SimplePublicObjectInput` with properties
  - Calls `.Crm.V3.Objects[objectType][id].PatchAsync(input)`
  - Maps `SimplePublicObject` to output
  **Acceptance**: `hubspot crm companies update 123 --properties name=NewName` returns updated object

- [x] 13. **Implement the `delete` command handler**
  **What**: Handler for `hubspot crm <type> delete <id>`.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/DeleteHandler.cs` (create)
  **Details**:
  - Arguments: `<id>` (required string)
  - Calls `.Crm.V3.Objects[objectType][id].DeleteAsync()`
  - Outputs `{ "deleted": true, "id": "<id>" }` on success (JSON) or simple message for table/CSV
  - Exit code 0 on success
  **Acceptance**: `hubspot crm companies delete 123` succeeds silently (or with confirmation message)

- [x] 14. **Implement the `search` command handler**
  **What**: Handler for `hubspot crm <type> search` with filter expressions.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/SearchHandler.cs` (create)
  **Details**:
  - Options:
    - `--filter` (string[], repeatable) — format: `"propertyName OPERATOR value"`, e.g., `"name EQ Acme"`, `"amount GT 1000"`
    - `--query` (string) — free-text search query
    - `--properties` (string[])
    - `--limit` (int, default 10)
    - `--sort` (string[]) — sort properties
    - `--after` (string) — pagination cursor
  - Filter parsing: split on spaces → `propertyName`, `operator` (mapped to `Filter_operator` enum), `value`
  - Builds `PublicObjectSearchRequest` with `FilterGroups` containing one `FilterGroup` with all filters
  - Calls `.Crm.V3.Objects[objectType].Search.PostAsync(request)`
  - Maps `CollectionResponseWithTotalSimplePublicObject` to output
  **Acceptance**: `hubspot crm companies search --filter "name EQ Acme" --properties name,domain` returns matching results

- [x] 15. **Create shared property parsing utility**
  **What**: Utility to parse `key=value` property strings from CLI args into `Dictionary<string, object>`.
  **Files**: `tool/HubSpot.Cli/Utilities/PropertyParser.cs` (create)
  **Details**:
  - `PropertyParser.Parse(string[] propertyArgs)` → `Dictionary<string, object>`
  - Handles two formats:
    - Single arg with commas: `"name=Acme,domain=acme.com"` → split on `,` then `=`
    - Multiple args: `--properties name=Acme --properties domain=acme.com`
  - Validates each has exactly one `=` sign; error on malformed input
  - Used by both `create` and `update` handlers
  **Acceptance**: Correctly parses `"name=Acme,domain=acme.com"` to `{ "name": "Acme", "domain": "acme.com" }`

- [x] 16. **Create shared response mapping utility**
  **What**: Utility to convert Kiota model objects to clean dictionaries for output formatting.
  **Files**: `tool/HubSpot.Cli/Utilities/ResponseMapper.cs` (create)
  **Details**:
  - `ResponseMapper.MapObject(SimplePublicObjectWithAssociations obj)` → `Dictionary<string, object?>`
    - Includes `id`, `createdAt`, `updatedAt`, `archived`, and all `Properties.AdditionalData` entries
  - `ResponseMapper.MapObject(SimplePublicObject obj)` → similar
  - `ResponseMapper.MapCollection(CollectionResponseSimplePublicObjectWithAssociations response)` → `{ results: [...], paging: { next: "..." } }`
  - Uses the generic `CRM.Objects.V3.Models` namespace types
  **Acceptance**: Clean dictionary output from Kiota model objects

### Phase 3: Remaining CRM Commands

- [x] 17. **Implement `crm properties` commands**
  **What**: Commands for listing and managing CRM property definitions.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/PropertiesCommand.cs` (create)
  **Details**:
  - `hubspot crm properties list <objectType>` — uses `HubSpotCRMPropertiesV3Client` → `.Crm.V3.Properties[objectType].GetAsync()`
  - `hubspot crm properties get <objectType> <propertyName>` — get single property definition
  - `hubspot crm properties groups list <objectType>` — list property groups
  - Formats property results showing: name, label, type, groupName, description
  **Acceptance**: `hubspot crm properties list companies` shows property definitions

- [x] 18. **Implement `crm pipelines` commands**
  **What**: Commands for listing and managing pipelines.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/PipelinesCommand.cs` (create)
  **Details**:
  - `hubspot crm pipelines list <objectType>` — uses `HubSpotCRMPipelinesV3Client` → `.Crm.V3.Pipelines[objectType].GetAsync()`
  - `hubspot crm pipelines get <objectType> <pipelineId>` — get single pipeline with stages
  - Object type is typically `deals` or `tickets`
  - Table format shows: id, label, displayOrder, stages count
  **Acceptance**: `hubspot crm pipelines list deals` shows pipeline list

- [x] 19. **Implement `crm owners` commands**
  **What**: Commands for listing owners.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/OwnersCommand.cs` (create)
  **Details**:
  - `hubspot crm owners list` — uses `HubSpotCRMCrmOwnersV3Client` → `.Crm.V3.Owners.GetAsync()`
  - `hubspot crm owners get <ownerId>` — get single owner
  - Table format shows: id, email, firstName, lastName, userId
  **Acceptance**: `hubspot crm owners list` shows owner list

- [x] 20. **Implement `crm associations` commands**
  **What**: Commands for managing CRM associations.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/AssociationsCommand.cs` (create)
  **Details**:
  - These use the V3 associations endpoints which are different from the object CRUD API
  - `hubspot crm associations batch-create <fromType> <toType> --from <id> --to <id> --type-id <typeId>` — batch create
  - `hubspot crm associations batch-read <fromType> <toType> --ids <id1,id2>` — batch read
  - Uses `HubSpotCRMAssociationsV3Client` → `.Crm.V3.Associations[fromType][toType].Batch.Create/Read`
  - For simpler per-object associations, users can use `hubspot crm <type> get <id> --associations contacts,deals`
  **Acceptance**: Association batch operations work correctly

- [x] 21. **Implement `crm schemas` commands**
  **What**: Commands for custom object schema management.
  **Files**: `tool/HubSpot.Cli/Commands/Crm/SchemasCommand.cs` (create)
  **Details**:
  - `hubspot crm schemas list` — list custom object schemas
  - `hubspot crm schemas get <objectType>` — get schema definition
  - Uses `HubSpotCRMSchemasV3Client`
  **Acceptance**: `hubspot crm schemas list` shows custom object schemas

### Phase 4: Non-CRM Commands (Higher Level)

These follow the same pattern as Phase 2-3 but for other API categories. Each gets its own command file.

- [x] 22. **Implement marketing commands**
  **What**: `hubspot marketing` subcommand group.
  **Files**: `tool/HubSpot.Cli/Commands/MarketingCommand.cs` (create)
  **Details**: Transactional emails, marketing events, forms. Uses respective Kiota clients from `Generated/Marketing/`.

- [x] 23. **Implement CMS commands**
  **What**: `hubspot cms` subcommand group.
  **Files**: `tool/HubSpot.Cli/Commands/CmsCommand.cs` (create)
  **Details**: Blog posts, pages, HubDB, domains, URL redirects. Uses clients from `Generated/CMS/`.

- [x] 24. **Implement files commands**
  **What**: `hubspot files` subcommand group.
  **Files**: `tool/HubSpot.Cli/Commands/FilesCommand.cs` (create)
  **Details**: Upload, list, download files. Uses `Generated/Files/` client.

- [x] 25. **Implement webhooks commands**
  **What**: `hubspot webhooks` subcommand group.
  **Files**: `tool/HubSpot.Cli/Commands/WebhooksCommand.cs` (create)
  **Details**: List/create/delete webhook subscriptions. Uses `Generated/Webhooks/` client.

### Phase 5: Polish and Packaging

- [x] 26. **Add tab completion support**
  **What**: Enable System.CommandLine's built-in tab completion with `dotnet-suggest`.
  **Files**: `tool/HubSpot.Cli/Program.cs` (modify)
  **Details**:
  - System.CommandLine provides tab completion out of the box via `dotnet-suggest` global tool
  - Add custom completions for object type arguments where applicable (e.g., complete `companies`, `contacts`, etc.)
  - Add `CompletionItem` suggestions for `--output` values

- [x] 27. **Implement config file management**
  **What**: Add `hubspot config` commands for managing `~/.hubspot/config.json`.
  **Files**: `tool/HubSpot.Cli/Commands/ConfigCommand.cs` (create)
  **Details**:
  - `hubspot config set-token <token>` — saves token to `~/.hubspot/config.json`
  - `hubspot config show` — displays current config (masking token)
  - `hubspot config clear` — removes config file
  - Config file format: `{ "accessToken": "pat-...", "baseUrl": null }`

- [x] 28. **Add error handling and exit codes**
  **What**: Consistent error handling across all commands.
  **Files**: `tool/HubSpot.Cli/ErrorHandler.cs` (create)
  **Details**:
  - Catch `ApiException` from Kiota and format error details to stderr
  - Exit code 0 = success, 1 = auth error, 2 = API error, 3 = input validation error
  - `--verbose` shows full stack traces; default shows user-friendly messages
  - All error output goes to stderr; data output to stdout (for piping)

- [x] 29. **Verify dotnet tool packaging**
  **What**: Ensure `dotnet pack` produces a valid tool package and `dotnet tool install` works.
  **Files**: No new files; verify existing `.csproj` settings
  **Details**:
  - `dotnet pack tool/HubSpot.Cli/HubSpot.Cli.csproj -c Release -o ./artifacts`
  - `dotnet tool install --global --add-source ./artifacts DamianH.HubSpot.Cli`
  - `hubspot --help` works after install
  - `hubspot --version` shows MinVer-derived version
  **Acceptance**: Tool installs and runs from `dotnet tool install`

- [x] 30. **Update CI workflow**
  **What**: Ensure the CI builds, tests, and packs the CLI tool.
  **Files**: `.github/workflows/ci.yml` (modify — if needed)
  **Details**:
  - The CLI project is already included via `hubspot.slnx`, so `dotnet build hubspot.slnx` will build it
  - `dotnet pack hubspot.slnx` will pack it since `IsPackable=true` and `PackAsTool=true`
  - May need to verify that the tool NuPkg is produced in the artifacts
  - No test project for the CLI in Phase 1 — CLI tests can be added later as integration tests

## File Structure Summary

```
tool/HubSpot.Cli/
├── HubSpot.Cli.csproj
├── Program.cs
├── CliContext.cs
├── ErrorHandler.cs
├── Auth/
│   ├── HubSpotAuth.cs
│   └── ConfigFile.cs
├── Output/
│   ├── OutputFormat.cs
│   ├── IOutputFormatter.cs
│   ├── JsonOutputFormatter.cs
│   ├── TableOutputFormatter.cs
│   └── CsvOutputFormatter.cs
├── Utilities/
│   ├── PropertyParser.cs
│   └── ResponseMapper.cs
└── Commands/
    ├── CrmCommand.cs
    ├── ConfigCommand.cs
    ├── MarketingCommand.cs
    ├── CmsCommand.cs
    ├── FilesCommand.cs
    ├── WebhooksCommand.cs
    └── Crm/
        ├── CrmObjectCommand.cs
        ├── ListHandler.cs
        ├── GetHandler.cs
        ├── CreateHandler.cs
        ├── UpdateHandler.cs
        ├── DeleteHandler.cs
        ├── SearchHandler.cs
        ├── PropertiesCommand.cs
        ├── PipelinesCommand.cs
        ├── OwnersCommand.cs
        ├── AssociationsCommand.cs
        └── SchemasCommand.cs
```

## Key Implementation Notes

### Using the Generic Objects V3 Client
```csharp
// All CRM CRUD goes through one client
var client = new HubSpotCRMObjectsV3Client(adapter);

// List companies
var result = await client.Crm.V3.Objects["companies"].GetAsync(q =>
{
    q.QueryParameters.Limit = 10;
    q.QueryParameters.Properties = ["name", "domain"];
});

// Get by ID
var company = await client.Crm.V3.Objects["companies"]["123"].GetAsync();

// Create
var created = await client.Crm.V3.Objects["companies"].PostAsync(input);

// Update
var updated = await client.Crm.V3.Objects["companies"]["123"].PatchAsync(input);

// Delete
await client.Crm.V3.Objects["companies"]["123"].DeleteAsync();

// Search
var results = await client.Crm.V3.Objects["companies"].Search.PostAsync(searchRequest);
```

### Property Parsing CLI Syntax
```bash
# Comma-separated in a single value
hubspot crm companies create --properties "name=Acme Corp,domain=acme.com"

# Multiple --properties flags
hubspot crm companies create --properties name="Acme Corp" --properties domain=acme.com
```

### Filter Parsing CLI Syntax
```bash
# Simple equality filter
hubspot crm companies search --filter "name EQ Acme"

# Multiple filters (AND within group)
hubspot crm companies search --filter "name EQ Acme" --filter "city EQ Austin"

# With query
hubspot crm companies search --query "acme" --properties name,domain
```

## Verification
- [x] `dotnet build hubspot.slnx -c Release` succeeds with no errors
- [x] `dotnet run --project tool/HubSpot.Cli -- --help` displays help
- [x] `dotnet run --project tool/HubSpot.Cli -- crm --help` lists all object types
- [x] `dotnet run --project tool/HubSpot.Cli -- crm companies list --help` shows list options
- [x] `dotnet pack tool/HubSpot.Cli/HubSpot.Cli.csproj -c Release` produces NuPkg
- [x] All existing tests pass: `dotnet test hubspot.slnx`
- [x] No regressions in CI pipeline
