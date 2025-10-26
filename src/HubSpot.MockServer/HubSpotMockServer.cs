using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using DamianH.HubSpot.MockServer.Repositories.AccountInfo;
using DamianH.HubSpot.MockServer.Repositories.Association;
using DamianH.HubSpot.MockServer.Repositories.Automation;
using DamianH.HubSpot.MockServer.Repositories.Blog;
using DamianH.HubSpot.MockServer.Repositories.CustomChannel;
using DamianH.HubSpot.MockServer.Routes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

    public static async Task<HubSpotMockServer> StartNew()
    {
        var builder = WebApplication.CreateBuilder([]);

        // Configure web host
        builder.WebHost.ConfigureKestrel(options => { options.Listen(System.Net.IPAddress.Loopback, 0); });

        // Configure services
        builder.Services
            .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
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
            .AddSingleton<AccountInfoRepository>()
            .AddSingleton<CurrencyRepository>()
            .AddSingleton<UserProvisioningRepository>()
            .AddSingleton<TaxRateRepository>()
            .AddSingleton<ObjectLibraryRepository>()
            .AddSingleton<PropertyValidationRepository>()
            .AddSingleton<FeatureFlagRepository>()
            .AddSingleton<LimitsTrackingRepository>()
            .AddSingleton<BusinessUnitRepository>()
            .AddSingleton<SchedulerMeetingRepository>()
            .AddSingleton<TagRepository>()
            .AddSingleton<BlogSettingsRepository>()
            .AddSingleton<BlogPostRepository>()
            .AddSingleton<BlogAuthorRepository>()
            .AddSingleton<ContentAuditRepository>()
            .AddSingleton<PageRepository>()
            .AddSingleton<DomainRepository>()
            .AddSingleton<UrlRedirectRepository>()
            .AddSingleton<HubDbRepository>()
            .AddSingleton<SourceCodeRepository>()
            .AddSingleton<SiteSearchRepository>()
            .AddSingleton<MediaBridgeRepository>()
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
        ApiRoutes.Properties.RegisterPropertyValidationsV3(app);

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
        ApiRoutes.RegisterConversationsApi(app);

        // Register CRM Extensions APIs (Batch 6)
        ApiRoutes.RegisterSchemasApi(app);
        ApiRoutes.RegisterImportsApi(app);
        ApiRoutes.RegisterExportsApi(app);
        ApiRoutes.RegisterTimelineApi(app);

        // Register CRM Extensions Integration APIs
        ApiRoutes.RegisterCrmExtensions(app);

        // Register Automation APIs
        ApiRoutes.Automation.RegisterAutomationActionsV4(app);
        ApiRoutes.Automation.RegisterAutomationSequencesV4(app);

        // Register Account & Settings APIs
        ApiRoutes.Account.RegisterAccountInfoV3Api(app);
        ApiRoutes.Account.RegisterAccountInfoV202509Api(app);
        ApiRoutes.Account.RegisterAuditLogsV3Api(app);
        ApiRoutes.Multicurrency.RegisterMulticurrencyV3Api(app);
        ApiRoutes.UserProvisioning.RegisterUserProvisioningV3Api(app);
        ApiRoutes.TaxRates.RegisterTaxRatesV1Api(app);

        // Register Business Units APIs
        ApiRoutes.BusinessUnits.RegisterBusinessUnitsV3Api(app);

        // Register Scheduler APIs
        ApiRoutes.Scheduler.RegisterSchedulerMeetingsV3Api(app);

        // Register CMS Blog APIs
        ApiRoutes.CmsTags.RegisterCmsTagsV3Api(app);
        ApiRoutes.CmsBlogSettings.RegisterCmsBlogSettingsV3Api(app);
        ApiRoutes.RegisterCmsBlogPostsApi(app);
        ApiRoutes.RegisterCmsBlogAuthorsApi(app);

        // Register CMS Pages & Content APIs
        ApiRoutes.RegisterCmsPagesApi(app);
        ApiRoutes.RegisterCmsDomainsApi(app);
        ApiRoutes.RegisterCmsUrlRedirectsApi(app);

        // Register CMS Advanced Features APIs
        ApiRoutes.RegisterCmsHubDbApi(app);
        ApiRoutes.RegisterCmsSourceCodeApi(app);
        ApiRoutes.RegisterCmsSiteSearchApi(app);
        ApiRoutes.RegisterCmsContentAuditApi(app);
        ApiRoutes.RegisterCmsMediaBridgeApi(app);

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
