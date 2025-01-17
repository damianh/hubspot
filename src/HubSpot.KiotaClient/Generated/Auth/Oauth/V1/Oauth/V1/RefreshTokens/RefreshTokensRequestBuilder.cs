// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens
{
    /// <summary>
    /// Builds and executes requests for operations under \oauth\v1\refresh-tokens
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class RefreshTokensRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.oauth.v1.refreshTokens.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.Item.WithTokenItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.Item.WithTokenItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("token", position);
                return new global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.Item.WithTokenItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.RefreshTokensRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RefreshTokensRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth/v1/refresh-tokens", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Auth.Oauth.V1.Oauth.V1.RefreshTokens.RefreshTokensRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RefreshTokensRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/oauth/v1/refresh-tokens", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
