// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item.Complete;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \automation\v4\actions\callbacks\{callbackId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithCallbackItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The complete property</summary>
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item.Complete.CompleteRequestBuilder Complete
        {
            get => new global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item.Complete.CompleteRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item.WithCallbackItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCallbackItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/automation/v4/actions/callbacks/{callbackId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Automation.V4.Actions.Callbacks.Item.WithCallbackItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCallbackItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/automation/v4/actions/callbacks/{callbackId}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618