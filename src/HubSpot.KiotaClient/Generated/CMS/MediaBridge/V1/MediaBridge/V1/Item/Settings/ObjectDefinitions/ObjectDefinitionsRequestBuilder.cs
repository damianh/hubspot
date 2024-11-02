// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.Item;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions
{
    /// <summary>
    /// Builds and executes requests for operations under \media-bridge\v1\{appId}\settings\object-definitions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ObjectDefinitionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.mediaBridge.v1.item.settings.objectDefinitions.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.Item.WithMediaTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.Item.WithMediaTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("mediaType", position);
                return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.Item.WithMediaTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectDefinitionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/settings/object-definitions", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectDefinitionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/settings/object-definitions", rawUrl)
        {
        }
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsPostResponse"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsPostResponse?> PostAsObjectDefinitionsPostResponseAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsPostResponse> PostAsObjectDefinitionsPostResponseAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsPostResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsPostResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsResponse"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        [Obsolete("This method is obsolete. Use PostAsObjectDefinitionsPostResponseAsync instead.")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsResponse?> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsResponse> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorObjectCreationRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.ObjectDefinitions.ObjectDefinitionsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ObjectDefinitionsRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
