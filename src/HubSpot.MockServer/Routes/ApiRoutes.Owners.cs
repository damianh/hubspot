using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.Owner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static partial class Owners
    {
        /// <summary>
        /// Register CRM Owners V3 API routes
        /// </summary>
        internal static void RegisterOwnersV3(WebApplication app)
        {
            var group = app.MapGroup("/crm/v3/owners")
                .WithTags("Owners V3");

            // Get all owners
            group.MapGet("", (
                string? email,
                bool? archived,
                string? after,
                int? limit,
                OwnerRepository repo) =>
            {
                var (owners, nextAfter) = repo.GetAllOwners(email, archived, after, limit);

                var results = owners.Select(o => new
                {
                    id = o.Id,
                    email = o.Email,
                    firstName = o.FirstName,
                    lastName = o.LastName,
                    type = o.Type,
                    archived = o.Archived,
                    userId = o.UserId,
                    userIdIncludingInactive = o.UserIdIncludingInactive,
                    createdAt = o.CreatedAt,
                    updatedAt = o.UpdatedAt,
                    teams = o.Teams?.Select(t => new
                    {
                        id = t.Id,
                        name = t.Name,
                        primary = t.Primary
                    }).ToArray()
                }).ToArray();

                var response = new
                {
                    results = results,
                    paging = nextAfter != null ? new
                    {
                        next = new
                        {
                            after = nextAfter,
                            link = $"?after={nextAfter}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            // Create an owner
            group.MapPost("", (
                [FromBody] OwnerCreateRequest request,
                OwnerRepository repo) =>
            {
                var owner = repo.CreateOwner(request.Email, request.FirstName ?? "", request.LastName ?? "", teams: request.Teams);

                var response = new
                {
                    id = owner.Id,
                    email = owner.Email,
                    firstName = owner.FirstName,
                    lastName = owner.LastName,
                    type = owner.Type,
                    archived = owner.Archived,
                    userId = owner.UserId,
                    userIdIncludingInactive = owner.UserIdIncludingInactive,
                    createdAt = owner.CreatedAt,
                    updatedAt = owner.UpdatedAt,
                    teams = owner.Teams?.Select(t => new
                    {
                        id = t.Id,
                        name = t.Name,
                        primary = t.Primary
                    }).ToArray()
                };

                return Results.Created($"/crm/v3/owners/{owner.Id}", response);
            });

            // Get a specific owner by ID
            group.MapGet("/{ownerId}", (
                [FromRoute] string ownerId,
                OwnerRepository repo) =>
            {
                var owner = repo.GetOwner(ownerId);
                if (owner == null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    id = owner.Id,
                    email = owner.Email,
                    firstName = owner.FirstName,
                    lastName = owner.LastName,
                    type = owner.Type,
                    archived = owner.Archived,
                    userId = owner.UserId,
                    userIdIncludingInactive = owner.UserIdIncludingInactive,
                    createdAt = owner.CreatedAt,
                    updatedAt = owner.UpdatedAt,
                    teams = owner.Teams?.Select(t => new
                    {
                        id = t.Id,
                        name = t.Name,
                        primary = t.Primary
                    }).ToArray()
                };

                return Results.Ok(response);
            });
        }

        private record OwnerCreateRequest(string Email, string? FirstName, string? LastName, List<Team>? Teams);
    }
}
