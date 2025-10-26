using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class Properties
    {
        internal static void RegisterPropertyValidationsV3(WebApplication app)
        {
            var group = app.MapGroup("/crm/v3/properties/{objectType}/{propertyName}/validations")
                .WithTags("Property Validations V3");

            group.MapGet("", async (string objectType, string propertyName, PropertyValidationRepository repo) =>
            {
                var validations = await repo.GetValidationsAsync(objectType, propertyName);
                return Results.Ok(new { results = validations });
            });

            group.MapPost("", async (string objectType, string propertyName, JsonElement body, PropertyValidationRepository repo) =>
            {
                var validation = await repo.CreateValidationAsync(objectType, propertyName, body);
                var validationId = validation.TryGetProperty("id", out var id) ? id.GetString() : "";
                return Results.Created($"/crm/v3/properties/{objectType}/{propertyName}/validations/{validationId}", validation);
            });

            group.MapPatch("/{validationId}", async (string objectType, string propertyName, string validationId, JsonElement body, PropertyValidationRepository repo) =>
            {
                var validation = await repo.UpdateValidationAsync(objectType, propertyName, validationId, body);
                return validation.HasValue ? Results.Ok(validation.Value) : Results.NotFound();
            });

            group.MapDelete("/{validationId}", async (string objectType, string propertyName, string validationId, PropertyValidationRepository repo) =>
            {
                var deleted = await repo.DeleteValidationAsync(objectType, propertyName, validationId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }

        /// <summary>
        /// Register CRM Properties V3 API routes
        /// </summary>
        internal static void RegisterPropertiesV3(WebApplication app)
        {
            var group = app.MapGroup("/crm/v3/properties/{objectType}")
                .WithTags("Properties V3");

            // Get all properties for an object type
            group.MapGet("", (
                [FromRoute] string objectType,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var properties = repo.GetProperties(objectType);

                var response = new
                {
                    results = properties.Select(p => new
                    {
                        name = p.Name,
                        label = p.Label,
                        type = p.Type,
                        fieldType = p.FieldType,
                        groupName = p.GroupName,
                        description = p.Description,
                        options = p.Options?.Select(o => new { label = o, value = o }).ToArray(),
                        hidden = p.Hidden,
                        displayOrder = p.DisplayOrder,
                        createdAt = p.CreatedAt,
                        updatedAt = p.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create a new property
            group.MapPost("", (
                [FromRoute] string objectType,
                [FromBody] CreatePropertyRequest request,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var property = repo.CreateProperty(
                    objectType,
                    request.Name,
                    request.Label,
                    request.Type,
                    request.FieldType,
                    request.GroupName,
                    request.Description,
                    request.Options);

                var response = new
                {
                    name = property.Name,
                    label = property.Label,
                    type = property.Type,
                    fieldType = property.FieldType,
                    groupName = property.GroupName,
                    description = property.Description,
                    options = property.Options?.Select(o => new { label = o, value = o }).ToArray(),
                    hidden = property.Hidden,
                    displayOrder = property.DisplayOrder,
                    createdAt = property.CreatedAt,
                    updatedAt = property.UpdatedAt
                };

                return Results.Created($"/crm/v3/properties/{objectType}/{property.Name}", response);
            });

            // Get a specific property
            group.MapGet("/{propertyName}", (
                [FromRoute] string objectType,
                [FromRoute] string propertyName,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var property = repo.GetProperty(objectType, propertyName);
                if (property == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    name = property.Name,
                    label = property.Label,
                    type = property.Type,
                    fieldType = property.FieldType,
                    groupName = property.GroupName,
                    description = property.Description,
                    options = property.Options?.Select(o => new { label = o, value = o }).ToArray(),
                    hidden = property.Hidden,
                    displayOrder = property.DisplayOrder,
                    createdAt = property.CreatedAt,
                    updatedAt = property.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Update a property
            group.MapPatch("/{propertyName}", (
                [FromRoute] string objectType,
                [FromRoute] string propertyName,
                [FromBody] UpdatePropertyRequest request,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var property = repo.UpdateProperty(
                    objectType,
                    propertyName,
                    request.Label,
                    request.Description,
                    request.GroupName);

                if (property == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    name = property.Name,
                    label = property.Label,
                    type = property.Type,
                    fieldType = property.FieldType,
                    groupName = property.GroupName,
                    description = property.Description,
                    options = property.Options?.Select(o => new { label = o, value = o }).ToArray(),
                    hidden = property.Hidden,
                    displayOrder = property.DisplayOrder,
                    createdAt = property.CreatedAt,
                    updatedAt = property.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Delete a property
            group.MapDelete("/{propertyName}", (
                [FromRoute] string objectType,
                [FromRoute] string propertyName,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var deleted = repo.DeleteProperty(objectType, propertyName);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            // Property Groups endpoints
            var groupsGroup = app.MapGroup("/crm/v3/properties/{objectType}/groups")
                .WithTags("Property Groups V3");

            // Get all property groups
            groupsGroup.MapGet("", (
                [FromRoute] string objectType,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var groups = repo.GetGroups(objectType);

                var response = new
                {
                    results = groups.Select(g => new
                    {
                        name = g.Name,
                        label = g.Label,
                        displayOrder = g.DisplayOrder,
                        createdAt = g.CreatedAt,
                        updatedAt = g.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create property group
            groupsGroup.MapPost("", (
                [FromRoute] string objectType,
                [FromBody] CreatePropertyGroupRequest request,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var group = repo.CreateGroup(
                    objectType,
                    request.Name,
                    request.Label,
                    request.DisplayOrder);

                var response = new
                {
                    name = group.Name,
                    label = group.Label,
                    displayOrder = group.DisplayOrder,
                    createdAt = group.CreatedAt,
                    updatedAt = group.UpdatedAt
                };

                return Results.Created($"/crm/v3/properties/{objectType}/groups/{group.Name}", response);
            });

            // Get specific group
            groupsGroup.MapGet("/{groupName}", (
                [FromRoute] string objectType,
                [FromRoute] string groupName,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var group = repo.GetGroup(objectType, groupName);
                if (group == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    name = group.Name,
                    label = group.Label,
                    displayOrder = group.DisplayOrder,
                    createdAt = group.CreatedAt,
                    updatedAt = group.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Delete property group
            groupsGroup.MapDelete("/{groupName}", (
                [FromRoute] string objectType,
                [FromRoute] string groupName,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var deleted = repo.DeleteGroup(objectType, groupName);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }

        /// <summary>
        /// Register CRM Properties V202509 API routes
        /// </summary>
        internal static void RegisterPropertiesV202509(WebApplication app)
        {
            var group = app.MapGroup("/crm/v202509/properties/{objectType}")
                .WithTags("Properties V202509");

            // Same implementation as V3
            group.MapGet("", (
                [FromRoute] string objectType,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var properties = repo.GetProperties(objectType);

                var response = new
                {
                    results = properties.Select(p => new
                    {
                        name = p.Name,
                        label = p.Label,
                        type = p.Type,
                        fieldType = p.FieldType,
                        groupName = p.GroupName,
                        description = p.Description,
                        options = p.Options?.Select(o => new { label = o, value = o }).ToArray(),
                        hidden = p.Hidden,
                        displayOrder = p.DisplayOrder,
                        createdAt = p.CreatedAt,
                        updatedAt = p.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            group.MapPost("", (
                [FromRoute] string objectType,
                [FromBody] CreatePropertyRequest request,
                [FromServices] PropertyDefinitionRepository repo) =>
            {
                var property = repo.CreateProperty(
                    objectType,
                    request.Name,
                    request.Label,
                    request.Type,
                    request.FieldType,
                    request.GroupName,
                    request.Description,
                    request.Options);

                var response = new
                {
                    name = property.Name,
                    label = property.Label,
                    type = property.Type,
                    fieldType = property.FieldType,
                    groupName = property.GroupName,
                    description = property.Description,
                    options = property.Options?.Select(o => new { label = o, value = o }).ToArray(),
                    hidden = property.Hidden,
                    displayOrder = property.DisplayOrder,
                    createdAt = property.CreatedAt,
                    updatedAt = property.UpdatedAt
                };

                return Results.Created($"/crm/v202509/properties/{objectType}/{property.Name}", response);
            });
        }

        // Request models
        private record CreatePropertyRequest(
            string Name,
            string Label,
            string Type,
            string FieldType,
            string? GroupName = null,
            string? Description = null,
            string[]? Options = null);

        private record UpdatePropertyRequest(
            string? Label = null,
            string? Description = null,
            string? GroupName = null);

        private record CreatePropertyGroupRequest(
            string Name,
            string Label,
            int DisplayOrder = 0);
    }
}
