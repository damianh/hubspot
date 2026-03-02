using System.Text.Json;

namespace DamianH.HubSpot.Cli.Auth;

internal sealed record ConfigFile(string? AccessToken);

internal static class ConfigFileLoader
{
    private static readonly string ConfigPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".hubspot",
        "config.json");

    public static string? LoadToken()
    {
        if (!File.Exists(ConfigPath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(ConfigPath);
            var config = JsonSerializer.Deserialize<ConfigFile>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return config?.AccessToken;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public static string GetConfigPath() => ConfigPath;

    public static void SaveToken(string token)
    {
        var dir = Path.GetDirectoryName(ConfigPath)!;
        Directory.CreateDirectory(dir);
        var config = new ConfigFile(token);
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        File.WriteAllText(ConfigPath, json);
    }

    public static void Clear()
    {
        if (File.Exists(ConfigPath))
        {
            File.Delete(ConfigPath);
        }
    }
}
