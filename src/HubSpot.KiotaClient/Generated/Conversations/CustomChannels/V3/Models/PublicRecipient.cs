// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicRecipient : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The actorId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ActorId { get; set; }
#nullable restore
#else
        public string ActorId { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The deliveryIdentifier property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicDeliveryIdentifier? DeliveryIdentifier { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicDeliveryIdentifier DeliveryIdentifier { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The recipientField property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RecipientField { get; set; }
#nullable restore
#else
        public string RecipientField { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicRecipient"/> and sets the default values.
        /// </summary>
        public PublicRecipient()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicRecipient"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicRecipient CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicRecipient();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "actorId", n => { ActorId = n.GetStringValue(); } },
                { "deliveryIdentifier", n => { DeliveryIdentifier = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicDeliveryIdentifier>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicDeliveryIdentifier.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "recipientField", n => { RecipientField = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("actorId", ActorId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.PublicDeliveryIdentifier>("deliveryIdentifier", DeliveryIdentifier);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("recipientField", RecipientField);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
