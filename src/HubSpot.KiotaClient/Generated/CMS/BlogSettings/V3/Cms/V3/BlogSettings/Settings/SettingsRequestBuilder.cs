// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.Item;
using DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.MultiLanguage;
using DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\blog-settings\settings
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SettingsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The multiLanguage property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.MultiLanguage.MultiLanguageRequestBuilder MultiLanguage
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.MultiLanguage.MultiLanguageRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.cms.v3.blogSettings.settings.item collection</summary>
        /// <param name="position">The Blog id.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.Item.WithBlogItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.Item.WithBlogItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("blogId", position);
                return new global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.Item.WithBlogItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SettingsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blog-settings/settings{?after*,archived*,createdAfter*,createdAt*,createdBefore*,limit*,sort*,updatedAfter*,updatedAt*,updatedBefore*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SettingsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blog-settings/settings{?after*,archived*,createdAfter*,createdAt*,createdBefore*,limit*,sort*,updatedAfter*,updatedAt*,updatedBefore*}", rawUrl)
        {
        }
        /// <summary>
        /// Get the list of Blogs. Supports paging and filtering. This method would be useful for an integration that examined these models and used an external service to suggest edits.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalBlogForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalBlogForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder.SettingsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalBlogForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder.SettingsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalBlogForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalBlogForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get the list of Blogs. Supports paging and filtering. This method would be useful for an integration that examined these models and used an external service to suggest edits.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder.SettingsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder.SettingsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get the list of Blogs. Supports paging and filtering. This method would be useful for an integration that examined these models and used an external service to suggest edits.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SettingsRequestBuilderGetQueryParameters 
        {
            /// <summary>The cursor token value to get the next set of results. You can get this from the `paging.next.after` JSON property of a paged response containing more results.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>Specifies whether to return archived Blogs. Defaults to `false`.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
            /// <summary>Only return Blogs created after the specified time.</summary>
            [QueryParameter("createdAfter")]
            public DateTimeOffset? CreatedAfter { get; set; }
            /// <summary>Only return Blogs created at exactly the specified time.</summary>
            [QueryParameter("createdAt")]
            public DateTimeOffset? CreatedAt { get; set; }
            /// <summary>Only return Blogs created before the specified time.</summary>
            [QueryParameter("createdBefore")]
            public DateTimeOffset? CreatedBefore { get; set; }
            /// <summary>The maximum number of results to return. Default is 100.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>Specifies which fields to use for sorting results. Valid fields are `name` and `id`</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("sort")]
            public string[]? Sort { get; set; }
#nullable restore
#else
            [QueryParameter("sort")]
            public string[] Sort { get; set; }
#endif
            /// <summary>Only return Blogs last updated after the specified time.</summary>
            [QueryParameter("updatedAfter")]
            public DateTimeOffset? UpdatedAfter { get; set; }
            /// <summary>Only return Blogs last updated at exactly the specified time.</summary>
            [QueryParameter("updatedAt")]
            public DateTimeOffset? UpdatedAt { get; set; }
            /// <summary>Only return Blogs last updated before the specified time.</summary>
            [QueryParameter("updatedBefore")]
            public DateTimeOffset? UpdatedBefore { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SettingsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Cms.V3.BlogSettings.Settings.SettingsRequestBuilder.SettingsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618