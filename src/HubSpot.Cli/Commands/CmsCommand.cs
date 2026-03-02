using System.CommandLine;
using DamianH.HubSpot.KiotaClient.CMS.Domains.V3;
using DamianH.HubSpot.KiotaClient.CMS.Posts.V3;
using DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3;
using DomainModels = DamianH.HubSpot.KiotaClient.CMS.Domains.V3.Models;
using PostModels = DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models;
using RedirectModels = DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models;

namespace DamianH.HubSpot.Cli.Commands;

internal static class CmsCommand
{
    public static Command Create()
    {
        var command = new Command("cms", "CMS operations");

        command.Add(CreatePostsListCommand());
        command.Add(CreatePostsGetCommand());
        command.Add(CreateDomainsListCommand());
        command.Add(CreateDomainsGetCommand());
        command.Add(CreateRedirectsListCommand());
        command.Add(CreateRedirectsGetCommand());
        command.Add(CreateRedirectsCreateCommand());
        command.Add(CreateRedirectsDeleteCommand());

        return command;
    }

    private static Command CreatePostsListCommand()
    {
        var command = new Command("posts", "List blog posts");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived posts", DefaultValueFactory = _ => false };
        var sortOption = new Option<string[]?>("--sort") { Description = "Sort by field(s)", AllowMultipleArgumentsPerToken = true };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(archivedOption);
        command.Add(sortOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var archived = parseResult.GetValue(archivedOption);
            var sort = parseResult.GetValue(sortOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSPostsV3Client(cli.Adapter);
                var result = await client.Cms.V3.Blogs.Posts.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }

                    if (sort is { Length: > 0 })
                    {
                        q.QueryParameters.Sort = sort;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No blog posts found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapPost).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet)
                {
                    if (result.Total is { } total)
                    {
                        cli.Error.WriteLine($"Total: {total}");
                    }

                    if (result.Paging?.Next?.After is { } nextCursor)
                    {
                        cli.Error.WriteLine($"Next page cursor: {nextCursor}");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreatePostsGetCommand()
    {
        var command = new Command("post", "Get a blog post by ID");

        var postIdArg = new Argument<string>("postId") { Description = "Blog post ID" };

        command.Add(postIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var postId = parseResult.GetValue(postIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSPostsV3Client(cli.Adapter);
                var result = await client.Cms.V3.Blogs.Posts[postId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Blog post not found", cli.Error);
                    return;
                }

                var mapped = MapPost(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateDomainsListCommand()
    {
        var command = new Command("domains", "List CMS domains");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived domains", DefaultValueFactory = _ => false };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var archived = parseResult.GetValue(archivedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSDomainsV3Client(cli.Adapter);
                var result = await client.Cms.V3.Domains.EmptyPathSegment.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No domains found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapDomain).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet && result.Paging?.Next?.After is { } nextCursor)
                {
                    cli.Error.WriteLine($"Next page cursor: {nextCursor}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateDomainsGetCommand()
    {
        var command = new Command("domain", "Get a domain by ID");

        var domainIdArg = new Argument<string>("domainId") { Description = "Domain ID" };

        command.Add(domainIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var domainId = parseResult.GetValue(domainIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSDomainsV3Client(cli.Adapter);
                var result = await client.Cms.V3.Domains[domainId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("Domain not found", cli.Error);
                    return;
                }

                var mapped = MapDomain(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateRedirectsListCommand()
    {
        var command = new Command("redirects", "List URL redirects");

        var limitOption = new Option<int>("--limit") { Description = "Maximum results", DefaultValueFactory = _ => 10 };
        var afterOption = new Option<string?>("--after") { Description = "Pagination cursor" };
        var archivedOption = new Option<bool>("--archived") { Description = "Include archived redirects", DefaultValueFactory = _ => false };

        command.Add(limitOption);
        command.Add(afterOption);
        command.Add(archivedOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var limit = parseResult.GetValue(limitOption);
            var after = parseResult.GetValue(afterOption);
            var archived = parseResult.GetValue(archivedOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSUrlRedirectsV3Client(cli.Adapter);
                var result = await client.Cms.V3.UrlRedirects.EmptyPathSegment.GetAsync(q =>
                {
                    q.QueryParameters.Limit = limit;
                    q.QueryParameters.Archived = archived;
                    if (after is not null)
                    {
                        q.QueryParameters.After = after;
                    }
                }, cancellationToken);

                if (result?.Results is null or { Count: 0 })
                {
                    cli.Formatter.WriteMessage("No URL redirects found", cli.Out);
                    return;
                }

                var items = result.Results.Select(MapRedirect).ToList();
                cli.Formatter.WriteCollection(items, cli.Out);

                if (!cli.Quiet && result.Paging?.Next?.After is { } nextCursor)
                {
                    cli.Error.WriteLine($"Next page cursor: {nextCursor}");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateRedirectsGetCommand()
    {
        var command = new Command("redirect", "Get a URL redirect by ID");

        var redirectIdArg = new Argument<string>("redirectId") { Description = "URL redirect ID" };

        command.Add(redirectIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var redirectId = parseResult.GetValue(redirectIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSUrlRedirectsV3Client(cli.Adapter);
                var result = await client.Cms.V3.UrlRedirects[redirectId].GetAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("URL redirect not found", cli.Error);
                    return;
                }

                var mapped = MapRedirect(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateRedirectsCreateCommand()
    {
        var command = new Command("redirect-create", "Create a URL redirect");

        var routePrefixOption = new Option<string>("--route-prefix") { Description = "Source URL path (e.g., /old-page)", Required = true };
        var destinationOption = new Option<string>("--destination") { Description = "Target URL (e.g., /new-page)", Required = true };
        var redirectStyleOption = new Option<int>("--redirect-style") { Description = "Redirect type (301 or 302)", DefaultValueFactory = _ => 301 };

        command.Add(routePrefixOption);
        command.Add(destinationOption);
        command.Add(redirectStyleOption);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var routePrefix = parseResult.GetValue(routePrefixOption)!;
            var destination = parseResult.GetValue(destinationOption)!;
            var redirectStyle = parseResult.GetValue(redirectStyleOption);

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var body = new RedirectModels.UrlMappingCreateRequestBody
                {
                    RoutePrefix = routePrefix,
                    Destination = destination,
                    RedirectStyle = redirectStyle,
                };

                var client = new HubSpotCMSUrlRedirectsV3Client(cli.Adapter);
                var result = await client.Cms.V3.UrlRedirects.EmptyPathSegment.PostAsync(body, cancellationToken: cancellationToken);

                if (result is null)
                {
                    cli.Formatter.WriteMessage("No response received", cli.Error);
                    return;
                }

                var mapped = MapRedirect(result);
                cli.Formatter.WriteObject(mapped, cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static Command CreateRedirectsDeleteCommand()
    {
        var command = new Command("redirect-delete", "Delete a URL redirect");

        var redirectIdArg = new Argument<string>("redirectId") { Description = "URL redirect ID" };

        command.Add(redirectIdArg);

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var redirectId = parseResult.GetValue(redirectIdArg)!;

            var cli = CliContext.FromParseResult(parseResult);
            if (cli is null)
            {
                return;
            }

            try
            {
                var client = new HubSpotCMSUrlRedirectsV3Client(cli.Adapter);
                await client.Cms.V3.UrlRedirects[redirectId].DeleteAsync(cancellationToken: cancellationToken);

                cli.Formatter.WriteMessage($"URL redirect {redirectId} deleted", cli.Out);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex, cli);
            }
        });

        return command;
    }

    private static IDictionary<string, object?> MapPost(PostModels.BlogPost post) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = post.Id,
        ["name"] = post.Name,
        ["slug"] = post.Slug,
        ["state"] = post.CurrentState?.ToString(),
        ["authorName"] = post.AuthorName,
        ["domain"] = post.Domain,
        ["currentlyPublished"] = post.CurrentlyPublished,
        ["created"] = post.Created?.ToString("O"),
        ["updated"] = post.Updated?.ToString("O"),
    };

    private static IDictionary<string, object?> MapDomain(DomainModels.Domain domain) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = domain.Id,
        ["domain"] = domain.DomainProp,
        ["isResolving"] = domain.IsResolving,
        ["isSslEnabled"] = domain.IsSslEnabled,
        ["isSslOnly"] = domain.IsSslOnly,
        ["correctCname"] = domain.CorrectCname,
        ["isUsedForBlogPost"] = domain.IsUsedForBlogPost,
        ["isUsedForSitePage"] = domain.IsUsedForSitePage,
        ["isUsedForLandingPage"] = domain.IsUsedForLandingPage,
        ["isUsedForEmail"] = domain.IsUsedForEmail,
        ["created"] = domain.Created?.ToString("O"),
        ["updated"] = domain.Updated?.ToString("O"),
    };

    private static IDictionary<string, object?> MapRedirect(RedirectModels.UrlMapping redirect) => new Dictionary<string, object?>(StringComparer.Ordinal)
    {
        ["id"] = redirect.Id,
        ["routePrefix"] = redirect.RoutePrefix,
        ["destination"] = redirect.Destination,
        ["redirectStyle"] = redirect.RedirectStyle,
        ["created"] = redirect.Created?.ToString("O"),
        ["updated"] = redirect.Updated?.ToString("O"),
    };
}
