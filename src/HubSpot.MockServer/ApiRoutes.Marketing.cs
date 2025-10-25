using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models;
using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DamianH.HubSpot.MockServer;

internal static partial class ApiRoutes
{
    internal static partial class Marketing
    {
        public static void RegisterMarketingEventsApi(WebApplication app)
        {
            var group = app.MapGroup("/marketing/v3/marketing-events");
            
            group.MapPost("/events", CreateMarketingEvent);
            group.MapGet("/events", ListMarketingEvents);
            group.MapGet("/events/{eventId}", GetMarketingEvent);
            group.MapPatch("/events/{eventId}", UpdateMarketingEvent);
            group.MapDelete("/events/{eventId}", DeleteMarketingEvent);
        }

        public static void RegisterMarketingEmailsApi(WebApplication app)
        {
            var group = app.MapGroup("/marketing/v3/emails");
            
            group.MapPost("", CreateMarketingEmail);
            group.MapGet("", ListMarketingEmails);
            group.MapGet("/{emailId}", GetMarketingEmail);
            group.MapPatch("/{emailId}", UpdateMarketingEmail);
            group.MapDelete("/{emailId}", DeleteMarketingEmail);
        }

        public static void RegisterCampaignsApi(WebApplication app)
        {
            var group = app.MapGroup("/marketing/v3/campaigns");
            
            group.MapPost("", CreateCampaign);
            group.MapGet("", ListCampaigns);
            group.MapGet("/{campaignId}", GetCampaign);
            group.MapPatch("/{campaignId}", UpdateCampaign);
            group.MapDelete("/{campaignId}", DeleteCampaign);
        }

        public static void RegisterSingleSendApi(WebApplication app)
        {
            var group = app.MapGroup("/marketing/v4/singlesend");
            
            group.MapPost("", CreateSingleSend);
            group.MapGet("", ListSingleSends);
            group.MapGet("/{emailId}", GetSingleSend);
            group.MapPatch("/{emailId}", UpdateSingleSend);
            group.MapDelete("/{emailId}", DeleteSingleSend);
        }

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

        // Marketing Events handlers
        private static IResult CreateMarketingEvent(MarketingEventRepository repository, MarketingEvent marketingEvent)
        {
            var created = repository.Create(marketingEvent);
            return Results.Ok(created);
        }

        private static IResult ListMarketingEvents(MarketingEventRepository repository)
        {
            var events = repository.GetAll();
            return Results.Ok(new { results = events });
        }

        private static IResult GetMarketingEvent(MarketingEventRepository repository, string eventId)
        {
            var marketingEvent = repository.GetById(eventId);
            return marketingEvent == null ? Results.NotFound() : Results.Ok(marketingEvent);
        }

        private static IResult UpdateMarketingEvent(MarketingEventRepository repository, string eventId, MarketingEvent marketingEvent)
        {
            try
            {
                var updated = repository.Update(eventId, marketingEvent);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        }

        private static IResult DeleteMarketingEvent(MarketingEventRepository repository, string eventId)
        {
            repository.Delete(eventId);
            return Results.NoContent();
        }

        // Marketing Emails handlers
        private static IResult CreateMarketingEmail(MarketingEmailRepository repository, MarketingEmail email)
        {
            var created = repository.Create(email);
            return Results.Ok(created);
        }

        private static IResult ListMarketingEmails(MarketingEmailRepository repository)
        {
            var emails = repository.GetAll();
            return Results.Ok(new { results = emails });
        }

        private static IResult GetMarketingEmail(MarketingEmailRepository repository, string emailId)
        {
            var email = repository.GetById(emailId);
            return email == null ? Results.NotFound() : Results.Ok(email);
        }

        private static IResult UpdateMarketingEmail(MarketingEmailRepository repository, string emailId, MarketingEmail email)
        {
            try
            {
                var updated = repository.Update(emailId, email);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        }

        private static IResult DeleteMarketingEmail(MarketingEmailRepository repository, string emailId)
        {
            repository.Delete(emailId);
            return Results.NoContent();
        }

        // Campaigns handlers
        private static IResult CreateCampaign(CampaignRepository repository, Campaign campaign)
        {
            var created = repository.Create(campaign);
            return Results.Ok(created);
        }

        private static IResult ListCampaigns(CampaignRepository repository)
        {
            var campaigns = repository.GetAll();
            return Results.Ok(new { results = campaigns });
        }

        private static IResult GetCampaign(CampaignRepository repository, string campaignId)
        {
            var campaign = repository.GetById(campaignId);
            return campaign == null ? Results.NotFound() : Results.Ok(campaign);
        }

        private static IResult UpdateCampaign(CampaignRepository repository, string campaignId, Campaign campaign)
        {
            try
            {
                var updated = repository.Update(campaignId, campaign);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        }

        private static IResult DeleteCampaign(CampaignRepository repository, string campaignId)
        {
            repository.Delete(campaignId);
            return Results.NoContent();
        }

        // SingleSend handlers
        private static IResult CreateSingleSend(SingleSendRepository repository, SingleSendEmail email)
        {
            var created = repository.Create(email);
            return Results.Ok(created);
        }

        private static IResult ListSingleSends(SingleSendRepository repository)
        {
            var emails = repository.GetAll();
            return Results.Ok(new { results = emails });
        }

        private static IResult GetSingleSend(SingleSendRepository repository, string emailId)
        {
            var email = repository.GetById(emailId);
            return email == null ? Results.NotFound() : Results.Ok(email);
        }

        private static IResult UpdateSingleSend(SingleSendRepository repository, string emailId, SingleSendEmail email)
        {
            try
            {
                var updated = repository.Update(emailId, email);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        }

        private static IResult DeleteSingleSend(SingleSendRepository repository, string emailId)
        {
            repository.Delete(emailId);
            return Results.NoContent();
        }
    }
}
