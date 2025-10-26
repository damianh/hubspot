using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static void RegisterCrmLists(WebApplication app)
    {
        var lists = app.MapGroup("/crm/v3/lists");

        lists.MapGet("/", (
            [FromServices] ListRepository repo) =>
        {
            var allLists = repo.GetAllLists();
            return Results.Ok(new
            {
                lists = allLists.Select(l => new
                {
                    listId = l.Id,
                    name = l.Name,
                    objectTypeId = l.ObjectTypeId,
                    processingType = l.ProcessingType,
                    createdAt = l.CreatedAt,
                    updatedAt = l.UpdatedAt,
                    filterBranch = l.FilterBranch
                })
            });
        });

        lists.MapPost("/", (
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] ListRepository repo) =>
        {
            var list = new ListDefinition
            {
                Name = request.GetProperty("name").GetString(),
                ObjectTypeId = request.TryGetProperty("objectTypeId", out var oti) ? oti.GetString() : "0-1",
                ProcessingType = request.TryGetProperty("processingType", out var pt) ? pt.GetString() : "MANUAL",
                FilterBranch = request.TryGetProperty("filterBranch", out var fb) ? System.Text.Json.JsonSerializer.Deserialize<object>(fb.GetRawText()) : null
            };

            var created = repo.CreateList(list);
            return Results.Ok(new
            {
                listId = created.Id,
                name = created.Name,
                objectTypeId = created.ObjectTypeId,
                processingType = created.ProcessingType,
                createdAt = created.CreatedAt,
                updatedAt = created.UpdatedAt,
                filterBranch = created.FilterBranch
            });
        });

        lists.MapGet("/{listId}", (
            string listId,
            [FromServices] ListRepository repo) =>
        {
            var list = repo.GetList(listId);
            if (list == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                listId = list.Id,
                name = list.Name,
                objectTypeId = list.ObjectTypeId,
                processingType = list.ProcessingType,
                createdAt = list.CreatedAt,
                updatedAt = list.UpdatedAt,
                filterBranch = list.FilterBranch
            });
        });

        lists.MapPatch("/{listId}", (
            string listId,
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] ListRepository repo) =>
        {
            var updates = new ListDefinition
            {
                Name = request.TryGetProperty("name", out var name) ? name.GetString() : null,
                FilterBranch = request.TryGetProperty("filterBranch", out var fb) ? System.Text.Json.JsonSerializer.Deserialize<object>(fb.GetRawText()) : null
            };

            var updated = repo.UpdateList(listId, updates);
            if (updated == null)
                return Results.NotFound();

            return Results.Ok(new
            {
                listId = updated.Id,
                name = updated.Name,
                objectTypeId = updated.ObjectTypeId,
                processingType = updated.ProcessingType,
                createdAt = updated.CreatedAt,
                updatedAt = updated.UpdatedAt,
                filterBranch = updated.FilterBranch
            });
        });

        lists.MapDelete("/{listId}", (
            string listId,
            [FromServices] ListRepository repo) =>
        {
            var deleted = repo.DeleteList(listId);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        lists.MapPut("/{listId}/memberships/add", (
            string listId,
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] ListRepository repo) =>
        {
            List<string> recordIds = new();
            foreach (var id in request.GetProperty("recordIds").EnumerateArray())
                recordIds.Add(id.GetString()!);

            repo.AddMemberships(listId, recordIds);
            return Results.Ok();
        });

        lists.MapPut("/{listId}/memberships/remove", (
            string listId,
            [FromBody] System.Text.Json.JsonElement request,
            [FromServices] ListRepository repo) =>
        {
            List<string> recordIds = new();
            foreach (var id in request.GetProperty("recordIds").EnumerateArray())
                recordIds.Add(id.GetString()!);

            repo.RemoveMemberships(listId, recordIds);
            return Results.Ok();
        });

        lists.MapGet("/{listId}/memberships", (
            string listId,
            [FromServices] ListRepository repo) =>
        {
            var members = repo.GetMemberships(listId);
            return Results.Ok(new { results = members });
        });
    }
}
