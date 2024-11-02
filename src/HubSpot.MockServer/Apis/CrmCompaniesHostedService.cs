using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer.Apis;

internal class CrmCompaniesHostedService(
    HubSpotObjectRepository     hubSpotObjectRepository,
    LoggerFactoryLoggerProvider loggerProvider)
    : ApiHostedService(loggerProvider)
{
    protected override void ConfigureApp(WebApplication app)
    {
        /*app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            using var reader   = new StreamReader(context.Request.Body, Encoding.UTF8);
            var       jsonBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            await next(context);
        });*/

        app.MapGet("", (
            [FromQuery] string?   after      = null,
            [FromQuery] int       limit      = 10,
            [FromQuery] string[]? properties = null) =>
        {
            
        });

        app.MapGet("/{companyId}", (
            [FromRoute] int    companyId,
            [FromQuery] string[]? properties = null) =>
        {
            var id    = HubSpotObjectId.From(companyId);
            var found = hubSpotObjectRepository.TryRead(id, out var hubSpotObject);

            if (!found)
            {
                return Results.NotFound();
            }

            // filter hubspotObject.Properties by properties
            var filteredProperties = properties is null
                ? hubSpotObject!.Properties
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value);

            // convert hubSpotObject to SimplePublicObject
            var simplePublicObject = new SimplePublicObject
            {
                Id         = hubSpotObject!.Id.Value.ToString(),
                CreatedAt  = hubSpotObject.CreatedAt,
                UpdatedAt  = hubSpotObject.UpdatedAt,
                Properties = filteredProperties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived   = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        }).WithOpenApi();

        app.MapPost("", (
            [FromBody]     SimplePublicObjectInputForCreate   inputForCreate,
            [FromServices] ILogger<CrmCompaniesHostedService> logger) =>
        {
            // convert request Associations to HubSpotAssociations
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
            var hubSpotObject               = hubSpotObjectRepository.Create(newHubSpotObject);
            var simplePublicObject = new SimplePublicObject
            {
                Id         = hubSpotObject.Id.Value.ToString(),
                CreatedAt  = hubSpotObject.CreatedAt,
                UpdatedAt  = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived   = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Created($"/{simplePublicObject.Id}", simplePublicObject);
        }).WithOpenApi();

        app.MapPatch("/{companyId}", (
            [FromRoute] string                     companyId,
            [FromBody]  SimplePublicObjectInput inputForUpdate) =>
        {
            var id    = HubSpotObjectId.From(companyId);
            var found = hubSpotObjectRepository.TryRead(id, out var hubSpotObject);

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

            hubSpotObjectRepository.Update(hubSpotObject!);
            hubSpotObjectRepository.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);
            var simplePublicObject = new SimplePublicObject
            {
                Id         = updatedHubSpotObject!.Id.Value.ToString(),
                CreatedAt  = updatedHubSpotObject.CreatedAt,
                UpdatedAt  = updatedHubSpotObject.UpdatedAt,
                Properties = updatedHubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived   = updatedHubSpotObject.Archived,
                ArchivedAt = updatedHubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        }).WithOpenApi();
    }
}
