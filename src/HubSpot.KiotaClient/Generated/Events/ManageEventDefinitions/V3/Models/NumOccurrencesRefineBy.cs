// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class NumOccurrencesRefineBy : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The maxOccurrences property</summary>
        public int? MaxOccurrences { get; set; }
        /// <summary>The minOccurrences property</summary>
        public int? MinOccurrences { get; set; }
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy_type? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy"/> and sets the default values.
        /// </summary>
        public NumOccurrencesRefineBy()
        {
            AdditionalData = new Dictionary<string, object>();
            Type = global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy_type.NumOccurrencesRefineBy;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "maxOccurrences", n => { MaxOccurrences = n.GetIntValue(); } },
                { "minOccurrences", n => { MinOccurrences = n.GetIntValue(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy_type>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("maxOccurrences", MaxOccurrences);
            writer.WriteIntValue("minOccurrences", MinOccurrences);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.NumOccurrencesRefineBy_type>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
