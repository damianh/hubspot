// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Groups;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Item;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \media-bridge\v1\{appId}\properties\{objectType}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithObjectTypeItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The groups property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Groups.GroupsRequestBuilder Groups
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Groups.GroupsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.mediaBridge.v1.item.properties.item.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Item.WithPropertyNameItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Item.WithPropertyNameItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("propertyName", position);
                return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Item.WithPropertyNameItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithObjectTypeItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/properties/{objectType}{?archived*,properties*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithObjectTypeItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/properties/{objectType}{?archived*,properties*}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponsePropertyNoPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponsePropertyNoPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder.WithObjectTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponsePropertyNoPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder.WithObjectTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponsePropertyNoPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponsePropertyNoPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property?> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyCreate body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyCreate body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder.WithObjectTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder.WithObjectTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyCreate body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyCreate body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class WithObjectTypeItemRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("properties")]
            public string? Properties { get; set; }
#nullable restore
#else
            [QueryParameter("properties")]
            public string Properties { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithObjectTypeItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.WithObjectTypeItemRequestBuilder.WithObjectTypeItemRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithObjectTypeItemRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
