using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

public class HubSpotMockServer : IAsyncDisposable
{
    private readonly WebApplication _app;

    private HubSpotMockServer(WebApplication app, Uri uri)
    {
        _app = app;
        Uri = uri;
    }

    public Uri Uri { get; private set; }
    
    public Uri BaseUri => Uri;

    public static Task<HubSpotMockServer> StartAsync()
    {
        return StartNew(new LoggerFactoryAdapter());
    }

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
            .AddSingleton<TransactionalEmailRepository>()
            .AddSingleton<WebhookRepository>()
            .AddSingleton(TimeProvider.System);

        builder.Services.AddEndpointsApiExplorer();
        
        // Configure JSON serialization to handle enums as strings
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });

        var app = builder.Build();

        // Register API route groups
        ApiRoutes.RegisterCrmCompanies(app);
        ApiRoutes.RegisterCrmContacts(app);
        ApiRoutes.RegisterCrmDeals(app);
        ApiRoutes.RegisterCrmLineItems(app);
        ApiRoutes.RegisterCrmTickets(app);
        ApiRoutes.RegisterCrmProducts(app);
        ApiRoutes.RegisterCrmQuotes(app);
        ApiRoutes.RegisterCrmCalls(app);
        ApiRoutes.RegisterCrmEmails(app);
        ApiRoutes.RegisterCrmMeetings(app);
        ApiRoutes.RegisterCrmNotes(app);
        ApiRoutes.RegisterCrmTasks(app);
        ApiRoutes.RegisterCrmCommunications(app);
        ApiRoutes.RegisterCrmPostalMail(app);
        ApiRoutes.RegisterCrmFeedbackSubmissions(app);
        ApiRoutes.RegisterCrmGoals(app);
        
        // Register generic CRM Objects API for dynamic/custom object types
        ApiRoutes.RegisterGenericCrmObjectsApi(app);
        
        // Register Marketing APIs
        ApiRoutes.Marketing.RegisterMarketingTransactionalApi(app);
        
        // Register Webhooks APIs
        ApiRoutes.Webhooks.RegisterWebhooksApi(app);

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