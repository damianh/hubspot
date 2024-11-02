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
    public partial class RelativeRangedTimestampRefineBy : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The lowerBoundOffset property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset? LowerBoundOffset { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset LowerBoundOffset { get; set; }
#endif
        /// <summary>The rangeType property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_rangeType? RangeType { get; set; }
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_type? Type { get; set; }
        /// <summary>The upperBoundOffset property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset? UpperBoundOffset { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset UpperBoundOffset { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy"/> and sets the default values.
        /// </summary>
        public RelativeRangedTimestampRefineBy()
        {
            AdditionalData = new Dictionary<string, object>();
            Type = global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_type.RelativeRangedTimestampRefineBy;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "lowerBoundOffset", n => { LowerBoundOffset = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset>(global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset.CreateFromDiscriminatorValue); } },
                { "rangeType", n => { RangeType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_rangeType>(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_type>(); } },
                { "upperBoundOffset", n => { UpperBoundOffset = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset>(global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset>("lowerBoundOffset", LowerBoundOffset);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_rangeType>("rangeType", RangeType);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.RelativeRangedTimestampRefineBy_type>("type", Type);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Events.ManageEventDefinitions.V3.Models.TimeOffset>("upperBoundOffset", UpperBoundOffset);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
