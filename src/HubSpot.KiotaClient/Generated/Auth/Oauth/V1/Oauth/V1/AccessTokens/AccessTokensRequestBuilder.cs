// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens
{
    /// <summary>
    /// Builds and executes requests for operations under \oauth\v1\access-tokens
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AccessTokensRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.oauth.v1.accessTokens.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.Item.WithTokenItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.Item.WithTokenItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("token", position);
                return new global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.Item.WithTokenItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.AccessTokensRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AccessTokensRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth/v1/access-tokens", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.AccessTokens.AccessTokensRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AccessTokensRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth/v1/access-tokens", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
