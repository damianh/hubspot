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
namespace DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\blogs\authors\batch\read
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ReadRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ReadRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blogs/authors/batch/read{?archived*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ReadRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/blogs/authors/batch/read{?archived*}", rawUrl)
        {
        }
        /// <summary>
        /// Retrieve the Blog Author objects identified in the request body.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchResponseBlogAuthor"/></returns>
        /// <param name="body">Wrapper for providing an array of strings as inputs.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchResponseBlogAuthor?> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder.ReadRequestBuilderPostQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchResponseBlogAuthor> PostAsync(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder.ReadRequestBuilderPostQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchResponseBlogAuthor>(requestInfo, global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchResponseBlogAuthor.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieve the Blog Author objects identified in the request body.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">Wrapper for providing an array of strings as inputs.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder.ReadRequestBuilderPostQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Models.BatchInputString body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder.ReadRequestBuilderPostQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Retrieve the Blog Author objects identified in the request body.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ReadRequestBuilderPostQueryParameters 
        {
            /// <summary>Specifies whether to return deleted Blog Authors. Defaults to `false`.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ReadRequestBuilderPostRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CMS.Authors.V3.Cms.V3.Blogs.Authors.Batch.Read.ReadRequestBuilder.ReadRequestBuilderPostQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
