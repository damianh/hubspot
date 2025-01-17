// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicListFolder : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>An array of list Id&apos;s contained in this folder.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<int?>? ChildLists { get; set; }
#nullable restore
#else
        public List<int?> ChildLists { get; set; }
#endif
        /// <summary>The childNodes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder>? ChildNodes { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder> ChildNodes { get; set; }
#endif
        /// <summary>The time the folder was created at.</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The Id of the folder.</summary>
        public int? Id { get; set; }
        /// <summary>The name of the folder.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The Id of the folder this folder is in, the root folder is represented as 0.</summary>
        public int? ParentFolderId { get; set; }
        /// <summary>The time the folder was last updated at.</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The time that the contents of the folder was last updated at.</summary>
        public DateTimeOffset? UpdatedContentsAt { get; set; }
        /// <summary>The user Id of the owner of the folder.</summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder"/> and sets the default values.
        /// </summary>
        public PublicListFolder()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "childLists", n => { ChildLists = n.GetCollectionOfPrimitiveValues<int?>()?.AsList(); } },
                { "childNodes", n => { ChildNodes = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder>(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder.CreateFromDiscriminatorValue)?.AsList(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "parentFolderId", n => { ParentFolderId = n.GetIntValue(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "updatedContentsAt", n => { UpdatedContentsAt = n.GetDateTimeOffsetValue(); } },
                { "userId", n => { UserId = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<int?>("childLists", ChildLists);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicListFolder>("childNodes", ChildNodes);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteIntValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteIntValue("parentFolderId", ParentFolderId);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteDateTimeOffsetValue("updatedContentsAt", UpdatedContentsAt);
            writer.WriteIntValue("userId", UserId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
