// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Clone;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Export;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Import;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Publish;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Reset;
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\hubdb\tables\{tableIdOrName}\draft
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DraftRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The clone property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Clone.CloneRequestBuilder Clone
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Clone.CloneRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The export property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Export.ExportRequestBuilder Export
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Export.ExportRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The import property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Import.ImportRequestBuilder Import
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Import.ImportRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The publish property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Publish.PublishRequestBuilder Publish
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Publish.PublishRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The reset property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Reset.ResetRequestBuilder Reset
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.Reset.ResetRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DraftRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/draft{?archived*,includeForeignIds*,isGetLocalizedSchema*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DraftRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/draft{?archived*,includeForeignIds*,isGetLocalizedSchema*}", rawUrl)
        {
        }
        /// <summary>
        /// Get the details for the draft version of a specific HubDB table. This will include the definitions for the columns in the table and the number of rows in the table.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Update an existing HubDB table. You can use this endpoint to add or remove columns to the table as well as restore an archived table. Tables updated using the endpoint will only modify the draft verion of the table. Use the `/publish` endpoint to push all the changes to the published version. To restore a table, include the query parameter `archived=true` and `&quot;archived&quot;: false` in the json body.**Note:** You need to include all the columns in the input when you are adding/removing/updating a column. If you do not include an already existing column in the request, it will be deleted.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3?> PatchAsync(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3Request body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderPatchQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3> PatchAsync(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3Request body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderPatchQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPatchRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get the details for the draft version of a specific HubDB table. This will include the definitions for the columns in the table and the number of rows in the table.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Update an existing HubDB table. You can use this endpoint to add or remove columns to the table as well as restore an archived table. Tables updated using the endpoint will only modify the draft verion of the table. Use the `/publish` endpoint to push all the changes to the published version. To restore a table, include the query parameter `archived=true` and `&quot;archived&quot;: false` in the json body.**Note:** You need to include all the columns in the input when you are adding/removing/updating a column. If you do not include an already existing column in the request, it will be deleted.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3Request body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderPatchQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.HubDbTableV3Request body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderPatchQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get the details for the draft version of a specific HubDB table. This will include the definitions for the columns in the table and the number of rows in the table.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class DraftRequestBuilderGetQueryParameters 
        {
            /// <summary>Set this to `true` to return an archived table. Defaults to `false`.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
            /// <summary>Set this to `true` to populate foreign ID values in the result.</summary>
            [QueryParameter("includeForeignIds")]
            public bool? IncludeForeignIds { get; set; }
            [QueryParameter("isGetLocalizedSchema")]
            public bool? IsGetLocalizedSchema { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class DraftRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Update an existing HubDB table. You can use this endpoint to add or remove columns to the table as well as restore an archived table. Tables updated using the endpoint will only modify the draft verion of the table. Use the `/publish` endpoint to push all the changes to the published version. To restore a table, include the query parameter `archived=true` and `&quot;archived&quot;: false` in the json body.**Note:** You need to include all the columns in the input when you are adding/removing/updating a column. If you do not include an already existing column in the request, it will be deleted.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class DraftRequestBuilderPatchQueryParameters 
        {
            /// <summary>Specifies whether to return archived tables. Defaults to `false`.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
            /// <summary>Set this to `true` to populate foreign ID values in the result.</summary>
            [QueryParameter("includeForeignIds")]
            public bool? IncludeForeignIds { get; set; }
            [QueryParameter("isGetLocalizedSchema")]
            public bool? IsGetLocalizedSchema { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class DraftRequestBuilderPatchRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Draft.DraftRequestBuilder.DraftRequestBuilderPatchQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
