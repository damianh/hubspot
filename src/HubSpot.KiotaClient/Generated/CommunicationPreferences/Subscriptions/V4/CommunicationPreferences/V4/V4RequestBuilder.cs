// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Definitions;
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4
{
    /// <summary>
    /// Builds and executes requests for operations under \communication-preferences\v4
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class V4RequestBuilder : BaseRequestBuilder
    {
        /// <summary>The definitions property</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Definitions.DefinitionsRequestBuilder Definitions
        {
            get => new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Definitions.DefinitionsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The statuses property</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.StatusesRequestBuilder Statuses
        {
            get => new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.Statuses.StatusesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.V4RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V4RequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences/v4", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.V4RequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public V4RequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences/v4", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
