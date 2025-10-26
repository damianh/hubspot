using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class VisitorIdentificationRepository
{
    private readonly ConcurrentDictionary<string, VisitorTokenData> _tokens = new();
    private readonly ConcurrentDictionary<string, string> _visitorToContact = new();
    private long _nextVisitorId = 1;

    public VisitorTokenData GenerateToken(string? email = null)
    {
        var visitorId = Interlocked.Increment(ref _nextVisitorId).ToString();
        var token = GenerateRandomToken();
        var now = DateTimeOffset.UtcNow;
        var expiresAt = now.AddHours(24);

        var tokenData = new VisitorTokenData
        {
            Token = token,
            VisitorId = visitorId,
            Email = email,
            CreatedAt = now,
            ExpiresAt = expiresAt
        };

        _tokens[token] = tokenData;

        return tokenData;
    }

    public VisitorTokenData? ValidateToken(string token)
    {
        if (!_tokens.TryGetValue(token, out var tokenData))
        {
            return null;
        }

        if (tokenData.ExpiresAt < DateTimeOffset.UtcNow)
        {
            return null; // Expired
        }

        return tokenData;
    }

    public VisitorTokenData? GetTokenByVisitorId(string visitorId) => _tokens.Values.FirstOrDefault(t => t.VisitorId == visitorId);

    public void IdentifyVisitor(string visitorId, string contactId) => _visitorToContact[visitorId] = contactId;

    public string? GetContactForVisitor(string visitorId) => _visitorToContact.GetValueOrDefault(visitorId);

    public void Clear()
    {
        _tokens.Clear();
        _visitorToContact.Clear();
        _nextVisitorId = 1;
    }

    private static string GenerateRandomToken()
    {
        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}

public class VisitorTokenData
{
    public string Token { get; set; } = string.Empty;
    public string VisitorId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}
