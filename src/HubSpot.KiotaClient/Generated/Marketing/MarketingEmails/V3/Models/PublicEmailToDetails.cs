// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models
{
    /// <summary>
    /// Data structure representing the to fields of the email.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PublicEmailToDetails : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Data structure representing lists of IDs that should be included and excluded.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients? ContactIds { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients ContactIds { get; set; }
#endif
        /// <summary>Data structure representing lists of IDs that should be included and excluded.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients? ContactIlsLists { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients ContactIlsLists { get; set; }
#endif
        /// <summary>Data structure representing lists of IDs that should be included and excluded.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients? ContactLists { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients ContactLists { get; set; }
#endif
        /// <summary>The limitSendFrequency property</summary>
        public bool? LimitSendFrequency { get; set; }
        /// <summary>The suppressGraymail property</summary>
        public bool? SuppressGraymail { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailToDetails"/> and sets the default values.
        /// </summary>
        public PublicEmailToDetails()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailToDetails"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailToDetails CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailToDetails();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "contactIds", n => { ContactIds = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients.CreateFromDiscriminatorValue); } },
                { "contactIlsLists", n => { ContactIlsLists = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients.CreateFromDiscriminatorValue); } },
                { "contactLists", n => { ContactLists = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients.CreateFromDiscriminatorValue); } },
                { "limitSendFrequency", n => { LimitSendFrequency = n.GetBoolValue(); } },
                { "suppressGraymail", n => { SuppressGraymail = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>("contactIds", ContactIds);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>("contactIlsLists", ContactIlsLists);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailRecipients>("contactLists", ContactLists);
            writer.WriteBoolValue("limitSendFrequency", LimitSendFrequency);
            writer.WriteBoolValue("suppressGraymail", SuppressGraymail);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
