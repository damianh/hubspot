# HubSpot Client

These HubSpot clients are generated using [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/overview).

Generating the clients requires the [.NET Tool](https://learn.microsoft.com/en-us/openapi/kiota/install?tabs=bash#install-as-net-tool).

[HubSpot OpenApi Catalog](https://api.hubspot.com/api-catalog-public/v1/apis)

Each HubSpot OpenAPI definition is segrated into an Area and a Feature and we use this to apply 
a folder convention `AREA/Feature` to the generated clients.

- To regenerate the client run `./generate.ps1`
- To add more API clients, edit `generate.ps1` accordingly