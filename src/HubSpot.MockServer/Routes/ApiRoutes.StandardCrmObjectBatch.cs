using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    /// <summary>
    /// Registers batch endpoints for a standard CRM object type.
    /// Pattern: /crm/v3/objects/{objectType}/batch/{operation}
    /// </summary>
    internal static void RegisterStandardCrmObjectBatchOperations(
        WebApplication app,
        string route,
        string tag)
    {
        var batchGroup = app.MapGroup($"/crm/v3/objects/{route}/batch")
            .WithTags(tag);

        // POST /crm/v3/objects/{objectType}/batch/create
        batchGroup.MapPost("/create", (
            [FromBody] BatchInputSimplePublicObjectInputForCreate batchInput,
            HubSpotObjectRepository repo) =>
        {
            if (batchInput?.Inputs == null || batchInput.Inputs.Count == 0)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var results = new List<SimplePublicObject>();

            foreach (var input in batchInput.Inputs)
            {
                var hubSpotAssociations = input.Associations
                    .Select(association => new
                    {
                        Association = association,
                        To = new HubSpotAssociationTo(association.To.Id)
                    })
                    .Select(to => new
                    {
                        To = to,
                        AssociationTypes = to.Association
                            .Types
                            .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                            .ToArray()
                    })
                    .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                    .ToList();

                var newHubSpotObject = new NewHubSpotObject(input.Properties, hubSpotAssociations);
                var hubSpotObject = repo.Create(newHubSpotObject);

                results.Add(new SimplePublicObject
                {
                    Id = hubSpotObject.Id.Value.ToString(),
                    CreatedAt = hubSpotObject.CreatedAt,
                    UpdatedAt = hubSpotObject.UpdatedAt,
                    Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                    Archived = hubSpotObject.Archived,
                    ArchivedAt = hubSpotObject.ArchivedAt
                });
            }

            return Results.Ok(new BatchResponseSimplePublicObject
            {
                Status = "COMPLETE",
                Results = results
            });
        });

        // POST /crm/v3/objects/{objectType}/batch/read
        batchGroup.MapPost("/read", (
            [FromBody] BatchReadInputSimplePublicObjectId batchInput,
            HubSpotObjectRepository repo,
            [FromQuery] string[]? properties = null) =>
        {
            if (batchInput?.Inputs == null || batchInput.Inputs.Count == 0)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var results = new List<SimplePublicObject>();

            foreach (var input in batchInput.Inputs)
            {
                var id = HubSpotObjectId.From(input.Id);
                if (repo.TryRead(id, out var hubSpotObject) && !hubSpotObject!.Archived)
                {
                    var filteredProperties = properties is null || properties.Length == 0
                        ? hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                        : hubSpotObject.Properties
                            .Where(p => properties.Contains(p.Key))
                            .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

                    results.Add(new SimplePublicObject
                    {
                        Id = hubSpotObject.Id.Value.ToString(),
                        CreatedAt = hubSpotObject.CreatedAt,
                        UpdatedAt = hubSpotObject.UpdatedAt,
                        Properties = filteredProperties,
                        Archived = hubSpotObject.Archived,
                        ArchivedAt = hubSpotObject.ArchivedAt
                    });
                }
            }

            return Results.Ok(new BatchResponseSimplePublicObject
            {
                Status = "COMPLETE",
                Results = results
            });
        });

        // POST /crm/v3/objects/{objectType}/batch/update
        batchGroup.MapPost("/update", (
            [FromBody] BatchInputSimplePublicObjectBatchInput batchInput,
            HubSpotObjectRepository repo) =>
        {
            if (batchInput?.Inputs == null || batchInput.Inputs.Count == 0)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var results = new List<SimplePublicObject>();

            foreach (var input in batchInput.Inputs)
            {
                var id = HubSpotObjectId.From(input.Id);
                if (repo.TryRead(id, out var hubSpotObject))
                {
                    foreach (var (key, value) in input.Properties)
                    {
                        if (hubSpotObject!.Properties.TryGetValue(key, out var property))
                        {
                            property.NewValue = value;
                        }
                        else
                        {
                            var hubSpotProperty = new HubSpotObjectProperty(key, [])
                            {
                                NewValue = value
                            };
                            hubSpotObject.Properties.Add(key, hubSpotProperty);
                        }
                    }

                    repo.Update(hubSpotObject);
                    repo.TryRead(hubSpotObject.Id, out var updatedHubSpotObject);

                    results.Add(new SimplePublicObject
                    {
                        Id = updatedHubSpotObject!.Id.Value.ToString(),
                        CreatedAt = updatedHubSpotObject.CreatedAt,
                        UpdatedAt = updatedHubSpotObject.UpdatedAt,
                        Properties = updatedHubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                        Archived = updatedHubSpotObject.Archived,
                        ArchivedAt = updatedHubSpotObject.ArchivedAt
                    });
                }
            }

            return Results.Ok(new BatchResponseSimplePublicObject
            {
                Status = "COMPLETE",
                Results = results
            });
        });

        // POST /crm/v3/objects/{objectType}/batch/archive
        batchGroup.MapPost("/archive", (
            [FromBody] BatchInputSimplePublicObjectId batchInput,
            HubSpotObjectRepository repo) =>
        {
            if (batchInput?.Inputs == null || batchInput.Inputs.Count == 0)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            foreach (var input in batchInput.Inputs)
            {
                var id = HubSpotObjectId.From(input.Id);
                repo.Archive(id);
            }

            return Results.NoContent();
        });

        // POST /crm/v3/objects/{objectType}/batch/upsert
        batchGroup.MapPost("/upsert", (
            [FromBody] BatchInputSimplePublicObjectBatchInputUpsert batchInput,
            HubSpotObjectRepository repo) =>
        {
            if (batchInput?.Inputs == null || batchInput.Inputs.Count == 0)
            {
                return Results.BadRequest(new { message = "Invalid request body" });
            }

            var results = new List<SimplePublicObject>();

            foreach (var input in batchInput.Inputs)
            {
                HubSpotObject? hubSpotObject = null;

                // Try to find existing object by ID if provided
                if (!string.IsNullOrEmpty(input.Id))
                {
                    var id = HubSpotObjectId.From(input.Id);
                    repo.TryRead(id, out hubSpotObject);
                }

                // If object exists, update it
                if (hubSpotObject != null)
                {
                    foreach (var (key, value) in input.Properties)
                    {
                        if (hubSpotObject.Properties.TryGetValue(key, out var property))
                        {
                            property.NewValue = value;
                        }
                        else
                        {
                            var hubSpotProperty = new HubSpotObjectProperty(key, [])
                            {
                                NewValue = value
                            };
                            hubSpotObject.Properties.Add(key, hubSpotProperty);
                        }
                    }

                    repo.Update(hubSpotObject);
                    repo.TryRead(hubSpotObject.Id, out var updatedHubSpotObject);

                    results.Add(new SimplePublicObject
                    {
                        Id = updatedHubSpotObject!.Id.Value.ToString(),
                        CreatedAt = updatedHubSpotObject.CreatedAt,
                        UpdatedAt = updatedHubSpotObject.UpdatedAt,
                        Properties = updatedHubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                        Archived = updatedHubSpotObject.Archived,
                        ArchivedAt = updatedHubSpotObject.ArchivedAt
                    });
                }
                else
                {
                    // Create new object
                    var newHubSpotObject = new NewHubSpotObject(
                        input.Properties,
                        new List<HubSpotAssociation>());
                    hubSpotObject = repo.Create(newHubSpotObject);

                    results.Add(new SimplePublicObject
                    {
                        Id = hubSpotObject.Id.Value.ToString(),
                        CreatedAt = hubSpotObject.CreatedAt,
                        UpdatedAt = hubSpotObject.UpdatedAt,
                        Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                        Archived = hubSpotObject.Archived,
                        ArchivedAt = hubSpotObject.ArchivedAt
                    });
                }
            }

            return Results.Ok(new BatchResponseSimplePublicUpsertObject
            {
                Status = "COMPLETE",
                Results = results
            });
        });
    }
}
