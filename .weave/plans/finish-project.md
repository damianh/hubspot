# Finish HubSpot .NET Client & Mock Server

## TL;DR
> **Summary**: Complete the remaining ~10% of the project: fix Release build (568+ errors), CI/CD pipelines, NuGet packaging metadata, working sample project, and polish.
> **Estimated Effort**: Large (split into 7 independently-shippable tasks)

## Context

### Original Request
Finish out the HubSpot .NET Client & Mock Server project. The gap analysis identified work across CI/CD, packaging, samples, and polish.

### Key Findings

**Reassessed "stub repositories"**: The gap analysis listed ~18 repositories as stubs, but after inspecting each one, they are **all fully implemented** with routes, repositories, and DI registration. For example:
- `BusinessUnitRepository` — full CRUD with `JsonElement` storage, routes in `ApiRoutes.BusinessUnits.cs`, tests in `BusinessUnitsTests.cs`
- `CallingExtensionRepository` — settings CRUD + recordings, routes in `ApiRoutes.Extensions.cs`, tests in `CrmExtensionsIntegrationTests.cs`
- `SchedulerMeetingRepository` — meeting links CRUD + availability, routes in `ApiRoutes.Scheduler.cs`
- `FeatureFlagRepository`, `ObjectLibraryRepository`, `PropertyValidationRepository`, `LimitsTrackingRepository` — all have routes registered in `ApiRoutes.Extensions.cs`
- `VideoConferencingRepository`, `TranscriptionRepository`, `CrmCardRepository` — all tested in `CrmExtensionsIntegrationTests.cs`
- `TagRepository` — routes in `ApiRoutes.CmsTags.cs`, tests in `CmsTagsKiotaTests.cs`
- `CurrencyRepository`, `TaxRateRepository` — routes + tests exist

**What IS missing**:
- Some test files have commented-out tests due to Kiota client generation mismatches (e.g., `BusinessUnitsTests.cs` line 30-60)
- No CI/CD pipeline at all (`.github/` is empty)
- NuGet metadata partially set up — has `PackageLicenseExpression`, `PackageIcon`, `PackageProjectUrl`, `PackageReleaseNotes` but is missing `PackageId`, `Description`, `Authors`, `Tags`, `RepositoryUrl`. Also `icon.png` file doesn't exist
- `PackageReadmePath` variable referenced in `Directory.Build.props` but never defined (only `PackageReadmeFile` is set)
- The package readme files exist (`src/HubSpot.MockServer/readme.md`, `src/HubSpot.KiotaClient/readme.md`) but the casing doesn't match `PackageReadmeFile` (README.md vs readme.md)
- Sample project is a placeholder with an empty test and wrong copyright header
- `HubSpot.KiotaClient.csproj` has duplicate `RootNamespace` declarations (lines 5-6)
- Tests use `UseMicrosoftTestingPlatformRunner` but the sample project errors on `testhost.dll` resolution

**Release Build Is Broken** (discovered in session 2):
- **KiotaClient**: 2 × CA1034 errors in `PropertyNames.cs` (nested public static classes `CrmCompany` and `CrmAssociations`). This is a hand-written file, safe to modify.
- **MockServer**: **568 errors** in Release mode when `TreatWarningsAsErrors` + `AnalysisMode=All` + `EnforceCodeStyleInBuild=true` combine. Breakdown:
  - 218 × CA1305 (IFormatProvider)
  - 76 × IDE0021 (expression body for constructors)
  - 70 × IDE0005 (unnecessary using directives)
  - 36 × CA1854 (prefer Dictionary.TryGetValue)
  - 32 × IDE0055 (formatting)
  - 22 × CS8604 (possible null reference argument)
  - 16 × CA5394 (insecure randomness)
  - 14 × CS8602 (dereference of possibly null reference)
  - 12 × CA1308 (normalize strings to uppercase)
  - 12 × CA1002 (don't expose generic lists)
  - Plus ~30 other error types with 2-10 occurrences each
  - Also 2 × AD0001 (analyzer crash in RouteHandlerAnalyzer)
- **Scoping**: `src/Directory.Build.props` only applies to `src/` projects. `test/` and `sample/` have **no** `Directory.Build.props`, so they do NOT inherit `TreatWarningsAsErrors`, `AnalysisMode=All`, or `EnforceCodeStyleInBuild`. CS8602 warnings in tests are harmless.
- **`.editorconfig`** at repo root promotes IDE0005, IDE0021, IDE0055 etc. to `warning` severity, which then become errors via `TreatWarningsAsErrors` in Release for `src/` projects.

**Project structure**:
- Solution: `hubspot.slnx` with 4 projects (KiotaClient, MockServer, Tests, Sample) + Generate tool
- Framework: net10.0 throughout
- Versioning: MinVer with `hs-` tag prefix
- Test framework: xunit.v3 with Shouldly assertions
- 49 test files with ~50+ test classes

## Objectives

### Core Objective
Make the project publishable to NuGet and maintainable via CI/CD, with a working sample that demonstrates usage.

### Deliverables
- [x] Release build succeeds with zero errors for both src projects
- [x] GitHub Actions CI/CD pipeline (build, test, pack, publish)
- [x] Complete NuGet package metadata for both packages
- [x] Working sample project with realistic usage examples
- [x] Fix duplicate RootNamespace in KiotaClient csproj
- [x] Fix PackageReadmeFile casing / PackageReadmePath reference

### Definition of Done
- [x] `dotnet build hubspot.slnx -c Release` succeeds with no errors
- [x] `dotnet test hubspot.slnx` passes all tests
- [x] `dotnet pack src/HubSpot.MockServer -c Release` produces valid .nupkg
- [x] `dotnet pack src/HubSpot.KiotaClient -c Release` produces valid .nupkg
- [x] GitHub Actions workflow exists and would build/test/pack on push

### Guardrails (Must NOT)
- Do NOT manually edit any code files in `src/HubSpot.KiotaClient/` — the entire project (including hand-written files like `PropertyNames.cs`) is treated as generated/tool-managed. Use `.csproj` suppressions instead.
- Do NOT change the existing test patterns or framework
- Do NOT add production authentication/authorization to the mock server
- Do NOT convert the `JsonElement`-based repositories to strongly-typed models (that's a deliberate design choice for flexibility)
- Do NOT add `icon.png` — just remove the reference or add a placeholder; the icon is cosmetic
- Do NOT lower code quality standards just to suppress warnings — prefer fixing code over blanket `NoWarn`

## TODOs

- [x] 1. **Fix Release Build: KiotaClient CA1034 Errors**
  **What**: Suppress the 2 CA1034 (nested public types) errors in the KiotaClient project. The KiotaClient project contains generated code and should not have code changes made to it.

  **Strategy**: Add `CA1034` to the `NoWarn` list in `src/HubSpot.KiotaClient/HubSpot.KiotaClient.csproj`. This is the standard approach for generated code projects where analyzer rules don't apply.

  **Files**:
    - `src/HubSpot.KiotaClient/HubSpot.KiotaClient.csproj` (add `<NoWarn>$(NoWarn);CA1034</NoWarn>`)
  **Acceptance**: `dotnet build src/HubSpot.KiotaClient -c Release` succeeds with zero errors.
  **Effort**: Quick

- [x] 2. **Fix Release Build: MockServer 568 Analyzer Errors**
  **What**: Fix the 568 Release-mode errors in MockServer caused by `AnalysisMode=All` + `EnforceCodeStyleInBuild=true` + `TreatWarningsAsErrors=true`.

  **Strategy — tiered approach** (prefer fixing over suppressing):

  **Tier A — Fix in code (high-value, improves quality)**:
  - **IDE0005** (70 occurrences): Remove unnecessary `using` directives. These are purely mechanical — use `dotnet format` to auto-fix.
  - **IDE0021** (76 occurrences): Convert constructor bodies to expression bodies. Auto-fixable with `dotnet format`.
  - **IDE0055** (32 occurrences): Formatting violations. Auto-fixable with `dotnet format`.
  - **CA1854** (36 occurrences): Replace `dict.ContainsKey(x) + dict[x]` with `dict.TryGetValue(x, out var v)`. Mechanical but improves perf.
  - **CS8604/CS8602** (36 combined): Add null checks or null-forgiving operator where appropriate.

  **Tier B — Suppress in MockServer csproj (pragmatic for a mock/test server)**:
  - **CA1305** (218 occurrences): `IFormatProvider` — excessive for a mock server that only deals with test data. Suppress: `<NoWarn>$(NoWarn);CA1305</NoWarn>`
  - **CA5394** (16 occurrences): Insecure randomness — not a security concern for a mock test server. Suppress: `<NoWarn>$(NoWarn);CA5394</NoWarn>`
  - **CA1308** (12 occurrences): Normalize strings to uppercase — HubSpot API uses lowercase, matching their convention. Suppress: `<NoWarn>$(NoWarn);CA1308</NoWarn>`
  - **CA1002** (12 occurrences): Don't expose generic lists — these are internal mock implementations. Suppress: `<NoWarn>$(NoWarn);CA1002</NoWarn>`
  - **AD0001** (2 occurrences): Analyzer crash — not our fault, suppress or ignore.

  **Tier C — Remaining low-count errors**: After Tier A and B, assess what's left (should be ~30 errors across ~15 categories). Fix individually if quick; suppress in csproj `NoWarn` if not.

  **Execution order**:
  1. Run `dotnet format src/HubSpot.MockServer` to auto-fix IDE0005, IDE0021, IDE0055
  2. Manually fix CA1854 (TryGetValue pattern — ~36 locations)
  3. Fix CS8604/CS8602 (null reference — ~36 locations)
  4. Add suppressions to `HubSpot.MockServer.csproj` for Tier B rules
  5. Build Release, assess remaining errors, fix/suppress as needed
  6. Verify: `dotnet build src/HubSpot.MockServer -c Release` succeeds

  **Files**:
    - `src/HubSpot.MockServer/HubSpot.MockServer.csproj` (add NoWarn for Tier B)
    - `src/HubSpot.MockServer/**/*.cs` (code fixes across many files)
  **Acceptance**: `dotnet build src/HubSpot.MockServer -c Release` succeeds with zero errors.
  **Effort**: Medium (largest single task — ~250 auto-fixable, ~70 manual, ~260 suppressed)

- [x] 3. **Fix Package Build Blockers**
  **What**: Fix issues that prevent `dotnet pack` from succeeding cleanly in Release mode.
  - Remove duplicate `<RootNamespace>` in `src/HubSpot.KiotaClient/HubSpot.KiotaClient.csproj` (line 5 has `HubSpot`, line 6 has `DamianH.HubSpot.KiotaClient` — keep line 6)
  - Fix `PackageReadmeFile` vs `PackageReadmePath` mismatch in `src/Directory.Build.props`:
    - `PackageReadmeFile` is set to `README.md` but the actual files are `readme.md` (lowercase)
    - `PackageReadmePath` variable is referenced in `<None Include="$(PackageReadmePath)"...` but never defined
    - Option A: Rename `readme.md` files to `README.md` in both src projects
    - Option B: Change `PackageReadmeFile` to `readme.md` and define `PackageReadmePath`
    - Recommendation: Option A is simpler. Set `<PackageReadmePath>readme.md</PackageReadmePath>` per-project in each `.csproj`, or define it globally and use `$(PackageReadmeFile)` consistently
  - Either add a placeholder `src/icon.png` (1x1 transparent PNG) or remove the `<PackageIcon>` and `<None Include="../icon.png"...` lines
  **Files**:
    - `src/HubSpot.KiotaClient/HubSpot.KiotaClient.csproj`
    - `src/Directory.Build.props`
    - `src/HubSpot.MockServer/readme.md` (rename to `README.md` if going Option A)
    - `src/HubSpot.KiotaClient/readme.md` (rename to `README.md` if going Option A)
    - Optionally: `src/icon.png` (create or remove reference)
  **Acceptance**: `dotnet pack hubspot.slnx -c Release` produces two `.nupkg` files without errors. Inspect with `dotnet nuget verify` or unzip to confirm readme is included.
  **Effort**: Quick

- [x] 4. **Complete NuGet Package Metadata**
  **What**: Add missing NuGet metadata to `src/Directory.Build.props` so packages are discoverable and professional.
  Add these properties:
  ```xml
  <Authors>Damian Hickey</Authors>
  <Description>HubSpot .NET client and mock server for testing</Description>
  <Tags>hubspot;api;crm;mock;testing;kiota</Tags>
  <RepositoryUrl>https://github.com/damianh/hubspot</RepositoryUrl>
  <RepositoryType>git</RepositoryType>
  ```
  Per-project overrides for Description:
  - `HubSpot.KiotaClient.csproj`: Add `<PackageId>DamianH.HubSpot.KiotaClient</PackageId>` and `<Description>Auto-generated HubSpot API client using Microsoft Kiota</Description>`
  - `HubSpot.MockServer.csproj`: Add `<PackageId>DamianH.HubSpot.MockServer</PackageId>` and `<Description>In-memory mock HubSpot server for testing without external dependencies</Description>`
  **Files**:
    - `src/Directory.Build.props`
    - `src/HubSpot.KiotaClient/HubSpot.KiotaClient.csproj`
    - `src/HubSpot.MockServer/HubSpot.MockServer.csproj`
  **Acceptance**: `dotnet pack -c Release` produces `.nupkg` files. Extract and inspect `.nuspec` inside — all metadata fields are populated. NuGet package analysis tool shows no missing metadata warnings.
  **Effort**: Quick

- [x] 5. **Create GitHub Actions CI/CD Pipeline**
  **What**: Add GitHub Actions workflows for build/test/pack/publish. Create two workflow files:

  **File 1**: `.github/workflows/ci.yml` — runs on every push and PR to main:
  ```yaml
  name: CI
  on:
    push:
      branches: [main]
    pull_request:
      branches: [main]
  jobs:
    build:
      runs-on: ubuntu-latest
      steps:
        - uses: actions/checkout@v4
          with:
            fetch-depth: 0  # Required for MinVer
        - uses: actions/setup-dotnet@v4
          with:
            dotnet-version: '10.0.x'
        - run: dotnet restore hubspot.slnx
        - run: dotnet build hubspot.slnx -c Release --no-restore
        - run: dotnet test hubspot.slnx -c Release --no-build --logger "trx;LogFileName=results.trx"
        - uses: actions/upload-artifact@v4
          if: always()
          with:
            name: test-results
            path: '**/TestResults/*.trx'
        - run: dotnet pack hubspot.slnx -c Release --no-build -o ./artifacts
        - uses: actions/upload-artifact@v4
          with:
            name: packages
            path: ./artifacts/*.nupkg
  ```

  **File 2**: `.github/workflows/release.yml` — runs on tag push matching `hs-*`:
  ```yaml
  name: Release
  on:
    push:
      tags: ['hs-*']
  jobs:
    publish:
      runs-on: ubuntu-latest
      steps:
        - uses: actions/checkout@v4
          with:
            fetch-depth: 0
        - uses: actions/setup-dotnet@v4
          with:
            dotnet-version: '10.0.x'
        - run: dotnet restore hubspot.slnx
        - run: dotnet build hubspot.slnx -c Release --no-restore
        - run: dotnet test hubspot.slnx -c Release --no-build
        - run: dotnet pack hubspot.slnx -c Release --no-build -o ./artifacts
        - run: dotnet nuget push ./artifacts/*.nupkg --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_API_KEY }} --skip-duplicate
  ```

  **Notes**:
  - .NET 10 is preview; may need `dotnet-quality: preview` or `include-prerelease: true` in setup-dotnet
  - MinVer requires `fetch-depth: 0` for tag-based versioning
  - The `hs-` tag prefix matches `MinVerTagPrefix` in `Directory.Build.props`
  - Sample project should be excluded from pack but included in build/test

  **Files**:
    - `.github/workflows/ci.yml` (create)
    - `.github/workflows/release.yml` (create)
  **Acceptance**: Workflow files pass `actionlint` validation. Manually verify structure matches MinVer tag convention.
  **Effort**: Short

- [x] 6. **Build Working Sample Project**
  **What**: Replace the empty placeholder in `sample/Sample/` with realistic usage examples that demonstrate how to use the MockServer with the Kiota client for testing. The sample should serve as living documentation.

  Fix the project references first:
  - Add `<ProjectReference>` to both `HubSpot.KiotaClient` and `HubSpot.MockServer` in `Sample.csproj`
  - Remove copyright header from Duende Software (incorrect attribution, line 1-2 of `UnitTest1.cs`)
  - Keep it as a test project (xunit) since that's the primary use case

  Replace `UnitTest1.cs` with these sample test files:

  **`sample/Sample/GettingStartedSamples.cs`** — basic mock server setup and CRM operations:
  - Start a `HubSpotMockServer`, create a Kiota `HttpClientRequestAdapter` pointing to it
  - Create, read, update, delete a Company using the typed Kiota client
  - Demonstrate listing with pagination
  - Show search with filters

  **`sample/Sample/AssociationSamples.cs`** — associating objects:
  - Create a Contact and a Company
  - Associate them using V4 associations API
  - Retrieve associations

  **`sample/Sample/MarketingSamples.cs`** — marketing APIs:
  - Send a transactional email
  - Check send status

  **`sample/Sample/OwnerAndPipelineSamples.cs`** — supporting APIs:
  - List owners (pre-seeded)
  - Create a pipeline with stages

  Pattern to follow: Use `IAsyncLifetime` with `HubSpotMockServer` (same as existing tests like `CrmCompaniesKiotaTests.cs` and `OwnerTests.cs`). Each sample class should be self-contained and readable.

  **Files**:
    - `sample/Sample/Sample.csproj` (add project references)
    - `sample/Sample/UnitTest1.cs` (delete or replace)
    - `sample/Sample/GettingStartedSamples.cs` (create)
    - `sample/Sample/AssociationSamples.cs` (create)
    - `sample/Sample/MarketingSamples.cs` (create)
    - `sample/Sample/OwnerAndPipelineSamples.cs` (create)
  **Acceptance**: `dotnet test sample/Sample/Sample.csproj` passes all sample tests. Each test demonstrates a distinct API capability.
  **Effort**: Medium

- [x] 7. **Expand Package READMEs for NuGet**
  **What**: The current `readme.md` files in both src projects are reasonable but need minor improvements for NuGet display. NuGet renders the readme on the package page.

  `src/HubSpot.KiotaClient/readme.md` (currently 13 lines):
  - Add a brief "What is this?" section
  - Add a minimal usage example showing how to create an adapter and make a request
  - Add link to the full repo README for more details
  - Add supported API categories list

  `src/HubSpot.MockServer/readme.md` (currently 221 lines, good):
  - Add a "Quick Install" section at the top with `dotnet add package DamianH.HubSpot.MockServer`
  - Add a "Requirements" section (net10.0+)
  - Ensure the usage example at the top shows the correct Kiota client setup (current example references `HubSpotClient` which doesn't exist — should show `HttpClientRequestAdapter` + specific Kiota client)

  **Files**:
    - `src/HubSpot.KiotaClient/readme.md`
    - `src/HubSpot.MockServer/readme.md`
  **Acceptance**: Both readmes render correctly in a markdown previewer. Usage examples compile (verify mentally or with a snippet test).
  **Effort**: Short

## Removed Tasks

### ~~Task 7 (old): Fix CS8602 Null-Reference Warnings in Tests~~
**Removed**: Investigation confirmed that `src/Directory.Build.props` (which contains `TreatWarningsAsErrors`) only applies to `src/` projects. The `test/` and `sample/` directories have no `Directory.Build.props` and do NOT inherit these settings. Therefore CS8602 warnings in the test project are just warnings — they never become errors and never block the build. This task was based on an incorrect assumption.

### ~~Task 5 (old): Add Error Scenario Tests~~
### ~~Task 6 (old): Add Request Logging Middleware~~
**Deferred**: These are nice-to-have polish items. They should be tracked as separate future work items rather than blocking the initial release. The current test suite with 49 test files and 50+ test classes provides adequate coverage.

## Verification

- [x] `dotnet build hubspot.slnx -c Release` succeeds with zero errors
- [x] `dotnet test hubspot.slnx` — all tests pass (existing)
- [x] `dotnet pack hubspot.slnx -c Release -o ./artifacts` produces 2 `.nupkg` files
- [x] `.github/workflows/ci.yml` and `.github/workflows/release.yml` exist and are valid YAML
- [x] Sample project runs and demonstrates real mock server usage
- [x] No regressions in existing test suite

## Dependency Graph

```
Task 1 (KiotaClient CA1034) ──┐
                               ├──> Task 3 (Package Build Blockers) ──> Task 4 (NuGet Metadata) ──> Task 5 (CI/CD)
Task 2 (MockServer 568 errs) ─┘
Task 6 (Sample Project) ............. independent (but needs Tasks 1-2 for Release build)
Task 7 (READMEs) ................... independent (but best after Task 4)
```

**Critical path**: Tasks 1 → 2 → 3 → 4 → 5 (Release build must work before packaging/CI).
Tasks 1 and 2 can run in parallel. Tasks 6 and 7 can start anytime but are best after the critical path.
