// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Detail;
using DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Render;
using DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \integrators\timeline\v3\events\{eventTemplateId}\{eventId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithEventItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The detail property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Detail.DetailRequestBuilder Detail
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Detail.DetailRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The render property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Render.RenderRequestBuilder Render
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.Render.RenderRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.WithEventItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEventItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/integrators/timeline/v3/events/{eventTemplateId}/{eventId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.WithEventItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEventItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/integrators/timeline/v3/events/{eventTemplateId}/{eventId}", rawUrl)
        {
        }
        /// <summary>
        /// This returns the previously created event. It contains all existing info for the event, but not necessarily the CRM object.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventResponse?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventResponse> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// This returns the previously created event. It contains all existing info for the event, but not necessarily the CRM object.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.WithEventItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.WithEventItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Integrators.Timeline.V3.Events.Item.Item.WithEventItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEventItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618