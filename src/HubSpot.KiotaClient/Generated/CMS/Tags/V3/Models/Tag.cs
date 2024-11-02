// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models
{
    /// <summary>
    /// Model definition for a Tag.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class Tag : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The created property</summary>
        public DateTimeOffset? Created { get; set; }
        /// <summary>The timestamp (ISO8601 format) when this Blog Tag was deleted.</summary>
        public DateTimeOffset? DeletedAt { get; set; }
        /// <summary>The unique ID of the Blog Tag.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The explicitly defined ISO 639 language code of the tag.</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag_language? Language { get; set; }
        /// <summary>The name of the tag.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>ID of the primary tag this object was translated from.</summary>
        public long? TranslatedFromId { get; set; }
        /// <summary>The updated property</summary>
        public DateTimeOffset? Updated { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag"/> and sets the default values.
        /// </summary>
        public Tag()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "created", n => { Created = n.GetDateTimeOffsetValue(); } },
                { "deletedAt", n => { DeletedAt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "language", n => { Language = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag_language>(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "translatedFromId", n => { TranslatedFromId = n.GetLongValue(); } },
                { "updated", n => { Updated = n.GetDateTimeOffsetValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteDateTimeOffsetValue("created", Created);
            writer.WriteDateTimeOffsetValue("deletedAt", DeletedAt);
            writer.WriteStringValue("id", Id);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Tags.V3.Models.Tag_language>("language", Language);
            writer.WriteStringValue("name", Name);
            writer.WriteLongValue("translatedFromId", TranslatedFromId);
            writer.WriteDateTimeOffsetValue("updated", Updated);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618