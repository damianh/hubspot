using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static void RegisterCmsContentAuditApi(this IEndpointRouteBuilder app, ContentAuditRepository repository)
    {
        var group = app.MapGroup("/cms/v3/audit-logs");

        group.MapGet("/", (HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;
            var objectId = context.Request.Query["objectId"].ToString();
            var userId = context.Request.Query["userId"].ToString();
            
            List<ContentAuditEntry> entries;
            if (!string.IsNullOrEmpty(objectId))
            {
                entries = repository.GetByObjectId(objectId);
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                entries = repository.GetByUserId(userId);
            }
            else
            {
                entries = repository.GetAll(offset, limit);
            }
            
            var total = repository.Count();
            
            return Results.Ok(new
            {
                total,
                results = entries.Select(MapAuditEntryToResponse)
            });
        });
    }

    private static object MapAuditEntryToResponse(ContentAuditEntry entry)
    {
        return new
        {
            id = entry.Id,
            timestamp = entry.Timestamp,
            eventType = entry.EventType,
            objectType = entry.ObjectType,
            objectId = entry.ObjectId,
            userId = entry.UserId,
            userEmail = entry.UserEmail,
            changes = entry.Changes
        };
    }
}
