using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.Tag;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static partial class CmsTags
    {
        public static void RegisterCmsTagsV3Api(WebApplication app)
        {
            var tags = app.MapGroup("/cms/v3/blogs/tags");

            // List tags
            tags.MapGet("/", (TagRepository repo, int? limit, string? after) =>
            {
                var results = repo.List(limit, after).ToList();
                return Results.Ok(new
                {
                    results = results.Select(t => new
                    {
                        id = t.Id,
                        name = t.Name,
                        language = t.Language,
                        created = t.Created,
                        updated = t.Updated
                    }),
                    paging = new
                    {
                        next = new { after = results.LastOrDefault()?.Id }
                    }
                });
            });

            // Create tag
            tags.MapPost("/", (TagRepository repo, TagCreateRequest request) =>
            {
                var tag = repo.Create(request.Name, request.Language);
                return Results.Ok(new
                {
                    id = tag.Id,
                    name = tag.Name,
                    language = tag.Language,
                    created = tag.Created,
                    updated = tag.Updated
                });
            });

            // Get tag
            tags.MapGet("/{objectId}", (TagRepository repo, string objectId) =>
            {
                var tag = repo.Get(objectId);
                if (tag == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    id = tag.Id,
                    name = tag.Name,
                    language = tag.Language,
                    created = tag.Created,
                    updated = tag.Updated
                });
            });

            // Update tag
            tags.MapPatch("/{objectId}", (TagRepository repo, string objectId, TagUpdateRequest request) =>
            {
                var tag = repo.Update(objectId, request.Name);
                if (tag == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    id = tag.Id,
                    name = tag.Name,
                    language = tag.Language,
                    created = tag.Created,
                    updated = tag.Updated
                });
            });

            // Delete tag
            tags.MapDelete("/{objectId}", (TagRepository repo, string objectId) =>
            {
                var deleted = repo.Delete(objectId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            // Batch endpoints
            RegisterBatchEndpoints(tags);

            // Multi-language endpoints
            RegisterMultiLanguageEndpoints(tags);
        }

        private static void RegisterBatchEndpoints(RouteGroupBuilder tags)
        {
            var batch = tags.MapGroup("/batch");

            // Batch create
            batch.MapPost("/create", (TagRepository repo, TagBatchCreateRequest request) =>
            {
                var results = request.Inputs.Select(input =>
                {
                    var tag = repo.Create(input.Name, input.Language);
                    return new
                    {
                        id = tag.Id,
                        name = tag.Name,
                        language = tag.Language,
                        created = tag.Created,
                        updated = tag.Updated
                    };
                }).ToList();

                return Results.Ok(new { status = "COMPLETE", results });
            });

            // Batch read
            batch.MapPost("/read", (TagRepository repo, TagBatchReadRequest request) =>
            {
                var results = request.Inputs.Select(input =>
                {
                    var tag = repo.Get(input.Id);
                    return tag != null ? new
                    {
                        id = tag.Id,
                        name = tag.Name,
                        language = tag.Language,
                        created = tag.Created,
                        updated = tag.Updated
                    } : null;
                }).Where(r => r != null).ToList();

                return Results.Ok(new { status = "COMPLETE", results });
            });

            // Batch update
            batch.MapPost("/update", (TagRepository repo, TagBatchUpdateRequest request) =>
            {
                var results = request.Inputs.Select(input =>
                {
                    var tag = repo.Update(input.Id, input.Name);
                    return tag != null ? new
                    {
                        id = tag.Id,
                        name = tag.Name,
                        language = tag.Language,
                        created = tag.Created,
                        updated = tag.Updated
                    } : null;
                }).Where(r => r != null).ToList();

                return Results.Ok(new { status = "COMPLETE", results });
            });

            // Batch archive
            batch.MapPost("/archive", (TagRepository repo, TagBatchArchiveRequest request) =>
            {
                foreach (var input in request.Inputs)
                {
                    repo.Delete(input.Id);
                }
                return Results.NoContent();
            });
        }

        private static void RegisterMultiLanguageEndpoints(RouteGroupBuilder tags)
        {
            var ml = tags.MapGroup("/multi-language");

            // Attach to language group
            ml.MapPost("/attach-to-lang-group", (TagRepository repo, AttachToLangGroupRequest request) =>
            {
                repo.AttachToLanguageGroup(request.PrimaryId, request.Id);
                return Results.NoContent();
            });

            // Detach from language group
            ml.MapPost("/detach-from-lang-group", (TagRepository repo, DetachFromLangGroupRequest request) =>
            {
                repo.DetachFromLanguageGroup(request.Id);
                return Results.NoContent();
            });

            // Create language variation
            ml.MapPost("/create-language-variation", (TagRepository repo, CreateLanguageVariationRequest request) =>
            {
                var tag = repo.Create(request.Name, request.Language);
                repo.AttachToLanguageGroup(request.PrimaryId, tag.Id);

                return Results.Ok(new
                {
                    id = tag.Id,
                    name = tag.Name,
                    language = tag.Language,
                    created = tag.Created,
                    updated = tag.Updated
                });
            });

            // Set new language primary
            ml.MapPut("/set-new-lang-primary", (SetNewLangPrimaryRequest request) =>
            {
                return Results.NoContent();
            });

            // Update languages
            ml.MapPost("/update-languages", (UpdateLanguagesRequest request) =>
            {
                return Results.NoContent();
            });
        }
    }

    // Request models for Tags API
    record TagCreateRequest(string Name, string? Language = null);
    record TagUpdateRequest(string Name);
    record TagBatchCreateRequest(List<TagCreateRequest> Inputs);
    record TagBatchReadRequest(List<TagIdInput> Inputs);
    record TagBatchUpdateRequest(List<TagUpdateInput> Inputs);
    record TagBatchArchiveRequest(List<TagIdInput> Inputs);
    record TagIdInput(string Id);
    record TagUpdateInput(string Id, string Name);
    record AttachToLangGroupRequest(string Id, string PrimaryId);
    record DetachFromLangGroupRequest(string Id);
    record CreateLanguageVariationRequest(string PrimaryId, string Name, string Language);
    record SetNewLangPrimaryRequest(string Id);
    record UpdateLanguagesRequest(string Id, Dictionary<string, string> Translations);
}
