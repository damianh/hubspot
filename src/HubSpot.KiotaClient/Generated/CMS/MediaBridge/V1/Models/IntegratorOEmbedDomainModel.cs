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
    public partial class IntegratorOEmbedDomainModel : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The appId property</summary>
        public int? AppId { get; set; }
        /// <summary>The createdAt property</summary>
        public long? CreatedAt { get; set; }
        /// <summary>The deletedAt property</summary>
        public long? DeletedAt { get; set; }
        /// <summary>The endpoints property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Endpoints? Endpoints { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Endpoints Endpoints { get; set; }
#endif
        /// <summary>The id property</summary>
        public long? Id { get; set; }
        /// <summary>The portalId property</summary>
        public int? PortalId { get; set; }
        /// <summary>The updatedAt property</summary>
        public long? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorOEmbedDomainModel"/> and sets the default values.
        /// </summary>
        public IntegratorOEmbedDomainModel()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorOEmbedDomainModel"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorOEmbedDomainModel CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.IntegratorOEmbedDomainModel();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "appId", n => { AppId = n.GetIntValue(); } },
                { "createdAt", n => { CreatedAt = n.GetLongValue(); } },
                { "deletedAt", n => { DeletedAt = n.GetLongValue(); } },
                { "endpoints", n => { Endpoints = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Endpoints>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Endpoints.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetLongValue(); } },
                { "portalId", n => { PortalId = n.GetIntValue(); } },
                { "updatedAt", n => { UpdatedAt = n.GetLongValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("appId", AppId);
            writer.WriteLongValue("createdAt", CreatedAt);
            writer.WriteLongValue("deletedAt", DeletedAt);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.Endpoints>("endpoints", Endpoints);
            writer.WriteLongValue("id", Id);
            writer.WriteIntValue("portalId", PortalId);
            writer.WriteLongValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
