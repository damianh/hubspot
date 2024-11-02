// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Files.Files.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search
{
    /// <summary>
    /// Builds and executes requests for operations under \files\v3\folders\search
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SearchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/search{?after*,before*,createdAt*,createdAtGte*,createdAtLte*,id*,limit*,name*,parentFolderId*,path*,properties*,sort*,updatedAt*,updatedAtGte*,updatedAtLte*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/search{?after*,before*,createdAt*,createdAtGte*,createdAtLte*,id*,limit*,name*,parentFolderId*,path*,properties*,sort*,updatedAt*,updatedAtGte*,updatedAtLte*}", rawUrl)
        {
        }
        /// <summary>
        /// Search for folders. Does not contain hidden or archived folders.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.CollectionResponseFolder"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.CollectionResponseFolder?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.CollectionResponseFolder> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.CollectionResponseFolder>(requestInfo, global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.CollectionResponseFolder.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Search for folders. Does not contain hidden or archived folders.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Search for folders. Does not contain hidden or archived folders.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SearchRequestBuilderGetQueryParameters 
        {
            /// <summary>The maximum offset of items for a given search is 10000. Narrow your search down if you are reaching this limit.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("before")]
            public string? Before { get; set; }
#nullable restore
#else
            [QueryParameter("before")]
            public string Before { get; set; }
#endif
            /// <summary>Search for folders with the given creation timestamp.</summary>
            [QueryParameter("createdAt")]
            public DateTimeOffset? CreatedAt { get; set; }
            [QueryParameter("createdAtGte")]
            public DateTimeOffset? CreatedAtGte { get; set; }
            [QueryParameter("createdAtLte")]
            public DateTimeOffset? CreatedAtLte { get; set; }
            /// <summary>Search folder by given ID.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("id")]
            public string? Id { get; set; }
#nullable restore
#else
            [QueryParameter("id")]
            public string Id { get; set; }
#endif
            /// <summary>Limit of results to return. Max limit is 100.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>Search for folders containing the specified name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("name")]
            public string? Name { get; set; }
#nullable restore
#else
            [QueryParameter("name")]
            public string Name { get; set; }
#endif
            /// <summary>Search for folders with the given parent folderId.</summary>
            [QueryParameter("parentFolderId")]
            public long? ParentFolderId { get; set; }
            /// <summary>Search for folders by path.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("path")]
            public string? Path { get; set; }
#nullable restore
#else
            [QueryParameter("path")]
            public string Path { get; set; }
#endif
            /// <summary>Properties that should be included in the returned folders.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("properties")]
            public string[]? Properties { get; set; }
#nullable restore
#else
            [QueryParameter("properties")]
            public string[] Properties { get; set; }
#endif
            /// <summary>Sort results by given property. For example -name sorts by name field descending, name sorts by name field ascending.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("sort")]
            public string[]? Sort { get; set; }
#nullable restore
#else
            [QueryParameter("sort")]
            public string[] Sort { get; set; }
#endif
            /// <summary>Search for folder at given update timestamp.</summary>
            [QueryParameter("updatedAt")]
            public DateTimeOffset? UpdatedAt { get; set; }
            [QueryParameter("updatedAtGte")]
            public DateTimeOffset? UpdatedAtGte { get; set; }
            [QueryParameter("updatedAtLte")]
            public DateTimeOffset? UpdatedAtLte { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SearchRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Search.SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
