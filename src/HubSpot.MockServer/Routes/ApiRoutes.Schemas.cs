using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterSchemasApi(WebApplication app)
    {
        var v3 = app.MapGroup("/crm/v3/schemas");

        // GET /crm/v3/schemas
        v3.MapGet("", ([FromServices]SchemaRepository schemaRepo, bool ? archived) =>
        {
            var schemas = schemaRepo.ListSchemas(archived ?? false);
            return Results.Ok(new { results = schemas });
        });

        // GET /crm/v3/schemas/{objectType}
        v3.MapGet("{objectType}", (SchemaRepository schemaRepo, string objectType) =>
        {
            var schema = schemaRepo.GetSchema(objectType);
            if (schema == null)
            {
                return Results.NotFound(new { message = $"Schema not found for object type: {objectType}" });
            }

            var properties = schemaRepo.GetProperties(objectType);
            var associations = schemaRepo.GetAssociationDefinitions(objectType);

            return Results.Ok(new
            {
                id = schema.Id,
                name = schema.Name,
                labels = schema.Labels,
                primaryDisplayProperty = schema.PrimaryDisplayProperty,
                requiredProperties = schema.RequiredProperties,
                searchableProperties = schema.SearchableProperties,
                secondaryDisplayProperties = schema.SecondaryDisplayProperties,
                properties = properties,
                associations = associations,
                createdAt = schema.CreatedAt,
                updatedAt = schema.UpdatedAt,
                archived = schema.Archived
            });
        });

        // POST /crm/v3/schemas
        v3.MapPost("", (SchemaRepository schemaRepo, [FromBody] SchemaCreateRequest request) =>
        {
            var schema = schemaRepo.CreateSchema(
                request.Name,
                request.Labels,
                request.PrimaryDisplayProperty,
                request.RequiredProperties,
                request.SearchableProperties,
                request.SecondaryDisplayProperties
            );

            // Add properties if provided
            if (request.Properties != null)
            {
                foreach (var prop in request.Properties)
                {
                    schemaRepo.AddProperty(
                        request.Name,
                        prop.Name,
                        prop.Label,
                        prop.Type,
                        prop.FieldType,
                        prop.GroupName,
                        prop.Description,
                        prop.Options,
                        prop.DisplayOrder
                    );
                }
            }

            var properties = schemaRepo.GetProperties(request.Name);
            var associations = schemaRepo.GetAssociationDefinitions(request.Name);

            return Results.Created($"/crm/v3/schemas/{schema.Name}", new
            {
                id = schema.Id,
                name = schema.Name,
                labels = schema.Labels,
                primaryDisplayProperty = schema.PrimaryDisplayProperty,
                requiredProperties = schema.RequiredProperties,
                searchableProperties = schema.SearchableProperties,
                secondaryDisplayProperties = schema.SecondaryDisplayProperties,
                properties = properties,
                associations = associations,
                createdAt = schema.CreatedAt,
                updatedAt = schema.UpdatedAt,
                archived = schema.Archived
            });
        });

        // PATCH /crm/v3/schemas/{objectType}
        v3.MapPatch("{objectType}", (SchemaRepository schemaRepo, string objectType, [FromBody] SchemaUpdateRequest request) =>
        {
            var schema = schemaRepo.UpdateSchema(
                objectType,
                request.Labels,
                request.PrimaryDisplayProperty,
                request.RequiredProperties,
                request.SearchableProperties,
                request.SecondaryDisplayProperties
            );

            if (schema == null)
            {
                return Results.NotFound(new { message = $"Schema not found for object type: {objectType}" });
            }

            var properties = schemaRepo.GetProperties(objectType);
            var associations = schemaRepo.GetAssociationDefinitions(objectType);

            return Results.Ok(new
            {
                id = schema.Id,
                name = schema.Name,
                labels = schema.Labels,
                primaryDisplayProperty = schema.PrimaryDisplayProperty,
                requiredProperties = schema.RequiredProperties,
                searchableProperties = schema.SearchableProperties,
                secondaryDisplayProperties = schema.SecondaryDisplayProperties,
                properties = properties,
                associations = associations,
                createdAt = schema.CreatedAt,
                updatedAt = schema.UpdatedAt,
                archived = schema.Archived
            });
        });

        // DELETE /crm/v3/schemas/{objectType}
        v3.MapDelete("{objectType}", (SchemaRepository schemaRepo, string objectType) =>
        {
            var deleted = schemaRepo.DeleteSchema(objectType);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Schema not found for object type: {objectType}" });
            }

            return Results.NoContent();
        });

        // Association definitions
        var associations = v3.MapGroup("{objectType}/associations");

        // POST /crm/v3/schemas/{objectType}/associations
        associations.MapPost("", (SchemaRepository schemaRepo, string objectType, [FromBody] AssociationDefinitionRequest request) =>
        {
            var definition = schemaRepo.CreateAssociationDefinition(
                objectType,
                request.ToObjectType,
                request.Name,
                request.Label
            );

            return Results.Created($"/crm/v3/schemas/{objectType}/associations/{definition.Id}", definition);
        });

        // DELETE /crm/v3/schemas/{objectType}/associations/{associationId}
        associations.MapDelete("{associationId}", (SchemaRepository schemaRepo, string objectType, string associationId) =>
        {
            var deleted = schemaRepo.DeleteAssociationDefinition(objectType, associationId);
            return !deleted
                ? Results.NotFound(new { message = $"Association definition not found: {associationId}" })
                : Results.NoContent();
        });
    }
}

internal record SchemaCreateRequest(
    string Name,
    Dictionary<string, string> Labels,
    string PrimaryDisplayProperty,
    List<string>? RequiredProperties = null,
    List<string>? SearchableProperties = null,
    List<string>? SecondaryDisplayProperties = null,
    List<PropertyCreateRequest>? Properties = null
);

internal record SchemaUpdateRequest(
    Dictionary<string, string>? Labels = null,
    string? PrimaryDisplayProperty = null,
    List<string>? RequiredProperties = null,
    List<string>? SearchableProperties = null,
    List<string>? SecondaryDisplayProperties = null
);

internal record PropertyCreateRequest(
    string Name,
    string Label,
    string Type,
    string FieldType,
    string? GroupName = null,
    string? Description = null,
    List<PropertyOption>? Options = null,
    int? DisplayOrder = null
);

internal record AssociationDefinitionRequest(
    string ToObjectType,
    string Name,
    string? Label = null
);
