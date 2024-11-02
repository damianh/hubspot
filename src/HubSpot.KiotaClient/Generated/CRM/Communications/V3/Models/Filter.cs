// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Filter : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The highValue property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HighValue { get; set; }
#nullable restore
#else
        public string HighValue { get; set; }
#endif
        /// <summary>null</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter_operator? Operator { get; set; }
        /// <summary>The propertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PropertyName { get; set; }
#nullable restore
#else
        public string PropertyName { get; set; }
#endif
        /// <summary>The value property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Value { get; set; }
#nullable restore
#else
        public string Value { get; set; }
#endif
        /// <summary>The values property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Values { get; set; }
#nullable restore
#else
        public List<string> Values { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter"/> and sets the default values.
        /// </summary>
        public Filter()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "highValue", n => { HighValue = n.GetStringValue(); } },
                { "operator", n => { Operator = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter_operator>(); } },
                { "propertyName", n => { PropertyName = n.GetStringValue(); } },
                { "value", n => { Value = n.GetStringValue(); } },
                { "values", n => { Values = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("highValue", HighValue);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Communications.V3.Models.Filter_operator>("operator", Operator);
            writer.WriteStringValue("propertyName", PropertyName);
            writer.WriteStringValue("value", Value);
            writer.WriteCollectionOfPrimitiveValues<string>("values", Values);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
