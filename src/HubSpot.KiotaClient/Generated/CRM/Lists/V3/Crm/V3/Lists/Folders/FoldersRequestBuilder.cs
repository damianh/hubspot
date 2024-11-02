// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.Item;
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.MoveList;
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\lists\folders
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class FoldersRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The moveList property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.MoveList.MoveListRequestBuilder MoveList
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.MoveList.MoveListRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Lists.V3.crm.v3.lists.folders.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.Item.WithFolderItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.Item.WithFolderItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("folderId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.Item.WithFolderItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FoldersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/folders{?folderId*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public FoldersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/folders{?folderId*}", rawUrl)
        {
        }
        /// <summary>
        /// Retrieves a folder and recursively includes all folders via the childNodes attribute.  The child lists field will be empty in all child nodes. Only the folder retrieved will include the child lists in that folder.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderFetchResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderFetchResponse?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder.FoldersRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderFetchResponse> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder.FoldersRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderFetchResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderFetchResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Creates a folder with the given information.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateResponse"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateResponse?> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateResponse> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieves a folder and recursively includes all folders via the childNodes attribute.  The child lists field will be empty in all child nodes. Only the folder retrieved will include the child lists in that folder.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder.FoldersRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder.FoldersRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Creates a folder with the given information.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ListFolderCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Retrieves a folder and recursively includes all folders via the childNodes attribute.  The child lists field will be empty in all child nodes. Only the folder retrieved will include the child lists in that folder.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FoldersRequestBuilderGetQueryParameters 
        {
            /// <summary>The Id of the folder to retrieve.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("folderId")]
            public string? FolderId { get; set; }
#nullable restore
#else
            [QueryParameter("folderId")]
            public string FolderId { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FoldersRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Folders.FoldersRequestBuilder.FoldersRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class FoldersRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
