using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

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
                [FromQuery] string? email,
                [FromServices] OwnerRepository repo) =>
            {
                var owners = repo.GetAllOwners(email);

                var response = new
                {
                    results = owners.Select(o => new
                    {
                        id = o.Id,
                        email = o.Email,
                        firstName = o.FirstName,
                        lastName = o.LastName,
                        type = o.Type,
                        createdAt = o.CreatedAt,
                        updatedAt = o.UpdatedAt
                    }).ToArray()
                };

                return Results.Ok(response);
            });

            // Create an owner
            group.MapPost("", (
                [FromBody] OwnerCreateRequest request,
                [FromServices] OwnerRepository repo) =>
            {
                var owner = repo.CreateOwner(request.Email, request.FirstName, request.LastName);

                var response = new
                {
                    id = owner.Id,
                    email = owner.Email,
                    firstName = owner.FirstName,
                    lastName = owner.LastName,
                    type = owner.Type,
                    createdAt = owner.CreatedAt,
                    updatedAt = owner.UpdatedAt
                };

                return Results.Created($"/crm/v3/owners/{owner.Id}", response);
            });

            // Get a specific owner by ID
            group.MapGet("/{ownerId}", (
                [FromRoute] string ownerId,
                [FromServices] OwnerRepository repo) =>
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
                    createdAt = owner.CreatedAt,
                    updatedAt = owner.UpdatedAt
                };

                return Results.Ok(response);
            });
        }

        private record OwnerCreateRequest(string Email, string? FirstName, string? LastName);
    }
}
