// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models
{
    /// <summary>
    /// State of card definition to be created
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CardCreateRequest : IAdditionalDataHolder, IParsable
    {
        /// <summary>Configuration for custom user actions on cards.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions? Actions { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions Actions { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Configuration for displayed info on a card</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody? Display { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody Display { get; set; }
#endif
        /// <summary>Configuration for this card&apos;s data fetch request.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardFetchBody? Fetch { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardFetchBody Fetch { get; set; }
#endif
        /// <summary>The top-level title for this card. Displayed to users in the CRM UI.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest"/> and sets the default values.
        /// </summary>
        public CardCreateRequest()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardCreateRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "actions", n => { Actions = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions.CreateFromDiscriminatorValue); } },
                { "display", n => { Display = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody.CreateFromDiscriminatorValue); } },
                { "fetch", n => { Fetch = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardFetchBody>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardFetchBody.CreateFromDiscriminatorValue); } },
                { "title", n => { Title = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions>("actions", Actions);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody>("display", Display);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardFetchBody>("fetch", Fetch);
            writer.WriteStringValue("title", Title);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
