using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static class TaxRates
    {
        public static void RegisterTaxRatesV1Api(WebApplication app)
        {
            var group = app.MapGroup("/tax-rates/v1/tax-rates");

            group.MapGet("", (
                TaxRateRepository repository,
                int? limit,
                string? after) =>
            {
                var groups = repository.GetTaxRateGroups(limit, after);
                var hasMore = limit.HasValue && groups.Count >= limit.Value;

                var response = new
                {
                    results = groups,
                    paging = hasMore && groups.Count > 0 ? new
                    {
                        next = new
                        {
                            after = groups.Last().Id,
                            link = $"/tax-rates/v1/tax-rates?limit={limit}&after={groups.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            group.MapGet("/{taxRateGroupId}", (
                TaxRateRepository repository,
                string taxRateGroupId) =>
            {
                var group = repository.GetTaxRateGroup(taxRateGroupId);
                if (group == null)
                {
                    return Results.NotFound(new { message = "Tax rate group not found" });
                }

                return Results.Ok(group);
            });
        }
    }
}
