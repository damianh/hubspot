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
    public partial class PublicRelativeComparativeTimestampRefineBy : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The comparison property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Comparison { get; set; }
#nullable restore
#else
        public string Comparison { get; set; }
#endif
        /// <summary>The timeOffset property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimeOffset? TimeOffset { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimeOffset TimeOffset { get; set; }
#endif
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy_type? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy"/> and sets the default values.
        /// </summary>
        public PublicRelativeComparativeTimestampRefineBy()
        {
            AdditionalData = new Dictionary<string, object>();
            Type = global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy_type.RELATIVE_COMPARATIVE;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "comparison", n => { Comparison = n.GetStringValue(); } },
                { "timeOffset", n => { TimeOffset = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimeOffset>(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimeOffset.CreateFromDiscriminatorValue); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy_type>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("comparison", Comparison);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimeOffset>("timeOffset", TimeOffset);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicRelativeComparativeTimestampRefineBy_type>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
