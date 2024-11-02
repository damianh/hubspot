// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update
{
    /// <summary>
    /// Builds and executes requests for operations under \files\v3\folders\update
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UpdateRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The async property</summary>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.AsyncRequestBuilder Async
        {
            get => new global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.AsyncRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.UpdateRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UpdateRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/update", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.UpdateRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UpdateRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/update", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618