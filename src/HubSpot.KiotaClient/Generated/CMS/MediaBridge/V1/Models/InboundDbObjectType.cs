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
    public partial class InboundDbObjectType : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The accessScopeName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AccessScopeName { get; set; }
#nullable restore
#else
        public string AccessScopeName { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The createDatePropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CreateDatePropertyName { get; set; }
#nullable restore
#else
        public string CreateDatePropertyName { get; set; }
#endif
        /// <summary>The defaultSearchPropertyNames property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? DefaultSearchPropertyNames { get; set; }
#nullable restore
#else
        public List<string> DefaultSearchPropertyNames { get; set; }
#endif
        /// <summary>The deleted property</summary>
        public bool? Deleted { get; set; }
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The fullyQualifiedName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FullyQualifiedName { get; set; }
#nullable restore
#else
        public string FullyQualifiedName { get; set; }
#endif
        /// <summary>The hasCustomProperties property</summary>
        public bool? HasCustomProperties { get; set; }
        /// <summary>The hasDefaultProperties property</summary>
        public bool? HasDefaultProperties { get; set; }
        /// <summary>The hasExternalObjectIds property</summary>
        public bool? HasExternalObjectIds { get; set; }
        /// <summary>The hasOwners property</summary>
        public bool? HasOwners { get; set; }
        /// <summary>The hasPipelines property</summary>
        public bool? HasPipelines { get; set; }
        /// <summary>The id property</summary>
        public int? Id { get; set; }
        /// <summary>The indexedForFiltersAndReports property</summary>
        public bool? IndexedForFiltersAndReports { get; set; }
        /// <summary>The integrationAppId property</summary>
        public int? IntegrationAppId { get; set; }
        /// <summary>The janusGroup property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? JanusGroup { get; set; }
#nullable restore
#else
        public string JanusGroup { get; set; }
#endif
        /// <summary>The lastModifiedPropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastModifiedPropertyName { get; set; }
#nullable restore
#else
        public string LastModifiedPropertyName { get; set; }
#endif
        /// <summary>The metaType property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType_metaType? MetaType { get; set; }
        /// <summary>The metaTypeId property</summary>
        public int? MetaTypeId { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The objectTypeId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectTypeId { get; set; }
#nullable restore
#else
        public string ObjectTypeId { get; set; }
#endif
        /// <summary>The ownerPortalId property</summary>
        public int? OwnerPortalId { get; set; }
        /// <summary>The permissioningType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PermissioningType { get; set; }
#nullable restore
#else
        public string PermissioningType { get; set; }
#endif
        /// <summary>The pipelineCloseDatePropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PipelineCloseDatePropertyName { get; set; }
#nullable restore
#else
        public string PipelineCloseDatePropertyName { get; set; }
#endif
        /// <summary>The pipelinePropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PipelinePropertyName { get; set; }
#nullable restore
#else
        public string PipelinePropertyName { get; set; }
#endif
        /// <summary>The pipelineStagePropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PipelineStagePropertyName { get; set; }
#nullable restore
#else
        public string PipelineStagePropertyName { get; set; }
#endif
        /// <summary>The pipelineTimeToClosePropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PipelineTimeToClosePropertyName { get; set; }
#nullable restore
#else
        public string PipelineTimeToClosePropertyName { get; set; }
#endif
        /// <summary>The pluralForm property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PluralForm { get; set; }
#nullable restore
#else
        public string PluralForm { get; set; }
#endif
        /// <summary>The primaryDisplayLabelPropertyName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PrimaryDisplayLabelPropertyName { get; set; }
#nullable restore
#else
        public string PrimaryDisplayLabelPropertyName { get; set; }
#endif
        /// <summary>The readScopeName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ReadScopeName { get; set; }
#nullable restore
#else
        public string ReadScopeName { get; set; }
#endif
        /// <summary>The requiredProperties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RequiredProperties { get; set; }
#nullable restore
#else
        public List<string> RequiredProperties { get; set; }
#endif
        /// <summary>The restorable property</summary>
        public bool? Restorable { get; set; }
        /// <summary>The scopeMappings property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ScopeMapping>? ScopeMappings { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ScopeMapping> ScopeMappings { get; set; }
#endif
        /// <summary>The secondaryDisplayLabelPropertyNames property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SecondaryDisplayLabelPropertyNames { get; set; }
#nullable restore
#else
        public List<string> SecondaryDisplayLabelPropertyNames { get; set; }
#endif
        /// <summary>The singularForm property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SingularForm { get; set; }
#nullable restore
#else
        public string SingularForm { get; set; }
#endif
        /// <summary>The writeScopeName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WriteScopeName { get; set; }
#nullable restore
#else
        public string WriteScopeName { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType"/> and sets the default values.
        /// </summary>
        public InboundDbObjectType()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "accessScopeName", n => { AccessScopeName = n.GetStringValue(); } },
                { "createDatePropertyName", n => { CreateDatePropertyName = n.GetStringValue(); } },
                { "defaultSearchPropertyNames", n => { DefaultSearchPropertyNames = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "deleted", n => { Deleted = n.GetBoolValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "fullyQualifiedName", n => { FullyQualifiedName = n.GetStringValue(); } },
                { "hasCustomProperties", n => { HasCustomProperties = n.GetBoolValue(); } },
                { "hasDefaultProperties", n => { HasDefaultProperties = n.GetBoolValue(); } },
                { "hasExternalObjectIds", n => { HasExternalObjectIds = n.GetBoolValue(); } },
                { "hasOwners", n => { HasOwners = n.GetBoolValue(); } },
                { "hasPipelines", n => { HasPipelines = n.GetBoolValue(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "indexedForFiltersAndReports", n => { IndexedForFiltersAndReports = n.GetBoolValue(); } },
                { "integrationAppId", n => { IntegrationAppId = n.GetIntValue(); } },
                { "janusGroup", n => { JanusGroup = n.GetStringValue(); } },
                { "lastModifiedPropertyName", n => { LastModifiedPropertyName = n.GetStringValue(); } },
                { "metaType", n => { MetaType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType_metaType>(); } },
                { "metaTypeId", n => { MetaTypeId = n.GetIntValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "objectTypeId", n => { ObjectTypeId = n.GetStringValue(); } },
                { "ownerPortalId", n => { OwnerPortalId = n.GetIntValue(); } },
                { "permissioningType", n => { PermissioningType = n.GetStringValue(); } },
                { "pipelineCloseDatePropertyName", n => { PipelineCloseDatePropertyName = n.GetStringValue(); } },
                { "pipelinePropertyName", n => { PipelinePropertyName = n.GetStringValue(); } },
                { "pipelineStagePropertyName", n => { PipelineStagePropertyName = n.GetStringValue(); } },
                { "pipelineTimeToClosePropertyName", n => { PipelineTimeToClosePropertyName = n.GetStringValue(); } },
                { "pluralForm", n => { PluralForm = n.GetStringValue(); } },
                { "primaryDisplayLabelPropertyName", n => { PrimaryDisplayLabelPropertyName = n.GetStringValue(); } },
                { "readScopeName", n => { ReadScopeName = n.GetStringValue(); } },
                { "requiredProperties", n => { RequiredProperties = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "restorable", n => { Restorable = n.GetBoolValue(); } },
                { "scopeMappings", n => { ScopeMappings = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ScopeMapping>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ScopeMapping.CreateFromDiscriminatorValue)?.AsList(); } },
                { "secondaryDisplayLabelPropertyNames", n => { SecondaryDisplayLabelPropertyNames = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "singularForm", n => { SingularForm = n.GetStringValue(); } },
                { "writeScopeName", n => { WriteScopeName = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("accessScopeName", AccessScopeName);
            writer.WriteStringValue("createDatePropertyName", CreateDatePropertyName);
            writer.WriteCollectionOfPrimitiveValues<string>("defaultSearchPropertyNames", DefaultSearchPropertyNames);
            writer.WriteBoolValue("deleted", Deleted);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("fullyQualifiedName", FullyQualifiedName);
            writer.WriteBoolValue("hasCustomProperties", HasCustomProperties);
            writer.WriteBoolValue("hasDefaultProperties", HasDefaultProperties);
            writer.WriteBoolValue("hasExternalObjectIds", HasExternalObjectIds);
            writer.WriteBoolValue("hasOwners", HasOwners);
            writer.WriteBoolValue("hasPipelines", HasPipelines);
            writer.WriteIntValue("id", Id);
            writer.WriteBoolValue("indexedForFiltersAndReports", IndexedForFiltersAndReports);
            writer.WriteIntValue("integrationAppId", IntegrationAppId);
            writer.WriteStringValue("janusGroup", JanusGroup);
            writer.WriteStringValue("lastModifiedPropertyName", LastModifiedPropertyName);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.InboundDbObjectType_metaType>("metaType", MetaType);
            writer.WriteIntValue("metaTypeId", MetaTypeId);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("objectTypeId", ObjectTypeId);
            writer.WriteIntValue("ownerPortalId", OwnerPortalId);
            writer.WriteStringValue("permissioningType", PermissioningType);
            writer.WriteStringValue("pipelineCloseDatePropertyName", PipelineCloseDatePropertyName);
            writer.WriteStringValue("pipelinePropertyName", PipelinePropertyName);
            writer.WriteStringValue("pipelineStagePropertyName", PipelineStagePropertyName);
            writer.WriteStringValue("pipelineTimeToClosePropertyName", PipelineTimeToClosePropertyName);
            writer.WriteStringValue("pluralForm", PluralForm);
            writer.WriteStringValue("primaryDisplayLabelPropertyName", PrimaryDisplayLabelPropertyName);
            writer.WriteStringValue("readScopeName", ReadScopeName);
            writer.WriteCollectionOfPrimitiveValues<string>("requiredProperties", RequiredProperties);
            writer.WriteBoolValue("restorable", Restorable);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ScopeMapping>("scopeMappings", ScopeMappings);
            writer.WriteCollectionOfPrimitiveValues<string>("secondaryDisplayLabelPropertyNames", SecondaryDisplayLabelPropertyNames);
            writer.WriteStringValue("singularForm", SingularForm);
            writer.WriteStringValue("writeScopeName", WriteScopeName);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
