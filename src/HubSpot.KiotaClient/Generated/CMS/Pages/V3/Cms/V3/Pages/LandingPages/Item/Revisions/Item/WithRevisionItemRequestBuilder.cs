// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.Restore;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.RestoreToDraft;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\pages\landing-pages\{objectId}\revisions\{revisionId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithRevisionItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The restore property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.Restore.RestoreRequestBuilder Restore
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.Restore.RestoreRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The restoreToDraft property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.RestoreToDraft.RestoreToDraftRequestBuilder RestoreToDraft
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.RestoreToDraft.RestoreToDraftRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.WithRevisionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithRevisionItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages/landing-pages/{objectId}/revisions/{revisionId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.WithRevisionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithRevisionItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages/landing-pages/{objectId}/revisions/{revisionId}", rawUrl)
        {
        }
        /// <summary>
        /// Retrieves a previous version of a Landing Page
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models.VersionPage"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models.VersionPage?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models.VersionPage> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models.VersionPage>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models.VersionPage.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieves a previous version of a Landing Page
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.WithRevisionItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.WithRevisionItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.LandingPages.Item.Revisions.Item.WithRevisionItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithRevisionItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
