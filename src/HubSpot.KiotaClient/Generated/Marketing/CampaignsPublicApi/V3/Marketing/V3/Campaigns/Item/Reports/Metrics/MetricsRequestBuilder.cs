// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\campaigns\{campaignGuid}\reports\metrics
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MetricsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MetricsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/reports/metrics{?endDate*,startDate*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MetricsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/reports/metrics{?endDate*,startDate*}", rawUrl)
        {
        }
        /// <summary>
        /// This endpoint retrieves key attribution metrics for a specified campaign, such as sessions, new contacts, and influenced contacts.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder.MetricsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder.MetricsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// This endpoint retrieves key attribution metrics for a specified campaign, such as sessions, new contacts, and influenced contacts.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder.MetricsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder.MetricsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// This endpoint retrieves key attribution metrics for a specified campaign, such as sessions, new contacts, and influenced contacts.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class MetricsRequestBuilderGetQueryParameters 
        {
            /// <summary>End date for the report data, formatted as YYYY-MM-DD.Default value: Current date</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("endDate")]
            public string? EndDate { get; set; }
#nullable restore
#else
            [QueryParameter("endDate")]
            public string EndDate { get; set; }
#endif
            /// <summary>The start date for the report data, formatted as YYYY-MM-DD.Default value: 2006-01-01</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("startDate")]
            public string? StartDate { get; set; }
#nullable restore
#else
            [QueryParameter("startDate")]
            public string StartDate { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class MetricsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Metrics.MetricsRequestBuilder.MetricsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
