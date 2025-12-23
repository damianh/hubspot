using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories.Blog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsBlogAuthorsApi(WebApplication app)
    {
        var group = app.MapGroup("/cms/v3/blogs/authors");

        group.MapGet("/", (BlogAuthorRepository repository, HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;

            var authors = repository.GetAll(offset, limit);
            var total = repository.Count();

            return Results.Ok(new
            {
                total,
                results = authors.Select(MapAuthorToResponse)
            });
        });

        group.MapGet("/{objectId}", (BlogAuthorRepository repository, string objectId) =>
        {
            var author = repository.GetById(objectId);
            if (author == null)
            {
                return Results.NotFound(new { message = $"Author {objectId} not found" });
            }

            return Results.Ok(MapAuthorToResponse(author));
        });

        group.MapPost("/", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var author = MapAuthorFromRequest(request);
            var created = repository.Create(author);

            return Results.Ok(MapAuthorToResponse(created));
        });

        group.MapPatch("/{objectId}", async (BlogAuthorRepository repository, string objectId, HttpContext context) =>
        {
            var author = repository.GetById(objectId);
            if (author == null)
            {
                return Results.NotFound(new { message = $"Author {objectId} not found" });
            }

            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var updated = MapAuthorFromRequest(request, author);
            updated = repository.Update(objectId, updated);

            return Results.Ok(MapAuthorToResponse(updated!));
        });

        group.MapDelete("/{objectId}", (BlogAuthorRepository repository, string objectId) =>
        {
            var deleted = repository.Delete(objectId);
            if (!deleted)
            {
                return Results.NotFound(new { message = $"Author {objectId} not found" });
            }

            return Results.NoContent();
        });

        // Batch operations
        var batchGroup = group.MapGroup("/batch");

        batchGroup.MapPost("/create", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var value))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var authors = inputs.Select(i => MapAuthorFromRequest(i)).ToList();
            var created = repository.BatchCreate(authors);

            return Results.Ok(new
            {
                status = "COMPLETE",
                results = created.Select(MapAuthorToResponse)
            });
        });

        batchGroup.MapPost("/read", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var value))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var ids = inputs.Select(i => i["id"].ToString()!).ToList();
            var authors = repository.BatchRead(ids);

            return Results.Ok(new
            {
                status = "COMPLETE",
                results = authors.Select(MapAuthorToResponse)
            });
        });

        batchGroup.MapPost("/update", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var value))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var authors = inputs.Select(i => MapAuthorFromRequest(i)).ToList();
            var updated = repository.BatchUpdate(authors);

            return Results.Ok(new
            {
                status = "COMPLETE",
                results = updated.Select(MapAuthorToResponse)
            });
        });

        batchGroup.MapPost("/archive", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var value))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(value.ToString()!);
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

        multiLangGroup.MapPost("/attach-to-lang-group", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var authorId = request["id"].ToString()!;
            var langGroupId = request["language"]?.ToString() ?? "default";

            repository.AttachToLanguageGroup(authorId, langGroupId);

            return Results.NoContent();
        });

        multiLangGroup.MapPost("/detach-from-lang-group", async (BlogAuthorRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var authorId = request["id"].ToString()!;
            repository.DetachFromLanguageGroup(authorId);

            return Results.NoContent();
        });
    }

    private static object MapAuthorToResponse(BlogAuthor author) => new
    {
        id = author.Id,
        fullName = author.FullName,
        email = author.Email,
        slug = author.Slug,
        bio = author.Bio,
        website = author.Website,
        twitter = author.Twitter,
        facebook = author.Facebook,
        linkedin = author.Linkedin,
        avatar = author.Avatar,
        language = author.Language,
        created = author.Created,
        updated = author.Updated
    };

    private static BlogAuthor MapAuthorFromRequest(Dictionary<string, object> request, BlogAuthor? existing = null)
    {
        var author = existing ?? new BlogAuthor();

        if (request.TryGetValue("id", out var id))
        {
            author.Id = id.ToString();
        }

        if (request.TryGetValue("fullName", out var fullName))
        {
            author.FullName = fullName.ToString();
        }

        if (request.TryGetValue("email", out var email))
        {
            author.Email = email.ToString();
        }

        if (request.TryGetValue("slug", out var slug))
        {
            author.Slug = slug.ToString();
        }

        if (request.TryGetValue("bio", out var bio))
        {
            author.Bio = bio.ToString();
        }

        if (request.TryGetValue("website", out var website))
        {
            author.Website = website.ToString();
        }

        if (request.TryGetValue("twitter", out var twitter))
        {
            author.Twitter = twitter.ToString();
        }

        if (request.TryGetValue("facebook", out var facebook))
        {
            author.Facebook = facebook.ToString();
        }

        if (request.TryGetValue("linkedin", out var linkedin))
        {
            author.Linkedin = linkedin.ToString();
        }

        if (request.TryGetValue("avatar", out var avatar))
        {
            author.Avatar = avatar.ToString();
        }

        if (request.TryGetValue("language", out var lang))
        {
            author.Language = lang.ToString();
        }

        return author;
    }
}
