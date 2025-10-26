using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static class BusinessUnits
    {
        public static void RegisterBusinessUnitsV3Api(WebApplication app)
        {
            var group = app.MapGroup("/business-units/v3/business-units")
                .WithTags("Business Units V3");

            group.MapGet("", async (BusinessUnitRepository repository) =>
            {
                var units = await repository.GetAllAsync();
                return Results.Ok(new { results = units });
            });

            group.MapGet("/{businessUnitId}", async (string businessUnitId, BusinessUnitRepository repository) =>
            {
                var unit = await repository.GetByIdAsync(businessUnitId);
                return unit.HasValue ? Results.Ok(unit.Value) : Results.NotFound();
            });

            group.MapPost("", async (JsonElement body, BusinessUnitRepository repository) =>
            {
                var unit = await repository.CreateAsync(body);
                var id = unit.TryGetProperty("id", out var idProp) ? idProp.GetString() : "";
                return Results.Created($"/business-units/v3/business-units/{id}", unit);
            });

            group.MapPatch("/{businessUnitId}", async (string businessUnitId, JsonElement body, BusinessUnitRepository repository) =>
            {
                var unit = await repository.UpdateAsync(businessUnitId, body);
                return unit.HasValue ? Results.Ok(unit.Value) : Results.NotFound();
            });

            group.MapDelete("/{businessUnitId}", async (string businessUnitId, BusinessUnitRepository repository) =>
            {
                var archived = await repository.ArchiveAsync(businessUnitId);
                return archived ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
