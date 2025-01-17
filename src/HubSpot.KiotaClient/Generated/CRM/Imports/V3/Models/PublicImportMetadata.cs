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
    public partial class PublicImportMetadata : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Summarized outcomes of each row a developer attempted to import into HubSpot.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata_counters? Counters { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata_counters Counters { get; set; }
#endif
        /// <summary>The IDs of files uploaded in the File Manager API.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? FileIds { get; set; }
#nullable restore
#else
        public List<string> FileIds { get; set; }
#endif
        /// <summary>The lists containing the imported objects.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicObjectListRecord>? ObjectLists { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicObjectListRecord> ObjectLists { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata"/> and sets the default values.
        /// </summary>
        public PublicImportMetadata()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "counters", n => { Counters = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata_counters>(global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata_counters.CreateFromDiscriminatorValue); } },
                { "fileIds", n => { FileIds = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "objectLists", n => { ObjectLists = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicObjectListRecord>(global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicObjectListRecord.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicImportMetadata_counters>("counters", Counters);
            writer.WriteCollectionOfPrimitiveValues<string>("fileIds", FileIds);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PublicObjectListRecord>("objectLists", ObjectLists);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
