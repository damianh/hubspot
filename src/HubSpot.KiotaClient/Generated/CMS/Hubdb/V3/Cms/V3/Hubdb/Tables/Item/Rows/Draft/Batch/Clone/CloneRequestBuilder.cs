// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\hubdb\tables\{tableIdOrName}\rows\draft\batch\clone
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CloneRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone.CloneRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CloneRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/rows/draft/batch/clone", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone.CloneRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CloneRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/rows/draft/batch/clone", rawUrl)
        {
        }
        /// <summary>
        /// Clones rows in the draft version of the specified table, given a set of row ids. Maximum of 100 row ids per call.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchResponseHubDbTableRowV3"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchResponseHubDbTableRowV3?> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchInputHubDbTableRowBatchCloneRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchResponseHubDbTableRowV3> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchInputHubDbTableRowBatchCloneRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchResponseHubDbTableRowV3>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchResponseHubDbTableRowV3.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Clones rows in the draft version of the specified table, given a set of row ids. Maximum of 100 row ids per call.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchInputHubDbTableRowBatchCloneRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.BatchInputHubDbTableRowBatchCloneRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone.CloneRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone.CloneRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Draft.Batch.Clone.CloneRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class CloneRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
