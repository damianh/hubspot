// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\marketing-events\events\{externalEventId}\cancel
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CancelRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CancelRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/events/{externalEventId}/cancel?externalAccountId={externalAccountId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CancelRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/events/{externalEventId}/cancel?externalAccountId={externalAccountId}", rawUrl)
        {
        }
        /// <summary>
        /// Mark a marketing event as cancelled.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventDefaultResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventDefaultResponse?> PostAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder.CancelRequestBuilderPostQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventDefaultResponse> PostAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder.CancelRequestBuilderPostQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToPostRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventDefaultResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventDefaultResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Mark a marketing event as cancelled.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder.CancelRequestBuilderPostQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder.CancelRequestBuilderPostQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Mark a marketing event as cancelled.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class CancelRequestBuilderPostQueryParameters 
        {
            /// <summary>The accountId that is associated with this marketing event in the external event application</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("externalAccountId")]
            public string? ExternalAccountId { get; set; }
#nullable restore
#else
            [QueryParameter("externalAccountId")]
            public string ExternalAccountId { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class CancelRequestBuilderPostRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.Item.Cancel.CancelRequestBuilder.CancelRequestBuilderPostQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
