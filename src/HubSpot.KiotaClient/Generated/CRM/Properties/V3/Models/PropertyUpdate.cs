// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PropertyUpdate : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Represents a formula that is used to compute a calculated property.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CalculationFormula { get; set; }
#nullable restore
#else
        public string CalculationFormula { get; set; }
#endif
        /// <summary>A description of the property that will be shown as help text in HubSpot.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>Properties are displayed in order starting with the lowest positive integer value. Values of -1 will cause the Property to be displayed after any positive values.</summary>
        public int? DisplayOrder { get; set; }
        /// <summary>Controls how the property appears in HubSpot.</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_fieldType? FieldType { get; set; }
        /// <summary>Whether or not the property can be used in a HubSpot form.</summary>
        public bool? FormField { get; set; }
        /// <summary>The name of the property group the property belongs to.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? GroupName { get; set; }
#nullable restore
#else
        public string GroupName { get; set; }
#endif
        /// <summary>If true, the property won&apos;t be visible and can&apos;t be used in HubSpot.</summary>
        public bool? Hidden { get; set; }
        /// <summary>A human-readable property label that will be shown in HubSpot.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>A list of valid options for the property.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.OptionInput>? Options { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.OptionInput> Options { get; set; }
#endif
        /// <summary>The data type of the property.</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_type? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate"/> and sets the default values.
        /// </summary>
        public PropertyUpdate()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "calculationFormula", n => { CalculationFormula = n.GetStringValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayOrder", n => { DisplayOrder = n.GetIntValue(); } },
                { "fieldType", n => { FieldType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_fieldType>(); } },
                { "formField", n => { FormField = n.GetBoolValue(); } },
                { "groupName", n => { GroupName = n.GetStringValue(); } },
                { "hidden", n => { Hidden = n.GetBoolValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "options", n => { Options = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.OptionInput>(global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.OptionInput.CreateFromDiscriminatorValue)?.AsList(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_type>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("calculationFormula", CalculationFormula);
            writer.WriteStringValue("description", Description);
            writer.WriteIntValue("displayOrder", DisplayOrder);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_fieldType>("fieldType", FieldType);
            writer.WriteBoolValue("formField", FormField);
            writer.WriteStringValue("groupName", GroupName);
            writer.WriteBoolValue("hidden", Hidden);
            writer.WriteStringValue("label", Label);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.OptionInput>("options", Options);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Properties.V3.Models.PropertyUpdate_type>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
