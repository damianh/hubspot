using DamianH.HubSpot.MockServer.Repositories.AccountInfo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static class Account
    {
        public static void RegisterAccountInfoV3Api(WebApplication app)
        {
            var group = app.MapGroup("/account-info/v3/api-usage");

            group.MapGet("/daily", (
                AccountInfoRepository repository) =>
            {
                var usage = repository.GetDailyApiUsage(30);
                return Results.Ok(new { results = usage });
            });

            group.MapGet("/daily/private-apps", (
                AccountInfoRepository repository) =>
            {
                var usage = repository.GetPrivateAppsDailyUsage(30);
                return Results.Ok(new { results = usage });
            });

            var detailsGroup = app.MapGroup("/account-info/v3");

            detailsGroup.MapGet("/details", (
                AccountInfoRepository repository) =>
            {
                var details = repository.GetAccountDetails();
                return Results.Ok(details);
            });
        }

        public static void RegisterAccountInfoV202509Api(WebApplication app)
        {
            var group = app.MapGroup("/account-info/api/v202509/api-usage");

            group.MapGet("/daily", (
                AccountInfoRepository repository) =>
            {
                var usage = repository.GetDailyApiUsage(30);
                return Results.Ok(new { results = usage });
            });

            group.MapGet("/daily/private-apps", (
                AccountInfoRepository repository) =>
            {
                var usage = repository.GetPrivateAppsDailyUsage(30);
                return Results.Ok(new { results = usage });
            });

            var detailsGroup = app.MapGroup("/account-info/api/v202509");

            detailsGroup.MapGet("/details", (
                AccountInfoRepository repository) =>
            {
                var details = repository.GetAccountDetails();
                return Results.Ok(details);
            });
        }

        public static void RegisterAuditLogsV3Api(WebApplication app)
        {
            var group = app.MapGroup("/account-info/v3/activity");

            group.MapGet("/audit-logs", (
                AccountInfoRepository repository,
                int? limit,
                string? after) =>
            {
                var logs = repository.GetAuditLogs("ACTIVITY", limit ?? 100, after);
                var hasMore = logs.Count >= (limit ?? 100);

                var response = new
                {
                    results = logs,
                    paging = hasMore ? new
                    {
                        next = new
                        {
                            after = logs.Last().Id,
                            link = $"/account-info/v3/audit-logs/activity?limit={limit ?? 100}&after={logs.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            group.MapGet("/login", (
                AccountInfoRepository repository,
                int? limit,
                string? after) =>
            {
                var logs = repository.GetAuditLogs("LOGIN", limit ?? 100, after);
                var hasMore = logs.Count >= (limit ?? 100);

                var response = new
                {
                    results = logs,
                    paging = hasMore ? new
                    {
                        next = new
                        {
                            after = logs.Last().Id,
                            link = $"/account-info/v3/audit-logs/login?limit={limit ?? 100}&after={logs.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            group.MapGet("/security", (
                AccountInfoRepository repository,
                int? limit,
                string? after) =>
            {
                var logs = repository.GetAuditLogs("SECURITY", limit ?? 100, after);
                var hasMore = logs.Count >= (limit ?? 100);

                var response = new
                {
                    results = logs,
                    paging = hasMore ? new
                    {
                        next = new
                        {
                            after = logs.Last().Id,
                            link = $"/account-info/v3/audit-logs/security?limit={limit ?? 100}&after={logs.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });
        }
    }
}
