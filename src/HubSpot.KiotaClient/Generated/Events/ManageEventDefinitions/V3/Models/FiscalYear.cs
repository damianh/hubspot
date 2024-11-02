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
    public partial class FiscalYear : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The day property</summary>
        public int? Day { get; set; }
        /// <summary>The hour property</summary>
        public int? Hour { get; set; }
        /// <summary>The millisecond property</summary>
        public int? Millisecond { get; set; }
        /// <summary>The minute property</summary>
        public int? Minute { get; set; }
        /// <summary>The month property</summary>
        public int? Month { get; set; }
        /// <summary>The referenceType property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear_referenceType? ReferenceType { get; set; }
        /// <summary>The second property</summary>
        public int? Second { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear"/> and sets the default values.
        /// </summary>
        public FiscalYear()
        {
            AdditionalData = new Dictionary<string, object>();
            ReferenceType = global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear_referenceType.FISCAL_YEAR;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "day", n => { Day = n.GetIntValue(); } },
                { "hour", n => { Hour = n.GetIntValue(); } },
                { "millisecond", n => { Millisecond = n.GetIntValue(); } },
                { "minute", n => { Minute = n.GetIntValue(); } },
                { "month", n => { Month = n.GetIntValue(); } },
                { "referenceType", n => { ReferenceType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear_referenceType>(); } },
                { "second", n => { Second = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("day", Day);
            writer.WriteIntValue("hour", Hour);
            writer.WriteIntValue("millisecond", Millisecond);
            writer.WriteIntValue("minute", Minute);
            writer.WriteIntValue("month", Month);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.FiscalYear_referenceType>("referenceType", ReferenceType);
            writer.WriteIntValue("second", Second);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
