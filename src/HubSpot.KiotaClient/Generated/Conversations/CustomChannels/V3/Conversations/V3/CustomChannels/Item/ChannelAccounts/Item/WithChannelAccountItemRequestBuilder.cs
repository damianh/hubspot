// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \conversations\v3\custom-channels\{channelId}\channel-accounts\{channelAccountId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithChannelAccountItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithChannelAccountItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/conversations/v3/custom-channels/{channelId}/channel-accounts/{channelAccountId}{?archived*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithChannelAccountItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/conversations/v3/custom-channels/{channelId}/channel-accounts/{channelAccountId}{?archived*}", rawUrl)
        {
        }
        /// <summary>
        /// Retrieve a PublicChannelAccount that contains all the metadata about your channel account. This includes information like its channel, associated inbox id, and delivery identifier information.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder.WithChannelAccountItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder.WithChannelAccountItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount>(requestInfo, global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// This API is used to update the name of the channel account and it&apos;s isAuthorized status. Setting to isAuthorized flag to False disables the channel account.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount?> PatchAsync(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccountUpdateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount> PatchAsync(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccountUpdateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPatchRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount>(requestInfo, global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccount.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieve a PublicChannelAccount that contains all the metadata about your channel account. This includes information like its channel, associated inbox id, and delivery identifier information.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder.WithChannelAccountItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder.WithChannelAccountItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// This API is used to update the name of the channel account and it&apos;s isAuthorized status. Setting to isAuthorized flag to False disables the channel account.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccountUpdateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicChannelAccountUpdateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PATCH, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Retrieve a PublicChannelAccount that contains all the metadata about your channel account. This includes information like its channel, associated inbox id, and delivery identifier information.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithChannelAccountItemRequestBuilderGetQueryParameters 
        {
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithChannelAccountItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Conversations.V3.CustomChannels.Item.ChannelAccounts.Item.WithChannelAccountItemRequestBuilder.WithChannelAccountItemRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithChannelAccountItemRequestBuilderPatchRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
