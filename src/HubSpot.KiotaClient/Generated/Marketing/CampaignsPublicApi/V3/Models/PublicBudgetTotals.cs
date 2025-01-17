// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicBudgetTotals : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The budgetItems property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetItem>? BudgetItems { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetItem> BudgetItems { get; set; }
#endif
        /// <summary>The budgetTotal property</summary>
        public double? BudgetTotal { get; set; }
        /// <summary>The currencyCode property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals_currencyCode? CurrencyCode { get; set; }
        /// <summary>The remainingBudget property</summary>
        public double? RemainingBudget { get; set; }
        /// <summary>The spendItems property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicSpendItem>? SpendItems { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicSpendItem> SpendItems { get; set; }
#endif
        /// <summary>The spendTotal property</summary>
        public double? SpendTotal { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals"/> and sets the default values.
        /// </summary>
        public PublicBudgetTotals()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "budgetItems", n => { BudgetItems = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetItem>(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "budgetTotal", n => { BudgetTotal = n.GetDoubleValue(); } },
                { "currencyCode", n => { CurrencyCode = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals_currencyCode>(); } },
                { "remainingBudget", n => { RemainingBudget = n.GetDoubleValue(); } },
                { "spendItems", n => { SpendItems = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicSpendItem>(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicSpendItem.CreateFromDiscriminatorValue)?.AsList(); } },
                { "spendTotal", n => { SpendTotal = n.GetDoubleValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetItem>("budgetItems", BudgetItems);
            writer.WriteDoubleValue("budgetTotal", BudgetTotal);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicBudgetTotals_currencyCode>("currencyCode", CurrencyCode);
            writer.WriteDoubleValue("remainingBudget", RemainingBudget);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicSpendItem>("spendItems", SpendItems);
            writer.WriteDoubleValue("spendTotal", SpendTotal);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
