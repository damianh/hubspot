// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item;
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v4\associations
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AssociationsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The usage property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.UsageRequestBuilder Usage
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Usage.UsageRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Associations.V4.crm.v4.associations.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.WithFromObjectTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.WithFromObjectTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("fromObjectType", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.WithFromObjectTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.AssociationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AssociationsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.AssociationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AssociationsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
