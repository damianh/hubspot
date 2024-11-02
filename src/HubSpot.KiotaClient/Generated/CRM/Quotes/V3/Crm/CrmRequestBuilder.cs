// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm.V3;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm
{
    /// <summary>
    /// Builds and executes requests for operations under \crm
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CrmRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The v3 property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm.V3.V3RequestBuilder V3
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm.V3.V3RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm.CrmRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CrmRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Quotes.V3.Crm.CrmRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CrmRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
