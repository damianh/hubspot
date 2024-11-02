// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\lists\records
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class RecordsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Lists.V3.crm.v3.lists.records.item collection</summary>
        /// <param name="position">Object type id of the record</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.Item.WithObjectTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.Item.WithObjectTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("objectTypeId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.Item.WithObjectTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.RecordsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RecordsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/records", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Records.RecordsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RecordsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/records", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618