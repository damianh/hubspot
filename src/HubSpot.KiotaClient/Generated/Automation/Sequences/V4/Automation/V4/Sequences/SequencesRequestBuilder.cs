// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Enrollments;
using DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Item;
using DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences
{
    /// <summary>
    /// Builds and executes requests for operations under \automation\v4\sequences
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SequencesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The enrollments property</summary>
        public global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Enrollments.EnrollmentsRequestBuilder Enrollments
        {
            get => new global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Enrollments.EnrollmentsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.automation.v4.sequences.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Item.WithSequenceItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Item.WithSequenceItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("sequenceId", position);
                return new global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.Item.WithSequenceItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SequencesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/automation/v4/sequences/?userId={userId}{&after*,limit*,name*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SequencesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/automation/v4/sequences/?userId={userId}{&after*,limit*,name*}", rawUrl)
        {
        }
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models.CollectionResponseWithTotalPublicSequenceLiteResponseForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models.CollectionResponseWithTotalPublicSequenceLiteResponseForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder.SequencesRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models.CollectionResponseWithTotalPublicSequenceLiteResponseForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder.SequencesRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models.CollectionResponseWithTotalPublicSequenceLiteResponseForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models.CollectionResponseWithTotalPublicSequenceLiteResponseForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder.SequencesRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder.SequencesRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder(rawUrl, RequestAdapter);
        }
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        #pragma warning disable CS1591
        public partial class SequencesRequestBuilderGetQueryParameters 
        #pragma warning restore CS1591
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            [QueryParameter("limit")]
            public int? Limit { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("name")]
            public string? Name { get; set; }
#nullable restore
#else
            [QueryParameter("name")]
            public string Name { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("userId")]
            public string? UserId { get; set; }
#nullable restore
#else
            [QueryParameter("userId")]
            public string UserId { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SequencesRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Automation.V4.Sequences.SequencesRequestBuilder.SequencesRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618