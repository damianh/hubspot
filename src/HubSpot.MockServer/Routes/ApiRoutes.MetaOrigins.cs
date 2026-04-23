using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class MetaOrigins
    {
        public static void RegisterMetaOriginsV1Api(WebApplication app)
        {
            var group = app.MapGroup("/meta/network-origins/2025-09/ip-ranges");
            group.MapGet("", ListIpRanges);
            group.MapGet("/simple", ListIpRangesSimple);
        }

        private static IResult ListIpRanges(string? direction = null, string? service = null)
        {
            var result = new
            {
                results = Array.Empty<object>()
            };
            return Results.Ok(result);
        }

        private static IResult ListIpRangesSimple(string? direction = null, string? service = null) =>
            Results.Text("[]");
    }
}
