// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Batch;
using DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Item;
using DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Search;
using DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\objects\products
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ProductsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The batch property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Batch.BatchRequestBuilder Batch
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Batch.BatchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The search property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Search.SearchRequestBuilder Search
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Search.SearchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Products.V3.crm.v3.objects.products.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Item.WithProductItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Item.WithProductItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("productId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.Item.WithProductItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ProductsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/products{?after*,archived*,associations*,limit*,properties*,propertiesWithHistory*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ProductsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/objects/products{?after*,archived*,associations*,limit*,properties*,propertiesWithHistory*}", rawUrl)
        {
        }
        /// <summary>
        /// Read a page of products. Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder.ProductsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder.ProductsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.CollectionResponseSimplePublicObjectWithAssociationsForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Create a product with the given properties and return a copy of the object, including the ID. Documentation and examples for creating standard products is provided.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObject"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObject?> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObjectInputForCreate body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObject> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObjectInputForCreate body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObject>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObject.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Read a page of products. Control what is returned via the `properties` query param.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder.ProductsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder.ProductsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Create a product with the given properties and return a copy of the object, including the ID. Documentation and examples for creating standard products is provided.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObjectInputForCreate body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Models.SimplePublicObjectInputForCreate body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Read a page of products. Control what is returned via the `properties` query param.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ProductsRequestBuilderGetQueryParameters 
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
        public partial class ProductsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Products.V3.Crm.V3.Objects.Products.ProductsRequestBuilder.ProductsRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ProductsRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
