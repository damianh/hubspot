// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Schemas;
using DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \media-bridge\v1\{appId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithAppItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The properties property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.PropertiesRequestBuilder Properties
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Properties.PropertiesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The schemas property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Schemas.SchemasRequestBuilder Schemas
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Schemas.SchemasRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The settings property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.SettingsRequestBuilder Settings
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.Settings.SettingsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.WithAppItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAppItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.MediaBridge.V1.Item.WithAppItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAppItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/media-bridge/v1/{appId}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
