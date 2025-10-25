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
                results = allLists.Select(l => new
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
            [FromBody] dynamic request,
            [FromServices] ListRepository repo) =>
        {
            var list = new ListDefinition
            {
                Name = request.name,
                ObjectTypeId = request.objectTypeId ?? "0-1",
                ProcessingType = request.processingType ?? "MANUAL",
                FilterBranch = request.filterBranch
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

        lists.MapPut("/{listId}", (
            string listId,
            [FromBody] dynamic request,
            [FromServices] ListRepository repo) =>
        {
            var updates = new ListDefinition
            {
                Name = request.name,
                FilterBranch = request.filterBranch
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
            [FromBody] dynamic request,
            [FromServices] ListRepository repo) =>
        {
            List<string> recordIds = new();
            foreach (var id in request.recordIds)
                recordIds.Add(id.ToString());

            repo.AddMemberships(listId, recordIds);
            return Results.Ok();
        });

        lists.MapPut("/{listId}/memberships/remove", (
            string listId,
            [FromBody] dynamic request,
            [FromServices] ListRepository repo) =>
        {
            List<string> recordIds = new();
            foreach (var id in request.recordIds)
                recordIds.Add(id.ToString());

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
