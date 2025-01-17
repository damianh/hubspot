// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\emails\statistics\histogram
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class HistogramRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public HistogramRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/statistics/histogram{?emailIds*,endTimestamp*,interval*,startTimestamp*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public HistogramRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/statistics/histogram{?emailIds*,endTimestamp*,interval*,startTimestamp*}", rawUrl)
        {
        }
        /// <summary>
        /// Get aggregated statistics in intervals for a specified time span. Each interval contains aggregated statistics of the emails that were sent in that time.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.CollectionResponseWithTotalEmailStatisticIntervalNoPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.CollectionResponseWithTotalEmailStatisticIntervalNoPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder.HistogramRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.CollectionResponseWithTotalEmailStatisticIntervalNoPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder.HistogramRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.CollectionResponseWithTotalEmailStatisticIntervalNoPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.CollectionResponseWithTotalEmailStatisticIntervalNoPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get aggregated statistics in intervals for a specified time span. Each interval contains aggregated statistics of the emails that were sent in that time.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder.HistogramRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder.HistogramRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get aggregated statistics in intervals for a specified time span. Each interval contains aggregated statistics of the emails that were sent in that time.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class HistogramRequestBuilderGetQueryParameters 
        {
            /// <summary>Filter by email IDs. Only include statistics of emails with these IDs.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("emailIds")]
            public long?[]? EmailIds { get; set; }
#nullable restore
#else
            [QueryParameter("emailIds")]
            public long?[] EmailIds { get; set; }
#endif
            /// <summary>The end timestamp of the time span, in ISO8601 representation.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("endTimestamp")]
            public string? EndTimestamp { get; set; }
#nullable restore
#else
            [QueryParameter("endTimestamp")]
            public string EndTimestamp { get; set; }
#endif
            /// <summary>The interval to aggregate statistics for.</summary>
            [Obsolete("This property is deprecated, use IntervalAsGetIntervalQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("interval")]
            public string? Interval { get; set; }
#nullable restore
#else
            [QueryParameter("interval")]
            public string Interval { get; set; }
#endif
            /// <summary>The interval to aggregate statistics for.</summary>
            [QueryParameter("interval")]
            public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.GetIntervalQueryParameterType? IntervalAsGetIntervalQueryParameterType { get; set; }
            /// <summary>The start timestamp of the time span, in ISO8601 representation.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("startTimestamp")]
            public string? StartTimestamp { get; set; }
#nullable restore
#else
            [QueryParameter("startTimestamp")]
            public string StartTimestamp { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class HistogramRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Statistics.Histogram.HistogramRequestBuilder.HistogramRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
