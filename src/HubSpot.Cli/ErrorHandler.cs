using Microsoft.Kiota.Abstractions;

namespace DamianH.HubSpot.Cli;

internal static class ErrorHandler
{
    public static int Handle(Exception ex, CliContext ctx)
    {
        if (ex is ApiException apiEx)
        {
            ctx.Error.WriteLine($"API Error: {apiEx.ResponseStatusCode} — {apiEx.Message}");
            if (ctx.Verbose)
            {
                ctx.Error.WriteLine(apiEx.ToString());
            }

            return 2;
        }

        if (ex is ArgumentException argEx)
        {
            ctx.Error.WriteLine($"Input Error: {argEx.Message}");
            return 3;
        }

        ctx.Error.WriteLine($"Error: {ex.Message}");
        if (ctx.Verbose)
        {
            ctx.Error.WriteLine(ex.ToString());
        }

        return 1;
    }
}
