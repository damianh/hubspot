// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v4\associations\usage\high-usage-report
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class HighUsageReportRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Associations.V4.crm.v4.associations.usage.highUsageReport.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder this[int position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("userId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Associations.V4.crm.v4.associations.usage.highUsageReport.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder"/></returns>
        [Obsolete("This indexer is deprecated and will be removed in the next major version. Use the one with the typed parameter instead.")]
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                if (!string.IsNullOrWhiteSpace(position)) urlTplParams.Add("userId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.Item.WithUserItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.HighUsageReportRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public HighUsageReportRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/usage/high-usage-report", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.HighUsageReport.HighUsageReportRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public HighUsageReportRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/usage/high-usage-report", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
