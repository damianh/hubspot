// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects.Companies;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ObjectsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The companies property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects.Companies.CompaniesRequestBuilder Companies
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects.Companies.CompaniesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects.ObjectsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Companies.V3.Crm.V3.Objects.ObjectsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
