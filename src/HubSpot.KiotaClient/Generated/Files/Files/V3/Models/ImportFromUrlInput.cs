// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Files.Files.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ImportFromUrlInput : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>PUBLIC_INDEXABLE: File is publicly accessible by anyone who has the URL. Search engines can index the file. PUBLIC_NOT_INDEXABLE: File is publicly accessible by anyone who has the URL. Search engines *can&apos;t* index the file. PRIVATE: File is NOT publicly accessible. Requires a signed URL to see content. Search engines *can&apos;t* index the file.</summary>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_access? Access { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>ENTIRE_PORTAL: Look for a duplicate file in the entire account. EXACT_FOLDER: Look for a duplicate file in the provided folder.</summary>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationScope? DuplicateValidationScope { get; set; }
        /// <summary>NONE: Do not run any duplicate validation. REJECT: Reject the upload if a duplicate is found. RETURN_EXISTING: If a duplicate file is found, do not upload a new file and return the found duplicate instead.</summary>
        public global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationStrategy? DuplicateValidationStrategy { get; set; }
        /// <summary>One of folderId or folderPath is required. Destination folderId for the uploaded file.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FolderId { get; set; }
#nullable restore
#else
        public string FolderId { get; set; }
#endif
        /// <summary>One of folderPath or folderId is required. Destination folder path for the uploaded file. If the folder path does not exist, there will be an attempt to create the folder path.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FolderPath { get; set; }
#nullable restore
#else
        public string FolderPath { get; set; }
#endif
        /// <summary>Name to give the resulting file in the file manager.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>If true, will overwrite existing file if one with the same name and extension exists in the given folder. The overwritten file will be deleted and the uploaded file will take its place with a new ID. If unset or set as false, the new file&apos;s name will be updated to prevent colliding with existing file if one exists with the same path, name, and extension</summary>
        public bool? Overwrite { get; set; }
        /// <summary>Time to live. If specified the file will be deleted after the given time frame. If left unset, the file will exist indefinitely</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Ttl { get; set; }
#nullable restore
#else
        public string Ttl { get; set; }
#endif
        /// <summary>URL to download the new file from.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Url { get; set; }
#nullable restore
#else
        public string Url { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput"/> and sets the default values.
        /// </summary>
        public ImportFromUrlInput()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "access", n => { Access = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_access>(); } },
                { "duplicateValidationScope", n => { DuplicateValidationScope = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationScope>(); } },
                { "duplicateValidationStrategy", n => { DuplicateValidationStrategy = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationStrategy>(); } },
                { "folderId", n => { FolderId = n.GetStringValue(); } },
                { "folderPath", n => { FolderPath = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "overwrite", n => { Overwrite = n.GetBoolValue(); } },
                { "ttl", n => { Ttl = n.GetStringValue(); } },
                { "url", n => { Url = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_access>("access", Access);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationScope>("duplicateValidationScope", DuplicateValidationScope);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Files.Files.V3.Models.ImportFromUrlInput_duplicateValidationStrategy>("duplicateValidationStrategy", DuplicateValidationStrategy);
            writer.WriteStringValue("folderId", FolderId);
            writer.WriteStringValue("folderPath", FolderPath);
            writer.WriteStringValue("name", Name);
            writer.WriteBoolValue("overwrite", Overwrite);
            writer.WriteStringValue("ttl", Ttl);
            writer.WriteStringValue("url", Url);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618