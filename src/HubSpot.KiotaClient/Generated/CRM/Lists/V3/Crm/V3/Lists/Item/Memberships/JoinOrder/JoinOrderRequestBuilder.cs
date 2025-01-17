// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\lists\{listId}\memberships\join-order
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class JoinOrderRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public JoinOrderRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/{listId}/memberships/join-order{?after*,before*,limit*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public JoinOrderRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/lists/{listId}/memberships/join-order{?after*,before*,limit*}", rawUrl)
        {
        }
        /// <summary>
        /// Fetch the memberships of a list in order sorted by the time the records were added to the list.The `recordId`s are sorted in *ascending* order if an `after` offset or no offset is provided. If only a `before` offset is provided, then the records are sorted in *descending* order.The `after` offset parameter will take precedence over the `before` offset in a case where both are provided.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ApiCollectionResponseJoinTimeAndRecordId"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ApiCollectionResponseJoinTimeAndRecordId?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder.JoinOrderRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ApiCollectionResponseJoinTimeAndRecordId> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder.JoinOrderRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ApiCollectionResponseJoinTimeAndRecordId>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.ApiCollectionResponseJoinTimeAndRecordId.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Fetch the memberships of a list in order sorted by the time the records were added to the list.The `recordId`s are sorted in *ascending* order if an `after` offset or no offset is provided. If only a `before` offset is provided, then the records are sorted in *descending* order.The `after` offset parameter will take precedence over the `before` offset in a case where both are provided.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder.JoinOrderRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder.JoinOrderRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Fetch the memberships of a list in order sorted by the time the records were added to the list.The `recordId`s are sorted in *ascending* order if an `after` offset or no offset is provided. If only a `before` offset is provided, then the records are sorted in *descending* order.The `after` offset parameter will take precedence over the `before` offset in a case where both are provided.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class JoinOrderRequestBuilderGetQueryParameters 
        {
            /// <summary>The paging offset token for the page that comes `after` the previously requested records.If provided, then the records in the response will be the records following the offset, sorted in *ascending* order. Takes precedence over the `before` offset.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>The paging offset token for the page that comes `before` the previously requested records.If provided, then the records in the response will be the records preceding the offset, sorted in *descending* order.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("before")]
            public string? Before { get; set; }
#nullable restore
#else
            [QueryParameter("before")]
            public string Before { get; set; }
#endif
            /// <summary>The number of records to return in the response. The maximum `limit` is 250.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class JoinOrderRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Crm.V3.Lists.Item.Memberships.JoinOrder.JoinOrderRequestBuilder.JoinOrderRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
