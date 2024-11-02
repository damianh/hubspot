// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth
{
    /// <summary>
    /// Builds and executes requests for operations under \oauth
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class OauthRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The v1 property</summary>
        public global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.V1RequestBuilder V1
        {
            get => new global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.V1RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.OauthRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public OauthRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.OauthRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public OauthRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618