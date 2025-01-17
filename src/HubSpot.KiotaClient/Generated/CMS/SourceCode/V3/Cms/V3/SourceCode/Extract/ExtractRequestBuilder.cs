// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract.Async;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\source-code\extract
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ExtractRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The async property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract.Async.AsyncRequestBuilder Async
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract.Async.AsyncRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract.ExtractRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtractRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/extract", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SourceCode.V3.Cms.V3.SourceCode.Extract.ExtractRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ExtractRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/source-code/extract", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
