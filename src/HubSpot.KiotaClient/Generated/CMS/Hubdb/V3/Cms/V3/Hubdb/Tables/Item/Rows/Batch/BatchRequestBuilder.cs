// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch.Read;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\hubdb\tables\{tableIdOrName}\rows\batch
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BatchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The read property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch.Read.ReadRequestBuilder Read
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch.Read.ReadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/rows/batch", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Cms.V3.Hubdb.Tables.Item.Rows.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/hubdb/tables/{tableIdOrName}/rows/batch", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
