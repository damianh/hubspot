// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicCommunicationSubscriptionFilter : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The acceptedOptStates property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AcceptedOptStates { get; set; }
#nullable restore
#else
        public List<string> AcceptedOptStates { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The businessUnitId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BusinessUnitId { get; set; }
#nullable restore
#else
        public string BusinessUnitId { get; set; }
#endif
        /// <summary>The channel property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Channel { get; set; }
#nullable restore
#else
        public string Channel { get; set; }
#endif
        /// <summary>The filterType property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter_filterType? FilterType { get; set; }
        /// <summary>The subscriptionIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SubscriptionIds { get; set; }
#nullable restore
#else
        public List<string> SubscriptionIds { get; set; }
#endif
        /// <summary>The subscriptionType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubscriptionType { get; set; }
#nullable restore
#else
        public string SubscriptionType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter"/> and sets the default values.
        /// </summary>
        public PublicCommunicationSubscriptionFilter()
        {
            AdditionalData = new Dictionary<string, object>();
            FilterType = global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter_filterType.COMMUNICATION_SUBSCRIPTION;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "acceptedOptStates", n => { AcceptedOptStates = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "businessUnitId", n => { BusinessUnitId = n.GetStringValue(); } },
                { "channel", n => { Channel = n.GetStringValue(); } },
                { "filterType", n => { FilterType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter_filterType>(); } },
                { "subscriptionIds", n => { SubscriptionIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "subscriptionType", n => { SubscriptionType = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("acceptedOptStates", AcceptedOptStates);
            writer.WriteStringValue("businessUnitId", BusinessUnitId);
            writer.WriteStringValue("channel", Channel);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCommunicationSubscriptionFilter_filterType>("filterType", FilterType);
            writer.WriteCollectionOfPrimitiveValues<string>("subscriptionIds", SubscriptionIds);
            writer.WriteStringValue("subscriptionType", SubscriptionType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
