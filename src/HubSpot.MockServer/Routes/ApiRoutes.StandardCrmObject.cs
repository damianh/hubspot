using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Webhooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    /// <summary>
    /// Registers standard CRUD endpoints for a CRM object type.
    /// This follows the HubSpot pattern: /crm/v3/objects/{objectType}
    /// </summary>
    /// <param name="app">The web application</param>
    /// <param name="route">The route path (e.g., "companies", "contacts", "0-3" for deals)</param>
    /// <param name="tag">The OpenAPI tag name for documentation</param>
    /// <param name="idParameterName">The name of the ID parameter (e.g., "companyId", "contactId")</param>
    internal static void RegisterStandardCrmObject(
        WebApplication app,
        string route,
        string tag,
        string idParameterName)
    {
        var group = app.MapGroup($"/crm/v3/objects/{route}")
            .WithTags(tag);

        // GET /crm/v3/objects/{objectType} - List objects
        group.MapGet("", (
            HubSpotObjectRepository repo,
            int limit = 10,
            string? after = null,
            bool archived = false,
            string[]? properties = null,
            string[]? propertiesWithHistory = null,
            string[]? associations = null) =>
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

                return new SimplePublicObject
                {
                    Id = obj.Id.Value.ToString(),
                    CreatedAt = obj.CreatedAt,
                    UpdatedAt = obj.UpdatedAt,
                    Properties = filteredProperties,
                    Archived = obj.Archived,
                    ArchivedAt = obj.ArchivedAt
                };
            }).ToList();

            var response = new CollectionResponseSimplePublicObject
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
        });

        // GET /crm/v3/objects/{objectType}/{objectId} - Get object by ID
        group.MapGet($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo,
            string[]? properties = null,
            string[]? propertiesWithHistory = null,
            string[]? associations = null,
            bool archived = false) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? hubSpotObject!.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            Dictionary<string, List<ValueWithTimestamp>>? historyProperties = null;
            if (propertiesWithHistory != null && propertiesWithHistory.Length > 0)
            {
                historyProperties = new Dictionary<string, List<ValueWithTimestamp>>();
                foreach (var propName in propertiesWithHistory)
                {
                    if (hubSpotObject!.Properties.TryGetValue(propName, out var property))
                    {
                        historyProperties[propName] = property.History
                            .Select(h => new ValueWithTimestamp
                            {
                                Value = h.Value,
                                Timestamp = h.Timestamp,
                                SourceId = "API",
                                SourceType = "API",
                                SourceLabel = "API"
                            })
                            .ToList();
                    }
                }
            }

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                PropertiesWithHistory = historyProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/{objectType} - Create object
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            HubSpotObjectRepository repo,
            WebhookEventChannel webhookChannel,
            TimeProvider timeProvider) =>
        {
            var hubSpotAssociations = inputForCreate.Associations
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

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var objectIdStr = hubSpotObject.Id.Value.ToString();
            var properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => (string?)p.Value.CurrentValue);
            webhookChannel.Writer.TryWrite(new ObjectCreatedEvent(route, objectIdStr, timeProvider.GetUtcNow(), properties));

            var simplePublicObject = new SimplePublicObject
            {
                Id = objectIdStr,
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            return Results.Created($"/crm/v3/objects/{route}/{simplePublicObject.Id}", simplePublicObject);
        });

        // PATCH /crm/v3/objects/{objectType}/{objectId} - Update object
        group.MapPatch($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo,
            WebhookEventChannel webhookChannel,
            TimeProvider timeProvider,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found)
            {
                return Results.NotFound();
            }

            foreach (var (key, value) in inputForUpdate.Properties)
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
                    hubSpotObject!.Properties.Add(key, hubSpotProperty);
                }
            }

            // Capture dirty properties before update commits them
            var changedProperties = hubSpotObject!.Properties.Values
                .Where(p => p.IsDirty)
                .Select(p => (p.Name, OldValue: p.CurrentValue, NewValue: p.NewValue))
                .ToList();

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

            var now = timeProvider.GetUtcNow();
            foreach (var (propName, oldValue, newValue) in changedProperties)
            {
                webhookChannel.Writer.TryWrite(new ObjectPropertyChangedEvent(
                    route, objectId, now, propName, oldValue, newValue));
            }

            var simplePublicObject = new SimplePublicObject
            {
                Id = updatedHubSpotObject!.Id.Value.ToString(),
                CreatedAt = updatedHubSpotObject.CreatedAt,
                UpdatedAt = updatedHubSpotObject.UpdatedAt,
                Properties = updatedHubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = updatedHubSpotObject.Archived,
                ArchivedAt = updatedHubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // DELETE /crm/v3/objects/{objectType}/{objectId} - Archive object
        group.MapDelete($"/{{{idParameterName}}}", (
            HttpContext context,
            HubSpotObjectRepository repo,
            WebhookEventChannel webhookChannel,
            TimeProvider timeProvider) =>
        {
            var objectId = context.Request.RouteValues[idParameterName]?.ToString();
            if (string.IsNullOrEmpty(objectId))
            {
                return Results.BadRequest();
            }

            var id = HubSpotObjectId.From(objectId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            webhookChannel.Writer.TryWrite(new ObjectDeletedEvent(route, objectId, timeProvider.GetUtcNow()));

            return Results.NoContent();
        });

        // POST /crm/v3/objects/{objectType}/search - Search objects
        group.MapPost("/search", (
            [FromBody] PublicObjectSearchRequest request,
            HubSpotObjectRepository repo) =>
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

            var results = paged.Select(obj =>
            {
                var filteredProperties = request.Properties is null || request.Properties.Count == 0
                    ? obj.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                    : obj.Properties
                        .Where(p => request.Properties.Contains(p.Key))
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
        });

        // POST /crm/v3/objects/{objectType}/merge - Merge objects
        group.MapPost("/merge", (
            [FromBody] PublicMergeInput input,
            HubSpotObjectRepository repo) =>
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

            // Merge properties from secondary into primary
            foreach (var (key, value) in mergeObject!.Properties)
            {
                if (!primaryObject!.Properties.ContainsKey(key))
                {
                    primaryObject.Properties.Add(key, new HubSpotObjectProperty(key, []) { NewValue = value.CurrentValue });
                }
            }

            repo.Update(primaryObject!);
            repo.Archive(mergeId);

            var merged = new SimplePublicObject
            {
                Id = primaryObject!.Id.Value.ToString(),
                CreatedAt = primaryObject.CreatedAt,
                UpdatedAt = primaryObject.UpdatedAt,
                Properties = primaryObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = primaryObject.Archived,
                ArchivedAt = primaryObject.ArchivedAt
            };

            return Results.Ok(merged);
        });

        // POST /crm/v3/objects/{objectType}/gdpr-delete - GDPR delete object
        group.MapPost("/gdpr-delete", (
            [FromBody] PublicGdprDeleteInput input,
            HubSpotObjectRepository repo) =>
        {
            var objectId = HubSpotObjectId.From(input.ObjectId);
            var archived = repo.Archive(objectId);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        // Register batch operations
        RegisterStandardCrmObjectBatchOperations(app, route, tag);
    }
}
