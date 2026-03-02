using DamianH.HubSpot.MockServer.Repositories.Automation;
using DamianH.HubSpot.MockServer.Repositories.Sequence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    internal static class Automation
    {
        public static void RegisterAutomationActionsV4(WebApplication app)
        {
            var group = app.MapGroup("/automation/v4/actions");

            // POST /automation/v4/actions/callbacks/complete
            group.MapPost("/callbacks/complete", (
                AutomationRepository repository,
                TimeProvider timeProvider,
                [FromBody] BatchInputCallbackCompletionRequest request) =>
            {
                foreach (var input in request.Inputs)
                {
                    var completion = new CallbackCompletion
                    {
                        CallbackId = input.CallbackId,
                        Status = input.OutputFields.TryGetValue("hs_execution_state", out var execState) &&
                                 execState.ToString() == "SUCCESS"
                            ? "SUCCESS"
                            : "FAILURE",
                        OutputFields = input.OutputFields,
                        CompletedAt = timeProvider.GetUtcNow()
                    };
                    repository.CompleteCallback(completion);
                }

                return Results.NoContent();
            });

            // GET /automation/v4/actions/{appId}
            group.MapGet("/{appId}", (int appId, AutomationRepository repo) =>
            {
                var actions = repo.GetActionsForApp(appId);
                return Results.Ok(new { actions });
            });
        }

        public static void RegisterAutomationSequencesV4(WebApplication app)
        {
            var group = app.MapGroup("/automation/v4/sequences");

            // POST /automation/v4/sequences/enrollments
            group.MapPost("/enrollments", (
                [FromBody] PublicSequenceEnrollmentRequest request,
                SequenceRepository repo,
                TimeProvider timeProvider) =>
            {
                // Check if sequence exists, create if not (for testing purposes)
                var sequence = repo.GetSequence(request.SequenceId);
                if (sequence == null)
                {
                    sequence = new Sequence
                    {
                        Id = request.SequenceId,
                        Name = $"Sequence {request.SequenceId}",
                        Steps = [],
                        CreatedAt = timeProvider.GetUtcNow(),
                        UpdatedAt = timeProvider.GetUtcNow()
                    };
                    repo.AddSequence(sequence);
                }

                // Check if contact is already enrolled in this sequence
                var existingEnrollments = repo.GetEnrollmentsByContact(request.ContactId);
                var activeEnrollment = existingEnrollments
                    .FirstOrDefault(e => e.SequenceId == request.SequenceId &&
                                        e.State == "ENROLLED");

                if (activeEnrollment != null)
                {
                    // Return existing enrollment (idempotent)
                    return Results.Ok(new PublicSequenceEnrollmentLiteResponse
                    {
                        Id = activeEnrollment.EnrollmentId,
                        ToEmail = request.SenderEmail,
                        EnrolledAt = activeEnrollment.EnrolledAt,
                        UpdatedAt = timeProvider.GetUtcNow()
                    });
                }

                // Create new enrollment
                var enrollment = repo.CreateEnrollment(request.ContactId, request.SequenceId, 0);

                return Results.Ok(new PublicSequenceEnrollmentLiteResponse
                {
                    Id = enrollment.EnrollmentId,
                    ToEmail = request.SenderEmail,
                    EnrolledAt = enrollment.EnrolledAt,
                    UpdatedAt = timeProvider.GetUtcNow()
                });
            });

            // POST /automation/v4/sequences/enrollments/contact
            group.MapPost("/enrollments/contact", (
                [FromBody] PublicSequenceEnrollmentRequest request,
                SequenceRepository repo,
                TimeProvider timeProvider) =>
            {
                // Same as enrollments endpoint
                var sequence = repo.GetSequence(request.SequenceId);
                if (sequence == null)
                {
                    sequence = new Sequence
                    {
                        Id = request.SequenceId,
                        Name = $"Sequence {request.SequenceId}",
                        Steps = [],
                        CreatedAt = timeProvider.GetUtcNow(),
                        UpdatedAt = timeProvider.GetUtcNow()
                    };
                    repo.AddSequence(sequence);
                }

                var enrollment = repo.CreateEnrollment(request.ContactId, request.SequenceId, 0);

                return Results.Ok(new PublicSequenceEnrollmentLiteResponse
                {
                    Id = enrollment.EnrollmentId,
                    ToEmail = request.SenderEmail,
                    EnrolledAt = enrollment.EnrolledAt,
                    UpdatedAt = timeProvider.GetUtcNow()
                });
            });

            // GET /automation/v4/sequences/{sequenceId}
            group.MapGet("/{sequenceId}", (string sequenceId, SequenceRepository repo) =>
            {
                var sequence = repo.GetSequence(sequenceId);
                if (sequence == null)
                {
                    return Results.NotFound(new { message = $"Sequence {sequenceId} not found" });
                }

                return Results.Ok(sequence);
            });

            // GET /automation/v4/sequences
            group.MapGet("", (SequenceRepository repo) =>
            {
                var sequences = repo.GetAllSequences();
                return Results.Ok(new { results = sequences, total = sequences.Count });
            });
        }
    }
}

// Request/Response models that match the Kiota-generated types
internal sealed record BatchInputCallbackCompletionRequest
{
    public List<CallbackCompletionInput> Inputs { get; init; } = [];
}

internal sealed record CallbackCompletionInput
{
    public string CallbackId { get; init; } = string.Empty;
    public Dictionary<string, object> OutputFields { get; init; } = new();
}

internal sealed record PublicSequenceEnrollmentRequest
{
    public string ContactId { get; init; } = string.Empty;
    public string SequenceId { get; init; } = string.Empty;
    public string? SenderEmail { get; init; }
    public string? SenderAliasAddress { get; init; }
}

internal sealed record PublicSequenceEnrollmentLiteResponse
{
    public string Id { get; init; } = string.Empty; //  Enrollment ID
    public string? ToEmail { get; init; }
    public DateTimeOffset EnrolledAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
