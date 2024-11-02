// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.Item;
using DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\extensions\cards-dev\{appId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithAppItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.crm.v3.extensions.cardsDev.item.item collection</summary>
        /// <param name="position">The ID of the target card.</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.Item.WithCardItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.Item.WithCardItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("cardId", position);
                return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.Item.WithCardItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.WithAppItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAppItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/extensions/cards-dev/{appId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.WithAppItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAppItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/extensions/cards-dev/{appId}", rawUrl)
        {
        }
        /// <summary>
        /// Returns a list of cards for a given app.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardListResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardListResponse?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardListResponse> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardListResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardListResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Defines a new card that will become active on an account when this app is installed.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse"/></returns>
        /// <param name="body">State of card definition to be created</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse?> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse> PostAsync(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse>(requestInfo, global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Returns a list of cards for a given app.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Defines a new card that will become active on an account when this app is installed.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">State of card definition to be created</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.WithAppItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.WithAppItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Crm.V3.Extensions.CardsDev.Item.WithAppItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithAppItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithAppItemRequestBuilderPostRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618