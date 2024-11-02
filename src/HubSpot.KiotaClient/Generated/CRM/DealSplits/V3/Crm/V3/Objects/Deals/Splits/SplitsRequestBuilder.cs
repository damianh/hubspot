// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits.Batch;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects\deals\splits
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SplitsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits.SplitsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SplitsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/deals/splits", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.DealSplits.V3.Crm.V3.Objects.Deals.Splits.SplitsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SplitsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/deals/splits", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
