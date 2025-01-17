// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\pages
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PagesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The landingPages property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.LandingPagesRequestBuilder LandingPages
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.LandingPagesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The sitePages property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.SitePagesRequestBuilder SitePages
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.SitePagesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.PagesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public PagesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.PagesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public PagesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
