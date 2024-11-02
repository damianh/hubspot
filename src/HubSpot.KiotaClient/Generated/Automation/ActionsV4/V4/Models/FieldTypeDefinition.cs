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
    public partial class FieldTypeDefinition : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The externalOptions property</summary>
        public bool? ExternalOptions { get; set; }
        /// <summary>The externalOptionsReferenceType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ExternalOptionsReferenceType { get; set; }
#nullable restore
#else
        public string ExternalOptionsReferenceType { get; set; }
#endif
        /// <summary>The fieldType property</summary>
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_fieldType? FieldType { get; set; }
        /// <summary>The helpText property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HelpText { get; set; }
#nullable restore
#else
        public string HelpText { get; set; }
#endif
        /// <summary>The label property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The options property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.Option>? Options { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.Option> Options { get; set; }
#endif
        /// <summary>The optionsUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OptionsUrl { get; set; }
#nullable restore
#else
        public string OptionsUrl { get; set; }
#endif
        /// <summary>The referencedObjectType property</summary>
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_referencedObjectType? ReferencedObjectType { get; set; }
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_type? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition"/> and sets the default values.
        /// </summary>
        public FieldTypeDefinition()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "description", n => { Description = n.GetStringValue(); } },
                { "externalOptions", n => { ExternalOptions = n.GetBoolValue(); } },
                { "externalOptionsReferenceType", n => { ExternalOptionsReferenceType = n.GetStringValue(); } },
                { "fieldType", n => { FieldType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_fieldType>(); } },
                { "helpText", n => { HelpText = n.GetStringValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "options", n => { Options = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.Option>(global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.Option.CreateFromDiscriminatorValue)?.AsList(); } },
                { "optionsUrl", n => { OptionsUrl = n.GetStringValue(); } },
                { "referencedObjectType", n => { ReferencedObjectType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_referencedObjectType>(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_type>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("description", Description);
            writer.WriteBoolValue("externalOptions", ExternalOptions);
            writer.WriteStringValue("externalOptionsReferenceType", ExternalOptionsReferenceType);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_fieldType>("fieldType", FieldType);
            writer.WriteStringValue("helpText", HelpText);
            writer.WriteStringValue("label", Label);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.Option>("options", Options);
            writer.WriteStringValue("optionsUrl", OptionsUrl);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_referencedObjectType>("referencedObjectType", ReferencedObjectType);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Automation.ActionsV4.V4.Models.FieldTypeDefinition_type>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
