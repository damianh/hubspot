// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects\deals\{dealId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithDealItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithDealItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/deals/{dealId}{?archived*,associations*,idProperty*,properties*,propertiesWithHistory*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithDealItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/deals/{dealId}{?archived*,associations*,idProperty*,properties*,propertiesWithHistory*}", rawUrl)
        {
        }
        /// <summary>
        /// Move an Object identified by `{dealId}` to the recycling bin.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Read an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectWithAssociations"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectWithAssociations?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectWithAssociations> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectWithAssociations>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectWithAssociations.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Perform a partial update of an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param. Provided property values will be overwritten. Read-only and non-existent properties will be ignored. Properties values can be cleared by passing an empty string.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObject"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObject?> PatchAsync(global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectInput body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderPatchQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObject> PatchAsync(global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectInput body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderPatchQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPatchRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObject>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObject.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Move an Object identified by `{dealId}` to the recycling bin.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "*/*");
            return requestInfo;
        }
        /// <summary>
        /// Read an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Perform a partial update of an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param. Provided property values will be overwritten. Read-only and non-existent properties will be ignored. Properties values can be cleared by passing an empty string.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectInput body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderPatchQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Models.SimplePublicObjectInput body, Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderPatchQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithDealItemRequestBuilderDeleteRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Read an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param.  Control what is returned via the `properties` query param.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithDealItemRequestBuilderGetQueryParameters 
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
        public partial class WithDealItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Perform a partial update of an Object identified by `{dealId}`. `{dealId}` refers to the internal object ID by default, or optionally any unique property value as specified by the `idProperty` query param. Provided property values will be overwritten. Read-only and non-existent properties will be ignored. Properties values can be cleared by passing an empty string.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithDealItemRequestBuilderPatchQueryParameters 
        {
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
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithDealItemRequestBuilderPatchRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Deals.V3.Crm.V3.Objects.Deals.Item.WithDealItemRequestBuilder.WithDealItemRequestBuilderPatchQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
