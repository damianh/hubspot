// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Attendance;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Batch;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Item;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Participations;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\marketing-events
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MarketingEventsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The associations property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.AssociationsRequestBuilder Associations
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.AssociationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The attendance property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Attendance.AttendanceRequestBuilder Attendance
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Attendance.AttendanceRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The events property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.EventsRequestBuilder Events
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Events.EventsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The participations property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Participations.ParticipationsRequestBuilder Participations
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Participations.ParticipationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.marketing.v3.marketingEvents.item collection</summary>
        /// <param name="position">The internal ID of the marketing event in HubSpot</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Item.AppItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Item.AppItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("app%2Did", position);
                return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Item.AppItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MarketingEventsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/{?after*,limit*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MarketingEventsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/{?after*,limit*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns all Marketing Events available on the portal, along with their properties, regardless of whether they were created manually or through the application.The marketing events returned by this endpoint are sorted by objectId.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.CollectionResponseMarketingEventPublicReadResponseV2ForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.CollectionResponseMarketingEventPublicReadResponseV2ForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder.MarketingEventsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.CollectionResponseMarketingEventPublicReadResponseV2ForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder.MarketingEventsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.CollectionResponseMarketingEventPublicReadResponseV2ForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.CollectionResponseMarketingEventPublicReadResponseV2ForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns all Marketing Events available on the portal, along with their properties, regardless of whether they were created manually or through the application.The marketing events returned by this endpoint are sorted by objectId.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder.MarketingEventsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder.MarketingEventsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns all Marketing Events available on the portal, along with their properties, regardless of whether they were created manually or through the application.The marketing events returned by this endpoint are sorted by objectId.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class MarketingEventsRequestBuilderGetQueryParameters 
        {
            /// <summary>The cursor indicating the position of the last retrieved item.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>The limit for response size. The default value is 10, the max number is 100</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class MarketingEventsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.MarketingEventsRequestBuilder.MarketingEventsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
