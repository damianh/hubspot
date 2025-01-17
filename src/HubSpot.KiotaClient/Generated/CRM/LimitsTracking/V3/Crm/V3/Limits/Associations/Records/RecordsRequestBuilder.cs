// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.From;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\limits\associations\records
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class RecordsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The from property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.From.FromRequestBuilder From
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.From.FromRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.crm.v3.limits.associations.records.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.Item.WithFromObjectTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.Item.WithFromObjectTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("fromObjectTypeId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.Item.WithFromObjectTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.RecordsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RecordsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/limits/associations/records", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.Records.RecordsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RecordsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/limits/associations/records", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
