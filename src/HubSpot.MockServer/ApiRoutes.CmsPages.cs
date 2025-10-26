using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static void RegisterCmsPagesApi(this IEndpointRouteBuilder app, PageRepository repository)
    {
        var group = app.MapGroup("/cms/v3/pages/site-pages");

        group.MapGet("/", (HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;
            
            var pages = repository.GetAll(offset, limit);
            var total = repository.Count();
            
            return Results.Ok(new
            {
                total,
                results = pages.Select(MapPageToResponse)
            });
        });

        group.MapGet("/{objectId}", (string objectId) =>
        {
            var page = repository.GetById(objectId);
            if (page == null)
            {
                return Results.NotFound(new { message = $"Page {objectId} not found" });
            }

            return Results.Ok(MapPageToResponse(page));
        });

        group.MapPost("/", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var page = MapPageFromRequest(request);
            var created = repository.Create(page);
            
            return Results.Ok(MapPageToResponse(created));
        });

        group.MapPatch("/{objectId}", async (string objectId, HttpContext context) =>
        {
            var page = repository.GetById(objectId);
            if (page == null)
            {
                return Results.NotFound(new { message = $"Page {objectId} not found" });
            }

            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var updated = MapPageFromRequest(request, page);
            updated = repository.Update(objectId, updated);
            
            return Results.Ok(MapPageToResponse(updated!));
        });

        group.MapDelete("/{objectId}", (string objectId) =>
        {
            var deleted = repository.Delete(objectId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Page {objectId} not found" });
            }

            return Results.NoContent();
        });

        // Batch operations
        var batchGroup = group.MapGroup("/batch");

        batchGroup.MapPost("/create", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.ContainsKey("inputs"))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(request["inputs"].ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var pages = inputs.Select(i => MapPageFromRequest(i)).ToList();
            var created = repository.BatchCreate(pages);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = created.Select(p => MapPageToResponse(p))
            });
        });

        batchGroup.MapPost("/read", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.ContainsKey("inputs"))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(request["inputs"].ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var ids = inputs.Select(i => i["id"].ToString()!).ToList();
            var pages = repository.BatchRead(ids);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = pages.Select(MapPageToResponse)
            });
        });

        batchGroup.MapPost("/update", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.ContainsKey("inputs"))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(request["inputs"].ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var pages = inputs.Select(i => MapPageFromRequest(i)).ToList();
            var updated = repository.BatchUpdate(pages);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = updated.Select(MapPageToResponse)
            });
        });

        batchGroup.MapPost("/archive", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.ContainsKey("inputs"))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(request["inputs"].ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var ids = inputs.Select(i => i["id"].ToString()!).ToList();
            repository.BatchDelete(ids);
            
            return Results.NoContent();
        });

        // Revision operations
        group.MapGet("/{objectId}/revisions", (string objectId) =>
        {
            var revisions = repository.GetRevisions(objectId);
            return Results.Ok(new
            {
                results = revisions.Select(r => new
                {
                    id = r.Id,
                    createdAt = r.CreatedAt,
                })
            });
        });

        group.MapGet("/{objectId}/revisions/{revisionId}", (string objectId, string revisionId) =>
        {
            var revision = repository.GetRevisionById(objectId, revisionId);
            if (revision == null)
            {
                return Results.NotFound(new { message = $"Revision {revisionId} not found" });
            }

            return Results.Ok(new
            {
                id = revision.Id,
                createdAt = revision.CreatedAt,
                content = MapPageToResponse(revision.Content!)
            });
        });
    }

    private static object MapPageToResponse(Page page)
    {
        return new
        {
            id = page.Id,
            name = page.Name,
            slug = page.Slug,
            state = page.State ?? "DRAFT",
            htmlTitle = page.HtmlTitle,
            pageBody = page.PageBody,
            metaDescription = page.MetaDescription,
            useFeaturedImage = page.UseFeaturedImage ?? false,
            featuredImage = page.FeaturedImage,
            featuredImageAltText = page.FeaturedImageAltText,
            publishDate = page.PublishDate,
            created = page.Created,
            updated = page.Updated,
            language = page.Language,
            translatedFromId = page.TranslatedFromId
        };
    }

    private static Page MapPageFromRequest(Dictionary<string, object> request, Page? existing = null)
    {
        var page = existing ?? new Page();
        
        if (request.TryGetValue("id", out var id))
        {
            page.Id = id.ToString();
        }

        if (request.TryGetValue("name", out var name))
        {
            page.Name = name.ToString();
        }

        if (request.TryGetValue("slug", out var slug))
        {
            page.Slug = slug.ToString();
        }

        if (request.TryGetValue("state", out var state))
        {
            page.State = state.ToString();
        }

        if (request.TryGetValue("htmlTitle", out var title))
        {
            page.HtmlTitle = title.ToString();
        }

        if (request.TryGetValue("pageBody", out var body))
        {
            page.PageBody = JsonSerializer.Deserialize<Dictionary<string, object>>(body.ToString()!);
        }

        if (request.TryGetValue("metaDescription", out var meta))
        {
            page.MetaDescription = meta.ToString();
        }

        if (request.TryGetValue("useFeaturedImage", out var useFeatImg))
        {
            page.UseFeaturedImage = bool.Parse(useFeatImg.ToString()!);
        }

        if (request.TryGetValue("featuredImage", out var featImg))
        {
            page.FeaturedImage = featImg.ToString();
        }

        if (request.TryGetValue("featuredImageAltText", out var featImgAlt))
        {
            page.FeaturedImageAltText = featImgAlt.ToString();
        }

        if (request.TryGetValue("publishDate", out var pubDate))
        {
            page.PublishDate = DateTime.Parse(pubDate.ToString()!);
        }

        if (request.TryGetValue("language", out var lang))
        {
            page.Language = lang.ToString();
        }

        if (request.TryGetValue("translatedFromId", out var transId))
        {
            page.TranslatedFromId = transId.ToString();
        }

        return page;
    }
}
