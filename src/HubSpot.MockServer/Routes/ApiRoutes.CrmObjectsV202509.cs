using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static void RegisterCrmObjectsV202509(WebApplication app)
    {
        var group = app.MapGroup("/crm/objects/2025-09/{objectType}")
            .WithTags("CRM Objects V202509");

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
        group.MapPost("gdpr-delete", GdprDeleteV202509);
    }

    private static IResult GetObjectsV202509(
        string objectType,
        HubSpotObjectRepository repo,
        int limit = 10,
        string? after = null,
        bool archived = false,
        string[]? properties = null,
        string[]? propertiesWithHistory = null,
        string[]? associations = null)
    {
        int? afterId = null;
        if (!string.IsNullOrEmpty(after) && int.TryParse(after, out var parsedAfter))
        {
            afterId = parsedAfter;
        }

        var objects = repo.List(limit, afterId, archived);

        var results = objects.Select(obj =>
        {
            var filteredProperties = properties is null || properties.Length == 0
                ? obj.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                : obj.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            return new SimplePublicObjectWithAssociations
            {
                Id = obj.Id.Value.ToString(),
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                Archived = obj.Archived,
                Properties = filteredProperties,
                Associations = new Dictionary<string, object>()
            };
        }).ToList();

        var response = new CollectionResponseSimplePublicObjectWithAssociations
        {
            Results = results
        };

        if (results.Count == limit)
        {
            var lastId = results.Last().Id;
            response.Paging = new Paging
            {
                Next = new NextPage
                {
                    After = lastId
                }
            };
        }

        return Results.Ok(response);
    }

    private static IResult CreateObjectV202509(
        string objectType,
        [FromBody] SimplePublicObjectInputForCreate input,
        HubSpotObjectRepository repo)
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
                    .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory ?? string.Empty))
                    .ToArray()
            })
            .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
            .ToList();

        var newHubSpotObject = new NewHubSpotObject(input.Properties, hubSpotAssociations);
        var hubSpotObject = repo.Create(newHubSpotObject);

        var simplePublicObject = new SimplePublicObject
        {
            Id = hubSpotObject.Id.Value.ToString(),
            CreatedAt = hubSpotObject.CreatedAt,
            UpdatedAt = hubSpotObject.UpdatedAt,
            Archived = false,
            Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
        };

        return Results.Created($"/crm/objects/2025-09/{objectType}/{hubSpotObject.Id.Value}", simplePublicObject);
    }

    private static IResult GetObjectByIdV202509(
        string objectType,
        string objectId,
        HubSpotObjectRepository repo,
        string[]? properties = null,
        string[]? propertiesWithHistory = null,
        string[]? associations = null,
        bool archived = false)
    {
        var id = HubSpotObjectId.From(objectId);

        if (!repo.TryRead(id, out var hubSpotObject))
        {
            return Results.NotFound();
        }

        if (hubSpotObject!.Archived != archived)
        {
            return Results.NotFound();
        }

        var filteredProperties = properties is null || properties.Length == 0
            ? hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
            : hubSpotObject.Properties
                .Where(p => properties.Contains(p.Key))
                .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

        var response = new SimplePublicObjectWithAssociations
        {
            Id = hubSpotObject.Id.Value.ToString(),
            CreatedAt = hubSpotObject.CreatedAt,
            UpdatedAt = hubSpotObject.UpdatedAt,
            Archived = hubSpotObject.Archived,
            Properties = filteredProperties,
            Associations = new Dictionary<string, object>()
        };

        return Results.Ok(response);
    }

    private static IResult UpdateObjectV202509(
        string objectType,
        string objectId,
        [FromBody] SimplePublicObjectInput input,
        HubSpotObjectRepository repo)
    {
        var id = HubSpotObjectId.From(objectId);

        if (!repo.TryRead(id, out var hubSpotObject))
        {
            return Results.NotFound();
        }

        foreach (var (key, value) in input.Properties)
        {
            if (hubSpotObject!.Properties.TryGetValue(key, out var existingProperty))
            {
                existingProperty.NewValue = value;
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

        var updated = repo.Update(hubSpotObject!);

        var response = new SimplePublicObject
        {
            Id = updated.Id.Value.ToString(),
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt,
            Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
            Archived = updated.Archived
        };

        return Results.Ok(response);
    }

    private static IResult DeleteObjectV202509(
        string objectType,
        string objectId,
        HubSpotObjectRepository repo)
    {
        var id = HubSpotObjectId.From(objectId);

        if (!repo.TryRead(id, out _))
        {
            return Results.NotFound();
        }

        var archived = repo.Archive(id);

        if (!archived)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }

    private static IResult BatchCreateV202509(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectInputForCreate input,
        HubSpotObjectRepository repo,
        TimeProvider timeProvider)
    {
        var results = new List<SimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            var hubSpotAssociations = item.Associations
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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory ?? string.Empty))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(item.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            results.Add(new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Archived = false,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
            });
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = timeProvider.GetUtcNow().UtcDateTime,
            StartedAt = timeProvider.GetUtcNow().UtcDateTime,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchReadV202509(
        string objectType,
        [FromBody] BatchReadInputSimplePublicObjectId input,
        HubSpotObjectRepository repo,
        TimeProvider timeProvider)
    {
        var results = new List<SimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            var id = HubSpotObjectId.From(item.Id);
            if (repo.TryRead(id, out var hubSpotObject))
            {
                results.Add(new SimplePublicObject
                {
                    Id = hubSpotObject!.Id.Value.ToString(),
                    CreatedAt = hubSpotObject.CreatedAt,
                    UpdatedAt = hubSpotObject.UpdatedAt,
                    Archived = hubSpotObject.Archived,
                    Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                });
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = timeProvider.GetUtcNow().UtcDateTime,
            StartedAt = timeProvider.GetUtcNow().UtcDateTime,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchUpdateV202509(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectBatchInput input,
        HubSpotObjectRepository repo,
        TimeProvider timeProvider)
    {
        var results = new List<SimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            var id = HubSpotObjectId.From(item.Id);
            if (repo.TryRead(id, out var hubSpotObject))
            {
                foreach (var (key, value) in item.Properties)
                {
                    if (hubSpotObject!.Properties.TryGetValue(key, out var existingProperty))
                    {
                        existingProperty.NewValue = value;
                    }
                    else
                    {
                        hubSpotObject.Properties.Add(key, new HubSpotObjectProperty(key, []) { NewValue = value });
                    }
                }

                var updated = repo.Update(hubSpotObject!);

                results.Add(new SimplePublicObject
                {
                    Id = updated.Id.Value.ToString(),
                    CreatedAt = updated.CreatedAt,
                    UpdatedAt = updated.UpdatedAt,
                    Archived = updated.Archived,
                    Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                });
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = timeProvider.GetUtcNow().UtcDateTime,
            StartedAt = timeProvider.GetUtcNow().UtcDateTime,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchUpsertV202509(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectBatchInputUpsert input,
        HubSpotObjectRepository repo,
        TimeProvider timeProvider)
    {
        var results = new List<SimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            HubSpotObject? hubSpotObject = null;

            if (!string.IsNullOrEmpty(item.Id))
            {
                var id = HubSpotObjectId.From(item.Id);
                repo.TryRead(id, out hubSpotObject);
            }

            if (hubSpotObject != null)
            {
                foreach (var (key, value) in item.Properties)
                {
                    if (hubSpotObject.Properties.TryGetValue(key, out var existingProperty))
                    {
                        existingProperty.NewValue = value;
                    }
                    else
                    {
                        hubSpotObject.Properties.Add(key, new HubSpotObjectProperty(key, []) { NewValue = value });
                    }
                }

                var updated = repo.Update(hubSpotObject);

                results.Add(new SimplePublicObject
                {
                    Id = updated.Id.Value.ToString(),
                    CreatedAt = updated.CreatedAt,
                    UpdatedAt = updated.UpdatedAt,
                    Archived = updated.Archived,
                    Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                });
            }
            else
            {
                var newHubSpotObject = new NewHubSpotObject(item.Properties, []);
                var created = repo.Create(newHubSpotObject);

                results.Add(new SimplePublicObject
                {
                    Id = created.Id.Value.ToString(),
                    CreatedAt = created.CreatedAt,
                    UpdatedAt = created.UpdatedAt,
                    Archived = false,
                    Properties = created.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                });
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = timeProvider.GetUtcNow().UtcDateTime,
            StartedAt = timeProvider.GetUtcNow().UtcDateTime,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchArchiveV202509(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectId input,
        HubSpotObjectRepository repo)
    {
        foreach (var item in input.Inputs)
        {
            var id = HubSpotObjectId.From(item.Id);
            repo.Archive(id);
        }

        return Results.NoContent();
    }

    private static IResult SearchObjectsV202509(
        string objectType,
        [FromBody] PublicObjectSearchRequest request,
        HubSpotObjectRepository repo)
    {
        var allObjects = repo.List(10000, null, false);

        var filtered = allObjects.Where(obj =>
        {
            if (request.FilterGroups == null || request.FilterGroups.Count == 0)
            {
                return true;
            }

            return request.FilterGroups.Any(group =>
            {
                if (group.Filters == null || group.Filters.Count == 0)
                {
                    return true;
                }

                return group.Filters.All(filter =>
                {
                    if (!obj.Properties.TryGetValue(filter.PropertyName, out var property))
                    {
                        return false;
                    }

                    var value = property.CurrentValue;
                    var filterValue = filter.Value;

                    return filter.Operator?.ToUpperInvariant() switch
                    {
                        "EQ" => value == filterValue,
                        "NEQ" => value != filterValue,
                        "LT" => string.Compare(value, filterValue, StringComparison.Ordinal) < 0,
                        "LTE" => string.Compare(value, filterValue, StringComparison.Ordinal) <= 0,
                        "GT" => string.Compare(value, filterValue, StringComparison.Ordinal) > 0,
                        "GTE" => string.Compare(value, filterValue, StringComparison.Ordinal) >= 0,
                        "CONTAINS_TOKEN" => value?.Contains(filterValue ?? "", StringComparison.OrdinalIgnoreCase) == true,
                        _ => true
                    };
                });
            });
        }).ToList();

        var sorted = request.Sorts != null && request.Sorts.Count > 0
            ? filtered.OrderBy(obj =>
            {
                var sortProperty = request.Sorts[0].PropertyName;
                return obj.Properties.TryGetValue(sortProperty, out var prop) ? prop.CurrentValue : "";
            }).ToList()
            : filtered;

        var limit = request.Limit ?? 10;
        var after = request.After != null && int.TryParse(request.After, out var afterId) ? afterId : (int?)null;

        var paged = after.HasValue
            ? sorted.SkipWhile(obj => obj.Id!.Value != after.Value).Skip(1).Take(limit).ToList()
            : sorted.Take(limit).ToList();

        var results = paged.Select(obj => new SimplePublicObjectWithAssociations
        {
            Id = obj.Id.Value.ToString(),
            CreatedAt = obj.CreatedAt,
            UpdatedAt = obj.UpdatedAt,
            Archived = obj.Archived,
            Properties = obj.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
            Associations = new Dictionary<string, object>()
        }).ToList();

        var response = new CollectionResponseWithTotalSimplePublicObjectWithAssociations
        {
            Results = results,
            Total = filtered.Count,
            Paging = results.Count == limit
                ? new Paging
                {
                    Next = new NextPage
                    {
                        After = results[^1].Id
                    }
                }
                : null
        };

        return Results.Ok(response);
    }

    private static IResult MergeV202509(
        string objectType,
        [FromBody] PublicMergeInput input,
        HubSpotObjectRepository repo)
    {
        var primaryId = HubSpotObjectId.From(input.PrimaryObjectId);
        var mergeId = HubSpotObjectId.From(input.ObjectIdToMerge);

        if (!repo.TryRead(primaryId, out var primaryObject))
        {
            return Results.NotFound();
        }

        if (!repo.TryRead(mergeId, out var mergeObject))
        {
            return Results.NotFound();
        }

        foreach (var (key, value) in mergeObject!.Properties)
        {
            if (!primaryObject!.Properties.ContainsKey(key))
            {
                primaryObject.Properties.Add(key, new HubSpotObjectProperty(key, []) { NewValue = value.CurrentValue });
            }
        }

        repo.Update(primaryObject!);
        repo.Archive(mergeId);

        var merged = new SimplePublicObjectWithAssociations
        {
            Id = primaryObject!.Id.Value.ToString(),
            CreatedAt = primaryObject.CreatedAt,
            UpdatedAt = primaryObject.UpdatedAt,
            Properties = primaryObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
            Archived = primaryObject.Archived,
            Associations = new Dictionary<string, object>()
        };

        return Results.Ok(merged);
    }

    private static IResult GdprDeleteV202509(
        string objectType,
        [FromBody] PublicGdprDeleteInput input,
        HubSpotObjectRepository repo)
    {
        var objectId = HubSpotObjectId.From(input.ObjectId);
        var archived = repo.Archive(objectId);

        if (!archived)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
}
