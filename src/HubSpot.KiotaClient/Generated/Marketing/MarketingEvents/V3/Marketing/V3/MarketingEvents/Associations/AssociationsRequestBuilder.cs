// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\marketing-events\associations
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AssociationsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.marketing.v3.marketingEvents.associations.item collection</summary>
        /// <param name="position">The accountId that is associated with this marketing event in the external event application</param>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.Item.ExternalAccountItemRequestBuilder"/></returns>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.Item.ExternalAccountItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("externalAccount%2Did", position);
                return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.Item.ExternalAccountItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.AssociationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AssociationsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/associations", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Marketing.V3.MarketingEvents.Associations.AssociationsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AssociationsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/marketing-events/associations", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
