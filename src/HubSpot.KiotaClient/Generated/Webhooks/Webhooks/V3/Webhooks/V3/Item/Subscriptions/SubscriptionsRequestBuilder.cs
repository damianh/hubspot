// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Batch;
using DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions
{
    /// <summary>
    /// Builds and executes requests for operations under \webhooks\v3\{appId}\subscriptions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SubscriptionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.webhooks.v3.item.subscriptions.item collection</summary>
        /// <param name="position">The ID of the event subscription.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder this[int position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("subscriptionId", position);
                return new global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.webhooks.v3.item.subscriptions.item collection</summary>
        /// <param name="position">The ID of the event subscription.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/></returns>
        [Obsolete("This indexer is deprecated and will be removed in the next major version. Use the one with the typed parameter instead.")]
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                if (!string.IsNullOrWhiteSpace(position)) urlTplParams.Add("subscriptionId", position);
                return new global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.Item.WithSubscriptionItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.SubscriptionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SubscriptionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/webhooks/v3/{appId}/subscriptions", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.SubscriptionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SubscriptionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/webhooks/v3/{appId}/subscriptions", rawUrl)
        {
        }
        /// <summary>
        /// Retrieve event subscriptions for the specified app.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionListResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionListResponse?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionListResponse> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionListResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionListResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Create new event subscription for the specified app.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionResponse"/></returns>
        /// <param name="body">New webhook settings for an app.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionResponse?> PostAsync(global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionResponse> PostAsync(global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieve event subscriptions for the specified app.
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
        /// Create new event subscription for the specified app.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">New webhook settings for an app.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SubscriptionCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.SubscriptionsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.SubscriptionsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Webhooks.V3.Item.Subscriptions.SubscriptionsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SubscriptionsRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SubscriptionsRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
