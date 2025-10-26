using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static class Associations
    {
        /// <summary>
        /// Register CRM Associations V3 API routes
        /// </summary>
        internal static void RegisterAssociationsV3(WebApplication app)
        {
            var group = app.MapGroup("/crm/v3/associations")
                .WithTags("Associations V3");

            // Batch read associations
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/read", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchReadRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var results = repo.GetAssociationsBatch(request.Inputs.Select(i => i.Id), fromObjectType, toObjectType);
                
                var response = new
                {
                    status = "COMPLETE",
                    results = results.Select(kvp => new
                    {
                        from = new { id = kvp.Key },
                        to = kvp.Value.Select(a => new
                        {
                            toObjectId = a.ToObjectId,
                            associationTypes = new[]
                            {
                                new
                                {
                                    category = "HUBSPOT_DEFINED",
                                    typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                    label = a.AssociationLabel ?? a.AssociationTypeId
                                }
                            }
                        }).ToArray()
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });

            // Batch create associations
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/create", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchCreateRequestV3? request,
                [FromServices] AssociationRepository repo) =>
            {
                if (request == null || request.Inputs == null || request.Inputs.Length == 0)
                {
                    return Results.BadRequest(new { error = "Request body is required and must contain inputs" });
                }

                var inputs = request.Inputs.Select(i => (
                    i.From.Id,
                    i.To.Id,
                    i.Type,
                    (string?)null
                ));

                var created = repo.CreateBatch(inputs, fromObjectType, toObjectType);

                var response = new
                {
                    status = "COMPLETE",
                    results = created.Select(a => new
                    {
                        from = new { id = a.FromObjectId },
                        to = new[]
                        {
                            new
                            {
                                toObjectId = a.ToObjectId,
                                associationTypes = new[]
                                {
                                    new
                                    {
                                        category = "HUBSPOT_DEFINED",
                                        typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                        label = a.AssociationLabel ?? a.AssociationTypeId
                                    }
                                }
                            }
                        }
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });

            // Batch archive (delete) associations
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/archive", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchArchiveRequestV3 request,
                [FromServices] AssociationRepository repo) =>
            {
                var inputs = request.Inputs.Select(i => (
                    i.From.Id,
                    i.To.Id,
                    i.Type
                ));

                repo.DeleteBatch(inputs, fromObjectType, toObjectType);

                return Results.NoContent();
            });

            // Get associations for a single object
            group.MapGet("/{fromObjectType}/{fromObjectId}/{toObjectType}", (
                [FromRoute] string fromObjectType,
                [FromRoute] string fromObjectId,
                [FromRoute] string toObjectType,
                [FromServices] AssociationRepository repo) =>
            {
                var associations = repo.GetAssociations(fromObjectType, fromObjectId, toObjectType);

                var response = new
                {
                    results = associations.Select(a => new
                    {
                        toObjectId = a.ToObjectId,
                        associationTypes = new[]
                        {
                            new
                            {
                                category = "HUBSPOT_DEFINED",
                                typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                label = a.AssociationLabel ?? a.AssociationTypeId
                            }
                        }
                    }).ToArray(),
                    paging = (object?)null
                };

                return Results.Ok(response);
            });

            // Create association for a single object
            group.MapPut("/{fromObjectType}/{fromObjectId}/{toObjectType}/{toObjectId}", (
                [FromRoute] string fromObjectType,
                [FromRoute] string fromObjectId,
                [FromRoute] string toObjectType,
                [FromRoute] string toObjectId,
                [FromBody] CreateAssociationRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var association = repo.Create(
                    fromObjectType,
                    fromObjectId,
                    toObjectType,
                    toObjectId,
                    request.AssociationTypeId,
                    request.AssociationCategory == "USER_DEFINED" ? request.AssociationTypeId : null);

                var response = new
                {
                    fromObjectTypeId = fromObjectType,
                    fromObjectId = association.FromObjectId,
                    toObjectTypeId = toObjectType,
                    toObjectId = association.ToObjectId,
                    labels = new[] { association.AssociationLabel }.Where(l => l != null).ToArray()
                };

                return Results.Ok(response);
            });

            // Delete association for a single object
            group.MapDelete("/{fromObjectType}/{fromObjectId}/{toObjectType}/{toObjectId}", (
                [FromRoute] string fromObjectType,
                [FromRoute] string fromObjectId,
                [FromRoute] string toObjectType,
                [FromRoute] string toObjectId,
                [FromServices] AssociationRepository repo) =>
            {
                // Default association type if not specified
                repo.Delete(fromObjectType, fromObjectId, toObjectType, toObjectId, "1");
                return Results.NoContent();
            });

            // Also register routes under /crm/v3/objects pattern for compatibility
            var objectsGroup = app.MapGroup("/crm/v3/objects/{objectType}/{objectId}/associations")
                .WithTags("Associations V3 (Objects)");

            // PUT /crm/v3/objects/{objectType}/{objectId}/associations/{toObjectType}/{toObjectId}/{associationTypeId}
            objectsGroup.MapPut("/{toObjectType}/{toObjectId}/{associationTypeId}", (
                [FromRoute] string objectType,
                [FromRoute] string objectId,
                [FromRoute] string toObjectType,
                [FromRoute] string toObjectId,
                [FromRoute] string associationTypeId,
                [FromServices] AssociationRepository repo) =>
            {
                var association = repo.Create(
                    objectType,
                    objectId,
                    toObjectType,
                    toObjectId,
                    associationTypeId,
                    null);

                var response = new
                {
                    fromObjectTypeId = objectType,
                    fromObjectId = association.FromObjectId,
                    toObjectTypeId = toObjectType,
                    toObjectId = association.ToObjectId,
                    labels = new[] { association.AssociationLabel }.Where(l => l != null).ToArray()
                };

                return Results.Ok(response);
            });

            // GET /crm/v3/objects/{objectType}/{objectId}/associations/{toObjectType}
            objectsGroup.MapGet("/{toObjectType}", (
                [FromRoute] string objectType,
                [FromRoute] string objectId,
                [FromRoute] string toObjectType,
                [FromServices] AssociationRepository repo) =>
            {
                var associations = repo.GetAssociations(objectType, objectId, toObjectType);

                var response = new
                {
                    results = associations.Select(a => new
                    {
                        toObjectId = a.ToObjectId,
                        associationTypes = new[]
                        {
                            new
                            {
                                category = "HUBSPOT_DEFINED",
                                typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                label = a.AssociationLabel ?? a.AssociationTypeId
                            }
                        }
                    }).ToArray(),
                    paging = (object?)null
                };

                return Results.Ok(response);
            });

            // DELETE /crm/v3/objects/{objectType}/{objectId}/associations/{toObjectType}/{toObjectId}/{associationTypeId}
            objectsGroup.MapDelete("/{toObjectType}/{toObjectId}/{associationTypeId}", (
                [FromRoute] string objectType,
                [FromRoute] string objectId,
                [FromRoute] string toObjectType,
                [FromRoute] string toObjectId,
                [FromRoute] string associationTypeId,
                [FromServices] AssociationRepository repo) =>
            {
                repo.Delete(objectType, objectId, toObjectType, toObjectId, associationTypeId);
                return Results.NoContent();
            });
        }

        /// <summary>
        /// Register CRM Associations V4 API routes
        /// </summary>
        internal static void RegisterAssociationsV4(WebApplication app)
        {
            var group = app.MapGroup("/crm/v4/associations")
                .WithTags("Associations V4");

            // V4 batch read - similar to V3 but with enhanced label support
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/read", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchReadRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var results = repo.GetAssociationsBatch(request.Inputs.Select(i => i.Id), fromObjectType, toObjectType);
                
                var response = new
                {
                    status = "COMPLETE",
                    results = results.Select(kvp => new
                    {
                        from = new { id = kvp.Key },
                        to = kvp.Value.Select(a => new
                        {
                            toObjectId = a.ToObjectId,
                            associationTypes = new[]
                            {
                                new
                                {
                                    category = "HUBSPOT_DEFINED",
                                    typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                    label = a.AssociationLabel ?? a.AssociationTypeId
                                }
                            }
                        }).ToArray()
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });

            // V4 batch create
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/create", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchCreateRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var inputs = request.Inputs.Select(i => (
                    i.From.Id,
                    i.To.Id,
                    i.Types[0].AssociationTypeId.ToString(),
                    i.Types[0].AssociationCategory == "USER_DEFINED" ? i.Types[0].AssociationTypeId.ToString() : null
                ));

                var created = repo.CreateBatch(inputs, fromObjectType, toObjectType);

                var response = new
                {
                    status = "COMPLETE",
                    results = created.Select(a => new
                    {
                        from = new { id = a.FromObjectId },
                        to = new[]
                        {
                            new
                            {
                                toObjectId = a.ToObjectId,
                                associationTypes = new[]
                                {
                                    new
                                    {
                                        category = "HUBSPOT_DEFINED",
                                        typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                        label = a.AssociationLabel ?? a.AssociationTypeId
                                    }
                                }
                            }
                        }
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });

            // V4 batch labels - manage association labels
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/labels", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchLabelsRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                // For simplicity, just return success
                var response = new
                {
                    status = "COMPLETE",
                    results = request.Inputs.Select(i => new
                    {
                        from = new { id = i.From.Id },
                        to = new { id = i.To.Id },
                        labels = i.Labels
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // V4 batch archive
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/archive", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchArchiveRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var inputs = request.Inputs.Select(i => (
                    i.From.Id,
                    i.To.Id,
                    i.Types[0].AssociationTypeId.ToString()
                ));

                repo.DeleteBatch(inputs, fromObjectType, toObjectType);

                return Results.NoContent();
            });

            // V4 association schema/definitions endpoints
            var schemaGroup = app.MapGroup("/crm/v4/associations/{fromObjectType}/{toObjectType}/labels")
                .WithTags("Association Schemas V4");

            // Get all association type definitions
            schemaGroup.MapGet("", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromServices] AssociationRepository repo) =>
            {
                var types = repo.GetAssociationTypes(fromObjectType, toObjectType);
                
                var response = new
                {
                    results = types.Select(t => new
                    {
                        category = t.Category,
                        typeId = int.Parse(t.Id),
                        label = t.Name
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create association type definition
            schemaGroup.MapPost("", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] CreateAssociationTypeRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var definition = repo.CreateAssociationType(request.Label ?? request.Name, "USER_DEFINED");

                var response = new
                {
                    category = "USER_DEFINED",
                    typeId = int.Parse(definition.Id),
                    label = definition.Name
                };

                return Results.Created($"/crm/v4/associations/{fromObjectType}/{toObjectType}/labels/{definition.Id}", response);
            });

            // Update association type definition
            schemaGroup.MapPut("/{associationTypeId}", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromRoute] string associationTypeId,
                [FromBody] UpdateAssociationTypeRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var type = repo.GetAssociationType(associationTypeId);
                if (type == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    category = type.Category,
                    typeId = int.Parse(type.Id),
                    label = request.Label ?? type.Name
                };

                return Results.Ok(response);
            });

            // Delete association type definition
            schemaGroup.MapDelete("/{associationTypeId}", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromRoute] string associationTypeId,
                [FromServices] AssociationRepository repo) =>
            {
                var deleted = repo.DeleteAssociationType(associationTypeId);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }

        /// <summary>
        /// Register CRM Associations V202509 API routes
        /// </summary>
        internal static void RegisterAssociationsV202509(WebApplication app)
        {
            var group = app.MapGroup("/crm/v202509/associations")
                .WithTags("Associations V202509");

            // V202509 uses same structure as V4, route to V4 implementation
            group.MapPost("/{fromObjectType}/{toObjectType}/batch/read", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchReadRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var results = repo.GetAssociationsBatch(request.Inputs.Select(i => i.Id), fromObjectType, toObjectType);
                
                var response = new
                {
                    status = "COMPLETE",
                    results = results.Select(kvp => new
                    {
                        from = new { id = kvp.Key },
                        to = kvp.Value.Select(a => new
                        {
                            toObjectId = a.ToObjectId,
                            associationTypes = new[]
                            {
                                new
                                {
                                    category = "HUBSPOT_DEFINED",
                                    typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                    label = a.AssociationLabel ?? a.AssociationTypeId
                                }
                            }
                        }).ToArray()
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });

            group.MapPost("/{fromObjectType}/{toObjectType}/batch/create", (
                [FromRoute] string fromObjectType,
                [FromRoute] string toObjectType,
                [FromBody] BatchCreateRequest request,
                [FromServices] AssociationRepository repo) =>
            {
                var inputs = request.Inputs.Select(i => (
                    i.From.Id,
                    i.To.Id,
                    i.Types[0].AssociationTypeId.ToString(),
                    i.Types[0].AssociationCategory == "USER_DEFINED" ? i.Types[0].AssociationTypeId.ToString() : (string?)null
                ));

                var created = repo.CreateBatch(inputs, fromObjectType, toObjectType);

                var response = new
                {
                    status = "COMPLETE",
                    results = created.Select(a => new
                    {
                        from = new { id = a.FromObjectId },
                        to = new[]
                        {
                            new
                            {
                                toObjectId = a.ToObjectId,
                                associationTypes = new[]
                                {
                                    new
                                    {
                                        category = "HUBSPOT_DEFINED",
                                        typeId = int.TryParse(a.AssociationTypeId, out var typeIdInt) ? typeIdInt : 0,
                                        label = a.AssociationLabel ?? a.AssociationTypeId
                                    }
                                }
                            }
                        }
                    }).ToArray(),
                    startedAt = DateTime.UtcNow,
                    completedAt = DateTime.UtcNow
                };

                return Results.Ok(response);
            });
        }

        // Request/Response models
        private record BatchReadRequest
        {
            [System.Text.Json.Serialization.JsonPropertyName("inputs")]
            public ObjectIdInput[] Inputs { get; init; } = [];
        }
        
        private record ObjectIdInput
        {
            [System.Text.Json.Serialization.JsonPropertyName("id")]
            public string Id { get; init; } = string.Empty;
        }

        // V3 models (simpler format with type as string)
        private record BatchCreateRequestV3
        {
            [System.Text.Json.Serialization.JsonPropertyName("inputs")]
            public AssociationInputV3[] Inputs { get; init; } = [];
        }
        
        private record AssociationInputV3
        {
            [System.Text.Json.Serialization.JsonPropertyName("from")]
            public ObjectIdInput From { get; init; } = new();
            
            [System.Text.Json.Serialization.JsonPropertyName("to")]
            public ObjectIdInput To { get; init; } = new();
            
            [System.Text.Json.Serialization.JsonPropertyName("type")]
            public string Type { get; init; } = string.Empty;
        }
        
        private record BatchArchiveRequestV3
        {
            [System.Text.Json.Serialization.JsonPropertyName("inputs")]
            public AssociationInputV3[] Inputs { get; init; } = [];
        }

        // V4 models (more complex with Types array)
        private record BatchCreateRequest
        {
            [System.Text.Json.Serialization.JsonPropertyName("inputs")]
            public AssociationInput[] Inputs { get; init; } = [];
        }
        
        private record AssociationInput
        {
            [System.Text.Json.Serialization.JsonPropertyName("from")]
            public ObjectIdInput From { get; init; } = new();
            
            [System.Text.Json.Serialization.JsonPropertyName("to")]
            public ObjectIdInput To { get; init; } = new();
            
            [System.Text.Json.Serialization.JsonPropertyName("types")]
            public AssociationType[] Types { get; init; } = [];
        }
        
        private record AssociationType
        {
            [System.Text.Json.Serialization.JsonPropertyName("associationCategory")]
            public string AssociationCategory { get; init; } = string.Empty;
            
            [System.Text.Json.Serialization.JsonPropertyName("associationTypeId")]
            public int AssociationTypeId { get; init; }
        }

        private record BatchArchiveRequest
        {
            [System.Text.Json.Serialization.JsonPropertyName("inputs")]
            public AssociationInput[] Inputs { get; init; } = [];
        }

        private record BatchLabelsRequest(LabelInput[] Inputs);
        private record LabelInput(ObjectIdInput From, ObjectIdInput To, string[] Labels);

        private record CreateAssociationRequest(string AssociationTypeId, string AssociationCategory);

        private record CreateAssociationTypeRequest(string? Name, string? Label);
        private record UpdateAssociationTypeRequest(string Label);
    }
}

