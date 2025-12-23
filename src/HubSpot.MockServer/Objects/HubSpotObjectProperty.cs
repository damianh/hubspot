namespace DamianH.HubSpot.MockServer.Objects;

internal class HubSpotObjectProperty
{
    private readonly List<PropertyValueEntry> _history = [];
    private string? _newValue;

    public HubSpotObjectProperty(string name, PropertyValueEntry[] propertyHistory)
    {
        Name = name;
        CurrentValue = propertyHistory.Any()
            ? propertyHistory.Last().Value
            : string.Empty;
        _history.AddRange(propertyHistory);
        _newValue = CurrentValue;
    }

    public string Name { get; init; }

    public string CurrentValue { get; }

    public IReadOnlyList<PropertyValueEntry> History => _history;

    public bool IsDirty => _newValue != null && _newValue != CurrentValue;

    public string? NewValue
    {
        get => _newValue;
        set => _newValue = value ?? throw new InvalidOperationException(
            "Can't set hubspot properties to null. You can only blank them out" +
            "with an empty string. See https://community.hubspot.com/t5/APIs-Integrations/How-to-unset-a-property/m-p/226613");
    }
}
