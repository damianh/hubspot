// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\lists\object-type-id
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ObjectTypeIdRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Lists.V3.crm.v3.lists.objectTypeId.item collection</summary>
        /// <param name="position">The object type ID of the object types stored by the list to fetch. For example, `0-1` for a `CONTACT` list.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.Item.WithObjectTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.Item.WithObjectTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("objectTypeId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.Item.WithObjectTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.ObjectTypeIdRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectTypeIdRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/object-type-id", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.ObjectTypeId.ObjectTypeIdRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectTypeIdRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/object-type-id", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618