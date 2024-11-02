// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\campaigns\{campaignGuid}\budget\totals
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class TotalsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TotalsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/budget/totals", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TotalsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/budget/totals", rawUrl)
        {
        }
        /// <summary>
        /// Retrieve detailed information about the budget and spend items for a specified campaign, including the total budget, total spend, and remaining budget.Budget and Spend items may be returned in any order, but the order field specifies their sequence based on the creation date. The item with order 0 is the oldest, and items with higher order values are newer
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Retrieve detailed information about the budget and spend items for a specified campaign, including the total budget, total spend, and remaining budget.Budget and Spend items may be returned in any order, but the order field specifies their sequence based on the creation date. The item with order 0 is the oldest, and items with higher order values are newer
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
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class TotalsRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
