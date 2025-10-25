using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static void RegisterEvents(WebApplication app)
    {
        var events = app.MapGroup("/events/v3");

        events.MapPost("/send", (
            [FromBody] dynamic request,
            [FromServices] EventRepository repo) =>
        {
            var customEvent = new CustomEvent
            {
                EventName = request.eventName,
                ObjectId = request.objectId,
                ObjectType = request.objectType,
                OccurredAt = request.occurredAt != null 
                    ? DateTime.Parse(request.occurredAt.ToString()) 
                    : DateTime.UtcNow,
                Properties = request.properties != null
                    ? ((object)request.properties).ToString()!
                        .Split(',')
                        .Select(kv => kv.Split('='))
                        .ToDictionary(kv => kv[0], kv => (object)kv[1])
                    : null
            };

            repo.SendEvent(customEvent);
            return Results.Ok(new
            {
                id = customEvent.Id,
                eventName = customEvent.EventName,
                objectId = customEvent.ObjectId,
                occurredAt = customEvent.OccurredAt
            });
        });

        var definitions = app.MapGroup("/events/v3/event-definitions");

        definitions.MapGet("/", (
            [FromServices] EventRepository repo) =>
        {
            var allDefinitions = repo.GetAllDefinitions();
            return Results.Ok(new
            {
                results = allDefinitions.Select(d => new
                {
                    id = d.Id,
                    name = d.Name,
                    label = d.Label,
                    description = d.Description,
                    createdAt = d.CreatedAt,
                    propertyDefinitions = d.PropertyDefinitions?.Select(p => new
                    {
                        name = p.Name,
                        label = p.Label,
                        type = p.Type
                    })
                })
            });
        });

        definitions.MapPost("/", (
            [FromBody] dynamic request,
            [FromServices] EventRepository repo) =>
        {
            var definition = new EventDefinition
            {
                Name = request.name,
                Label = request.label,
                Description = request.description,
                PropertyDefinitions = request.propertyDefinitions != null
                    ? new List<EventPropertyDefinition>()
                    : null
            };

            if (request.propertyDefinitions != null)
            {
                foreach (var prop in request.propertyDefinitions)
                {
                    definition.PropertyDefinitions!.Add(new EventPropertyDefinition
                    {
                        Name = prop.name,
                        Label = prop.label,
                        Type = prop.type ?? "string"
                    });
                }
            }

            var created = repo.CreateDefinition(definition);
            return Results.Ok(new
            {
                id = created.Id,
                name = created.Name,
                label = created.Label,
                description = created.Description,
                createdAt = created.CreatedAt,
                propertyDefinitions = created.PropertyDefinitions?.Select(p => new
                {
                    name = p.Name,
                    label = p.Label,
                    type = p.Type
                })
            });
        });

        definitions.MapGet("/{definitionId}", (
            string definitionId,
            [FromServices] EventRepository repo) =>
        {
            var definition = repo.GetDefinition(definitionId);
            if (definition == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                id = definition.Id,
                name = definition.Name,
                label = definition.Label,
                description = definition.Description,
                createdAt = definition.CreatedAt,
                propertyDefinitions = definition.PropertyDefinitions?.Select(p => new
                {
                    name = p.Name,
                    label = p.Label,
                    type = p.Type
                })
            });
        });

        definitions.MapDelete("/{definitionId}", (
            string definitionId,
            [FromServices] EventRepository repo) =>
        {
            var deleted = repo.DeleteDefinition(definitionId);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}
