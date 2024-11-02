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
    public partial class RangedNumberPropertyOperation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The defaultValue property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DefaultValue { get; set; }
#nullable restore
#else
        public string DefaultValue { get; set; }
#endif
        /// <summary>The includeObjectsWithNoValueSet property</summary>
        public bool? IncludeObjectsWithNoValueSet { get; set; }
        /// <summary>The lowerBound property</summary>
        public int? LowerBound { get; set; }
        /// <summary>The operationType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OperationType { get; set; }
#nullable restore
#else
        public string OperationType { get; set; }
#endif
        /// <summary>The operator property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_operator? Operator { get; set; }
        /// <summary>The operatorName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OperatorName { get; set; }
#nullable restore
#else
        public string OperatorName { get; set; }
#endif
        /// <summary>The propertyType property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_propertyType? PropertyType { get; set; }
        /// <summary>The upperBound property</summary>
        public int? UpperBound { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation"/> and sets the default values.
        /// </summary>
        public RangedNumberPropertyOperation()
        {
            AdditionalData = new Dictionary<string, object>();
            PropertyType = global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_propertyType.NumberRanged;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "defaultValue", n => { DefaultValue = n.GetStringValue(); } },
                { "includeObjectsWithNoValueSet", n => { IncludeObjectsWithNoValueSet = n.GetBoolValue(); } },
                { "lowerBound", n => { LowerBound = n.GetIntValue(); } },
                { "operationType", n => { OperationType = n.GetStringValue(); } },
                { "operator", n => { Operator = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_operator>(); } },
                { "operatorName", n => { OperatorName = n.GetStringValue(); } },
                { "propertyType", n => { PropertyType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_propertyType>(); } },
                { "upperBound", n => { UpperBound = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("defaultValue", DefaultValue);
            writer.WriteBoolValue("includeObjectsWithNoValueSet", IncludeObjectsWithNoValueSet);
            writer.WriteIntValue("lowerBound", LowerBound);
            writer.WriteStringValue("operationType", OperationType);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_operator>("operator", Operator);
            writer.WriteStringValue("operatorName", OperatorName);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RangedNumberPropertyOperation_propertyType>("propertyType", PropertyType);
            writer.WriteIntValue("upperBound", UpperBound);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
