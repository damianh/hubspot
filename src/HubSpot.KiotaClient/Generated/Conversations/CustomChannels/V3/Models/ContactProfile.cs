// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ContactProfile : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The addresses property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactAddress>? Addresses { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactAddress> Addresses { get; set; }
#endif
        /// <summary>The emails property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactEmail>? Emails { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactEmail> Emails { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactName? Name { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactName Name { get; set; }
#endif
        /// <summary>The org property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactOrg? Org { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactOrg Org { get; set; }
#endif
        /// <summary>The phones property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactPhone>? Phones { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactPhone> Phones { get; set; }
#endif
        /// <summary>The urls property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactUrl>? Urls { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactUrl> Urls { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactProfile"/> and sets the default values.
        /// </summary>
        public ContactProfile()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactProfile"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactProfile CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactProfile();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "addresses", n => { Addresses = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactAddress>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactAddress.CreateFromDiscriminatorValue)?.AsList(); } },
                { "emails", n => { Emails = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactEmail>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactEmail.CreateFromDiscriminatorValue)?.AsList(); } },
                { "name", n => { Name = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactName>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactName.CreateFromDiscriminatorValue); } },
                { "org", n => { Org = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactOrg>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactOrg.CreateFromDiscriminatorValue); } },
                { "phones", n => { Phones = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactPhone>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactPhone.CreateFromDiscriminatorValue)?.AsList(); } },
                { "urls", n => { Urls = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactUrl>(global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactUrl.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactAddress>("addresses", Addresses);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactEmail>("emails", Emails);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactName>("name", Name);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactOrg>("org", Org);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactPhone>("phones", Phones);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Conversations.CustomChannels.V3.Models.ContactUrl>("urls", Urls);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618