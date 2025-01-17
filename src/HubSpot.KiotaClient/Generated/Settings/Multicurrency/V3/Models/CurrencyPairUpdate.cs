// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class CurrencyPairUpdate : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The fromCurrencyCode property</summary>
        public global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_fromCurrencyCode? FromCurrencyCode { get; set; }
        /// <summary>The toCurrencyCode property</summary>
        public global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_toCurrencyCode? ToCurrencyCode { get; set; }
        /// <summary>The visibleInUI property</summary>
        public bool? VisibleInUI { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate"/> and sets the default values.
        /// </summary>
        public CurrencyPairUpdate()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "fromCurrencyCode", n => { FromCurrencyCode = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_fromCurrencyCode>(); } },
                { "toCurrencyCode", n => { ToCurrencyCode = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_toCurrencyCode>(); } },
                { "visibleInUI", n => { VisibleInUI = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_fromCurrencyCode>("fromCurrencyCode", FromCurrencyCode);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Models.CurrencyPairUpdate_toCurrencyCode>("toCurrencyCode", ToCurrencyCode);
            writer.WriteBoolValue("visibleInUI", VisibleInUI);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
