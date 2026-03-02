# DamianH.HubSpot.KiotaClient

Auto-generated .NET HubSpot API client built with [Microsoft Kiota](https://learn.microsoft.com/en-us/openapi/kiota/overview).

## What is this?

This package provides strongly-typed .NET clients for the HubSpot REST API, generated directly from [HubSpot's official OpenAPI definitions](https://api.hubspot.com/api-catalog-public/v1/apis). Each HubSpot API area is exposed as a separate client class so you only take the dependencies you need.

## Quick Install

```bash
dotnet add package DamianH.HubSpot.KiotaClient
```

## Requirements

- .NET 10.0 or later
- `Microsoft.Kiota.Http.HttpClientLibrary` (peer dependency)

## Minimal Usage Example

```csharp
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

// Implement IAccessTokenProvider to supply your HubSpot access token
sealed class HubSpotTokenProvider : IAccessTokenProvider
{
    public AllowedHostsValidator AllowedHostsValidator { get; } = new();
    public Task<string> GetAuthorizationTokenAsync(
        Uri uri,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
        => Task.FromResult("your-hubspot-access-token");
}

// Create an adapter with your token provider
var authProvider = new BaseBearerTokenAuthenticationProvider(new HubSpotTokenProvider());
var adapter = new HttpClientRequestAdapter(authProvider)
{
    BaseUrl = "https://api.hubapi.com"
};

// Instantiate the client for the API area you need
var client = new HubSpotCRMCompaniesV3Client(adapter);

// Create a company
var company = await client.Crm.V3.Objects.Companies.PostAsync(new()
{
    Properties = new() { AdditionalData = new() { ["name"] = "Acme Corp" } }
});

Console.WriteLine($"Created company: {company!.Entity!.Id}");
```

## Testing with the Mock Server

Pair this package with `DamianH.HubSpot.MockServer` to run tests without hitting the real HubSpot API:

```csharp
await using var server = await HubSpotMockServer.StartNew();

var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
{
    BaseUrl = server.Uri.ToString()
};

var client = new HubSpotCRMCompaniesV3Client(adapter);
// Use client in tests — no real API calls made
```

## Supported API Categories

Clients are generated per HubSpot API area:

- **CRM** — Companies, Contacts, Deals, Line Items, Products, Tickets, Quotes, Goals, Calls, Emails, Meetings, Notes, Tasks, Postal Mail, Communications
- **CRM Associations** — V3 & V4 association APIs
- **CRM Properties** — Property definitions and groups
- **CRM Pipelines** — Deals and tickets pipelines
- **CRM Owners** — Owner listing
- **CRM Schemas** — Custom object schema management
- **Marketing** — Transactional email, marketing events, marketing emails, forms, campaigns
- **CMS** — Blog posts, authors, pages, HubDB, domains, URL redirects, source code, tags
- **Conversations** — Visitor identification, custom channels
- **Automation** — Workflows
- **Files** — File upload and management
- **Webhooks** — Subscriptions and settings
- **Events** — Custom analytics events
- **Business Units**, **User Provisioning**, **Account Info**, **Multicurrency**, **Tax Rates**
- **Extensions** — Calling, CRM Cards, Video Conferencing, Transcription

## Regenerating Clients

The clients in this package are generated from HubSpot's OpenAPI catalog. To regenerate:

```bash
./generate.ps1
```

See the [repository](https://github.com/damianh/hubspot) for full details.
