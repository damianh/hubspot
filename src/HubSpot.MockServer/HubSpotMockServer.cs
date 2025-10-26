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
            .AddSingleton<AssociationRepository>()
            .AddSingleton<PropertyDefinitionRepository>()
            .AddSingleton<PipelineRepository>()
            .AddSingleton<OwnerRepository>()
            .AddSingleton<ListRepository>()
            .AddSingleton<FileRepository>()
            .AddSingleton<EventRepository>()
            .AddSingleton<MarketingEventRepository>()
            .AddSingleton<MarketingEmailRepository>()
            .AddSingleton<CampaignRepository>()
            .AddSingleton<SingleSendRepository>()
            .AddSingleton<SubscriptionRepository>()
            .AddSingleton<ConversationRepository>()
            .AddSingleton<CustomChannelRepository>()
            .AddSingleton<VisitorIdentificationRepository>()
            .AddSingleton<SchemaRepository>()
            .AddSingleton<ImportRepository>()
            .AddSingleton<ExportRepository>()
            .AddSingleton<TimelineRepository>()
            .AddSingleton<CallingExtensionRepository>()
            .AddSingleton<CrmCardRepository>()
            .AddSingleton<VideoConferencingRepository>()
            .AddSingleton<TranscriptionRepository>()
            .AddSingleton<SequenceRepository>()
            .AddSingleton<AutomationRepository>()
            .AddSingleton(TimeProvider.System);

        builder.Services.AddEndpointsApiExplorer();
        
        // Configure JSON serialization to handle enums as strings
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
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
        
        // Batch 1: Additional standard objects
        ApiRoutes.RegisterCrmAppointments(app);
        ApiRoutes.RegisterCrmLeads(app);
        ApiRoutes.RegisterCrmUsers(app);
        
        // Batch 2: Commerce objects
        ApiRoutes.RegisterCrmCarts(app);
        ApiRoutes.RegisterCrmOrders(app);
        ApiRoutes.RegisterCrmInvoices(app);
        ApiRoutes.RegisterCrmDiscounts(app);
        ApiRoutes.RegisterCrmFees(app);
        ApiRoutes.RegisterCrmTaxes(app);
        ApiRoutes.RegisterCrmCommercePayments(app);
        ApiRoutes.RegisterCrmCommerceSubscriptions(app);
        
        // Batch 3: Specialized objects
        ApiRoutes.RegisterCrmListings(app);
        ApiRoutes.RegisterCrmContracts(app);
        ApiRoutes.RegisterCrmCourses(app);
        ApiRoutes.RegisterCrmServices(app);
        ApiRoutes.RegisterCrmDealSplits(app);
        ApiRoutes.RegisterCrmGoalTargets(app);
        ApiRoutes.RegisterCrmPartnerClients(app);
        ApiRoutes.RegisterCrmPartnerServices(app);
        ApiRoutes.RegisterCrmTranscriptions(app);
        
        // Register Associations APIs (must be before RegisterGenericCrmObjectsApi to match specific routes first)
        ApiRoutes.Associations.RegisterAssociationsV3(app);
        ApiRoutes.Associations.RegisterAssociationsV4(app);
        ApiRoutes.Associations.RegisterAssociationsV202509(app);
        
        // Register generic CRM Objects API for dynamic/custom object types
        // Note: This must be registered AFTER specific object routes and associations
        // as it uses catch-all pattern /crm/v3/objects/{objectType}
        ApiRoutes.RegisterGenericCrmObjectsApi(app);
        
        // Register Properties APIs
        ApiRoutes.Properties.RegisterPropertiesV3(app);
        ApiRoutes.Properties.RegisterPropertiesV202509(app);
        
        // Register Pipelines APIs
        ApiRoutes.Pipelines.RegisterPipelinesV3(app);
        
        // Register Owners APIs
        ApiRoutes.Owners.RegisterOwnersV3(app);
        
        // Register Marketing APIs
        ApiRoutes.Marketing.RegisterMarketingTransactionalApi(app);
        ApiRoutes.Marketing.RegisterMarketingEventsApi(app);
        ApiRoutes.Marketing.RegisterMarketingEmailsApi(app);
        ApiRoutes.Marketing.RegisterCampaignsApi(app);
        ApiRoutes.Marketing.RegisterSingleSendApi(app);
        
        // Register Communication Preferences APIs
        ApiRoutes.Subscriptions.RegisterSubscriptionsV3Api(app);
        ApiRoutes.Subscriptions.RegisterSubscriptionsV4Api(app);
        
        // Register Webhooks APIs
        ApiRoutes.Webhooks.RegisterWebhooksApi(app);
        
        // Register Lists API
        ApiRoutes.RegisterCrmLists(app);
        
        // Register Files API
        ApiRoutes.RegisterFiles(app);
        
        // Register Events API
        ApiRoutes.RegisterEvents(app);
        
        // Register Conversations APIs
        ApiRoutes.RegisterConversationsApi(
            app,
            app.Services.GetRequiredService<ConversationRepository>(),
            app.Services.GetRequiredService<CustomChannelRepository>(),
            app.Services.GetRequiredService<VisitorIdentificationRepository>());
        
        // Register CRM Extensions APIs (Batch 6)
        ApiRoutes.RegisterSchemasApi(app, app.Services.GetRequiredService<SchemaRepository>());
        ApiRoutes.RegisterImportsApi(app, app.Services.GetRequiredService<ImportRepository>());
        ApiRoutes.RegisterExportsApi(app, app.Services.GetRequiredService<ExportRepository>());
        ApiRoutes.RegisterTimelineApi(app, app.Services.GetRequiredService<TimelineRepository>());
        
        // Register CRM Extensions Integration APIs
        ApiRoutes.RegisterCrmExtensions(app);
        
        // Register Automation APIs
        ApiRoutes.Automation.RegisterAutomationActionsV4(app, app.Services.GetRequiredService<AutomationRepository>());
        ApiRoutes.Automation.RegisterAutomationSequencesV4(app, app.Services.GetRequiredService<SequenceRepository>());

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