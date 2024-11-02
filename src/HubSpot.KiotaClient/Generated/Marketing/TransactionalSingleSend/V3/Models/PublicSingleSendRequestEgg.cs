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
    /// A request to send a single transactional email asynchronously.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PublicSingleSendRequestEgg : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The contactProperties field is a map of contact property values. Each contact property value contains a name and value property. Each property will get set on the contact record and will be visible in the template under {{ contact.NAME }}. Use these properties when you want to set a contact property while you’re sending the email. For example, when sending a reciept you may want to set a last_paid_date property, as the sending of the receipt will have information about the last payment.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_contactProperties? ContactProperties { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_contactProperties ContactProperties { get; set; }
#endif
        /// <summary>The customProperties field is a map of property values. Each property value contains a name and value property. Each property will be visible in the template under {{ custom.NAME }}.Note: Custom properties do not currently support arrays. To provide a listing in an email, one workaround is to build an HTML list (either with tables or ul) and specify it as a custom property.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_customProperties? CustomProperties { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_customProperties CustomProperties { get; set; }
#endif
        /// <summary>The content ID for the transactional email, which can be found in email tool UI.</summary>
        public int? EmailId { get; set; }
        /// <summary>A JSON object containing anything you want to override.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendEmail? Message { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendEmail Message { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg"/> and sets the default values.
        /// </summary>
        public PublicSingleSendRequestEgg()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "contactProperties", n => { ContactProperties = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_contactProperties>(global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_contactProperties.CreateFromDiscriminatorValue); } },
                { "customProperties", n => { CustomProperties = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_customProperties>(global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_customProperties.CreateFromDiscriminatorValue); } },
                { "emailId", n => { EmailId = n.GetIntValue(); } },
                { "message", n => { Message = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendEmail>(global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendEmail.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_contactProperties>("contactProperties", ContactProperties);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendRequestEgg_customProperties>("customProperties", CustomProperties);
            writer.WriteIntValue("emailId", EmailId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.TransactionalSingleSend.V3.Models.PublicSingleSendEmail>("message", Message);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618