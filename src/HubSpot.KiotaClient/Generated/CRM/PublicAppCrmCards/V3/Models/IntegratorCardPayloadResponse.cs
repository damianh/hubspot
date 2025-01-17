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
    /// The card details payload, sent to HubSpot by an app in response to a data fetch request when a user visits a CRM record page.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class IntegratorCardPayloadResponse : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>URL to a page the integrator has built that displays all details for this card. This URL will be displayed to users under a `See more [x]` link if there are more than five items in your response, where `[x]` is the value of `itemLabel`.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AllItemsLinkUrl { get; set; }
#nullable restore
#else
        public string AllItemsLinkUrl { get; set; }
#endif
        /// <summary>The label to be used for the `allItemsLinkUrl` link (e.g. &apos;See more tickets&apos;). If not provided, this falls back to the card&apos;s title.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CardLabel { get; set; }
#nullable restore
#else
        public string CardLabel { get; set; }
#endif
        /// <summary>The responseVersion property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse_responseVersion? ResponseVersion { get; set; }
        /// <summary>A list of up to five valid card sub categories.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorObjectResult>? Sections { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorObjectResult> Sections { get; set; }
#endif
        /// <summary>The topLevelActions property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.TopLevelActions? TopLevelActions { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.TopLevelActions TopLevelActions { get; set; }
#endif
        /// <summary>The total number of card properties that will be sent in this response.</summary>
        public int? TotalCount { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse"/> and sets the default values.
        /// </summary>
        public IntegratorCardPayloadResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "allItemsLinkUrl", n => { AllItemsLinkUrl = n.GetStringValue(); } },
                { "cardLabel", n => { CardLabel = n.GetStringValue(); } },
                { "responseVersion", n => { ResponseVersion = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse_responseVersion>(); } },
                { "sections", n => { Sections = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorObjectResult>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorObjectResult.CreateFromDiscriminatorValue)?.AsList(); } },
                { "topLevelActions", n => { TopLevelActions = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.TopLevelActions>(global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.TopLevelActions.CreateFromDiscriminatorValue); } },
                { "totalCount", n => { TotalCount = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("allItemsLinkUrl", AllItemsLinkUrl);
            writer.WriteStringValue("cardLabel", CardLabel);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorCardPayloadResponse_responseVersion>("responseVersion", ResponseVersion);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.IntegratorObjectResult>("sections", Sections);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PublicAppCrmCards.V3.Models.TopLevelActions>("topLevelActions", TopLevelActions);
            writer.WriteIntValue("totalCount", TotalCount);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
