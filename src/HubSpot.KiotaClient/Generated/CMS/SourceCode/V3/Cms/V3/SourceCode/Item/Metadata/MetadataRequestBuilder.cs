// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\source-code\{environment}\metadata
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MetadataRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.cms.v3.sourceCode.item.metadata.item collection</summary>
        /// <param name="position">The file system location of the file.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.Item.WithPathItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.Item.WithPathItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("path", position);
                return new global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.Item.WithPathItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.MetadataRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MetadataRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/{environment}/metadata", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Item.Metadata.MetadataRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MetadataRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/{environment}/metadata", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
