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
    public partial class CollectionResponseObjectSchemaNoPaging : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The results property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectSchema>? Results { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectSchema> Results { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponseObjectSchemaNoPaging"/> and sets the default values.
        /// </summary>
        public CollectionResponseObjectSchemaNoPaging()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponseObjectSchemaNoPaging"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponseObjectSchemaNoPaging CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.CollectionResponseObjectSchemaNoPaging();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "results", n => { Results = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectSchema>(global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectSchema.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.MediaBridge.V1.Models.ObjectSchema>("results", Results);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
