using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterCrmObjectsV202603(WebApplication app)
    {
        var group = app.MapGroup("/crm/objects/2026-03/{objectType}")
            .WithTags("CRM Objects V202603");

        // V202603 is structurally identical to V202509 but without gdpr-delete
        group.MapGet("", GetObjectsV202509);
        group.MapPost("", CreateObjectV202509);
        group.MapGet("{objectId}", GetObjectByIdV202509);
        group.MapPatch("{objectId}", UpdateObjectV202509);
        group.MapDelete("{objectId}", DeleteObjectV202509);

        group.MapPost("batch/create", BatchCreateV202509);
        group.MapPost("batch/read", BatchReadV202509);
        group.MapPost("batch/update", BatchUpdateV202509);
        group.MapPost("batch/upsert", BatchUpsertV202509);
        group.MapPost("batch/archive", BatchArchiveV202509);

        group.MapPost("search", SearchObjectsV202509);
        group.MapPost("merge", MergeV202509);
    }
}
