using DamianH.HubSpot.MockServer.Repositories.Blog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static partial class CmsBlogSettings
    {
        public static void RegisterCmsBlogSettingsV3Api(WebApplication app)
        {
            var settings = app.MapGroup("/cms/v3/blog-settings/settings");

            // List blog settings
            settings.MapGet("/", (BlogSettingsRepository repo) =>
            {
                var results = repo.List().ToList();
                return Results.Ok(new
                {
                    results = results.Select(s => new
                    {
                        blogId = s.BlogId,
                        name = s.Name,
                        language = s.Language,
                        created = s.Created,
                        updated = s.Updated,
                        publicAccessRules = s.PublicAccessRules,
                        htmlTitle = s.HtmlTitle,
                        domain = s.Domain
                    })
                });
            });

            // Create blog settings
            settings.MapPost("/", (BlogSettingsRepository repo, BlogSettingsCreateRequest request) =>
            {
                var blogSettings = repo.Create(request.BlogId, request.Name, request.Language);
                return Results.Ok(new
                {
                    blogId = blogSettings.BlogId,
                    name = blogSettings.Name,
                    language = blogSettings.Language,
                    created = blogSettings.Created,
                    updated = blogSettings.Updated,
                    publicAccessRules = blogSettings.PublicAccessRules,
                    htmlTitle = blogSettings.HtmlTitle,
                    domain = blogSettings.Domain
                });
            });

            // Get blog settings
            settings.MapGet("/{blogId}", (BlogSettingsRepository repo, string blogId) =>
            {
                var blogSettings = repo.Get(blogId);
                if (blogSettings == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    blogId = blogSettings.BlogId,
                    name = blogSettings.Name,
                    language = blogSettings.Language,
                    created = blogSettings.Created,
                    updated = blogSettings.Updated,
                    publicAccessRules = blogSettings.PublicAccessRules,
                    htmlTitle = blogSettings.HtmlTitle,
                    domain = blogSettings.Domain
                });
            });

            // Update blog settings
            settings.MapPatch("/{blogId}", (BlogSettingsRepository repo, string blogId, BlogSettingsUpdateRequest request) =>
            {
                var blogSettings = repo.Update(blogId, s =>
                {
                    if (request.Name != null)
                    {
                        s.Name = request.Name;
                    }

                    if (request.HtmlTitle != null)
                    {
                        s.HtmlTitle = request.HtmlTitle;
                    }

                    if (request.Domain != null)
                    {
                        s.Domain = request.Domain;
                    }
                });

                if (blogSettings == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    blogId = blogSettings.BlogId,
                    name = blogSettings.Name,
                    language = blogSettings.Language,
                    created = blogSettings.Created,
                    updated = blogSettings.Updated,
                    publicAccessRules = blogSettings.PublicAccessRules,
                    htmlTitle = blogSettings.HtmlTitle,
                    domain = blogSettings.Domain
                });
            });

            // Revisions
            RegisterRevisionEndpoints(settings);

            // Multi-language
            RegisterMultiLanguageEndpoints(settings);
        }

        private static void RegisterRevisionEndpoints(RouteGroupBuilder settings)
        {
            // List revisions
            settings.MapGet("/{blogId}/revisions", (BlogSettingsRepository repo, string blogId) =>
            {
                var revisions = repo.GetRevisions(blogId).ToList();
                return Results.Ok(new
                {
                    results = revisions.Select(r => new
                    {
                        id = r.Id,
                        blogId = r.BlogId,
                        timestamp = r.Timestamp,
                        name = r.Name,
                        language = r.Language
                    })
                });
            });

            // Get specific revision
            settings.MapGet("/{blogId}/revisions/{revisionId}", (BlogSettingsRepository repo, string blogId, string revisionId) =>
            {
                var revision = repo.GetRevision(blogId, revisionId);
                if (revision == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    id = revision.Id,
                    blogId = revision.BlogId,
                    timestamp = revision.Timestamp,
                    name = revision.Name,
                    language = revision.Language
                });
            });
        }

        private static void RegisterMultiLanguageEndpoints(RouteGroupBuilder settings)
        {
            var ml = settings.MapGroup("/multi-language");

            // Attach to language group
            ml.MapPost("/attach-to-lang-group", (BlogSettingsRepository repo, BlogSettingsAttachToLangGroupRequest request) =>
            {
                repo.AttachToLanguageGroup(request.PrimaryId, request.Id);
                return Results.NoContent();
            });

            // Detach from language group
            ml.MapPost("/detach-from-lang-group", (BlogSettingsRepository repo, BlogSettingsDetachFromLangGroupRequest request) =>
            {
                repo.DetachFromLanguageGroup(request.Id);
                return Results.NoContent();
            });

            // Create language variation
            ml.MapPost("/create-language-variation", (BlogSettingsRepository repo, BlogSettingsCreateLanguageVariationRequest request) =>
            {
                var blogSettings = repo.Create(request.BlogId, request.Name, request.Language);
                repo.AttachToLanguageGroup(request.PrimaryId, blogSettings.BlogId);

                return Results.Ok(new
                {
                    blogId = blogSettings.BlogId,
                    name = blogSettings.Name,
                    language = blogSettings.Language,
                    created = blogSettings.Created,
                    updated = blogSettings.Updated
                });
            });

            // Set new language primary
            ml.MapPut("/set-new-lang-primary", (BlogSettingsSetNewLangPrimaryRequest request) =>
            {
                return Results.NoContent();
            });

            // Update languages
            ml.MapPost("/update-languages", (BlogSettingsUpdateLanguagesRequest request) =>
            {
                return Results.NoContent();
            });
        }
    }

    // Request models
    record BlogSettingsCreateRequest(string BlogId, string Name, string? Language = null);
    record BlogSettingsUpdateRequest(string? Name, string? HtmlTitle, string? Domain);
    record BlogSettingsAttachToLangGroupRequest(string Id, string PrimaryId);
    record BlogSettingsDetachFromLangGroupRequest(string Id);
    record BlogSettingsCreateLanguageVariationRequest(string PrimaryId, string BlogId, string Name, string Language);
    record BlogSettingsSetNewLangPrimaryRequest(string Id);
    record BlogSettingsUpdateLanguagesRequest(string Id, Dictionary<string, string> Translations);
}
