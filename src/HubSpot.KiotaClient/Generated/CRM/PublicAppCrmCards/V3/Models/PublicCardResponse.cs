// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicCardResponse : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
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
        /// <summary>The auditHistory property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardAuditResponse>? AuditHistory { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardAuditResponse> AuditHistory { get; set; }
#endif
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>Configuration for displayed info on a card</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody? Display { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody Display { get; set; }
#endif
        /// <summary>The fetch property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardFetchBody? Fetch { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardFetchBody Fetch { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The title property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Title { get; set; }
#nullable restore
#else
        public string Title { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse"/> and sets the default values.
        /// </summary>
        public PublicCardResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardResponse();
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
                { "auditHistory", n => { AuditHistory = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardAuditResponse>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardAuditResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "display", n => { Display = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody.CreateFromDiscriminatorValue); } },
                { "fetch", n => { Fetch = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardFetchBody>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardFetchBody.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "title", n => { Title = n.GetStringValue(); } },
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
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardActions>("actions", Actions);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardAuditResponse>("auditHistory", AuditHistory);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.CardDisplayBody>("display", Display);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.PublicCardFetchBody>("fetch", Fetch);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("title", Title);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
