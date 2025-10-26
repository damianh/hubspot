using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.UserProvisioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static class UserProvisioning
    {
        public static void RegisterUserProvisioningV3Api(WebApplication app)
        {
            var group = app.MapGroup("/settings/v3/users");

            group.MapGet("", (
                UserProvisioningRepository repository,
                int? limit,
                string? after,
                string? email) =>
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
                UserProvisioningRepository repository,
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
                UserProvisioningRepository repository,
                string userId,
                string? idProperty) =>
            {
                var user = repository.GetUser(userId, idProperty ?? "id");
                return user == null
                    ? Results.NotFound(new { message = "User not found" })
                    : Results.Ok(user);
            });

            group.MapPut("/{userId}", async (
                UserProvisioningRepository repository,
                string userId,
                string? idProperty,
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
                return updated == null
                    ? Results.NotFound(new { message = "User not found" })
                    : Results.Ok(updated);
            });

            group.MapDelete("/{userId}", (
                UserProvisioningRepository repository,
                string userId,
                string? idProperty) =>
            {
                var deleted = repository.DeleteUser(userId, idProperty ?? "id");
                return !deleted
                    ? Results.NotFound(new { message = "User not found" })
                    : Results.NoContent();
            });

            group.MapGet("/roles", (
                UserProvisioningRepository repository) =>
            {
                var roles = repository.GetRoles();
                return Results.Ok(new { results = roles });
            });

            group.MapGet("/teams", (
                UserProvisioningRepository repository) =>
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
