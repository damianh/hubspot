// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.Read;
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll
{
    /// <summary>
    /// Builds and executes requests for operations under \communication-preferences\v4\statuses\batch\unsubscribe-all
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UnsubscribeAllRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The read property</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.Read.ReadRequestBuilder Read
        {
            get => new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.Read.ReadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UnsubscribeAllRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences/v4/statuses/batch/unsubscribe-all?channel={channel}{&businessUnitId*,verbose*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UnsubscribeAllRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences/v4/statuses/batch/unsubscribe-all?channel={channel}{&businessUnitId*,verbose*}", rawUrl)
        {
        }
        /// <summary>
        /// Unsubscribe a set of contacts from all email subscriptions.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchResponsePublicBulkOptOutFromAllResponse"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchResponsePublicBulkOptOutFromAllResponse?> PostAsync(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder.UnsubscribeAllRequestBuilderPostQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchResponsePublicBulkOptOutFromAllResponse> PostAsync(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder.UnsubscribeAllRequestBuilderPostQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchResponsePublicBulkOptOutFromAllResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchResponsePublicBulkOptOutFromAllResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Unsubscribe a set of contacts from all email subscriptions.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder.UnsubscribeAllRequestBuilderPostQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder.UnsubscribeAllRequestBuilderPostQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Unsubscribe a set of contacts from all email subscriptions.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UnsubscribeAllRequestBuilderPostQueryParameters 
        {
            /// <summary>If you have the [business unit add-on](https://developers.hubspot.com/beta-docs/guides/api/settings/business-units-api), include this parameter to filter results by business unit ID. The default Account business unit will always use `0`.</summary>
            [QueryParameter("businessUnitId")]
            public long? BusinessUnitId { get; set; }
            /// <summary>The channel type for the subscription type. Currently, the only supported channel type is `EMAIL`.</summary>
            [Obsolete("This property is deprecated, use ChannelAsPostChannelQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("channel")]
            public string? Channel { get; set; }
#nullable restore
#else
            [QueryParameter("channel")]
            public string Channel { get; set; }
#endif
            /// <summary>The channel type for the subscription type. Currently, the only supported channel type is `EMAIL`.</summary>
            [QueryParameter("channel")]
            public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.PostChannelQueryParameterType? ChannelAsPostChannelQueryParameterType { get; set; }
            /// <summary>Set to `true` to include the details of the updated subscription statuses in the response. Not including this parameter will result in an empty response.</summary>
            [QueryParameter("verbose")]
            public bool? Verbose { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UnsubscribeAllRequestBuilderPostRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.Batch.UnsubscribeAll.UnsubscribeAllRequestBuilder.UnsubscribeAllRequestBuilderPostQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
