using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterTimelineApi(WebApplication app, TimelineRepository timelineRepo)
    {
        var v3 = app.MapGroup("/crm/v3/timeline");

        // Event Templates
        var templates = v3.MapGroup("/event-templates");

        // GET /crm/v3/timeline/events/templates - List all templates (alternate path)
        var eventsTemplates = v3.MapGroup("/events/templates");
        eventsTemplates.MapGet("/", ([FromQuery] string? appId) =>
        {
            var allTemplates = timelineRepo.ListEventTemplates();
            return Results.Ok(new { results = allTemplates });
        });

        // POST /crm/v3/timeline/events/templates - Create template (alternate path)
        eventsTemplates.MapPost("/", ([FromBody] TimelineEventTemplateRequest request) =>
        {
            var template = timelineRepo.CreateEventTemplate(
                request.Name,
                request.ObjectType,
                request.Tokens,
                request.HeaderTemplate,
                request.DetailTemplate
            );

            return Results.Created($"/crm/v3/timeline/event-templates/{template.Id}", template);
        });

        // POST /crm/v3/timeline/event-templates
        templates.MapPost("/", ([FromBody] TimelineEventTemplateRequest request) =>
        {
            var template = timelineRepo.CreateEventTemplate(
                request.Name,
                request.ObjectType,
                request.Tokens,
                request.HeaderTemplate,
                request.DetailTemplate
            );

            return Results.Created($"/crm/v3/timeline/event-templates/{template.Id}", template);
        });

        // GET /crm/v3/timeline/event-templates/{templateId}
        templates.MapGet("/{templateId}", (string templateId) =>
        {
            var template = timelineRepo.GetEventTemplate(templateId);
            if (template == null)
            {
                return Results.NotFound(new { message = $"Event template not found: {templateId}" });
            }

            return Results.Ok(template);
        });

        // PUT /crm/v3/timeline/event-templates/{templateId}
        templates.MapPut("/{templateId}", (string templateId, [FromBody] TimelineEventTemplateUpdateRequest request) =>
        {
            var template = timelineRepo.UpdateEventTemplate(
                templateId,
                request.Name,
                request.Tokens,
                request.HeaderTemplate,
                request.DetailTemplate
            );

            if (template == null)
            {
                return Results.NotFound(new { message = $"Event template not found: {templateId}" });
            }

            return Results.Ok(template);
        });

        // DELETE /crm/v3/timeline/event-templates/{templateId}
        templates.MapDelete("/{templateId}", (string templateId) =>
        {
            var deleted = timelineRepo.DeleteEventTemplate(templateId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Event template not found: {templateId}" });
            }

            return Results.NoContent();
        });

        // Events
        var events = v3.MapGroup("/events");

        // POST /crm/v3/timeline/events
        events.MapPost("/", ([FromBody] TimelineEventRequest request) =>
        {
            var timelineEvent = timelineRepo.CreateEvent(
                request.EventTemplateId,
                request.ObjectType,
                request.ObjectId,
                request.Tokens,
                request.Timestamp
            );

            return Results.Created($"/crm/v3/timeline/events/{timelineEvent.Id}", timelineEvent);
        });

        // GET /crm/v3/timeline/events/{objectType}/{objectId}
        events.MapGet("/{objectType}/{objectId}", (string objectType, string objectId) =>
        {
            var eventsList = timelineRepo.ListEvents(objectType, objectId);
            
            return Results.Ok(new { results = eventsList });
        });

        // GET /crm/v3/timeline/events/{eventId} - Get specific event by ID
        events.MapGet("/{eventId}", (string eventId) =>
        {
            var timelineEvent = timelineRepo.GetEvent(eventId);
            if (timelineEvent == null)
            {
                return Results.NotFound(new { message = $"Event not found: {eventId}" });
            }

            return Results.Ok(timelineEvent);
        });

        // DELETE /crm/v3/timeline/events/{eventId}
        events.MapDelete("/{eventId}", (string eventId) =>
        {
            var deleted = timelineRepo.DeleteEvent(eventId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Event not found: {eventId}" });
            }

            return Results.NoContent();
        });
    }
}

public record TimelineEventTemplateRequest(
    string Name,
    string ObjectType,
    List<EventToken> Tokens,
    string HeaderTemplate,
    string? DetailTemplate = null
);

public record TimelineEventTemplateUpdateRequest(
    string? Name = null,
    List<EventToken>? Tokens = null,
    string? HeaderTemplate = null,
    string? DetailTemplate = null
);

public record TimelineEventRequest(
    string EventTemplateId,
    string ObjectType,
    string ObjectId,
    Dictionary<string, string> Tokens,
    DateTimeOffset? Timestamp = null
);
