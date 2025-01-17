// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.Item;
using DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\url-redirects
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UrlRedirectsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.cms.v3.urlRedirects.item collection</summary>
        /// <param name="position">The ID of the target redirect.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.Item.WithUrlRedirectItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.Item.WithUrlRedirectItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("urlRedirectId", position);
                return new global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.Item.WithUrlRedirectItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UrlRedirectsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/url-redirects/{?after*,archived*,createdAfter*,createdAt*,createdBefore*,limit*,sort*,updatedAfter*,updatedAt*,updatedBefore*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UrlRedirectsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/url-redirects/{?after*,archived*,createdAfter*,createdAt*,createdBefore*,limit*,sort*,updatedAfter*,updatedAt*,updatedBefore*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns all existing URL redirects. Results can be limited and filtered by creation or updated date.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.CollectionResponseWithTotalUrlMappingForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.CollectionResponseWithTotalUrlMappingForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder.UrlRedirectsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.CollectionResponseWithTotalUrlMappingForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder.UrlRedirectsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.CollectionResponseWithTotalUrlMappingForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.CollectionResponseWithTotalUrlMappingForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Creates and configures a new URL redirect.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMapping"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMapping?> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMapping> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMapping>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMapping.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns all existing URL redirects. Results can be limited and filtered by creation or updated date.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder.UrlRedirectsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder.UrlRedirectsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Creates and configures a new URL redirect.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns all existing URL redirects. Results can be limited and filtered by creation or updated date.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UrlRedirectsRequestBuilderGetQueryParameters 
        {
            /// <summary>The paging cursor token of the last successfully read resource will be returned as the `paging.next.after` JSON property of a paged response containing more results.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
            /// <summary>Only return redirects created after this date.</summary>
            [QueryParameter("createdAfter")]
            public DateTimeOffset? CreatedAfter { get; set; }
            /// <summary>Only return redirects created on exactly this date.</summary>
            [QueryParameter("createdAt")]
            public DateTimeOffset? CreatedAt { get; set; }
            /// <summary>Only return redirects created before this date.</summary>
            [QueryParameter("createdBefore")]
            public DateTimeOffset? CreatedBefore { get; set; }
            /// <summary>Maximum number of result per page</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("sort")]
            public string[]? Sort { get; set; }
#nullable restore
#else
            [QueryParameter("sort")]
            public string[] Sort { get; set; }
#endif
            /// <summary>Only return redirects last updated after this date.</summary>
            [QueryParameter("updatedAfter")]
            public DateTimeOffset? UpdatedAfter { get; set; }
            /// <summary>Only return redirects last updated on exactly this date.</summary>
            [QueryParameter("updatedAt")]
            public DateTimeOffset? UpdatedAt { get; set; }
            /// <summary>Only return redirects last updated before this date.</summary>
            [QueryParameter("updatedBefore")]
            public DateTimeOffset? UpdatedBefore { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UrlRedirectsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Cms.V3.UrlRedirects.UrlRedirectsRequestBuilder.UrlRedirectsRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UrlRedirectsRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
