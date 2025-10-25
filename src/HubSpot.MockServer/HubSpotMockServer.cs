using DamianH.HubSpot.MockServer.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

public partial class HubSpotMockServer : IAsyncDisposable
{
    private readonly WebApplication _app;

    private HubSpotMockServer(WebApplication app, Uri uri)
    {
        _app = app;
        Uri = uri;
    }

    public Uri Uri { get; private set; }

    public static async Task<HubSpotMockServer> StartNew(ILoggerFactory loggerFactory)
    {
        var provider = new LoggerFactoryLoggerProvider(loggerFactory);
        var builder = WebApplication.CreateBuilder([]);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(provider);
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        // Configure web host
        builder.WebHost.ConfigureKestrel(options => { options.Listen(System.Net.IPAddress.Loopback, 0); });

        // Configure services
        builder.Services
            .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
            .AddSingleton<LoggerFactoryLoggerProvider>()
            .AddSingleton<HubSpotObjectRepository>()
            .AddSingleton<HubSpotObjectIdGenerator>()
            .AddSingleton(TimeProvider.System);

        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Register API route groups
        ApiRoutes.RegisterCrmCompanies(app);
        ApiRoutes.RegisterCrmContacts(app);
        ApiRoutes.RegisterCrmDeals(app);
        ApiRoutes.RegisterCrmLineItems(app);
        // ... other APIs

        await app.StartAsync();

        var addresses = app.Urls;
        var uri = new Uri(addresses.First());

        return new HubSpotMockServer(app, uri);
    }

    public async ValueTask DisposeAsync()
    {
        await _app.StopAsync();
        await _app.DisposeAsync();
    }
}