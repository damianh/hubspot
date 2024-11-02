// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.AbTest;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Draft;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Revisions;
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\emails\{emailId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithEmailItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The abTest property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.AbTest.AbTestRequestBuilder AbTest
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.AbTest.AbTestRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The draft property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Draft.DraftRequestBuilder Draft
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Draft.DraftRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The revisions property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Revisions.RevisionsRequestBuilder Revisions
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.Revisions.RevisionsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEmailItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/{emailId}{?archived*,includeStats*,includedProperties*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithEmailItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/emails/{emailId}{?archived*,includeStats*,includedProperties*}", rawUrl)
        {
        }
        /// <summary>
        /// Delete a marketing email.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderDeleteQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderDeleteQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get the details for a marketing email.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Change properties of a marketing email.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail"/></returns>
        /// <param name="body">Properties of a marketing email you can update via the API.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail?> PatchAsync(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.EmailUpdateRequest body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderPatchQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail> PatchAsync(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.EmailUpdateRequest body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderPatchQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPatchRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.PublicEmail.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Delete a marketing email.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderDeleteQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderDeleteQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "*/*");
            return requestInfo;
        }
        /// <summary>
        /// Get the details for a marketing email.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Change properties of a marketing email.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">Properties of a marketing email you can update via the API.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.EmailUpdateRequest body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderPatchQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Models.EmailUpdateRequest body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderPatchQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PATCH, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Delete a marketing email.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderDeleteQueryParameters 
        {
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderDeleteRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderDeleteQueryParameters>
        {
        }
        /// <summary>
        /// Get the details for a marketing email.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderGetQueryParameters 
        {
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("includedProperties")]
            public string[]? IncludedProperties { get; set; }
#nullable restore
#else
            [QueryParameter("includedProperties")]
            public string[] IncludedProperties { get; set; }
#endif
            /// <summary>Include statistics with email</summary>
            [QueryParameter("includeStats")]
            public bool? IncludeStats { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Change properties of a marketing email.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderPatchQueryParameters 
        {
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithEmailItemRequestBuilderPatchRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmailsV3.V3.Marketing.V3.Emails.Item.WithEmailItemRequestBuilder.WithEmailItemRequestBuilderPatchQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
