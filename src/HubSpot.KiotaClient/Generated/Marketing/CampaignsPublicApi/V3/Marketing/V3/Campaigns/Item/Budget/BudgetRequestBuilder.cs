// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\campaigns\{campaignGuid}\budget
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BudgetRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The totals property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder Totals
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.Totals.TotalsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.BudgetRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BudgetRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/budget", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.BudgetRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BudgetRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/budget", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618