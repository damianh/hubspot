// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects\subscriptions\{subscriptionId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithSubscriptionItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithSubscriptionItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/subscriptions/{subscriptionId}{?archived*,associations*,idProperty*,properties*,propertiesWithHistory*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithSubscriptionItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/subscriptions/{subscriptionId}{?archived*,associations*,idProperty*,properties*,propertiesWithHistory*}", rawUrl)
        {
        }
        /// <summary>
        /// Read an Object identified by `{subscriptionId}`. `{subscriptionId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.SimplePublicObjectWithAssociations"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.SimplePublicObjectWithAssociations?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder.WithSubscriptionItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.SimplePublicObjectWithAssociations> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder.WithSubscriptionItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.SimplePublicObjectWithAssociations>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Models.SimplePublicObjectWithAssociations.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Read an Object identified by `{subscriptionId}`. `{subscriptionId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder.WithSubscriptionItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder.WithSubscriptionItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Read an Object identified by `{subscriptionId}`. `{subscriptionId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithSubscriptionItemRequestBuilderGetQueryParameters 
        {
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
            /// <summary>The name of a property whose values are unique for this object type</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("idProperty")]
            public string? IdProperty { get; set; }
#nullable restore
#else
            [QueryParameter("idProperty")]
            public string IdProperty { get; set; }
#endif
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
            /// <summary>A comma separated list of the properties to be returned along with their history of previous values. If any of the specified properties are not present on the requested object(s), they will be ignored.</summary>
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
        public partial class WithSubscriptionItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.CommerceSubscriptions.V3.Crm.V3.Objects.Subscriptions.Item.WithSubscriptionItemRequestBuilder.WithSubscriptionItemRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
