using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

internal class AutomationRepository
{
    private readonly ConcurrentDictionary<int, List<CustomAction>> _actionsByApp = new();
    private readonly ConcurrentDictionary<string, CallbackCompletion> _callbacks = new();

    public IReadOnlyList<CustomAction> GetActionsForApp(int appId)
    {
        if (_actionsByApp.TryGetValue(appId, out var actions))
        {
            return actions;
        }
        return Array.Empty<CustomAction>();
    }

    public void AddAction(CustomAction action) =>
        _actionsByApp.AddOrUpdate(
            action.AppId,
            [action],
            (_, list) =>
            {
                list.Add(action);
                return list;
            });

    public void CompleteCallback(CallbackCompletion completion) => _callbacks[completion.CallbackId] = completion;

    public CallbackCompletion? GetCallback(string callbackId)
    {
        _callbacks.TryGetValue(callbackId, out var completion);
        return completion;
    }

    public void Clear()
    {
        _actionsByApp.Clear();
        _callbacks.Clear();
    }
}

public record CustomAction
{
    public string Id { get; init; } = string.Empty;
    public int AppId { get; init; }
    public string Label { get; init; } = string.Empty;
    public string ActionUrl { get; init; } = string.Empty;
    public List<ActionInput> InputFields { get; init; } = [];
}

public record ActionInput
{
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = ""; // TEXT, NUMBER, BOOLEAN, etc.
    public string Label { get; init; } = string.Empty;
    public bool Required { get; init; }
}

public record CallbackCompletion
{
    public string CallbackId { get; init; } = string.Empty;
    public string Status { get; init; } = ""; // SUCCESS, FAILURE
    public Dictionary<string, object> OutputFields { get; init; } = new();
    public DateTimeOffset CompletedAt { get; init; }
}
