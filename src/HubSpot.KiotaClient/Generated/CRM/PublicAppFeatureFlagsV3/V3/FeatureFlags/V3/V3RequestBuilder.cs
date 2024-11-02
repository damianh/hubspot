// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3
{
    /// <summary>
    /// Builds and executes requests for operations under \feature-flags\v3
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class V3RequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.featureFlags.v3.item collection</summary>
        /// <param name="position">The ID of the app.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder this[int position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("appId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.featureFlags.v3.item collection</summary>
        /// <param name="position">The ID of the app.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder"/></returns>
        [Obsolete("This indexer is deprecated and will be removed in the next major version. Use the one with the typed parameter instead.")]
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                if (!string.IsNullOrWhiteSpace(position)) urlTplParams.Add("appId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.Item.WithAppItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.V3RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V3RequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/feature-flags/v3", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppFeatureFlagsV3.V3.FeatureFlags.V3.V3RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V3RequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/feature-flags/v3", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
