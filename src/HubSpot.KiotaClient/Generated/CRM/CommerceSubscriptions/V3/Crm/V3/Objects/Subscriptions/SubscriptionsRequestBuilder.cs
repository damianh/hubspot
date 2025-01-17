// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item;
using DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Search;
using DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects\subscriptions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SubscriptionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The search property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Search.SearchRequestBuilder Search
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Search.SearchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.crm.v3.objects.subscriptions.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("subscriptionId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SubscriptionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/subscriptions{?after*,archived*,associations*,limit*,properties*,propertiesWithHistory*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SubscriptionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/subscriptions{?after*,archived*,associations*,limit*,properties*,propertiesWithHistory*}", rawUrl)
        {
        }
        /// <summary>
        /// Read a page of subscriptions. Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder.SubscriptionsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder.SubscriptionsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Read a page of subscriptions. Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder.SubscriptionsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder.SubscriptionsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Read a page of subscriptions. Control what is returned via the `properties` query param.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SubscriptionsRequestBuilderGetQueryParameters 
        {
            /// <summary>The paging cursor token of the last successfully read resource will be returned as the `paging.next.after` JSON property of a paged response containing more results.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>Whether to return only results that have been archived.</summary>
            [QueryParameter("archived")]
            public bool? Archived { get; set; }
            /// <summary>A comma separated list of object types to retrieve associated IDs for. If any of the specified associations do not exist, they will be ignored.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("associations")]
            public string[]? Associations { get; set; }
#nullable restore
#else
            [QueryParameter("associations")]
            public string[] Associations { get; set; }
#endif
            /// <summary>The maximum number of results to display per page.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>A comma separated list of the properties to be returned in the response. If any of the specified properties are not present on the requested object(s), they will be ignored.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("properties")]
            public string[]? Properties { get; set; }
#nullable restore
#else
            [QueryParameter("properties")]
            public string[] Properties { get; set; }
#endif
            /// <summary>A comma separated list of the properties to be returned along with their history of previous values. If any of the specified properties are not present on the requested object(s), they will be ignored. Usage of this parameter will reduce the maximum number of objects that can be read by a single request.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("propertiesWithHistory")]
            public string[]? PropertiesWithHistory { get; set; }
#nullable restore
#else
            [QueryParameter("propertiesWithHistory")]
            public string[] PropertiesWithHistory { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SubscriptionsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.SubscriptionsRequestBuilder.SubscriptionsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
