// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest.CreateVariation;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\emails\ab-test
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AbTestRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The createVariation property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest.CreateVariation.CreateVariationRequestBuilder CreateVariation
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest.CreateVariation.CreateVariationRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest.AbTestRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AbTestRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/ab-test", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Marketing.V3.Emails.AbTest.AbTestRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AbTestRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/ab-test", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618