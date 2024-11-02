// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Content;
using DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata;
using DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Validate;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\source-code\{environment}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithEnvironmentItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The content property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Content.ContentRequestBuilder Content
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Content.ContentRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The metadata property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.MetadataRequestBuilder Metadata
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.MetadataRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The validate property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Validate.ValidateRequestBuilder Validate
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Validate.ValidateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.WithEnvironmentItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEnvironmentItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/{environment}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.WithEnvironmentItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEnvironmentItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/{environment}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
