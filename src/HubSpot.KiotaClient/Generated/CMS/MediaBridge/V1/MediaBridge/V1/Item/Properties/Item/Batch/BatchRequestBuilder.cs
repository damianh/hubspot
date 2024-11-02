// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Archive;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Create;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Read;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch
{
    /// <summary>
    /// Builds and executes requests for operations under \media-bridge\v1\{appId}\properties\{objectType}\batch
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BatchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The archive property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Archive.ArchiveRequestBuilder Archive
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Archive.ArchiveRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The create property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Create.CreateRequestBuilder Create
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Create.CreateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The read property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Read.ReadRequestBuilder Read
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.Read.ReadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/properties/{objectType}/batch", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.Item.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}/properties/{objectType}/batch", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
