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
    public partial class ObjectTypeDefinition : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The archived property</summary>
        public bool? Archived { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
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
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The labels property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinitionLabels? Labels { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinitionLabels Labels { get; set; }
#endif
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
        /// <summary>The portalId property</summary>
        public int? PortalId { get; set; }
        /// <summary>The primaryDisplayProperty property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PrimaryDisplayProperty { get; set; }
#nullable restore
#else
        public string PrimaryDisplayProperty { get; set; }
#endif
        /// <summary>The requiredProperties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RequiredProperties { get; set; }
#nullable restore
#else
        public List<string> RequiredProperties { get; set; }
#endif
        /// <summary>The searchableProperties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SearchableProperties { get; set; }
#nullable restore
#else
        public List<string> SearchableProperties { get; set; }
#endif
        /// <summary>The secondaryDisplayProperties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? SecondaryDisplayProperties { get; set; }
#nullable restore
#else
        public List<string> SecondaryDisplayProperties { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinition"/> and sets the default values.
        /// </summary>
        public ObjectTypeDefinition()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinition"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinition CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinition();
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
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "fullyQualifiedName", n => { FullyQualifiedName = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "labels", n => { Labels = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinitionLabels>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinitionLabels.CreateFromDiscriminatorValue); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "objectTypeId", n => { ObjectTypeId = n.GetStringValue(); } },
                { "portalId", n => { PortalId = n.GetIntValue(); } },
                { "primaryDisplayProperty", n => { PrimaryDisplayProperty = n.GetStringValue(); } },
                { "requiredProperties", n => { RequiredProperties = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "searchableProperties", n => { SearchableProperties = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "secondaryDisplayProperties", n => { SecondaryDisplayProperties = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
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
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("fullyQualifiedName", FullyQualifiedName);
            writer.WriteStringValue("id", Id);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectTypeDefinitionLabels>("labels", Labels);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("objectTypeId", ObjectTypeId);
            writer.WriteIntValue("portalId", PortalId);
            writer.WriteStringValue("primaryDisplayProperty", PrimaryDisplayProperty);
            writer.WriteCollectionOfPrimitiveValues<string>("requiredProperties", RequiredProperties);
            writer.WriteCollectionOfPrimitiveValues<string>("searchableProperties", SearchableProperties);
            writer.WriteCollectionOfPrimitiveValues<string>("secondaryDisplayProperties", SecondaryDisplayProperties);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
