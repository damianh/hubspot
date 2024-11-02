using DamianH.HubSpot.MockServer.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

public class HubSpotMockServer : IAsyncDisposable
{
    private readonly IHost _app;

    private HubSpotMockServer(IHost app, Uri uri)
    {
        _app = app;
        Uri  = uri;
    }

    public Uri Uri { get; private set; }

    public static async Task<HubSpotMockServer> StartNew(ILoggerFactory loggerFactory)
    {
        var provider = new LoggerFactoryLoggerProvider(loggerFactory);
        var builder  = Host.CreateApplicationBuilder([]);
        builder.Logging.AddProvider(provider);
        builder.Logging.SetMinimumLevel(LogLevel.Error);
        
        builder.Logging.AddFilter("Logicality.Extensions.Hosting.HostedServiceWrapper", LogLevel.Error);
        builder.Services
            .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
            .AddSingleton<LoggerFactoryLoggerProvider>()
            .AddSingleton<HubSpotObjectRepository>()
            .AddSingleton<HubSpotObjectIdGenerator>()
            .AddSingleton(TimeProvider.System);
            

        var app = builder.Build();
        await app.StartAsync();

        return new HubSpotMockServer(app, new Uri("http://localhost"));
    }

    public async ValueTask DisposeAsync()
    {
        await _app.StopAsync();
        _app.Dispose();
    }
}
