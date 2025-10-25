using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static partial class Marketing
    {
        public static void RegisterMarketingTransactionalApi(WebApplication app)
        {
            var transactionalGroup = app.MapGroup("/marketing/v3/transactional");

            // Single Email Send
            transactionalGroup.MapPost("/single-email/send", SendSingleEmail);

            // SMTP Tokens
            var smtpGroup = transactionalGroup.MapGroup("/smtp-tokens");
            smtpGroup.MapGet("", ListSmtpTokens);
            smtpGroup.MapPost("", CreateSmtpToken);
            smtpGroup.MapGet("/{tokenId}", GetSmtpToken);
            smtpGroup.MapPut("/{tokenId}", ResetSmtpTokenPassword);
            smtpGroup.MapDelete("/{tokenId}", DeleteSmtpToken);
        }

        private static IResult SendSingleEmail(
            TransactionalEmailRepository repository,
            PublicSingleSendRequestEgg request)
        {
            var status = repository.SendEmail(request);
            return Results.Ok(status);
        }

        private static IResult ListSmtpTokens(
            TransactionalEmailRepository repository,
            string? campaignName = null,
            string? emailCampaignId = null,
            int? limit = null,
            string? after = null)
        {
            var result = repository.ListSmtpTokens(campaignName, emailCampaignId, limit, after);
            return Results.Ok(result);
        }

        private static IResult CreateSmtpToken(
            TransactionalEmailRepository repository,
            SmtpApiTokenRequestEgg request)
        {
            var token = repository.CreateSmtpToken(request);
            return Results.Ok(token);
        }

        private static IResult GetSmtpToken(
            TransactionalEmailRepository repository,
            string tokenId)
        {
            var token = repository.GetSmtpToken(tokenId);
            if (token == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(token);
        }

        private static IResult ResetSmtpTokenPassword(
            TransactionalEmailRepository repository,
            string tokenId)
        {
            var token = repository.ResetSmtpTokenPassword(tokenId);
            if (token == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(token);
        }

        private static IResult DeleteSmtpToken(
            TransactionalEmailRepository repository,
            string tokenId)
        {
            var success = repository.DeleteSmtpToken(tokenId);
            if (!success)
            {
                return Results.NotFound();
            }
            return Results.NoContent();
        }
    }
}
