using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    // Map to track which object belongs to which type
    private static readonly Dictionary<int, string> _objectTypeMap = new();
    private static readonly object _objectTypeMapLock = new();

    internal static void RegisterCrmCompanies(WebApplication app) => RegisterStandardCrmObject(app, "companies", "Companies", "companyId");

    internal static void RegisterCrmContacts(WebApplication app) => RegisterStandardCrmObject(app, "contacts", "Contacts", "contactId");

    internal static void RegisterCrmDeals(WebApplication app) => RegisterStandardCrmObject(app, "0-3", "Deals", "dealId");

    internal static void RegisterCrmLineItems(WebApplication app) => RegisterStandardCrmObject(app, "line_items", "LineItems", "lineItemId");

    internal static void RegisterCrmTickets(WebApplication app) => RegisterStandardCrmObject(app, "tickets", "Tickets", "ticketId");

    internal static void RegisterCrmProducts(WebApplication app) => RegisterStandardCrmObject(app, "products", "Products", "productId");

    internal static void RegisterCrmQuotes(WebApplication app) => RegisterStandardCrmObject(app, "quotes", "Quotes", "quoteId");

    internal static void RegisterCrmCalls(WebApplication app) => RegisterStandardCrmObject(app, "calls", "Calls", "callId");

    internal static void RegisterCrmEmails(WebApplication app) => RegisterStandardCrmObject(app, "emails", "Emails", "emailId");

    internal static void RegisterCrmMeetings(WebApplication app) => RegisterStandardCrmObject(app, "meetings", "Meetings", "meetingId");

    internal static void RegisterCrmNotes(WebApplication app) => RegisterStandardCrmObject(app, "notes", "Notes", "noteId");

    internal static void RegisterCrmTasks(WebApplication app) => RegisterStandardCrmObject(app, "tasks", "Tasks", "taskId");

    internal static void RegisterCrmCommunications(WebApplication app) => RegisterStandardCrmObject(app, "communications", "Communications", "communicationId");

    internal static void RegisterCrmPostalMail(WebApplication app) => RegisterStandardCrmObject(app, "postal_mail", "PostalMail", "postalMailId");

    internal static void RegisterCrmFeedbackSubmissions(WebApplication app) => RegisterStandardCrmObject(app, "feedback_submissions", "FeedbackSubmissions", "feedbackSubmissionId");

    internal static void RegisterCrmGoals(WebApplication app) => RegisterStandardCrmObject(app, "goal_targets", "Goals", "goalId");

    // Batch 1: Additional standard objects
    internal static void RegisterCrmAppointments(WebApplication app) => RegisterStandardCrmObject(app, "appointments", "Appointments", "appointmentId");

    internal static void RegisterCrmLeads(WebApplication app) => RegisterStandardCrmObject(app, "leads", "Leads", "leadId");

    internal static void RegisterCrmUsers(WebApplication app) => RegisterStandardCrmObject(app, "users", "Users", "userId");

    // Batch 2: Commerce objects
    internal static void RegisterCrmCarts(WebApplication app) => RegisterStandardCrmObject(app, "carts", "Carts", "cartId");

    internal static void RegisterCrmOrders(WebApplication app) => RegisterStandardCrmObject(app, "orders", "Orders", "orderId");

    internal static void RegisterCrmInvoices(WebApplication app) => RegisterStandardCrmObject(app, "invoices", "Invoices", "invoiceId");

    internal static void RegisterCrmDiscounts(WebApplication app) => RegisterStandardCrmObject(app, "discounts", "Discounts", "discountId");

    internal static void RegisterCrmFees(WebApplication app) => RegisterStandardCrmObject(app, "fees", "Fees", "feeId");

    internal static void RegisterCrmTaxes(WebApplication app) => RegisterStandardCrmObject(app, "taxes", "Taxes", "taxId");

    internal static void RegisterCrmCommercePayments(WebApplication app) => RegisterStandardCrmObject(app, "commerce_payments", "CommercePayments", "commercePaymentId");

    internal static void RegisterCrmCommerceSubscriptions(WebApplication app) => RegisterStandardCrmObject(app, "commerce_subscriptions", "CommerceSubscriptions", "commerceSubscriptionId");

    // Batch 3: Specialized objects
    internal static void RegisterCrmListings(WebApplication app) => RegisterStandardCrmObject(app, "listings", "Listings", "listingId");

    internal static void RegisterCrmContracts(WebApplication app) => RegisterStandardCrmObject(app, "contracts", "Contracts", "contractId");

    internal static void RegisterCrmCourses(WebApplication app) => RegisterStandardCrmObject(app, "courses", "Courses", "courseId");

    internal static void RegisterCrmServices(WebApplication app) => RegisterStandardCrmObject(app, "services", "Services", "serviceId");

    internal static void RegisterCrmDealSplits(WebApplication app) => RegisterStandardCrmObject(app, "deal_splits", "DealSplits", "dealSplitId");

    internal static void RegisterCrmGoalTargets(WebApplication app) => RegisterStandardCrmObject(app, "goal_targets", "GoalTargets", "goalTargetId");

    internal static void RegisterCrmPartnerClients(WebApplication app) => RegisterStandardCrmObject(app, "partner_clients", "PartnerClients", "partnerClientId");

    internal static void RegisterCrmPartnerServices(WebApplication app) => RegisterStandardCrmObject(app, "partner_services", "PartnerServices", "partnerServiceId");

    internal static void RegisterCrmTranscriptions(WebApplication app) => RegisterStandardCrmObject(app, "transcriptions", "Transcriptions", "transcriptionId");

    /// <summary>
    /// Registers the generic CRM Objects API that works with any object type dynamically.
    /// This allows custom objects to be created without explicit registration.
    /// Pattern: /crm/v3/objects/{objectType}
    /// </summary>
    internal static void RegisterGenericCrmObjectsApi(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/{objectType}")
            .WithTags("Generic CRM Objects");

        // Individual operations
        group.MapGet("", GetGenericObjects);
        group.MapPost("", CreateGenericObject);
        group.MapGet("{objectId}", GetGenericObjectById);
        group.MapPatch("{objectId}", UpdateGenericObject);
        group.MapDelete("{objectId}", DeleteGenericObject);

        // Batch operations  
        group.MapPost("batch/create", BatchCreateGeneric);
        group.MapPost("batch/read", BatchReadGeneric);
        group.MapPost("batch/update", BatchUpdateGeneric);
        group.MapPost("batch/upsert", BatchUpsertGeneric);
        group.MapPost("batch/archive", BatchArchiveGeneric);

        // Search
        group.MapPost("search", SearchGenericObjects);
    }

    private static IResult GetGenericObjects(
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

        var allObjects = repo.List(10000, null, archived);

        // Filter by object type
        var objectsOfType = allObjects.Where(obj =>
        {
            lock (_objectTypeMapLock)
            {
                return _objectTypeMap.TryGetValue(obj.Id.Value, out var type) && type == objectType;
            }
        }).ToList();

        // Apply pagination
        var startIndex = 0;
        if (afterId.HasValue)
        {
            var afterIndex = objectsOfType.FindIndex(o => o.Id.Value == afterId.Value);
            if (afterIndex >= 0)
            {
                startIndex = afterIndex + 1;
            }
        }

        var paged = objectsOfType.Skip(startIndex).Take(limit).ToList();

        var results = paged.Select(obj =>
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
            Results = results,
            Paging = results.Count == limit && startIndex + limit < objectsOfType.Count
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

    private static IResult CreateGenericObject(
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
                    .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                    .ToArray()
            })
            .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
            .ToList();

        var newHubSpotObject = new NewHubSpotObject(input.Properties, hubSpotAssociations);
        var hubSpotObject = repo.Create(newHubSpotObject);

        // Track the object type
        lock (_objectTypeMapLock)
        {
            _objectTypeMap[hubSpotObject.Id.Value] = objectType;
        }

        var response = new CreatedResponseSimplePublicObject
        {
            CreatedResourceId = hubSpotObject.Id.Value.ToString(),
            Entity = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Archived = false,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
            },
            Location = $"/crm/v3/objects/{objectType}/{hubSpotObject.Id.Value}"
        };

        return Results.Ok(response);
    }

    private static IResult GetGenericObjectById(
        string objectType,
        string objectId,
        HubSpotObjectRepository repo,
        string[]? properties = null,
        string[]? propertiesWithHistory = null,
        string[]? associations = null,
        bool archived = false,
        string? idProperty = null)
    {
        if (!int.TryParse(objectId, out var id))
        {
            return Results.NotFound();
        }

        var hubSpotObjectId = HubSpotObjectId.From(id);
        if (!repo.TryRead(hubSpotObjectId, out var hubSpotObject))
        {
            return Results.NotFound();
        }

        // Check object type
        lock (_objectTypeMapLock)
        {
            if (!_objectTypeMap.TryGetValue(id, out var storedType) || storedType != objectType)
            {
                return Results.NotFound();
            }
        }

        if (hubSpotObject.Archived != archived)
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

    private static IResult UpdateGenericObject(
        string objectType,
        string objectId,
        [FromBody] SimplePublicObjectInput input,
        HubSpotObjectRepository repo,
        string? idProperty = null)
    {
        if (!int.TryParse(objectId, out var id))
        {
            return Results.NotFound();
        }

        var hubSpotObjectId = HubSpotObjectId.From(id);
        if (!repo.TryRead(hubSpotObjectId, out var hubSpotObject))
        {
            return Results.NotFound();
        }

        // Check object type
        lock (_objectTypeMapLock)
        {
            if (!_objectTypeMap.TryGetValue(id, out var storedType) || storedType != objectType)
            {
                return Results.NotFound();
            }
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
                hubSpotObject!.Properties.Add(key, hubSpotProperty);
            }
        }

        var updated = repo.Update(hubSpotObject!);

        var response = new SimplePublicObject
        {
            Id = updated.Id.Value.ToString(),
            CreatedAt = updated.CreatedAt,
            UpdatedAt = updated.UpdatedAt,
            Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
            Archived = updated.Archived,
        };

        return Results.Ok(response);
    }

    private static IResult DeleteGenericObject(
        string objectType,
        string objectId,
        HubSpotObjectRepository repo)
    {
        if (!int.TryParse(objectId, out var id))
        {
            return Results.NotFound();
        }

        var hubSpotObjectId = HubSpotObjectId.From(id);
        if (!repo.TryRead(hubSpotObjectId, out var hubSpotObject))
        {
            return Results.NotFound();
        }

        // Check object type
        lock (_objectTypeMapLock)
        {
            if (!_objectTypeMap.TryGetValue(id, out var storedType) || storedType != objectType)
            {
                return Results.NotFound();
            }
        }

        var archived = repo.Archive(hubSpotObjectId);

        if (!archived)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }

    private static IResult BatchCreateGeneric(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectInputForCreate input,
        HubSpotObjectRepository repo)
    {
        var results = new List<CreatedResponseSimplePublicObject>();

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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(item.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            // Track the object type
            lock (_objectTypeMapLock)
            {
                _objectTypeMap[hubSpotObject.Id.Value] = objectType;
            }

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Archived = false,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
            };

            results.Add(new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/{objectType}/{simplePublicObject.Id}"
            });
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = DateTime.UtcNow,
            StartedAt = DateTime.UtcNow,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchReadGeneric(
        string objectType,
        [FromBody] BatchReadInputSimplePublicObjectId input,
        HubSpotObjectRepository repo)
    {
        var results = new List<CreatedResponseSimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            if (int.TryParse(item.Id, out var id))
            {
                var hubSpotObjectId = HubSpotObjectId.From(id);
                if (repo.TryRead(hubSpotObjectId, out var hubSpotObject))
                {
                    // Check object type
                    lock (_objectTypeMapLock)
                    {
                        if (_objectTypeMap.TryGetValue(id, out var storedType) && storedType == objectType)
                        {
                            var simplePublicObject = new SimplePublicObject
                            {
                                Id = hubSpotObject.Id.Value.ToString(),
                                CreatedAt = hubSpotObject.CreatedAt,
                                UpdatedAt = hubSpotObject.UpdatedAt,
                                Archived = hubSpotObject.Archived,
                                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                            };

                            results.Add(new CreatedResponseSimplePublicObject
                            {
                                CreatedResourceId = simplePublicObject.Id,
                                Entity = simplePublicObject,
                                Location = $"/crm/v3/objects/{objectType}/{simplePublicObject.Id}"
                            });
                        }
                    }
                }
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = DateTime.UtcNow,
            StartedAt = DateTime.UtcNow,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchUpdateGeneric(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectBatchInput input,
        HubSpotObjectRepository repo)
    {
        var results = new List<CreatedResponseSimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            if (int.TryParse(item.Id, out var id))
            {
                var hubSpotObjectId = HubSpotObjectId.From(id);
                if (repo.TryRead(hubSpotObjectId, out var hubSpotObject))
                {
                    // Check object type
                    bool isCorrectType;
                    lock (_objectTypeMapLock)
                    {
                        isCorrectType = _objectTypeMap.TryGetValue(id, out var storedType) && storedType == objectType;
                    }

                    if (isCorrectType)
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

                        var simplePublicObject = new SimplePublicObject
                        {
                            Id = updated.Id.Value.ToString(),
                            CreatedAt = updated.CreatedAt,
                            UpdatedAt = updated.UpdatedAt,
                            Archived = updated.Archived,
                            Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                        };

                        results.Add(new CreatedResponseSimplePublicObject
                        {
                            CreatedResourceId = simplePublicObject.Id,
                            Entity = simplePublicObject,
                            Location = $"/crm/v3/objects/{objectType}/{simplePublicObject.Id}"
                        });
                    }
                }
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = DateTime.UtcNow,
            StartedAt = DateTime.UtcNow,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchUpsertGeneric(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectBatchInputUpsert input,
        HubSpotObjectRepository repo)
    {
        var results = new List<CreatedResponseSimplePublicObject>();

        foreach (var item in input.Inputs)
        {
            HubSpotObject? hubSpotObject = null;

            if (!string.IsNullOrEmpty(item.Id) && int.TryParse(item.Id, out var id))
            {
                var hubSpotObjectId = HubSpotObjectId.From(id);
                repo.TryRead(hubSpotObjectId, out hubSpotObject);

                // Check if it's the correct object type
                if (hubSpotObject != null)
                {
                    lock (_objectTypeMapLock)
                    {
                        if (!_objectTypeMap.TryGetValue(id, out var storedType) || storedType != objectType)
                        {
                            hubSpotObject = null;
                        }
                    }
                }
            }

            if (hubSpotObject != null)
            {
                // Update existing object
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

                var simplePublicObject = new SimplePublicObject
                {
                    Id = updated.Id.Value.ToString(),
                    CreatedAt = updated.CreatedAt,
                    UpdatedAt = updated.UpdatedAt,
                    Archived = updated.Archived,
                    Properties = updated.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                };

                results.Add(new CreatedResponseSimplePublicObject
                {
                    CreatedResourceId = simplePublicObject.Id,
                    Entity = simplePublicObject,
                    Location = $"/crm/v3/objects/{objectType}/{simplePublicObject.Id}"
                });
            }
            else
            {
                // Create new object
                var newHubSpotObject = new NewHubSpotObject(item.Properties, []);
                var created = repo.Create(newHubSpotObject);

                // Track the object type
                lock (_objectTypeMapLock)
                {
                    _objectTypeMap[created.Id.Value] = objectType;
                }

                var simplePublicObject = new SimplePublicObject
                {
                    Id = created.Id.Value.ToString(),
                    CreatedAt = created.CreatedAt,
                    UpdatedAt = created.UpdatedAt,
                    Archived = false,
                    Properties = created.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue)
                };

                results.Add(new CreatedResponseSimplePublicObject
                {
                    CreatedResourceId = simplePublicObject.Id,
                    Entity = simplePublicObject,
                    Location = $"/crm/v3/objects/{objectType}/{simplePublicObject.Id}"
                });
            }
        }

        var response = new BatchResponseSimplePublicObject
        {
            CompletedAt = DateTime.UtcNow,
            StartedAt = DateTime.UtcNow,
            Status = "COMPLETE",
            Results = results
        };

        return Results.Ok(response);
    }

    private static IResult BatchArchiveGeneric(
        string objectType,
        [FromBody] BatchInputSimplePublicObjectId input,
        HubSpotObjectRepository repo)
    {
        foreach (var item in input.Inputs)
        {
            if (int.TryParse(item.Id, out var id))
            {
                var hubSpotObjectId = HubSpotObjectId.From(id);
                if (repo.TryRead(hubSpotObjectId, out var hubSpotObject))
                {
                    // Check object type
                    lock (_objectTypeMapLock)
                    {
                        if (_objectTypeMap.TryGetValue(id, out var storedType) && storedType == objectType)
                        {
                            repo.Archive(hubSpotObjectId);
                        }
                    }
                }
            }
        }

        return Results.NoContent();
    }

    private static IResult SearchGenericObjects(
        string objectType,
        [FromBody] PublicObjectSearchRequest request,
        HubSpotObjectRepository repo)
    {
        var allObjects = repo.List(10000, null, false);

        // Filter by object type
        var objectsOfType = allObjects.Where(obj =>
        {
            lock (_objectTypeMapLock)
            {
                return _objectTypeMap.TryGetValue(obj.Id.Value, out var type) && type == objectType;
            }
        }).ToList();

        var filtered = objectsOfType.Where(obj =>
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
}
