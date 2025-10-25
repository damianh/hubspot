using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

public class HubSpotMockServer : IAsyncDisposable
{
    private readonly WebApplication _app;

    private HubSpotMockServer(WebApplication app, Uri uri)
    {
        _app = app;
        Uri = uri;
    }

    public Uri Uri { get; private set; }

    public static async Task<HubSpotMockServer> StartNew(ILoggerFactory loggerFactory)
    {
        var provider = new LoggerFactoryLoggerProvider(loggerFactory);
        var builder = WebApplication.CreateBuilder([]);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(provider);
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        // Configure web host
        builder.WebHost.ConfigureKestrel(options => { options.Listen(System.Net.IPAddress.Loopback, 0); });

        // Configure services
        builder.Services
            .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
            .AddSingleton<LoggerFactoryLoggerProvider>()
            .AddSingleton<HubSpotObjectRepository>()
            .AddSingleton<HubSpotObjectIdGenerator>()
            .AddSingleton(TimeProvider.System);

        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Register API route groups
        RegisterCrmCompaniesApi(app);
        // RegisterCrmContactsApi(app);
        // RegisterCrmDealsApi(app);
        // ... other APIs

        await app.StartAsync();

        var addresses = app.Urls;
        var uri = new Uri(addresses.First());

        return new HubSpotMockServer(app, uri);
    }

    private static void RegisterCrmCompaniesApi(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/companies")
            .WithTags("Companies");

        // GET /crm/v3/objects/companies - List companies
        group.MapGet("", (
            [FromServices] HubSpotObjectRepository repo,
            [FromQuery] int limit = 10,
            [FromQuery] string? after = null,
            [FromQuery] bool archived = false,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null) =>
        {
            int? afterId = null;
            if (!string.IsNullOrEmpty(after) && int.TryParse(after, out var parsedAfter))
            {
                afterId = parsedAfter;
            }

            var objects = repo.List(limit, afterId, archived);

            var results = objects.Select(obj =>
            {
                var filteredProperties = properties is null || properties.Length == 0
                    ? obj.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                    : obj.Properties
                        .Where(p => properties.Contains(p.Key))
                        .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

                return new SimplePublicObject
                {
                    Id = obj.Id.Value.ToString(),
                    CreatedAt = obj.CreatedAt,
                    UpdatedAt = obj.UpdatedAt,
                    Properties = filteredProperties,
                    Archived = obj.Archived,
                    ArchivedAt = obj.ArchivedAt
                };
            }).ToList();

            var response = new CollectionResponseSimplePublicObject
            {
                Results = results
            };

            // Add paging if there might be more results
            if (results.Count == limit)
            {
                var lastId = results.Last().Id;
                response.Paging = new Paging
                {
                    Next = new NextPage
                    {
                        After = lastId
                    }
                };
            }

            return Results.Ok(response);
        });

        // GET /crm/v3/objects/companies/{companyId} - Get company by ID
        group.MapGet("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null,
            [FromQuery] bool archived = false) =>
        {
            var id = HubSpotObjectId.From(companyId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>() // Return empty properties if none requested
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/companies - Create company
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            [FromServices] HubSpotObjectRepository repo) =>
        {
            var hubSpotAssociations = inputForCreate.Associations
                .Select(association => new
                {
                    Association = association,
                    To = new HubSpotAssociationTo(association.To.Id)
                })
                .Select(to => new
                {
                    To = to,
                    AssociationTypes = to.Association
                        .Types
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            var createdResponse = new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/companies/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/companies/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/companies/{companyId} - Update company
        group.MapPatch("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var id = HubSpotObjectId.From(companyId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found)
            {
                return Results.NotFound();
            }

            foreach (var (key, value) in inputForUpdate.Properties)
            {
                if (hubSpotObject!.Properties.TryGetValue(key, out var property))
                {
                    property.NewValue = value;
                }
                else
                {
                    var hubSpotProperty = new HubSpotObjectProperty(key, [])
                    {
                        NewValue = value
                    };
                    hubSpotObject!.Properties.Add(key, hubSpotProperty);
                }
            }

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = updatedHubSpotObject!.Id.Value.ToString(),
                CreatedAt = updatedHubSpotObject.CreatedAt,
                UpdatedAt = updatedHubSpotObject.UpdatedAt,
                Properties = updatedHubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = updatedHubSpotObject.Archived,
                ArchivedAt = updatedHubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // DELETE /crm/v3/objects/companies/{companyId} - Archive company
        group.MapDelete("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId) =>
        {
            var id = HubSpotObjectId.From(companyId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }

    public async ValueTask DisposeAsync()
    {
        await _app.StopAsync();
        await _app.DisposeAsync();
    }
}
