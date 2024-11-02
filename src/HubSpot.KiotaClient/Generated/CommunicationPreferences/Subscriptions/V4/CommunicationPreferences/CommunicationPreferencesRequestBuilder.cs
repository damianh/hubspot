// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences
{
    /// <summary>
    /// Builds and executes requests for operations under \communication-preferences
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CommunicationPreferencesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The v4 property</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.V4RequestBuilder V4
        {
            get => new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.V4.V4RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.CommunicationPreferencesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CommunicationPreferencesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.CommunicationPreferences.CommunicationPreferencesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CommunicationPreferencesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/communication-preferences", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618