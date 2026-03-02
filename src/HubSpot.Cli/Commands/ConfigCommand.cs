using System.CommandLine;
using DamianH.HubSpot.Cli.Auth;

namespace DamianH.HubSpot.Cli.Commands;

internal static class ConfigCommand
{
    public static Command Create()
    {
        var command = new Command("config", "Manage CLI configuration");

        command.Add(CreateSetTokenCommand());
        command.Add(CreateShowCommand());
        command.Add(CreateClearCommand());

        return command;
    }

    private static Command CreateSetTokenCommand()
    {
        var command = new Command("set-token", "Save an access token to ~/.hubspot/config.json");

        var tokenArg = new Argument<string>("token") { Description = "HubSpot access token to save" };

        command.Add(tokenArg);

        command.SetAction((parseResult, _) =>
        {
            var token = parseResult.GetValue(tokenArg)!;

            ConfigFileLoader.SaveToken(token);

            Console.Out.WriteLine($"Token saved to {ConfigFileLoader.GetConfigPath()}");

            return Task.CompletedTask;
        });

        return command;
    }

    private static Command CreateShowCommand()
    {
        var command = new Command("show", "Display current configuration");

        command.SetAction((_, _) =>
        {
            var configPath = ConfigFileLoader.GetConfigPath();
            var token = ConfigFileLoader.LoadToken();

            Console.Out.WriteLine($"Config file: {configPath}");

            if (token is null)
            {
                Console.Out.WriteLine("Token: (not set)");
            }
            else
            {
                var masked = token.Length > 8
                    ? string.Concat(token.AsSpan(0, 4), "****", token.AsSpan(token.Length - 4))
                    : "****";
                Console.Out.WriteLine($"Token: {masked}");
            }

            var envToken = Environment.GetEnvironmentVariable("HUBSPOT_ACCESS_TOKEN");
            Console.Out.WriteLine(envToken is not null
                ? "Env var HUBSPOT_ACCESS_TOKEN: set"
                : "Env var HUBSPOT_ACCESS_TOKEN: (not set)");

            return Task.CompletedTask;
        });

        return command;
    }

    private static Command CreateClearCommand()
    {
        var command = new Command("clear", "Remove the configuration file");

        command.SetAction((_, _) =>
        {
            ConfigFileLoader.Clear();
            Console.Out.WriteLine("Configuration cleared");

            return Task.CompletedTask;
        });

        return command;
    }
}
