// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MarketingRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The v3 property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.V3RequestBuilder V3
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.V3RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.MarketingRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MarketingRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.MarketingRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MarketingRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
