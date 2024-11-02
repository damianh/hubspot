// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Property_1 : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The archived property</summary>
        public bool? Archived { get; set; }
        /// <summary>The archivedAt property</summary>
        public DateTimeOffset? ArchivedAt { get; set; }
        /// <summary>The calculated property</summary>
        public bool? Calculated { get; set; }
        /// <summary>The calculationFormula property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CalculationFormula { get; set; }
#nullable restore
#else
        public string CalculationFormula { get; set; }
#endif
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The createdUserId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CreatedUserId { get; set; }
#nullable restore
#else
        public string CreatedUserId { get; set; }
#endif
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The displayOrder property</summary>
        public int? DisplayOrder { get; set; }
        /// <summary>The externalOptions property</summary>
        public bool? ExternalOptions { get; set; }
        /// <summary>The fieldType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FieldType { get; set; }
#nullable restore
#else
        public string FieldType { get; set; }
#endif
        /// <summary>The formField property</summary>
        public bool? FormField { get; set; }
        /// <summary>The groupName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? GroupName { get; set; }
#nullable restore
#else
        public string GroupName { get; set; }
#endif
        /// <summary>The hasUniqueValue property</summary>
        public bool? HasUniqueValue { get; set; }
        /// <summary>The hidden property</summary>
        public bool? Hidden { get; set; }
        /// <summary>The hubspotDefined property</summary>
        public bool? HubspotDefined { get; set; }
        /// <summary>The label property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>The modificationMetadata property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyModificationMetadata? ModificationMetadata { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyModificationMetadata ModificationMetadata { get; set; }
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
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Option_1>? Options { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Option_1> Options { get; set; }
#endif
        /// <summary>The referencedObjectType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReferencedObjectType { get; set; }
#nullable restore
#else
        public string ReferencedObjectType { get; set; }
#endif
        /// <summary>The showCurrencySymbol property</summary>
        public bool? ShowCurrencySymbol { get; set; }
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The updatedUserId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UpdatedUserId { get; set; }
#nullable restore
#else
        public string UpdatedUserId { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property_1"/> and sets the default values.
        /// </summary>
        public Property_1()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property_1"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property_1 CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Property_1();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "archived", n => { Archived = n.GetBoolValue(); } },
                { "archivedAt", n => { ArchivedAt = n.GetDateTimeOffsetValue(); } },
                { "calculated", n => { Calculated = n.GetBoolValue(); } },
                { "calculationFormula", n => { CalculationFormula = n.GetStringValue(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "createdUserId", n => { CreatedUserId = n.GetStringValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayOrder", n => { DisplayOrder = n.GetIntValue(); } },
                { "externalOptions", n => { ExternalOptions = n.GetBoolValue(); } },
                { "fieldType", n => { FieldType = n.GetStringValue(); } },
                { "formField", n => { FormField = n.GetBoolValue(); } },
                { "groupName", n => { GroupName = n.GetStringValue(); } },
                { "hasUniqueValue", n => { HasUniqueValue = n.GetBoolValue(); } },
                { "hidden", n => { Hidden = n.GetBoolValue(); } },
                { "hubspotDefined", n => { HubspotDefined = n.GetBoolValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "modificationMetadata", n => { ModificationMetadata = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyModificationMetadata>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyModificationMetadata.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "options", n => { Options = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Option_1>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Option_1.CreateFromDiscriminatorValue)?.AsList(); } },
                { "referencedObjectType", n => { ReferencedObjectType = n.GetStringValue(); } },
                { "showCurrencySymbol", n => { ShowCurrencySymbol = n.GetBoolValue(); } },
                { "type", n => { Type = n.GetStringValue(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "updatedUserId", n => { UpdatedUserId = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("archived", Archived);
            writer.WriteDateTimeOffsetValue("archivedAt", ArchivedAt);
            writer.WriteBoolValue("calculated", Calculated);
            writer.WriteStringValue("calculationFormula", CalculationFormula);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("createdUserId", CreatedUserId);
            writer.WriteStringValue("description", Description);
            writer.WriteIntValue("displayOrder", DisplayOrder);
            writer.WriteBoolValue("externalOptions", ExternalOptions);
            writer.WriteStringValue("fieldType", FieldType);
            writer.WriteBoolValue("formField", FormField);
            writer.WriteStringValue("groupName", GroupName);
            writer.WriteBoolValue("hasUniqueValue", HasUniqueValue);
            writer.WriteBoolValue("hidden", Hidden);
            writer.WriteBoolValue("hubspotDefined", HubspotDefined);
            writer.WriteStringValue("label", Label);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.PropertyModificationMetadata>("modificationMetadata", ModificationMetadata);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Option_1>("options", Options);
            writer.WriteStringValue("referencedObjectType", ReferencedObjectType);
            writer.WriteBoolValue("showCurrencySymbol", ShowCurrencySymbol);
            writer.WriteStringValue("type", Type);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteStringValue("updatedUserId", UpdatedUserId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618