using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static class Scheduler
    {
        public static void RegisterSchedulerMeetingsV3Api(WebApplication app)
        {
            var linksGroup = app.MapGroup("/scheduler/v3/meetings/links")
                .WithTags("Scheduler Meetings V3");

            linksGroup.MapGet("", async (SchedulerMeetingRepository repository) =>
            {
                var links = await repository.GetAllLinksAsync();
                return Results.Ok(new { results = links });
            });

            linksGroup.MapGet("/{linkId}", async (string linkId, SchedulerMeetingRepository repository) =>
            {
                var link = await repository.GetLinkAsync(linkId);
                return link.HasValue ? Results.Ok(link.Value) : Results.NotFound();
            });

            linksGroup.MapPost("", async (JsonElement body, SchedulerMeetingRepository repository) =>
            {
                var link = await repository.CreateLinkAsync(body);
                var id = link.TryGetProperty("id", out var idProp) ? idProp.GetString() : "";
                return Results.Created($"/scheduler/v3/meetings/links/{id}", link);
            });

            linksGroup.MapPatch("/{linkId}", async (string linkId, JsonElement body, SchedulerMeetingRepository repository) =>
            {
                var link = await repository.UpdateLinkAsync(linkId, body);
                return link.HasValue ? Results.Ok(link.Value) : Results.NotFound();
            });

            linksGroup.MapDelete("/{linkId}", async (string linkId, SchedulerMeetingRepository repository) =>
            {
                var deleted = await repository.DeleteLinkAsync(linkId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            var availabilityGroup = app.MapGroup("/scheduler/v3/meetings/availability")
                .WithTags("Scheduler Availability V3");

            availabilityGroup.MapGet("/{linkId}", async (
                string linkId, 
                string? startDate, 
                string? endDate, 
                SchedulerMeetingRepository repository) =>
            {
                var start = startDate != null ? DateTimeOffset.Parse(startDate) : DateTimeOffset.UtcNow;
                var end = endDate != null ? DateTimeOffset.Parse(endDate) : start.AddDays(7);
                
                var availability = await repository.GetAvailabilityAsync(linkId, start, end);
                return Results.Ok(availability);
            });
        }
    }
}
