using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static void RegisterCmsHubDbApi(this IEndpointRouteBuilder app, HubDbRepository repository)
    {
        var tablesGroup = app.MapGroup("/cms/v3/hubdb/tables");

        // Tables endpoints
        tablesGroup.MapGet("/", (HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;
            
            var tables = repository.GetAllTables(offset, limit);
            var total = repository.GetTableCount();
            
            return Results.Ok(new
            {
                total,
                results = tables.Select(MapTableToResponse)
            });
        });

        tablesGroup.MapGet("/{tableIdOrName}", (string tableIdOrName) =>
        {
            var table = repository.GetTableById(tableIdOrName);
            if (table == null)
            {
                return Results.NotFound(new { message = $"Table {tableIdOrName} not found" });
            }

            return Results.Ok(MapTableToResponse(table));
        });

        tablesGroup.MapPost("/", async (HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var table = MapTableFromRequest(request);
            var created = repository.CreateTable(table);
            
            return Results.Ok(MapTableToResponse(created));
        });

        tablesGroup.MapPatch("/{tableIdOrName}", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var table = MapTableFromRequest(request);
            var updated = repository.UpdateTable(tableIdOrName, table);
            
            if (updated == null)
            {
                return Results.NotFound(new { message = $"Table {tableIdOrName} not found" });
            }

            return Results.Ok(MapTableToResponse(updated));
        });

        tablesGroup.MapDelete("/{tableIdOrName}", (string tableIdOrName) =>
        {
            var success = repository.DeleteTable(tableIdOrName);
            if (!success)
            {
                return Results.NotFound(new { message = $"Table {tableIdOrName} not found" });
            }

            return Results.NoContent();
        });

        // Rows endpoints (nested under tables)
        var rowsGroup = app.MapGroup("/cms/v3/hubdb/tables/{tableIdOrName}/rows");

        rowsGroup.MapGet("/", (string tableIdOrName, HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;
            
            var rows = repository.GetAllRows(tableIdOrName, offset, limit);
            var total = repository.GetRowCount(tableIdOrName);
            
            return Results.Ok(new
            {
                total,
                results = rows.Select(MapRowToResponse)
            });
        });

        rowsGroup.MapGet("/{rowId}", (string tableIdOrName, string rowId) =>
        {
            var row = repository.GetRowById(tableIdOrName, rowId);
            if (row == null)
            {
                return Results.NotFound(new { message = $"Row {rowId} not found in table {tableIdOrName}" });
            }

            return Results.Ok(MapRowToResponse(row));
        });

        rowsGroup.MapPost("/", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var row = MapRowFromRequest(request);
            try
            {
                var created = repository.CreateRow(tableIdOrName, row);
                return Results.Ok(MapRowToResponse(created));
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
        });

        rowsGroup.MapPatch("/{rowId}", async (string tableIdOrName, string rowId, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var row = MapRowFromRequest(request);
            var updated = repository.UpdateRow(tableIdOrName, rowId, row);
            
            if (updated == null)
            {
                return Results.NotFound(new { message = $"Row {rowId} not found in table {tableIdOrName}" });
            }

            return Results.Ok(MapRowToResponse(updated));
        });

        rowsGroup.MapDelete("/{rowId}", (string tableIdOrName, string rowId) =>
        {
            var success = repository.DeleteRow(tableIdOrName, rowId);
            if (!success)
            {
                return Results.NotFound(new { message = $"Row {rowId} not found in table {tableIdOrName}" });
            }

            return Results.NoContent();
        });

        // Batch operations for rows
        rowsGroup.MapPost("/batch/create", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var inputsObj))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(inputsObj.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var results = new List<object>();
            foreach (var input in inputs)
            {
                var row = MapRowFromRequest(input);
                try
                {
                    var created = repository.CreateRow(tableIdOrName, row);
                    results.Add(MapRowToResponse(created));
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { message = ex.Message });
                }
            }

            return Results.Ok(new { results });
        });

        rowsGroup.MapPost("/batch/read", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var inputsObj))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(inputsObj.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var results = new List<object>();
            foreach (var input in inputs)
            {
                if (input.TryGetValue("id", out var idObj))
                {
                    var row = repository.GetRowById(tableIdOrName, idObj.ToString()!);
                    if (row != null)
                    {
                        results.Add(MapRowToResponse(row));
                    }
                }
            }

            return Results.Ok(new { results });
        });

        rowsGroup.MapPost("/batch/update", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var inputsObj))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(inputsObj.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            var results = new List<object>();
            foreach (var input in inputs)
            {
                if (input.TryGetValue("id", out var idObj))
                {
                    var row = MapRowFromRequest(input);
                    var updated = repository.UpdateRow(tableIdOrName, idObj.ToString()!, row);
                    if (updated != null)
                    {
                        results.Add(MapRowToResponse(updated));
                    }
                }
            }

            return Results.Ok(new { results });
        });

        rowsGroup.MapPost("/batch/archive", async (string tableIdOrName, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null || !request.TryGetValue("inputs", out var inputsObj))
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var inputs = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(inputsObj.ToString()!);
            if (inputs == null)
            {
                return Results.BadRequest(new { message = "Invalid inputs" });
            }

            foreach (var input in inputs)
            {
                if (input.TryGetValue("id", out var idObj))
                {
                    repository.DeleteRow(tableIdOrName, idObj.ToString()!);
                }
            }

            return Results.NoContent();
        });
    }

    private static object MapTableToResponse(HubDbTable table)
    {
        return new
        {
            id = table.Id,
            name = table.Name,
            label = table.Label,
            published = table.Published,
            columns = table.Columns,
            allowPublicApiAccess = table.AllowPublicApiAccess,
            allowChildTables = table.AllowChildTables,
            enableChildTablePages = table.EnableChildTablePages,
            useForPages = table.UseForPages,
            createdAt = table.CreatedAt,
            updatedAt = table.UpdatedAt
        };
    }

    private static HubDbTable MapTableFromRequest(Dictionary<string, object> request)
    {
        var table = new HubDbTable
        {
            Name = request.GetValueOrDefault("name")?.ToString(),
            Label = request.GetValueOrDefault("label")?.ToString(),
            Published = request.GetValueOrDefault("published") is bool pub && pub,
            AllowPublicApiAccess = request.GetValueOrDefault("allowPublicApiAccess") is bool api && api,
            AllowChildTables = request.GetValueOrDefault("allowChildTables") is bool child && child,
            EnableChildTablePages = request.GetValueOrDefault("enableChildTablePages") is bool pages && pages,
            UseForPages = request.GetValueOrDefault("useForPages") is bool use && use
        };

        if (request.TryGetValue("columns", out var columnsObj))
        {
            var columnsJson = JsonSerializer.Serialize(columnsObj);
            table.Columns = JsonSerializer.Deserialize<List<HubDbColumn>>(columnsJson);
        }

        return table;
    }

    private static object MapRowToResponse(HubDbRow row)
    {
        return new
        {
            id = row.Id,
            values = row.Values,
            path = row.Path,
            name = row.Name,
            createdAt = row.CreatedAt,
            updatedAt = row.UpdatedAt
        };
    }

    private static HubDbRow MapRowFromRequest(Dictionary<string, object> request)
    {
        var row = new HubDbRow
        {
            Path = request.GetValueOrDefault("path")?.ToString(),
            Name = request.GetValueOrDefault("name")?.ToString()
        };

        if (request.TryGetValue("values", out var valuesObj))
        {
            var valuesJson = JsonSerializer.Serialize(valuesObj);
            row.Values = JsonSerializer.Deserialize<Dictionary<string, object>>(valuesJson);
        }

        return row;
    }
}
