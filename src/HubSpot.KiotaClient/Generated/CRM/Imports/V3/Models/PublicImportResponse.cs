// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicImportResponse : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The importName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ImportName { get; set; }
#nullable restore
#else
        public string ImportName { get; set; }
#endif
        /// <summary>The importRequestJson property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importRequestJson? ImportRequestJson { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importRequestJson ImportRequestJson { get; set; }
#endif
        /// <summary>The importSource property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importSource? ImportSource { get; set; }
        /// <summary>The importTemplate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.ImportTemplate? ImportTemplate { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.ImportTemplate ImportTemplate { get; set; }
#endif
        /// <summary>The mappedObjectTypeIds property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? MappedObjectTypeIds { get; set; }
#nullable restore
#else
        public List<string> MappedObjectTypeIds { get; set; }
#endif
        /// <summary>The metadata property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata? Metadata { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata Metadata { get; set; }
#endif
        /// <summary>Whether or not the import is a list of people disqualified from receiving emails.</summary>
        public bool? OptOutImport { get; set; }
        /// <summary>The status of the import.</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_state? State { get; set; }
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse"/> and sets the default values.
        /// </summary>
        public PublicImportResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "importName", n => { ImportName = n.GetStringValue(); } },
                { "importRequestJson", n => { ImportRequestJson = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importRequestJson>(global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importRequestJson.CreateFromDiscriminatorValue); } },
                { "importSource", n => { ImportSource = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importSource>(); } },
                { "importTemplate", n => { ImportTemplate = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.ImportTemplate>(global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.ImportTemplate.CreateFromDiscriminatorValue); } },
                { "mappedObjectTypeIds", n => { MappedObjectTypeIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "metadata", n => { Metadata = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata>(global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata.CreateFromDiscriminatorValue); } },
                { "optOutImport", n => { OptOutImport = n.GetBoolValue(); } },
                { "state", n => { State = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_state>(); } },
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
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("importName", ImportName);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importRequestJson>("importRequestJson", ImportRequestJson);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_importSource>("importSource", ImportSource);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.ImportTemplate>("importTemplate", ImportTemplate);
            writer.WriteCollectionOfPrimitiveValues<string>("mappedObjectTypeIds", MappedObjectTypeIds);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata>("metadata", Metadata);
            writer.WriteBoolValue("optOutImport", OptOutImport);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportResponse_state>("state", State);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
