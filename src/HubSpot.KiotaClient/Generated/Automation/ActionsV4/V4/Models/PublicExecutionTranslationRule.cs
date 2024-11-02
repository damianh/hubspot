// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicExecutionTranslationRule : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The conditions property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule_conditions? Conditions { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule_conditions Conditions { get; set; }
#endif
        /// <summary>The labelName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LabelName { get; set; }
#nullable restore
#else
        public string LabelName { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule"/> and sets the default values.
        /// </summary>
        public PublicExecutionTranslationRule()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "conditions", n => { Conditions = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule_conditions>(global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule_conditions.CreateFromDiscriminatorValue); } },
                { "labelName", n => { LabelName = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.PublicExecutionTranslationRule_conditions>("conditions", Conditions);
            writer.WriteStringValue("labelName", LabelName);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
