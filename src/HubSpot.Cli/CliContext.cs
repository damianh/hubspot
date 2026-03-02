using System.CommandLine;
using DamianH.HubSpot.Cli.Auth;
using DamianH.HubSpot.Cli.Output;
using Microsoft.Kiota.Abstractions;

namespace DamianH.HubSpot.Cli;

internal sealed class CliContext
{
    public required IRequestAdapter Adapter { get; init; }

    public required IOutputFormatter Formatter { get; init; }

    public required bool Verbose { get; init; }

    public required bool Quiet { get; init; }

    public required TextWriter Out { get; init; }

    public required TextWriter Error { get; init; }

    public static CliContext? Create(
        string? token,
        string? baseUrl,
        OutputFormat outputFormat,
        bool verbose,
        bool quiet,
        TextWriter stdout,
        TextWriter stderr)
    {
        var resolvedToken = HubSpotAuth.ResolveToken(token);
        if (resolvedToken is null)
        {
            stderr.WriteLine("Error: No access token found.");
            stderr.WriteLine("Provide one via --token, HUBSPOT_ACCESS_TOKEN env var, or ~/.hubspot/config.json");
            return null;
        }

        var adapter = HubSpotAuth.CreateAdapter(resolvedToken, baseUrl);
        var formatter = OutputFormatterFactory.Create(outputFormat);

        return new CliContext
        {
            Adapter = adapter,
            Formatter = formatter,
            Verbose = verbose,
            Quiet = quiet,
            Out = stdout,
            Error = stderr,
        };
    }

    public static CliContext? FromParseResult(ParseResult parseResult)
    {
        var token = parseResult.GetValue(GlobalOptions.Token);
        var baseUrl = parseResult.GetValue(GlobalOptions.BaseUrl);
        var outputFormat = parseResult.GetValue(GlobalOptions.Output);
        var verbose = parseResult.GetValue(GlobalOptions.Verbose);
        var quiet = parseResult.GetValue(GlobalOptions.Quiet);

        return Create(token, baseUrl, outputFormat, verbose, quiet, Console.Out, Console.Error);
    }
}
