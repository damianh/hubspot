using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories.LimitsTracking;

internal class LimitsTrackingRepository(TimeProvider timeProvider)
{
    private readonly Dictionary<string, int> _apiCallCounts = new();
    private readonly Dictionary<string, DateTimeOffset> _resetTimes = new();

    public Task<JsonElement> GetRateLimitsAsync()
    {
        var now = timeProvider.GetUtcNow();
        var limits = JsonSerializer.SerializeToElement(new
        {
            rateLimits = new[]
            {
                new
                {
                    name = "API Requests",
                    maxAllowed = 100000,
                    currentUsage = _apiCallCounts.Sum(x => x.Value),
                    resetTime = now.AddHours(1).ToUnixTimeSeconds()
                }
            },
            timestamp = now.ToString("o")
        });

        return Task.FromResult(limits);
    }

    public Task<JsonElement> GetUsageAsync(int days = 7)
    {
        var now = timeProvider.GetUtcNow();
        var usage = new List<object>();

        for (var i = 0; i < days; i++)
        {
            var date = now.AddDays(-i);
            usage.Add(new
            {
                date = date.ToString("yyyy-MM-dd"),
                totalCalls = Random.Shared.Next(1000, 5000),
                successfulCalls = Random.Shared.Next(900, 1000),
                failedCalls = Random.Shared.Next(0, 100)
            });
        }

        var usageData = JsonSerializer.SerializeToElement(new
        {
            results = usage,
            timestamp = now.ToString("o")
        });

        return Task.FromResult(usageData);
    }

    public Task TrackApiCallAsync(string endpoint)
    {
        if (_apiCallCounts.ContainsKey(endpoint))
        {
            _apiCallCounts[endpoint]++;
        }
        else
        {
            _apiCallCounts[endpoint] = 1;
        }

        return Task.CompletedTask;
    }

    public Task ResetLimitsAsync()
    {
        _apiCallCounts.Clear();
        _resetTimes.Clear();
        return Task.CompletedTask;
    }
}
