using System.CommandLine;
using DamianH.HubSpot.Cli.Output;

namespace DamianH.HubSpot.Cli;

internal static class GlobalOptions
{
    public static Option<string?> Token { get; } = new("--token")
    {
        Description = "HubSpot access token (overrides env var and config file)",
    };

    public static Option<string?> BaseUrl { get; } = new("--base-url")
    {
        Description = "HubSpot API base URL override",
    };

    public static Option<OutputFormat> Output { get; } = new("--output")
    {
        Description = "Output format: json, table, csv",
        DefaultValueFactory = _ => OutputFormat.Json,
    };

    public static Option<bool> Verbose { get; } = new("--verbose")
    {
        Description = "Enable verbose output",
        DefaultValueFactory = _ => false,
    };

    public static Option<bool> Quiet { get; } = new("--quiet")
    {
        Description = "Suppress informational messages",
        DefaultValueFactory = _ => false,
    };

    public static IReadOnlyList<Option> All { get; } =
    [
        Token,
        BaseUrl,
        Output,
        Verbose,
        Quiet,
    ];
}
