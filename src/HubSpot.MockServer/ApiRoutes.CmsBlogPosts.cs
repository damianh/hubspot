using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static void RegisterCmsBlogPostsApi(this IEndpointRouteBuilder app, BlogPostRepository repository, ContentAuditRepository auditRepository)
    {
        var group = app.MapGroup("/cms/v3/blogs/posts");

        // List all posts
        group.MapGet("/", (HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;
            
            var posts = repository.GetAll(offset, limit);
            var total = repository.Count();
            
            return Results.Ok(new
            {
                total,
                results = posts.Select(MapToResponse)
            });
        });

        // Get single post
        group.MapGet("/{objectId}", (string objectId) =>
        {
            var post = repository.GetById(objectId);
            if (post == null)
            {
                return Results.NotFound(new { message = $"Post {objectId} not found" });
            }

            return Results.Ok(MapToResponse(post));
        });

        // Create post
        group.MapPost("/", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var post = MapFromRequest(request);
            var created = repository.Create(post);
            
            auditRepository.AddEntry(new ContentAuditEntry
            {
                EventType = "CREATED",
                ObjectType = "BLOG_POST",
                ObjectId = created.Id,
                UserId = "mock-user",
                UserEmail = "mock@example.com"
            });
            
            return Results.Ok(MapToResponse(created));
        });

        // Update post
        group.MapPatch("/{objectId}", async (string objectId, HttpContext context) =>
        {
            var post = repository.GetById(objectId);
            if (post == null)
            {
                return Results.NotFound(new { message = $"Post {objectId} not found" });
            }

            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var updated = MapFromRequest(request, post);
            updated = repository.Update(objectId, updated);
            
            auditRepository.AddEntry(new ContentAuditEntry
            {
                EventType = "UPDATED",
                ObjectType = "BLOG_POST",
                ObjectId = objectId,
                UserId = "mock-user",
                UserEmail = "mock@example.com"
            });
            
            return Results.Ok(MapToResponse(updated!));
        });

        // Delete post
        group.MapDelete("/{objectId}", (string objectId) =>
        {
            var deleted = repository.Delete(objectId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Post {objectId} not found" });
            }

            auditRepository.AddEntry(new ContentAuditEntry
            {
                EventType = "DELETED",
                ObjectType = "BLOG_POST",
                ObjectId = objectId,
                UserId = "mock-user",
                UserEmail = "mock@example.com"
            });
            
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

            var posts = inputs.Select(i => MapFromRequest(i)).ToList();
            var created = repository.BatchCreate(posts);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = created.Select(p => MapToResponse(p))
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
            var posts = repository.BatchRead(ids);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = posts.Select(MapToResponse)
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

            var posts = inputs.Select(i => MapFromRequest(i)).ToList();
            var updated = repository.BatchUpdate(posts);
            
            return Results.Ok(new
            {
                status = "COMPLETE",
                results = updated.Select(MapToResponse)
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

        // Multi-language operations
        var multiLangGroup = group.MapGroup("/multi-language");

        multiLangGroup.MapPost("/attach-to-lang-group", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var postId = request["id"].ToString()!;
            var langGroupId = request["language"]?.ToString() ?? "default";
            
            repository.AttachToLanguageGroup(postId, langGroupId);
            
            return Results.NoContent();
        });

        multiLangGroup.MapPost("/detach-from-lang-group", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var postId = request["id"].ToString()!;
            repository.DetachFromLanguageGroup(postId);
            
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
                content = MapToResponse(revision.Content!)
            });
        });

        group.MapPost("/{objectId}/revisions/{revisionId}/restore-to-draft", (string objectId, string revisionId) =>
        {
            var restored = repository.RestoreRevision(objectId, revisionId);
            return restored == null
                ? Results.NotFound(new { message = $"Revision {revisionId} not found" })
                : Results.Ok(MapToResponse(restored));
        });
    }

    private static object MapToResponse(BlogPost post) =>
        new
        {
            id = post.Id,
            name = post.Name,
            slug = post.Slug,
            contentGroupId = post.ContentGroupId,
            blogAuthorId = post.BlogAuthorId,
            campaign = post.Campaign,
            state = post.State ?? "DRAFT",
            postBody = post.PostBody,
            postSummary = post.PostSummary,
            rssBody = post.RssBody,
            rssSummary = post.RssSummary,
            metaDescription = post.MetaDescription,
            useFeaturedImage = post.UseFeaturedImage ?? false,
            featuredImage = post.FeaturedImage,
            featuredImageAltText = post.FeaturedImageAltText,
            publishDate = post.PublishDate,
            created = post.Created,
            updated = post.Updated,
            tagIds = post.TagIds ?? [],
            language = post.Language,
            translatedFromId = post.TranslatedFromId
        };

    private static BlogPost MapFromRequest(Dictionary<string, object> request, BlogPost? existing = null)
    {
        var post = existing ?? new BlogPost();
        
        if (request.TryGetValue("id", out var id))
        {
            post.Id = id.ToString();
        }

        if (request.TryGetValue("name", out var name))
        {
            post.Name = name.ToString();
        }

        if (request.TryGetValue("slug", out var slug))
        {
            post.Slug = slug.ToString();
        }

        if (request.TryGetValue("contentGroupId", out var cgId))
        {
            post.ContentGroupId = cgId.ToString();
        }

        if (request.TryGetValue("blogAuthorId", out var authorId))
        {
            post.BlogAuthorId = authorId.ToString();
        }

        if (request.TryGetValue("campaign", out var campaign))
        {
            post.Campaign = campaign.ToString();
        }

        if (request.TryGetValue("state", out var state))
        {
            post.State = state.ToString();
        }

        if (request.TryGetValue("postBody", out var body))
        {
            post.PostBody = JsonSerializer.Deserialize<Dictionary<string, object>>(body.ToString()!);
        }

        if (request.TryGetValue("postSummary", out var summary))
        {
            post.PostSummary = summary.ToString();
        }

        if (request.TryGetValue("metaDescription", out var meta))
        {
            post.MetaDescription = meta.ToString();
        }

        if (request.TryGetValue("publishDate", out var pubDate))
        {
            post.PublishDate = DateTime.Parse(pubDate.ToString()!);
        }

        if (request.TryGetValue("language", out var lang))
        {
            post.Language = lang.ToString();
        }

        if (request.TryGetValue("tagIds", out var tags))
        {
            post.TagIds = JsonSerializer.Deserialize<List<string>>(tags.ToString()!);
        }

        return post;
    }
}
