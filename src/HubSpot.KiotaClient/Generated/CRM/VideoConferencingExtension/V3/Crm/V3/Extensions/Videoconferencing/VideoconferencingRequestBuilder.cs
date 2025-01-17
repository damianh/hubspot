// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing.Settings;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\extensions\videoconferencing
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class VideoconferencingRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The settings property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing.Settings.SettingsRequestBuilder Settings
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing.Settings.SettingsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing.VideoconferencingRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public VideoconferencingRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/extensions/videoconferencing", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.VideoConferencingExtension.V3.Crm.V3.Extensions.Videoconferencing.VideoconferencingRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public VideoconferencingRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/extensions/videoconferencing", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
