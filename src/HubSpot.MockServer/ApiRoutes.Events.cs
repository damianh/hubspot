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

        // POST /events/v3/send - Send single analytics event
        events.MapPost("/send", (
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] EventRepository repo) =>
        {
            var customEvent = new CustomEvent
            {
                EventName = request.GetProperty("eventName").GetString()!,
                Email = request.TryGetProperty("email", out var email) ? email.GetString() : null,
                ObjectId = request.TryGetProperty("objectId", out var oid) ? oid.GetString() : null,
                ObjectType = request.TryGetProperty("objectType", out var ot) ? ot.GetString() : null,
                OccurredAt = request.TryGetProperty("occurredAt", out var occurred) 
                    ? DateTime.Parse(occurred.GetString()!) 
                    : DateTime.UtcNow,
                Properties = request.TryGetProperty("properties", out var props)
                    ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(props.GetRawText())
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

        // POST /events/v3/send/batch - Send batch analytics events
        events.MapPost("/send/batch", (
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] EventRepository repo) =>
        {
            var eventsToSend = new List<CustomEvent>();
            
            foreach (var input in request.GetProperty("inputs").EnumerateArray())
            {
                var customEvent = new CustomEvent
                {
                    EventName = input.GetProperty("eventName").GetString()!,
                    Email = input.TryGetProperty("email", out var email) ? email.GetString() : null,
                    ObjectId = input.TryGetProperty("objectId", out var oid) ? oid.GetString() : null,
                    ObjectType = input.TryGetProperty("objectType", out var ot) ? ot.GetString() : null,
                    OccurredAt = input.TryGetProperty("occurredAt", out var occurred) 
                        ? DateTime.Parse(occurred.GetString()!) 
                        : DateTime.UtcNow,
                    Properties = input.TryGetProperty("properties", out var props)
                        ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(props.GetRawText())
                        : null
                };
                
                repo.SendEvent(customEvent);
                eventsToSend.Add(customEvent);
            }

            return Results.Ok(new
            {
                results = eventsToSend.Select(e => new
                {
                    id = e.Id,
                    eventName = e.EventName,
                    occurredAt = e.OccurredAt
                })
            });
        });

        // POST /events/v3/events - Create custom behavioral event
        events.MapPost("/events", (
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] EventRepository repo) =>
        {
            var customEvent = new CustomEvent
            {
                EventName = request.GetProperty("eventName").GetString()!,
                ObjectId = request.TryGetProperty("objectId", out var oid) ? oid.GetString() : null,
                ObjectType = request.TryGetProperty("objectType", out var ot) ? ot.GetString() : "contact",
                OccurredAt = request.TryGetProperty("occurredAt", out var occurred) 
                    ? DateTime.Parse(occurred.GetString()!) 
                    : DateTime.UtcNow,
                Properties = request.TryGetProperty("properties", out var props)
                    ? System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(props.GetRawText())
                    : null
            };

            repo.SendEvent(customEvent);
            return Results.Ok(new
            {
                id = customEvent.Id,
                eventName = customEvent.EventName,
                objectId = customEvent.ObjectId,
                objectType = customEvent.ObjectType,
                occurredAt = customEvent.OccurredAt
            });
        });

        // GET /events/v3/events - List events for an object
        events.MapGet("/events", (
            [FromQuery] string? objectType,
            [FromQuery] string? objectId,
            [FromServices] EventRepository repo) =>
        {
            var allEvents = repo.GetAllEvents();
            
            if (!string.IsNullOrEmpty(objectType))
            {
                allEvents = allEvents.Where(e => e.ObjectType == objectType).ToList();
            }

            if (!string.IsNullOrEmpty(objectId))
            {
                allEvents = allEvents.Where(e => e.ObjectId == objectId).ToList();
            }

            return Results.Ok(new
            {
                results = allEvents.Select(e => new
                {
                    id = e.Id,
                    eventName = e.EventName,
                    objectId = e.ObjectId,
                    objectType = e.ObjectType,
                    occurredAt = e.OccurredAt,
                    properties = e.Properties
                })
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
                    ? []
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
            {
                return Results.NotFound();
            }

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
