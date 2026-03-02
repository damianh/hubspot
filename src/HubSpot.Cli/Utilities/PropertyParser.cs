namespace DamianH.HubSpot.Cli.Utilities;

internal static class PropertyParser
{
    public static Dictionary<string, object> Parse(string[] propertyArgs)
    {
        var result = new Dictionary<string, object>(StringComparer.Ordinal);

        foreach (var arg in propertyArgs)
        {
            // Support comma-separated pairs in a single arg: "name=Acme,domain=acme.com"
            var pairs = arg.Contains(',') && arg.Contains('=')
                ? arg.Split(',')
                : [arg];

            foreach (var pair in pairs)
            {
                var eqIndex = pair.IndexOf('=');
                if (eqIndex <= 0)
                {
                    throw new ArgumentException($"Invalid property format: '{pair}'. Expected 'key=value'.");
                }

                var key = pair[..eqIndex].Trim();
                var value = pair[(eqIndex + 1)..].Trim();
                result[key] = value;
            }
        }

        return result;
    }
}
