using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.Cli.Auth;

internal static class HubSpotAuth
{
    private const string DefaultBaseUrl = "https://api.hubapi.com";
    private const string EnvVarName = "HUBSPOT_ACCESS_TOKEN";

    public static string? ResolveToken(string? flagToken)
    {
        // 1. Explicit flag
        if (!string.IsNullOrEmpty(flagToken))
        {
            return flagToken;
        }

        // 2. Environment variable
        var envToken = Environment.GetEnvironmentVariable(EnvVarName);
        if (!string.IsNullOrEmpty(envToken))
        {
            return envToken;
        }

        // 3. Config file
        var configToken = ConfigFileLoader.LoadToken();
        if (!string.IsNullOrEmpty(configToken))
        {
            return configToken;
        }

        return null;
    }

    public static IRequestAdapter CreateAdapter(string token, string? baseUrl)
    {
        var authProvider = new BaseBearerTokenAuthenticationProvider(new StaticTokenProvider(token));
        var adapter = new HttpClientRequestAdapter(authProvider)
        {
            BaseUrl = baseUrl ?? DefaultBaseUrl,
        };
        return adapter;
    }

    private sealed class StaticTokenProvider : IAccessTokenProvider
    {
        private readonly string _token;

        public StaticTokenProvider(string token)
        {
            _token = token;
            AllowedHostsValidator = new AllowedHostsValidator();
        }

        public AllowedHostsValidator AllowedHostsValidator { get; }

        public Task<string> GetAuthorizationTokenAsync(
            Uri uri,
            Dictionary<string, object>? additionalAuthenticationContext,
            CancellationToken cancellationToken) => Task.FromResult(_token);
    }
}
