using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.MediaBridge;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static void RegisterCmsMediaBridgeApi(WebApplication app)
    {
        var group = app.MapGroup("/cms/v3/media-bridge");

        group.MapGet("/", (MediaBridgeRepository repository, HttpContext context) =>
        {
            var limit = int.TryParse(context.Request.Query["limit"], out var l) ? l : 100;
            var offset = int.TryParse(context.Request.Query["offset"], out var o) ? o : 0;

            var assets = repository.GetAll(offset, limit);
            var total = repository.Count();

            return Results.Ok(new
            {
                total,
                results = assets.Select(MapMediaAssetToResponse)
            });
        });

        group.MapGet("/{assetId}", (MediaBridgeRepository repository, string assetId) =>
        {
            var asset = repository.GetById(assetId);
            if (asset == null)
            {
                return Results.NotFound(new { message = $"Media asset {assetId} not found" });
            }

            return Results.Ok(MapMediaAssetToResponse(asset));
        });

        group.MapPost("/", async (MediaBridgeRepository repository, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var asset = MapMediaAssetFromRequest(request);
            var created = repository.Create(asset);

            return Results.Ok(MapMediaAssetToResponse(created));
        });

        group.MapPatch("/{assetId}", async (MediaBridgeRepository repository, string assetId, HttpContext context) =>
        {
            var request = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
            if (request == null)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var asset = MapMediaAssetFromRequest(request);
            var updated = repository.Update(assetId, asset);

            return updated == null
                ? Results.NotFound(new { message = $"Media asset {assetId} not found" })
                : Results.Ok(MapMediaAssetToResponse(updated));
        });

        group.MapDelete("/{assetId}", (MediaBridgeRepository repository, string assetId) =>
        {
            var success = repository.Delete(assetId);
            return !success
                ? Results.NotFound(new { message = $"Media asset {assetId} not found" })
                : Results.NoContent();
        });
    }

    private static object MapMediaAssetToResponse(MediaAsset asset) => new
    {
        id = asset.Id,
        name = asset.Name,
        url = asset.Url,
        thumbnailUrl = asset.ThumbnailUrl,
        type = asset.Type,
        size = asset.Size,
        mimeType = asset.MimeType,
        width = asset.Width,
        height = asset.Height,
        externalId = asset.ExternalId,
        providerId = asset.ProviderId,
        createdAt = asset.CreatedAt,
        updatedAt = asset.UpdatedAt,
        metadata = asset.Metadata
    };

    private static MediaAsset MapMediaAssetFromRequest(Dictionary<string, object> request)
    {
        var asset = new MediaAsset
        {
            Name = request.GetValueOrDefault("name")?.ToString(),
            Url = request.GetValueOrDefault("url")?.ToString(),
            ThumbnailUrl = request.GetValueOrDefault("thumbnailUrl")?.ToString(),
            Type = request.GetValueOrDefault("type")?.ToString(),
            MimeType = request.GetValueOrDefault("mimeType")?.ToString(),
            ExternalId = request.GetValueOrDefault("externalId")?.ToString(),
            ProviderId = request.GetValueOrDefault("providerId")?.ToString()
        };

        if (request.TryGetValue("size", out var sizeObj) && long.TryParse(sizeObj.ToString(), out var size))
        {
            asset.Size = size;
        }

        if (request.TryGetValue("width", out var widthObj) && int.TryParse(widthObj.ToString(), out var width))
        {
            asset.Width = width;
        }

        if (request.TryGetValue("height", out var heightObj) && int.TryParse(heightObj.ToString(), out var height))
        {
            asset.Height = height;
        }

        if (request.TryGetValue("metadata", out var metadataObj))
        {
            var metadataJson = JsonSerializer.Serialize(metadataObj);
            asset.Metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(metadataJson);
        }

        return asset;
    }
}
