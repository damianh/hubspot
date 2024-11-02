// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class MarketingEventSubscriber : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Timestamp in milliseconds at which the contact subscribed to the event.</summary>
        public long? InteractionDateTime { get; set; }
        /// <summary>The properties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber_properties? Properties { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber_properties Properties { get; set; }
#endif
        /// <summary>The vid property</summary>
        public int? Vid { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber"/> and sets the default values.
        /// </summary>
        public MarketingEventSubscriber()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "interactionDateTime", n => { InteractionDateTime = n.GetLongValue(); } },
                { "properties", n => { Properties = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber_properties>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber_properties.CreateFromDiscriminatorValue); } },
                { "vid", n => { Vid = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteLongValue("interactionDateTime", InteractionDateTime);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventSubscriber_properties>("properties", Properties);
            writer.WriteIntValue("vid", Vid);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
