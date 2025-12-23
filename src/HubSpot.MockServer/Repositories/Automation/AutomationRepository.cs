using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.Automation;

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
