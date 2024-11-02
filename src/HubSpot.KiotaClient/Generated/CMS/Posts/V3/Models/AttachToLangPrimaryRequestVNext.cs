// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models
{
    /// <summary>
    /// Request body object for attaching objects to multi-language groups.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AttachToLangPrimaryRequestVNext : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>ID of the object to add to a multi-language group.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>Designated language of the object to add to a multi-language group.</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext_language? Language { get; set; }
        /// <summary>ID of primary language object in multi-language group.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PrimaryId { get; set; }
#nullable restore
#else
        public string PrimaryId { get; set; }
#endif
        /// <summary>Primary language of the multi-language group.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PrimaryLanguage { get; set; }
#nullable restore
#else
        public string PrimaryLanguage { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext"/> and sets the default values.
        /// </summary>
        public AttachToLangPrimaryRequestVNext()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "id", n => { Id = n.GetStringValue(); } },
                { "language", n => { Language = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext_language>(); } },
                { "primaryId", n => { PrimaryId = n.GetStringValue(); } },
                { "primaryLanguage", n => { PrimaryLanguage = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("id", Id);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.AttachToLangPrimaryRequestVNext_language>("language", Language);
            writer.WriteStringValue("primaryId", PrimaryId);
            writer.WriteStringValue("primaryLanguage", PrimaryLanguage);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
