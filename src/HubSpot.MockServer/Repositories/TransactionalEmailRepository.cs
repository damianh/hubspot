using System.Collections.Concurrent;
using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models;

namespace DamianH.HubSpot.MockServer.Repositories;

public class TransactionalEmailRepository
{
    private readonly ConcurrentDictionary<string, EmailSendStatusView> _emailSends = new();
    private readonly ConcurrentDictionary<string, SmtpApiTokenView> _smtpTokens = new();
    private int _emailSendCounter = 0;
    private int _smtpTokenCounter = 0;

    public EmailSendStatusView SendEmail(PublicSingleSendRequestEgg request)
    {
        var statusId = Interlocked.Increment(ref _emailSendCounter).ToString();
        var now = DateTimeOffset.UtcNow;

        var status = new EmailSendStatusView
        {
            StatusId = statusId,
            Status = EmailSendStatusView_status.COMPLETE,
            SendResult = EmailSendStatusView_sendResult.SENT,
            RequestedAt = now,
            StartedAt = now,
            CompletedAt = now,
            EventId = new EventIdView
            {
                Id = Guid.NewGuid(),
                Created = now
            }
        };

        _emailSends[statusId] = status;
        return status;
    }

    public EmailSendStatusView? GetEmailSendStatus(string statusId)
    {
        _emailSends.TryGetValue(statusId, out var status);
        return status;
    }

    public SmtpApiTokenView CreateSmtpToken(SmtpApiTokenRequestEgg request)
    {
        var tokenId = Interlocked.Increment(ref _smtpTokenCounter).ToString();
        var password = Guid.NewGuid().ToString("N");

        var token = new SmtpApiTokenView
        {
            Id = tokenId,
            CampaignName = request.CampaignName,
            EmailCampaignId = Guid.NewGuid().ToString(),
            CreateContact = request.CreateContact ?? false,
            Password = password,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = "mock-server@test.com"
        };

        _smtpTokens[tokenId] = token;
        return token;
    }

    public SmtpApiTokenView? GetSmtpToken(string tokenId)
    {
        if (_smtpTokens.TryGetValue(tokenId, out var token))
        {
            return new SmtpApiTokenView
            {
                Id = token.Id,
                CampaignName = token.CampaignName,
                EmailCampaignId = token.EmailCampaignId,
                CreateContact = token.CreateContact,
                CreatedAt = token.CreatedAt,
                CreatedBy = token.CreatedBy
            };
        }
        return null;
    }

    public CollectionResponseSmtpApiTokenViewForwardPaging ListSmtpTokens(
        string? campaignName = null,
        string? emailCampaignId = null,
        int? limit = null,
        string? after = null)
    {
        var tokens = _smtpTokens.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(campaignName))
        {
            tokens = tokens.Where(t => t.CampaignName == campaignName);
        }

        if (!string.IsNullOrEmpty(emailCampaignId))
        {
            tokens = tokens.Where(t => t.EmailCampaignId == emailCampaignId);
        }

        var tokensList = tokens.OrderBy(t => t.Id).ToList();

        if (!string.IsNullOrEmpty(after))
        {
            var afterIndex = tokensList.FindIndex(t => t.Id == after);
            if (afterIndex >= 0)
            {
                tokensList = tokensList.Skip(afterIndex + 1).ToList();
            }
        }

        var pageSize = limit ?? 10;
        var results = tokensList.Take(pageSize).Select(t => new SmtpApiTokenView
        {
            Id = t.Id,
            CampaignName = t.CampaignName,
            EmailCampaignId = t.EmailCampaignId,
            CreateContact = t.CreateContact,
            CreatedAt = t.CreatedAt,
            CreatedBy = t.CreatedBy
        }).ToList();

        var response = new CollectionResponseSmtpApiTokenViewForwardPaging
        {
            Results = results
        };

        if (results.Count == pageSize && tokensList.Count > pageSize)
        {
            response.Paging = new ForwardPaging
            {
                Next = new NextPage
                {
                    After = results.Last().Id
                }
            };
        }

        return response;
    }

    public SmtpApiTokenView? ResetSmtpTokenPassword(string tokenId)
    {
        if (_smtpTokens.TryGetValue(tokenId, out var token))
        {
            var newPassword = Guid.NewGuid().ToString("N");
            token.Password = newPassword;

            return new SmtpApiTokenView
            {
                Id = token.Id,
                CampaignName = token.CampaignName,
                EmailCampaignId = token.EmailCampaignId,
                CreateContact = token.CreateContact,
                Password = newPassword,
                CreatedAt = token.CreatedAt,
                CreatedBy = token.CreatedBy
            };
        }
        return null;
    }

    public bool DeleteSmtpToken(string tokenId) => _smtpTokens.TryRemove(tokenId, out _);
}
