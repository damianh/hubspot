// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary.Enablement;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\object-library
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ObjectLibraryRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The enablement property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary.Enablement.EnablementRequestBuilder Enablement
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary.Enablement.EnablementRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary.ObjectLibraryRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectLibraryRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/object-library", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.ObjectLibrary.V3.Crm.V3.ObjectLibrary.ObjectLibraryRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ObjectLibraryRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/object-library", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
