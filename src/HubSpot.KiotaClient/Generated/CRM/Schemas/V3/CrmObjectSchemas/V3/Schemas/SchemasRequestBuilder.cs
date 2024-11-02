// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.Item;
using DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas
{
    /// <summary>
    /// Builds and executes requests for operations under \crm-object-schemas\v3\schemas
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SchemasRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.crmObjectSchemas.v3.schemas.item collection</summary>
        /// <param name="position">Fully qualified name or object type ID of your schema.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.Item.WithObjectTypeItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.Item.WithObjectTypeItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("objectType", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.Item.WithObjectTypeItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SchemasRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm-object-schemas/v3/schemas{?archived*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SchemasRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm-object-schemas/v3/schemas{?archived*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns all object schemas that have been defined for your account.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.CollectionResponseObjectSchemaNoPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.CollectionResponseObjectSchemaNoPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder.SchemasRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.CollectionResponseObjectSchemaNoPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder.SchemasRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.CollectionResponseObjectSchemaNoPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.CollectionResponseObjectSchemaNoPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Define a new object schema, along with custom properties and associations. The entire object schema, including its object type ID, properties, and associations will be returned in the response.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchema"/></returns>
        /// <param name="body">Defines a new object type, its properties, and associations.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchema?> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchemaEgg body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchema> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchemaEgg body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchema>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchema.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns all object schemas that have been defined for your account.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder.SchemasRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder.SchemasRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Define a new object schema, along with custom properties and associations. The entire object schema, including its object type ID, properties, and associations will be returned in the response.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">Defines a new object type, its properties, and associations.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchemaEgg body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.Models.ObjectSchemaEgg body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns all object schemas that have been defined for your account.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SchemasRequestBuilderGetQueryParameters 
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
        public partial class SchemasRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.CRM.Schemas.V3.CrmObjectSchemas.V3.Schemas.SchemasRequestBuilder.SchemasRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SchemasRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
