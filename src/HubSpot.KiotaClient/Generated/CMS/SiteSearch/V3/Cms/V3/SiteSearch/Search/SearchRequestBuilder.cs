// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\site-search\search
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SearchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/site-search/search{?autocomplete*,boostLimit*,boostRecent*,domain*,groupId*,hubdbQuery*,language*,length*,limit*,matchPrefix*,offset*,pathPrefix*,popularityBoost*,property*,q*,tableId*,type*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/site-search/search{?autocomplete*,boostLimit*,boostRecent*,domain*,groupId*,hubdbQuery*,language*,length*,limit*,matchPrefix*,offset*,pathPrefix*,popularityBoost*,property*,q*,tableId*,type*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns any website content matching the given search criteria for a given HubSpot account. Searches can be filtered by content type, domain, or URL path.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models.PublicSearchResults"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models.PublicSearchResults?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models.PublicSearchResults> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models.PublicSearchResults>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Models.PublicSearchResults.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns any website content matching the given search criteria for a given HubSpot account. Searches can be filtered by content type, domain, or URL path.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns any website content matching the given search criteria for a given HubSpot account. Searches can be filtered by content type, domain, or URL path.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SearchRequestBuilderGetQueryParameters 
        {
            /// <summary>Specifies whether or not you are showing autocomplete results. Defaults to false.</summary>
            [QueryParameter("autocomplete")]
            public bool? Autocomplete { get; set; }
            /// <summary>Specifies the maximum amount a result will be boosted based on its view count. Defaults to 5.0. Read more about elasticsearch boosting [here](https://www.elastic.co/guide/en/elasticsearch/reference/current/mapping-boost.html).</summary>
            [QueryParameter("boostLimit")]
            public double? BoostLimit { get; set; }
            /// <summary>Specifies a relative time window where scores of documents published outside this time window decay. This can only be used for blog posts. For example, boostRecent=10d will boost documents published within the last 10 days. Supported timeunits are ms (milliseconds), s (seconds), m (minutes), h (hours), d (days).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("boostRecent")]
            public string? BoostRecent { get; set; }
#nullable restore
#else
            [QueryParameter("boostRecent")]
            public string BoostRecent { get; set; }
#endif
            /// <summary>A domain to match search results for. Multiple domains can be provided with &amp;.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("domain")]
            public string[]? Domain { get; set; }
#nullable restore
#else
            [QueryParameter("domain")]
            public string[] Domain { get; set; }
#endif
            /// <summary>Specifies which blog(s) to be searched by blog ID. Can be used multiple times to search more than one blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("groupId")]
            public long?[]? GroupId { get; set; }
#nullable restore
#else
            [QueryParameter("groupId")]
            public long?[] GroupId { get; set; }
#endif
            /// <summary>Specify a HubDB query to further filter the search results.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("hubdbQuery")]
            public string? HubdbQuery { get; set; }
#nullable restore
#else
            [QueryParameter("hubdbQuery")]
            public string HubdbQuery { get; set; }
#endif
            /// <summary>Specifies the language of content to be searched. This value must be a valid [ISO 639-1 language code](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes) (e.g. `es` for Spanish)</summary>
            [Obsolete("This property is deprecated, use LanguageAsGetLanguageQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("language")]
            public string? Language { get; set; }
#nullable restore
#else
            [QueryParameter("language")]
            public string Language { get; set; }
#endif
            /// <summary>Specifies the language of content to be searched. This value must be a valid [ISO 639-1 language code](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes) (e.g. `es` for Spanish)</summary>
            [QueryParameter("language")]
            public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.GetLanguageQueryParameterType? LanguageAsGetLanguageQueryParameterType { get; set; }
            /// <summary>Specifies the length of the search results. Can be set to `LONG` or `SHORT`. `SHORT` will return the first 128 characters of the content&apos;s meta description. `LONG` will build a more detailed content snippet based on the html/content of the page.</summary>
            [Obsolete("This property is deprecated, use LengthAsGetLengthQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("length")]
            public string? Length { get; set; }
#nullable restore
#else
            [QueryParameter("length")]
            public string Length { get; set; }
#endif
            /// <summary>Specifies the length of the search results. Can be set to `LONG` or `SHORT`. `SHORT` will return the first 128 characters of the content&apos;s meta description. `LONG` will build a more detailed content snippet based on the html/content of the page.</summary>
            [QueryParameter("length")]
            public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.GetLengthQueryParameterType? LengthAsGetLengthQueryParameterType { get; set; }
            /// <summary>Specifies the number of results to be returned in a single response. Defaults to `10`. Maximum value is `100`.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>Inverts the behavior of the pathPrefix filter when set to `false`. Defaults to `true`.</summary>
            [QueryParameter("matchPrefix")]
            public bool? MatchPrefix { get; set; }
            /// <summary>Used to page through the results. If there are more results than specified by the `limit` parameter, you will need to use the value of offset returned in the previous request to get the next set of results.</summary>
            [QueryParameter("offset")]
            public int? Offset { get; set; }
            /// <summary>Specifies a path prefix to filter search results. Will only return results with URL paths that start with the specified parameter. Can be used multiple times.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("pathPrefix")]
            public string[]? PathPrefix { get; set; }
#nullable restore
#else
            [QueryParameter("pathPrefix")]
            public string[] PathPrefix { get; set; }
#endif
            /// <summary>Specifies how strongly a result is boosted based on its view count. Defaults to 1.0.</summary>
            [QueryParameter("popularityBoost")]
            public double? PopularityBoost { get; set; }
            /// <summary>Specifies which properties to include in the search. Options include `title`, `description`, and `html`. All properties will be searched by default.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("property")]
            public string[]? Property { get; set; }
#nullable restore
#else
            [QueryParameter("property")]
            public string[] Property { get; set; }
#endif
            /// <summary>The term to search for.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("q")]
            public string? Q { get; set; }
#nullable restore
#else
            [QueryParameter("q")]
            public string Q { get; set; }
#endif
            /// <summary>Specifies a specific HubDB table to search. Only returns results from the specified table. Can be used in tandem with the `hubdbQuery` parameter to further filter results.</summary>
            [QueryParameter("tableId")]
            public long? TableId { get; set; }
            /// <summary>Specifies the type of content to search. Can be one or more of SITE_PAGE, LANDING_PAGE, BLOG_POST, LISTING_PAGE, and KNOWLEDGE_ARTICLE. Defaults to all content types except LANDING_PAGE and KNOWLEDGE_ARTICLE</summary>
            [Obsolete("This property is deprecated, use TypeAsGetTypeQueryParameterType instead")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("type")]
            public string[]? Type { get; set; }
#nullable restore
#else
            [QueryParameter("type")]
            public string[] Type { get; set; }
#endif
            /// <summary>Specifies the type of content to search. Can be one or more of SITE_PAGE, LANDING_PAGE, BLOG_POST, LISTING_PAGE, and KNOWLEDGE_ARTICLE. Defaults to all content types except LANDING_PAGE and KNOWLEDGE_ARTICLE</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("type")]
            public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.GetTypeQueryParameterType[]? TypeAsGetTypeQueryParameterType { get; set; }
#nullable restore
#else
            [QueryParameter("type")]
            public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.GetTypeQueryParameterType[] TypeAsGetTypeQueryParameterType { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SearchRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
