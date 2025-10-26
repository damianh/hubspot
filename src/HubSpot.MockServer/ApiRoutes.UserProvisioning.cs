using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    public static class UserProvisioning
    {
        public static void RegisterUserProvisioningV3Api(WebApplication app)
        {
            var group = app.MapGroup("/settings/v3/users");

            group.MapGet("", (
                [FromServices] UserProvisioningRepository repository,
                [FromQuery] int? limit,
                [FromQuery] string? after,
                [FromQuery] string? email) =>
            {
                var users = repository.GetUsers(limit, after, email);
                var hasMore = limit.HasValue && users.Count >= limit.Value;

                var response = new
                {
                    results = users,
                    paging = hasMore && users.Count > 0 ? new
                    {
                        next = new
                        {
                            after = users.Last().Id,
                            link = $"/settings/v3/users?limit={limit}&after={users.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            group.MapPost("", async (
                [FromServices] UserProvisioningRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<UserProvisionRequest>();
                if (body == null || string.IsNullOrEmpty(body.Email))
                {
                    return Results.BadRequest(new { message = "Email is required" });
                }

                var user = new UserAccount
                {
                    Id = string.Empty,
                    Email = body.Email,
                    FirstName = body.FirstName ?? string.Empty,
                    LastName = body.LastName ?? string.Empty,
                    RoleIds = body.RoleIds ?? [],
                    PrimaryTeamId = body.PrimaryTeamId ?? [],
                    SecondaryTeamIds = body.SecondaryTeamIds ?? []
                };

                var created = repository.CreateUser(user);
                return Results.Ok(created);
            });

            group.MapGet("/{userId}", (
                [FromServices] UserProvisioningRepository repository,
                string userId,
                [FromQuery] string? idProperty) =>
            {
                var user = repository.GetUser(userId, idProperty ?? "id");
                if (user == null)
                {
                    return Results.NotFound(new { message = "User not found" });
                }

                return Results.Ok(user);
            });

            group.MapPut("/{userId}", async (
                [FromServices] UserProvisioningRepository repository,
                string userId,
                [FromQuery] string? idProperty,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<UserUpdateRequest>();
                if (body == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var updates = new UserAccount
                {
                    Id = userId,
                    Email = string.Empty,
                    FirstName = body.FirstName ?? string.Empty,
                    LastName = body.LastName ?? string.Empty,
                    RoleIds = body.RoleIds ?? [],
                    PrimaryTeamId = body.PrimaryTeamId ?? [],
                    SecondaryTeamIds = body.SecondaryTeamIds ?? []
                };

                var updated = repository.UpdateUser(userId, updates, idProperty ?? "id");
                if (updated == null)
                {
                    return Results.NotFound(new { message = "User not found" });
                }

                return Results.Ok(updated);
            });

            group.MapDelete("/{userId}", (
                [FromServices] UserProvisioningRepository repository,
                string userId,
                [FromQuery] string? idProperty) =>
            {
                var deleted = repository.DeleteUser(userId, idProperty ?? "id");
                if (!deleted)
                {
                    return Results.NotFound(new { message = "User not found" });
                }

                return Results.NoContent();
            });

            group.MapGet("/roles", (
                [FromServices] UserProvisioningRepository repository) =>
            {
                var roles = repository.GetRoles();
                return Results.Ok(new { results = roles });
            });

            group.MapGet("/teams", (
                [FromServices] UserProvisioningRepository repository) =>
            {
                var teams = repository.GetTeams();
                return Results.Ok(new { results = teams });
            });
        }
    }

    private record UserProvisionRequest(
        string Email,
        string? FirstName,
        string? LastName,
        List<string>? RoleIds,
        List<string>? PrimaryTeamId,
        List<string>? SecondaryTeamIds);

    private record UserUpdateRequest(
        string? FirstName,
        string? LastName,
        List<string>? RoleIds,
        List<string>? PrimaryTeamId,
        List<string>? SecondaryTeamIds);
}
