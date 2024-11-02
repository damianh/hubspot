// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class AssociationSpec : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Either `HUBSPOT_DEFINED` (default label) or `USER_DEFINED` (custom label).</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec_associationCategory? AssociationCategory { get; set; }
        /// <summary>The [association type ID](https://developers.hubspot.com/docs/guides/api/crm/associations/associations-v4#association-type-id-values) (e.g., `4` for contact-to-company associations).</summary>
        public int? AssociationTypeId { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec"/> and sets the default values.
        /// </summary>
        public AssociationSpec()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "associationCategory", n => { AssociationCategory = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec_associationCategory>(); } },
                { "associationTypeId", n => { AssociationTypeId = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.AssociationSpec_associationCategory>("associationCategory", AssociationCategory);
            writer.WriteIntValue("associationTypeId", AssociationTypeId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
