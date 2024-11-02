// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class IFrameActionBody : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The height property</summary>
        public int? Height { get; set; }
        /// <summary>The label property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>The propertyNamesIncluded property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? PropertyNamesIncluded { get; set; }
#nullable restore
#else
        public List<string> PropertyNamesIncluded { get; set; }
#endif
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody_type? Type { get; set; }
        /// <summary>The url property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>The width property</summary>
        public int? Width { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody"/> and sets the default values.
        /// </summary>
        public IFrameActionBody()
        {
            AdditionalData = new Dictionary<string, object>();
            Type = global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody_type.IFRAME;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "height", n => { Height = n.GetIntValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "propertyNamesIncluded", n => { PropertyNamesIncluded = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody_type>(); } },
                { "url", n => { Url = n.GetStringValue(); } },
                { "width", n => { Width = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("height", Height);
            writer.WriteStringValue("label", Label);
            writer.WriteCollectionOfPrimitiveValues<string>("propertyNamesIncluded", PropertyNamesIncluded);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IFrameActionBody_type>("type", Type);
            writer.WriteStringValue("url", Url);
            writer.WriteIntValue("width", Width);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618