using System.Text.Json;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterCrmExtensions(WebApplication app)
    {
        RegisterCallingExtensions(app);
        RegisterCrmCards(app);
        RegisterVideoConferencing(app);
        RegisterTranscriptions(app);
        RegisterObjectLibraryV4(app);
        RegisterFeatureFlagsV3(app);
        RegisterLimitsTrackingV3(app);
    }

    private static void RegisterObjectLibraryV4(WebApplication app)
    {
        var group = app.MapGroup("/crm/v4/object-library")
            .WithTags("CRM Object Library");

        group.MapGet("", async (ObjectLibraryRepository repo) =>
        {
            var items = await repo.GetAllAsync();
            return Results.Ok(new { results = items });
        });

        group.MapGet("/{objectType}", async (string objectType, ObjectLibraryRepository repo) =>
        {
            var item = await repo.GetByObjectTypeAsync(objectType);
            return item.HasValue ? Results.Ok(item.Value) : Results.NotFound();
        });

        group.MapPost("", async (JsonElement body, ObjectLibraryRepository repo) =>
        {
            var objectType = body.TryGetProperty("objectType", out var ot)
                ? ot.GetString() ?? "custom_object"
                : "custom_object";
            var item = await repo.CreateAsync(objectType, body);
            return Results.Created($"/crm/v4/object-library/{objectType}", item);
        });

        group.MapPatch("/{objectType}", async (string objectType, JsonElement body, ObjectLibraryRepository repo) =>
        {
            var item = await repo.UpdateAsync(objectType, body);
            return item.HasValue ? Results.Ok(item.Value) : Results.NotFound();
        });

        group.MapDelete("/{objectType}", async (string objectType, ObjectLibraryRepository repo) =>
        {
            await repo.DeleteAsync(objectType);
            return Results.NoContent();
        });
    }

    private static void RegisterFeatureFlagsV3(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/apps/features")
            .WithTags("CRM Feature Flags");

        group.MapGet("", async (FeatureFlagRepository repo) =>
        {
            var flags = await repo.GetAllAsync();
            return Results.Ok(new { results = flags });
        });

        group.MapGet("/{featureKey}", async (string featureKey, FeatureFlagRepository repo) =>
        {
            var flag = await repo.GetAsync(featureKey);
            return flag.HasValue ? Results.Ok(flag.Value) : Results.NotFound();
        });

        group.MapPost("/{featureKey}", async (string featureKey, JsonElement body, FeatureFlagRepository repo) =>
        {
            var flag = await repo.EnableAsync(featureKey, body);
            return Results.Created($"/crm/v3/apps/features/{featureKey}", flag);
        });

        group.MapDelete("/{featureKey}", async (string featureKey, FeatureFlagRepository repo) =>
        {
            var deleted = await repo.DeleteAsync(featureKey);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }

    private static void RegisterLimitsTrackingV3(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3")
            .WithTags("CRM Limits Tracking");

        group.MapGet("/rate-limits", async (LimitsTrackingRepository repo) =>
        {
            var limits = await repo.GetRateLimitsAsync();
            return Results.Ok(limits);
        });

        group.MapGet("/rate-limits/usage", async (LimitsTrackingRepository repo, int? days) =>
        {
            var usage = await repo.GetUsageAsync(days ?? 7);
            return Results.Ok(usage);
        });
    }

    private static void RegisterCallingExtensions(WebApplication app)
    {
        var calling = app.MapGroup("/crm/v3/extensions/calling")
            .WithTags("CRM Extensions - Calling");

        calling.MapGet("/{appId}/settings", async (string appId, CallingExtensionRepository repo) =>
        {
            var settings = await repo.GetSettingsAsync(appId);
            return settings != null ? Results.Ok(settings) : Results.NotFound();
        });

        calling.MapPost("/{appId}/settings", async (string appId, JsonElement body, CallingExtensionRepository repo) =>
        {
            var settings = await repo.CreateSettingsAsync(appId, body);
            return Results.Created($"/crm/v3/extensions/calling/{appId}/settings", settings);
        });

        calling.MapPatch("/{appId}/settings", async (string appId, JsonElement body, CallingExtensionRepository repo) =>
        {
            var settings = await repo.UpdateSettingsAsync(appId, body);
            return Results.Ok(settings);
        });

        calling.MapPost("/{appId}/settings/ready", async (string appId, CallingExtensionRepository repo) =>
        {
            await repo.MarkAsReadyAsync(appId);
            return Results.NoContent();
        });

        calling.MapDelete("/{appId}/settings", async (string appId, CallingExtensionRepository repo) =>
        {
            await repo.DeleteSettingsAsync(appId);
            return Results.NoContent();
        });

        calling.MapPost("/{engagementId}/recordings/{recordingId}", async (
            string engagementId,
            string recordingId,
            JsonElement body,
            CallingExtensionRepository repo) =>
        {
            var recording = await repo.AddRecordingAsync(engagementId, recordingId, body);
            return Results.Created($"/crm/v3/extensions/calling/{engagementId}/recordings/{recordingId}", recording);
        });

        calling.MapGet("/{engagementId}/recordings", async (string engagementId, CallingExtensionRepository repo) =>
        {
            var recordings = await repo.GetRecordingsAsync(engagementId);
            return Results.Ok(new { results = recordings });
        });
    }

    private static void RegisterCrmCards(WebApplication app)
    {
        var cards = app.MapGroup("/crm/v3/extensions/cards")
            .WithTags("CRM Extensions - Cards");

        cards.MapGet("/{appId}", async (string appId, CrmCardRepository repo) =>
        {
            var cardList = await repo.GetCardsAsync(appId);
            return Results.Ok(new { results = cardList });
        });

        cards.MapPost("/{appId}", async (string appId, JsonElement body, CrmCardRepository repo) =>
        {
            var card = await repo.CreateCardAsync(appId, body);
            var cardId = card.TryGetProperty("id", out var id) ? id.GetString() : "";
            return Results.Created($"/crm/v3/extensions/cards/{appId}/{cardId}", card);
        });

        cards.MapPatch("/{appId}/{cardId}", async (string appId, string cardId, JsonElement body, CrmCardRepository repo) =>
        {
            var card = await repo.UpdateCardAsync(appId, cardId, body);
            return card.HasValue ? Results.Ok(card.Value) : Results.NotFound();
        });

        cards.MapDelete("/{appId}/{cardId}", async (string appId, string cardId, CrmCardRepository repo) =>
        {
            await repo.DeleteCardAsync(appId, cardId);
            return Results.NoContent();
        });

        cards.MapPost("/sample-response", async (JsonElement body) =>
        {
            return Results.Ok(new
            {
                results = new[]
                {
                    new
                    {
                        objectId = 12345,
                        title = "Sample Card",
                        properties = new[]
                        {
                            new { label = "Property 1", value = "Value 1", dataType = "STRING" }
                        }
                    }
                }
            });
        });
    }

    private static void RegisterVideoConferencing(WebApplication app)
    {
        var video = app.MapGroup("/crm/v3/extensions/videoconferencing")
            .WithTags("CRM Extensions - Video Conferencing");

        video.MapGet("/{appId}/settings", async (string appId, VideoConferencingRepository repo) =>
        {
            var settings = await repo.GetSettingsAsync(appId);
            return settings != null ? Results.Ok(settings) : Results.NotFound();
        });

        video.MapPost("/{appId}/settings", async (string appId, JsonElement body, VideoConferencingRepository repo) =>
        {
            var settings = await repo.CreateSettingsAsync(appId, body);
            return Results.Created($"/crm/v3/extensions/videoconferencing/{appId}/settings", settings);
        });

        video.MapPatch("/{appId}/settings", async (string appId, JsonElement body, VideoConferencingRepository repo) =>
        {
            var settings = await repo.UpdateSettingsAsync(appId, body);
            return Results.Ok(settings);
        });

        video.MapDelete("/{appId}/settings", async (string appId, VideoConferencingRepository repo) =>
        {
            await repo.DeleteSettingsAsync(appId);
            return Results.NoContent();
        });
    }

    private static void RegisterTranscriptions(WebApplication app)
    {
        var transcriptions = app.MapGroup("/crm/v3/extensions/transcriptions")
            .WithTags("CRM Extensions - Transcriptions");

        transcriptions.MapPost("/{engagementId}", async (
            string engagementId,
            JsonElement body,
            TranscriptionRepository repo) =>
        {
            var transcription = await repo.CreateTranscriptionAsync(engagementId, body);
            return Results.Created($"/crm/v3/extensions/transcriptions/{engagementId}", transcription);
        });

        transcriptions.MapGet("/{engagementId}", async (string engagementId, TranscriptionRepository repo) =>
        {
            var transcription = await repo.GetTranscriptionAsync(engagementId);
            return transcription != null ? Results.Ok(transcription) : Results.NotFound();
        });
    }
}
