using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class CrmCardRepository
{
    private readonly ConcurrentDictionary<string, List<JsonElement>> _cardsByApp = new();
    private int _nextId;

    public Task<List<JsonElement>> GetCardsAsync(string appId)
    {
        var cards = _cardsByApp.GetValueOrDefault(appId) ?? [];
        return Task.FromResult(cards);
    }

    public Task<JsonElement> CreateCardAsync(string appId, JsonElement body)
    {
        var cardId = Interlocked.Increment(ref _nextId).ToString();
        var card = CreateCardObject(cardId, appId, body);

        var cards = _cardsByApp.GetOrAdd(appId, _ => []);
        cards.Add(card);

        return Task.FromResult(card);
    }

    public Task<JsonElement?> UpdateCardAsync(string appId, string cardId, JsonElement body)
    {
        var cards = _cardsByApp.GetValueOrDefault(appId);
        if (cards == null)
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var index = cards.FindIndex(c =>
            c.TryGetProperty("id", out var id) && id.GetString() == cardId);

        if (index == -1)
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var existingCard = cards[index];
        var updatedCard = MergeCard(existingCard, body);
        cards[index] = updatedCard;

        return Task.FromResult<JsonElement?>(updatedCard);
    }

    public Task DeleteCardAsync(string appId, string cardId)
    {
        var cards = _cardsByApp.GetValueOrDefault(appId);
        if (cards != null)
        {
            var index = cards.FindIndex(c =>
                c.TryGetProperty("id", out var id) && id.GetString() == cardId);
            if (index != -1)
            {
                cards.RemoveAt(index);
            }
        }

        return Task.CompletedTask;
    }

    public Task<JsonElement?> GetCardAsync(string appId, string cardId)
    {
        var cards = _cardsByApp.GetValueOrDefault(appId);
        if (cards == null)
        {
            return Task.FromResult<JsonElement?>(null);
        }

        var card = cards.FirstOrDefault(c =>
            c.TryGetProperty("id", out var id) && id.GetString() == cardId);

        return Task.FromResult(card.ValueKind != JsonValueKind.Undefined ? (JsonElement?)card : null);
    }

    private static JsonElement CreateCardObject(string cardId, string appId, JsonElement body)
    {
        var card = new Dictionary<string, object?>
        {
            ["id"] = cardId,
            ["appId"] = appId,
            ["title"] = body.TryGetProperty("title", out var title) ? title.GetString() : "Untitled Card",
            ["createdAt"] = DateTimeOffset.UtcNow,
            ["updatedAt"] = DateTimeOffset.UtcNow
        };

        if (body.TryGetProperty("fetch", out var fetch))
        {
            card["fetch"] = JsonSerializer.Deserialize<object>(fetch.GetRawText());
        }

        if (body.TryGetProperty("display", out var display))
        {
            card["display"] = JsonSerializer.Deserialize<object>(display.GetRawText());
        }

        if (body.TryGetProperty("actions", out var actions))
        {
            card["actions"] = JsonSerializer.Deserialize<object>(actions.GetRawText());
        }

        return JsonSerializer.SerializeToElement(card);
    }

    private static JsonElement MergeCard(JsonElement existing, JsonElement updates)
    {
        var card = JsonSerializer.Deserialize<Dictionary<string, object?>>(existing.GetRawText())
            ?? new Dictionary<string, object?>();

        if (updates.TryGetProperty("title", out var title))
        {
            card["title"] = title.GetString();
        }

        if (updates.TryGetProperty("fetch", out var fetch))
        {
            card["fetch"] = JsonSerializer.Deserialize<object>(fetch.GetRawText());
        }

        if (updates.TryGetProperty("display", out var display))
        {
            card["display"] = JsonSerializer.Deserialize<object>(display.GetRawText());
        }

        if (updates.TryGetProperty("actions", out var actions))
        {
            card["actions"] = JsonSerializer.Deserialize<object>(actions.GetRawText());
        }

        card["updatedAt"] = DateTimeOffset.UtcNow;

        return JsonSerializer.SerializeToElement(card);
    }
}
