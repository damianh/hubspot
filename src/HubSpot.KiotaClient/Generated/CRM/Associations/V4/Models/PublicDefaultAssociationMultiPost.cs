// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicDefaultAssociationMultiPost : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The from property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId? From { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId From { get; set; }
#endif
        /// <summary>The to property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId? To { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId To { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicDefaultAssociationMultiPost"/> and sets the default values.
        /// </summary>
        public PublicDefaultAssociationMultiPost()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicDefaultAssociationMultiPost"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicDefaultAssociationMultiPost CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicDefaultAssociationMultiPost();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "from", n => { From = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId>(global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId.CreateFromDiscriminatorValue); } },
                { "to", n => { To = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId>(global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId>("from", From);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Associations.V4.Models.PublicObjectId>("to", To);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
