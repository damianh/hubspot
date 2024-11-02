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
    public partial class PublicRollingDateRangePropertyOperation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The includeObjectsWithNoValueSet property</summary>
        public bool? IncludeObjectsWithNoValueSet { get; set; }
        /// <summary>The numberOfDays property</summary>
        public int? NumberOfDays { get; set; }
        /// <summary>The operationType property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation_operationType? OperationType { get; set; }
        /// <summary>The operator property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Operator { get; set; }
#nullable restore
#else
        public string Operator { get; set; }
#endif
        /// <summary>The requiresTimeZoneConversion property</summary>
        public bool? RequiresTimeZoneConversion { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation"/> and sets the default values.
        /// </summary>
        public PublicRollingDateRangePropertyOperation()
        {
            AdditionalData = new Dictionary<string, object>();
            OperationType = global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation_operationType.ROLLING_DATE_RANGE;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "includeObjectsWithNoValueSet", n => { IncludeObjectsWithNoValueSet = n.GetBoolValue(); } },
                { "numberOfDays", n => { NumberOfDays = n.GetIntValue(); } },
                { "operationType", n => { OperationType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation_operationType>(); } },
                { "operator", n => { Operator = n.GetStringValue(); } },
                { "requiresTimeZoneConversion", n => { RequiresTimeZoneConversion = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("includeObjectsWithNoValueSet", IncludeObjectsWithNoValueSet);
            writer.WriteIntValue("numberOfDays", NumberOfDays);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRollingDateRangePropertyOperation_operationType>("operationType", OperationType);
            writer.WriteStringValue("operator", Operator);
            writer.WriteBoolValue("requiresTimeZoneConversion", RequiresTimeZoneConversion);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
