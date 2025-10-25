using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

public class CustomChannelRepository
{
    private readonly ConcurrentDictionary<string, CustomChannelData> _channels = new();
    private long _nextChannelId = 1;

    public CustomChannelData CreateChannel(string name, string? accountId = null)
    {
        var id = Interlocked.Increment(ref _nextChannelId).ToString();
        var now = DateTimeOffset.UtcNow;
        
        var channel = new CustomChannelData
        {
            Id = id,
            AccountId = accountId ?? "default-account",
            Name = name,
            CreatedAt = now,
            Active = true
        };

        _channels[id] = channel;
        
        return channel;
    }

    public CustomChannelData? GetChannel(string channelId)
    {
        return _channels.TryGetValue(channelId, out var channel) 
            ? channel 
            : null;
    }

    public List<CustomChannelData> ListChannels()
    {
        return _channels.Values.OrderBy(c => c.CreatedAt).ToList();
    }

    public CustomChannelData? UpdateChannel(string channelId, string? name = null, bool? active = null)
    {
        if (!_channels.TryGetValue(channelId, out var channel))
        {
            return null;
        }

        if (name != null)
        {
            channel.Name = name;
        }

        if (active.HasValue)
        {
            channel.Active = active.Value;
        }

        return channel;
    }

    public bool DeleteChannel(string channelId)
    {
        return _channels.TryRemove(channelId, out _);
    }

    public void Clear()
    {
        _channels.Clear();
        _nextChannelId = 1;
    }
}

public class CustomChannelData
{
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public bool Active { get; set; }
}
