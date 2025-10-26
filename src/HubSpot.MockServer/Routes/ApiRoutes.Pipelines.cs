using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class Pipelines
    {
        /// <summary>
        /// Register CRM Pipelines V3 API routes
        /// </summary>
        internal static void RegisterPipelinesV3(WebApplication app)
        {
            var group = app.MapGroup("/crm/v3/pipelines/{objectType}")
                .WithTags("Pipelines V3");

            // Get all pipelines for an object type
            group.MapGet("", (
                [FromRoute] string objectType,
                PipelineRepository repo) =>
            {
                var pipelines = repo.GetPipelines(objectType);

                var response = new
                {
                    results = pipelines.Select(p => new
                    {
                        id = p.Id,
                        label = p.Label,
                        displayOrder = p.DisplayOrder,
                        stages = repo.GetStages(p.Id).Select(s => new
                        {
                            id = s.Id,
                            label = s.Label,
                            displayOrder = s.DisplayOrder,
                            metadata = s.Metadata
                        }).ToArray(),
                        createdAt = p.CreatedAt,
                        updatedAt = p.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create a new pipeline
            group.MapPost("", (
                [FromRoute] string objectType,
                [FromBody] CreatePipelineRequest request,
                PipelineRepository repo) =>
            {
                var pipeline = repo.CreatePipeline(
                    objectType,
                    request.Label,
                    request.DisplayOrder);

                // Create stages if provided
                var stages = new List<object>();
                if (request.Stages != null)
                {
                    foreach (var stageRequest in request.Stages)
                    {
                        var stage = repo.CreateStage(
                            pipeline.Id,
                            stageRequest.Label,
                            stageRequest.DisplayOrder,
                            stageRequest.Metadata);

                        stages.Add(new
                        {
                            id = stage.Id,
                            label = stage.Label,
                            displayOrder = stage.DisplayOrder,
                            metadata = stage.Metadata
                        });
                    }
                }

                var response = new
                {
                    id = pipeline.Id,
                    label = pipeline.Label,
                    displayOrder = pipeline.DisplayOrder,
                    stages = stages.ToArray(),
                    createdAt = pipeline.CreatedAt,
                    updatedAt = pipeline.UpdatedAt
                };

                return Results.Created($"/crm/v3/pipelines/{objectType}/{pipeline.Id}", response);
            });

            // Get a specific pipeline
            group.MapGet("/{pipelineId}", (
                [FromRoute] string objectType,
                [FromRoute] string pipelineId,
                PipelineRepository repo) =>
            {
                var pipeline = repo.GetPipeline(objectType, pipelineId);
                if (pipeline == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    id = pipeline.Id,
                    label = pipeline.Label,
                    displayOrder = pipeline.DisplayOrder,
                    stages = repo.GetStages(pipeline.Id).Select(s => new
                    {
                        id = s.Id,
                        label = s.Label,
                        displayOrder = s.DisplayOrder,
                        metadata = s.Metadata
                    }).ToArray(),
                    createdAt = pipeline.CreatedAt,
                    updatedAt = pipeline.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Update a pipeline
            group.MapPatch("/{pipelineId}", (
                [FromRoute] string objectType,
                [FromRoute] string pipelineId,
                [FromBody] UpdatePipelineRequest request,
                PipelineRepository repo) =>
            {
                var pipeline = repo.UpdatePipeline(
                    objectType,
                    pipelineId,
                    request.Label,
                    request.DisplayOrder);

                if (pipeline == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    id = pipeline.Id,
                    label = pipeline.Label,
                    displayOrder = pipeline.DisplayOrder,
                    stages = repo.GetStages(pipeline.Id).Select(s => new
                    {
                        id = s.Id,
                        label = s.Label,
                        displayOrder = s.DisplayOrder,
                        metadata = s.Metadata
                    }).ToArray(),
                    createdAt = pipeline.CreatedAt,
                    updatedAt = pipeline.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Delete a pipeline
            group.MapDelete("/{pipelineId}", (
                [FromRoute] string objectType,
                [FromRoute] string pipelineId,
                PipelineRepository repo) =>
            {
                var deleted = repo.DeletePipeline(objectType, pipelineId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            // Pipeline Stages endpoints
            var stagesGroup = app.MapGroup("/crm/v3/pipelines/{objectType}/{pipelineId}/stages")
                .WithTags("Pipeline Stages V3");

            // Get all stages
            stagesGroup.MapGet("", (
                [FromRoute] string pipelineId,
                PipelineRepository repo) =>
            {
                var stages = repo.GetStages(pipelineId);

                var response = new
                {
                    results = stages.Select(s => new
                    {
                        id = s.Id,
                        label = s.Label,
                        displayOrder = s.DisplayOrder,
                        metadata = s.Metadata,
                        createdAt = s.CreatedAt,
                        updatedAt = s.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create a stage
            stagesGroup.MapPost("", (
                [FromRoute] string pipelineId,
                [FromBody] CreateStageRequest request,
                PipelineRepository repo) =>
            {
                var stage = repo.CreateStage(
                    pipelineId,
                    request.Label,
                    request.DisplayOrder,
                    request.Metadata);

                var response = new
                {
                    id = stage.Id,
                    label = stage.Label,
                    displayOrder = stage.DisplayOrder,
                    metadata = stage.Metadata,
                    createdAt = stage.CreatedAt,
                    updatedAt = stage.UpdatedAt
                };

                return Results.Created($"/crm/v3/pipelines/{pipelineId}/stages/{stage.Id}", response);
            });

            // Get a specific stage
            stagesGroup.MapGet("/{stageId}", (
                [FromRoute] string pipelineId,
                [FromRoute] string stageId,
                PipelineRepository repo) =>
            {
                var stage = repo.GetStage(pipelineId, stageId);
                if (stage == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    id = stage.Id,
                    label = stage.Label,
                    displayOrder = stage.DisplayOrder,
                    metadata = stage.Metadata,
                    createdAt = stage.CreatedAt,
                    updatedAt = stage.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Update a stage
            stagesGroup.MapPatch("/{stageId}", (
                [FromRoute] string pipelineId,
                [FromRoute] string stageId,
                [FromBody] UpdateStageRequest request,
                PipelineRepository repo) =>
            {
                var stage = repo.UpdateStage(
                    pipelineId,
                    stageId,
                    request.Label,
                    request.DisplayOrder,
                    request.Metadata);

                if (stage == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    id = stage.Id,
                    label = stage.Label,
                    displayOrder = stage.DisplayOrder,
                    metadata = stage.Metadata,
                    createdAt = stage.CreatedAt,
                    updatedAt = stage.UpdatedAt
                };

                return Results.Ok(response);
            });

            // Delete a stage
            stagesGroup.MapDelete("/{stageId}", (
                [FromRoute] string pipelineId,
                [FromRoute] string stageId,
                PipelineRepository repo) =>
            {
                var deleted = repo.DeleteStage(pipelineId, stageId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }

        // Request models
        private record CreatePipelineRequest(
            string Label,
            int DisplayOrder = 0,
            CreateStageRequest[]? Stages = null);

        private record UpdatePipelineRequest(
            string? Label = null,
            int? DisplayOrder = null);

        private record CreateStageRequest(
            string Label,
            int DisplayOrder = 0,
            Dictionary<string, string>? Metadata = null);

        private record UpdateStageRequest(
            string? Label = null,
            int? DisplayOrder = null,
            Dictionary<string, string>? Metadata = null);
    }
}
