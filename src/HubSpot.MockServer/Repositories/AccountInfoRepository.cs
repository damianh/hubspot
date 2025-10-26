using DamianH.HubSpot.MockServer.Objects;

namespace DamianH.HubSpot.MockServer.Repositories;

public class AccountInfoRepository
{
    private readonly TimeProvider _timeProvider;
    private AccountDetail _accountDetail;
    private readonly List<ApiUsageData> _apiUsageData = new();
    private readonly List<AuditLogEntry> _auditLogs = new();
    private int _auditLogIdCounter = 1;

    public AccountInfoRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        
        _accountDetail = new AccountDetail
        {
            PortalId = 12345678,
            AccountType = "MARKETING_HUB_PROFESSIONAL",
            TimeZone = "US/Eastern",
            CompanyCurrency = "USD",
            AdditionalCurrencies = new List<string> { "EUR", "GBP" },
            DataHostingLocation = "US",
            UiDomain = "app.hubspot.com",
            UtcOffset = "-05:00",
            UtcOffsetMilliseconds = -18000000
        };

        InitializeDefaultData();
    }

    private void InitializeDefaultData()
    {
        var today = _timeProvider.GetUtcNow().Date;
        for (int i = 0; i < 30; i++)
        {
            var date = today.AddDays(-i);
            _apiUsageData.Add(new ApiUsageData
            {
                Date = date.ToString("yyyy-MM-dd"),
                TotalUsage = Random.Shared.Next(100, 1000),
                ApiCallUsage = Random.Shared.Next(100, 1000),
                UsageBySource = new Dictionary<string, int>
                {
                    ["API_CALLS"] = Random.Shared.Next(50, 500),
                    ["OAUTH"] = Random.Shared.Next(10, 100),
                    ["PRIVATE_APPS"] = Random.Shared.Next(10, 100)
                }
            });
        }

        AddAuditLog("ACTIVITY", "USER_LOGIN", "user@example.com");
        AddAuditLog("LOGIN", "SUCCESSFUL_LOGIN", "user@example.com");
        AddAuditLog("SECURITY", "PASSWORD_CHANGED", "user@example.com");
    }

    public AccountDetail GetAccountDetails() => _accountDetail;

    public void UpdateAccountDetails(AccountDetail details)
    {
        _accountDetail = details;
    }

    public List<ApiUsageData> GetDailyApiUsage(int days = 30)
    {
        return _apiUsageData
            .OrderByDescending(d => d.Date)
            .Take(days)
            .ToList();
    }

    public List<ApiUsageData> GetPrivateAppsDailyUsage(int days = 30)
    {
        return _apiUsageData
            .OrderByDescending(d => d.Date)
            .Take(days)
            .Select(d => new ApiUsageData
            {
                Date = d.Date,
                TotalUsage = d.UsageBySource.GetValueOrDefault("PRIVATE_APPS", 0),
                ApiCallUsage = d.UsageBySource.GetValueOrDefault("PRIVATE_APPS", 0),
                UsageBySource = new Dictionary<string, int>
                {
                    ["PRIVATE_APPS"] = d.UsageBySource.GetValueOrDefault("PRIVATE_APPS", 0)
                }
            })
            .ToList();
    }

    public List<AuditLogEntry> GetAuditLogs(string eventType, int limit = 100, string? after = null)
    {
        var query = _auditLogs.Where(log => log.EventType == eventType);

        if (!string.IsNullOrEmpty(after))
        {
            query = query.Where(log => string.Compare(log.Id, after) > 0);
        }

        return query
            .OrderByDescending(log => log.Timestamp)
            .Take(limit)
            .ToList();
    }

    public void AddAuditLog(string eventType, string action, string userEmail)
    {
        var log = new AuditLogEntry
        {
            Id = _auditLogIdCounter++.ToString(),
            EventType = eventType,
            Timestamp = _timeProvider.GetUtcNow(),
            UserId = Guid.NewGuid().ToString(),
            UserEmail = userEmail,
            IpAddress = "192.168.1.1",
            Action = action,
            Details = new Dictionary<string, object>
            {
                ["timestamp"] = _timeProvider.GetUtcNow().ToString("O"),
                ["action"] = action
            }
        };

        _auditLogs.Add(log);
    }
}
