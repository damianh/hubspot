// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models
{
    /// <summary>
    /// The ID of a send event.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class EventIdView : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Time of event creation.</summary>
        public DateTimeOffset? Created { get; set; }
        /// <summary>Identifier of event.</summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.EventIdView"/> and sets the default values.
        /// </summary>
        public EventIdView()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.EventIdView"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.EventIdView CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.EventIdView();
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
                { "id", n => { Id = n.GetGuidValue(); } },
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
            writer.WriteGuidValue("id", Id);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
