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
    public partial class AssociationDefinition : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The fromObjectTypeId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FromObjectTypeId { get; set; }
#nullable restore
#else
        public string FromObjectTypeId { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The toObjectTypeId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ToObjectTypeId { get; set; }
#nullable restore
#else
        public string ToObjectTypeId { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.AssociationDefinition"/> and sets the default values.
        /// </summary>
        public AssociationDefinition()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.AssociationDefinition"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.AssociationDefinition CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.AssociationDefinition();
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
                { "fromObjectTypeId", n => { FromObjectTypeId = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "toObjectTypeId", n => { ToObjectTypeId = n.GetStringValue(); } },
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
            writer.WriteStringValue("fromObjectTypeId", FromObjectTypeId);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("toObjectTypeId", ToObjectTypeId);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
