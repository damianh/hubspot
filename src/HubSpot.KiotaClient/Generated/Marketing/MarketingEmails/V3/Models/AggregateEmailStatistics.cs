// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models
{
    /// <summary>
    /// Aggregated statistics for the given interval, plus the IDs of emails that were sent during that interval.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AggregateEmailStatistics : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The aggregate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.EmailStatisticsData? Aggregate { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.EmailStatisticsData Aggregate { get; set; }
#endif
        /// <summary>The aggregated statistics per campaign.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics_campaignAggregations? CampaignAggregations { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics_campaignAggregations CampaignAggregations { get; set; }
#endif
        /// <summary>List of email IDs that were sent during the time span.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<long?>? Emails { get; set; }
#nullable restore
#else
        public List<long?> Emails { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics"/> and sets the default values.
        /// </summary>
        public AggregateEmailStatistics()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aggregate", n => { Aggregate = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.EmailStatisticsData>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.EmailStatisticsData.CreateFromDiscriminatorValue); } },
                { "campaignAggregations", n => { CampaignAggregations = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics_campaignAggregations>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics_campaignAggregations.CreateFromDiscriminatorValue); } },
                { "emails", n => { Emails = n.GetCollectionOfPrimitiveValues<long?>()?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.EmailStatisticsData>("aggregate", Aggregate);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.AggregateEmailStatistics_campaignAggregations>("campaignAggregations", CampaignAggregations);
            writer.WriteCollectionOfPrimitiveValues<long?>("emails", Emails);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
