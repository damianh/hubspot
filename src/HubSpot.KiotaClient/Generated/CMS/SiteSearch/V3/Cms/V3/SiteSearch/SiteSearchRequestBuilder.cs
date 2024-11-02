// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.IndexedData;
using DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\site-search
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SiteSearchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The indexedData property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.IndexedData.IndexedDataRequestBuilder IndexedData
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.IndexedData.IndexedDataRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The search property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder Search
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.Search.SearchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.SiteSearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SiteSearchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/site-search", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.SiteSearch.V3.Cms.V3.SiteSearch.SiteSearchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SiteSearchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/site-search", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
