// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3.Transactional;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class V3RequestBuilder : BaseRequestBuilder
    {
        /// <summary>The transactional property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3.Transactional.TransactionalRequestBuilder Transactional
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3.Transactional.TransactionalRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3.V3RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V3RequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Marketing.V3.V3RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V3RequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
