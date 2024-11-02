// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class MetricsCounters : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The influencedContacts property</summary>
        public int? InfluencedContacts { get; set; }
        /// <summary>The newContactsFirstTouch property</summary>
        public int? NewContactsFirstTouch { get; set; }
        /// <summary>The newContactsLastTouch property</summary>
        public int? NewContactsLastTouch { get; set; }
        /// <summary>The sessions property</summary>
        public int? Sessions { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters"/> and sets the default values.
        /// </summary>
        public MetricsCounters()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.MetricsCounters();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "influencedContacts", n => { InfluencedContacts = n.GetIntValue(); } },
                { "newContactsFirstTouch", n => { NewContactsFirstTouch = n.GetIntValue(); } },
                { "newContactsLastTouch", n => { NewContactsLastTouch = n.GetIntValue(); } },
                { "sessions", n => { Sessions = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("influencedContacts", InfluencedContacts);
            writer.WriteIntValue("newContactsFirstTouch", NewContactsFirstTouch);
            writer.WriteIntValue("newContactsLastTouch", NewContactsLastTouch);
            writer.WriteIntValue("sessions", Sessions);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618