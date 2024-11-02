// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item.Batch;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v4\associations\{fromObjectType}\{toObjectType}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithToObjectTypeItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item.WithToObjectTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithToObjectTypeItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/{fromObjectType}/{toObjectType}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Crm.V4.Associations.Item.Item.WithToObjectTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithToObjectTypeItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/{fromObjectType}/{toObjectType}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618