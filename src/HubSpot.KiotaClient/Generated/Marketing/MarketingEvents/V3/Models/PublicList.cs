// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicList : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The createdById property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CreatedById { get; set; }
#nullable restore
#else
        public string CreatedById { get; set; }
#endif
        /// <summary>The deletedAt property</summary>
        public DateTimeOffset? DeletedAt { get; set; }
        /// <summary>The filtersUpdatedAt property</summary>
        public DateTimeOffset? FiltersUpdatedAt { get; set; }
        /// <summary>The listId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ListId { get; set; }
#nullable restore
#else
        public string ListId { get; set; }
#endif
        /// <summary>The listVersion property</summary>
        public int? ListVersion { get; set; }
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
        /// <summary>The processingStatus property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProcessingStatus { get; set; }
#nullable restore
#else
        public string ProcessingStatus { get; set; }
#endif
        /// <summary>The processingType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProcessingType { get; set; }
#nullable restore
#else
        public string ProcessingType { get; set; }
#endif
        /// <summary>The size property</summary>
        public long? Size { get; set; }
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The updatedById property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UpdatedById { get; set; }
#nullable restore
#else
        public string UpdatedById { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PublicList"/> and sets the default values.
        /// </summary>
        public PublicList()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PublicList"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PublicList CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PublicList();
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
                { "createdById", n => { CreatedById = n.GetStringValue(); } },
                { "deletedAt", n => { DeletedAt = n.GetDateTimeOffsetValue(); } },
                { "filtersUpdatedAt", n => { FiltersUpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "listId", n => { ListId = n.GetStringValue(); } },
                { "listVersion", n => { ListVersion = n.GetIntValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "objectTypeId", n => { ObjectTypeId = n.GetStringValue(); } },
                { "processingStatus", n => { ProcessingStatus = n.GetStringValue(); } },
                { "processingType", n => { ProcessingType = n.GetStringValue(); } },
                { "size", n => { Size = n.GetLongValue(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "updatedById", n => { UpdatedById = n.GetStringValue(); } },
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
            writer.WriteStringValue("createdById", CreatedById);
            writer.WriteDateTimeOffsetValue("deletedAt", DeletedAt);
            writer.WriteDateTimeOffsetValue("filtersUpdatedAt", FiltersUpdatedAt);
            writer.WriteStringValue("listId", ListId);
            writer.WriteIntValue("listVersion", ListVersion);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("objectTypeId", ObjectTypeId);
            writer.WriteStringValue("processingStatus", ProcessingStatus);
            writer.WriteStringValue("processingType", ProcessingType);
            writer.WriteLongValue("size", Size);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteStringValue("updatedById", UpdatedById);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
