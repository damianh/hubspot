// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\blogs\authors\multi-language\set-new-lang-primary
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SetNewLangPrimaryRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SetNewLangPrimaryRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blogs/authors/multi-language/set-new-lang-primary", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SetNewLangPrimaryRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blogs/authors/multi-language/set-new-lang-primary", rawUrl)
        {
        }
        /// <summary>
        /// Set a Blog Author as the primary language of a multi-language group.
        /// </summary>
        /// <param name="body">Request body object for setting a new primary language.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task PutAsync(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.SetNewLanguagePrimaryRequestVNext body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task PutAsync(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.SetNewLanguagePrimaryRequestVNext body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPutRequestInformation(body, requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Set a Blog Author as the primary language of a multi-language group.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">Request body object for setting a new primary language.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPutRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.SetNewLanguagePrimaryRequestVNext body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPutRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.SetNewLanguagePrimaryRequestVNext body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PUT, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "*/*");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SetNewLangPrimaryRequestBuilderPutRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
