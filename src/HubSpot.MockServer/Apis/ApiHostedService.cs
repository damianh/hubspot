using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer.Apis;

internal abstract class ApiHostedService : IHostedService
{
    private readonly WebApplication _app;

    protected ApiHostedService(LoggerFactoryLoggerProvider loggerProvider)
    {
        var builder = WebApplication.CreateBuilder([]);
        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(loggerProvider);
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        builder.WebHost.UseUrls("http://::1:0");
        builder.Services.Configure<ConsoleLifetimeOptions>(options => { options.SuppressStatusMessages = true; });
        builder.Services.AddEndpointsApiExplorer();

        _app = builder.Build();
    }

    protected abstract void   ConfigureApp(WebApplication app);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        ConfigureApp(_app);
        await _app.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _app.StopAsync(cancellationToken);
        await _app.DisposeAsync();
    }
}