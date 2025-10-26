using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    /// <summary>
    /// Registers standard CRUD endpoints for a CRM object type.
    /// This follows the HubSpot pattern: /crm/v3/objects/{objectType}
    /// </summary>
    /// <param name="app">The web application</param>
    /// <param name="route">The route path (e.g., "companies", "contacts", "0-3" for deals)</param>
    /// <param name="tag">The OpenAPI tag name for documentation</param>
    /// <param name="idParameterName">The name of the ID parameter (e.g., "companyId", "contactId")</param>
    internal static void RegisterStandardCrmObject(
        WebApplication app,
        string route,
        string tag,
        string idParameterName)
    {
        var group = app.MapGroup($"/crm/v3/objects/{route}")
            .WithTags(tag);

        // GET /crm/v3/objects/{objectType} - List objects
        group.MapGet("", (
            HubSpotObjectRepository repo,
            int limit = 10,
            string? after = null,
            bool archived = false,
            string[]? properties = null,
            string[]? propertiesWithHistory = null,
            string[]? associations = null) =>
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

        // GET /crm/v3/objects/{objectType}/{objectId} - Get object by ID
        group.MapGet($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo,
            string[]? properties = null,
            string[]? propertiesWithHistory = null,
            string[]? associations = null,
            bool archived = false) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>()
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

        // POST /crm/v3/objects/{objectType} - Create object
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            HubSpotObjectRepository repo) =>
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
                Location = $"/crm/v3/objects/{route}/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/{route}/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/{objectType}/{objectId} - Update object
        group.MapPatch($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
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

        // DELETE /crm/v3/objects/{objectType}/{objectId} - Archive object
        group.MapDelete($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }
}
