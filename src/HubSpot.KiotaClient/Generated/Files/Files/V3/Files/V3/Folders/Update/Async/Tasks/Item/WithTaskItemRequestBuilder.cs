// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item.Status;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \files\v3\folders\update\async\tasks\{taskId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithTaskItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The status property</summary>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item.Status.StatusRequestBuilder Status
        {
            get => new global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item.Status.StatusRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item.WithTaskItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithTaskItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/update/async/tasks/{taskId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Files.V3.Folders.Update.Async.Tasks.Item.WithTaskItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithTaskItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/files/v3/folders/update/async/tasks/{taskId}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618