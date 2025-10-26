namespace DamianH.HubSpot;

public static class ShouldlyExtensions
{
    public static void ShouldSatisfyAll<T>(this T actual, params Action<T>[] assertions)
    {
        var actions = assertions.Select(a => new Action(() => a.Invoke(actual))).ToArray();

        actual.ShouldSatisfyAllConditions(actions);
    }
}
