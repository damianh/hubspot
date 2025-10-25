using DamianH.HubSpot.MockServer.Apis.Models;
using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static void RegisterCrmCompanies(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/companies")
            .WithTags("Companies");

        // GET /crm/v3/objects/companies - List companies
        group.MapGet("", (
            [FromServices] HubSpotObjectRepository repo,
            [FromQuery] int limit = 10,
            [FromQuery] string? after = null,
            [FromQuery] bool archived = false,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null) =>
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

            // Add paging if there might be more results
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

        // GET /crm/v3/objects/companies/{companyId} - Get company by ID
        group.MapGet("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null,
            [FromQuery] bool archived = false) =>
        {
            var id = HubSpotObjectId.From(companyId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>() // Return empty properties if none requested
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/companies - Create company
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            [FromServices] HubSpotObjectRepository repo) =>
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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            var createdResponse = new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/companies/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/companies/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/companies/{companyId} - Update company
        group.MapPatch("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var id = HubSpotObjectId.From(companyId);
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

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

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

        // DELETE /crm/v3/objects/companies/{companyId} - Archive company
        group.MapDelete("/{companyId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string companyId) =>
        {
            var id = HubSpotObjectId.From(companyId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }

    internal static void RegisterCrmContacts(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/contacts")
            .WithTags("Contacts");

        // GET /crm/v3/objects/contacts - List contacts
        group.MapGet("", (
            [FromServices] HubSpotObjectRepository repo,
            [FromQuery] int limit = 10,
            [FromQuery] string? after = null,
            [FromQuery] bool archived = false,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null) =>
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

        // GET /crm/v3/objects/contacts/{contactId} - Get contact by ID
        group.MapGet("/{contactId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string contactId,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null,
            [FromQuery] bool archived = false) =>
        {
            var id = HubSpotObjectId.From(contactId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>()
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/contacts - Create contact
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            [FromServices] HubSpotObjectRepository repo) =>
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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            var createdResponse = new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/contacts/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/contacts/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/contacts/{contactId} - Update contact
        group.MapPatch("/{contactId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string contactId,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var id = HubSpotObjectId.From(contactId);
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

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

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

        // DELETE /crm/v3/objects/contacts/{contactId} - Archive contact
        group.MapDelete("/{contactId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string contactId) =>
        {
            var id = HubSpotObjectId.From(contactId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }

    internal static void RegisterCrmDeals(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/0-3")
            .WithTags("Deals");

        // GET /crm/v3/objects/0-3 - List deals
        group.MapGet("", (
            [FromServices] HubSpotObjectRepository repo,
            [FromQuery] int limit = 10,
            [FromQuery] string? after = null,
            [FromQuery] bool archived = false,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null) =>
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

        // GET /crm/v3/objects/0-3/{dealId} - Get deal by ID
        group.MapGet("/{dealId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string dealId,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null,
            [FromQuery] bool archived = false) =>
        {
            var id = HubSpotObjectId.From(dealId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>()
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/0-3 - Create deal
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            [FromServices] HubSpotObjectRepository repo) =>
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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            var createdResponse = new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/0-3/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/0-3/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/0-3/{dealId} - Update deal
        group.MapPatch("/{dealId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string dealId,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var id = HubSpotObjectId.From(dealId);
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

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

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

        // DELETE /crm/v3/objects/0-3/{dealId} - Archive deal
        group.MapDelete("/{dealId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string dealId) =>
        {
            var id = HubSpotObjectId.From(dealId);
            var archived = repo.Archive(id);

            if (!archived)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }

    internal static void RegisterCrmLineItems(WebApplication app)
    {
        var group = app.MapGroup("/crm/v3/objects/line_items")
            .WithTags("LineItems");

        // GET /crm/v3/objects/line_items - List line items
        group.MapGet("", (
            [FromServices] HubSpotObjectRepository repo,
            [FromQuery] int limit = 10,
            [FromQuery] string? after = null,
            [FromQuery] bool archived = false,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null) =>
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

        // GET /crm/v3/objects/line_items/{lineItemId} - Get line item by ID
        group.MapGet("/{lineItemId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string lineItemId,
            [FromQuery] string[]? properties = null,
            [FromQuery] string[]? propertiesWithHistory = null,
            [FromQuery] string[]? associations = null,
            [FromQuery] bool archived = false) =>
        {
            var id = HubSpotObjectId.From(lineItemId);
            var found = repo.TryRead(id, out var hubSpotObject);

            if (!found || hubSpotObject!.Archived)
            {
                return Results.NotFound();
            }

            var filteredProperties = properties is null || properties.Length == 0
                ? new Dictionary<string, string>()
                : hubSpotObject!.Properties
                    .Where(p => properties.Contains(p.Key))
                    .ToDictionary(p => p.Key, p => p.Value.CurrentValue);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject!.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = filteredProperties,
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };
            return Results.Ok(simplePublicObject);
        });

        // POST /crm/v3/objects/line_items - Create line item
        group.MapPost("", (
            [FromBody] SimplePublicObjectInputForCreate inputForCreate,
            [FromServices] HubSpotObjectRepository repo) =>
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
                        .Select(t => new HubSpotAssociationType(t.AssociationTypeId, t.AssociationCategory))
                        .ToArray()
                })
                .Select(t => new HubSpotAssociation(t.To.To, t.AssociationTypes))
                .ToList();

            var newHubSpotObject = new NewHubSpotObject(inputForCreate.Properties, hubSpotAssociations);
            var hubSpotObject = repo.Create(newHubSpotObject);

            var simplePublicObject = new SimplePublicObject
            {
                Id = hubSpotObject.Id.Value.ToString(),
                CreatedAt = hubSpotObject.CreatedAt,
                UpdatedAt = hubSpotObject.UpdatedAt,
                Properties = hubSpotObject.Properties.ToDictionary(p => p.Key, p => p.Value.CurrentValue),
                Archived = hubSpotObject.Archived,
                ArchivedAt = hubSpotObject.ArchivedAt
            };

            var createdResponse = new CreatedResponseSimplePublicObject
            {
                CreatedResourceId = simplePublicObject.Id,
                Entity = simplePublicObject,
                Location = $"/crm/v3/objects/line_items/{simplePublicObject.Id}"
            };

            return Results.Created($"/crm/v3/objects/line_items/{simplePublicObject.Id}", createdResponse);
        });

        // PATCH /crm/v3/objects/line_items/{lineItemId} - Update line item
        group.MapPatch("/{lineItemId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string lineItemId,
            [FromBody] SimplePublicObjectInput inputForUpdate) =>
        {
            var id = HubSpotObjectId.From(lineItemId);
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

            repo.Update(hubSpotObject!);
            repo.TryRead(hubSpotObject!.Id, out var updatedHubSpotObject);

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

        // DELETE /crm/v3/objects/line_items/{lineItemId} - Archive line item
        group.MapDelete("/{lineItemId}", (
            [FromServices] HubSpotObjectRepository repo,
            [FromRoute] string lineItemId) =>
        {
            var id = HubSpotObjectId.From(lineItemId);
            var archived = repo.Archive(id);

            return !archived 
                ? Results.NotFound() 
                : Results.NoContent();
        });
    }
}