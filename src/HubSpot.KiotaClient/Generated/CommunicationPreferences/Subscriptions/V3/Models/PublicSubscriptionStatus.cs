// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicSubscriptionStatus : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The ID of the brand that the subscription is associated with, if there is one.</summary>
        public long? BrandId { get; set; }
        /// <summary>A description of the subscription.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The ID for the subscription.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The legal reason for the current status of the subscription.</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_legalBasis? LegalBasis { get; set; }
        /// <summary>A more detailed explanation to go with the legal basis.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LegalBasisExplanation { get; set; }
#nullable restore
#else
        public string LegalBasisExplanation { get; set; }
#endif
        /// <summary>The name of the subscription.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The name of the preferences group that the subscription is associated with.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PreferenceGroupName { get; set; }
#nullable restore
#else
        public string PreferenceGroupName { get; set; }
#endif
        /// <summary>Where the status is determined from e.g. PORTAL_WIDE_STATUS if the contact opted out from the portal.</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_sourceOfStatus? SourceOfStatus { get; set; }
        /// <summary>Whether the contact is subscribed.</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_status? Status { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus"/> and sets the default values.
        /// </summary>
        public PublicSubscriptionStatus()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "brandId", n => { BrandId = n.GetLongValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "legalBasis", n => { LegalBasis = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_legalBasis>(); } },
                { "legalBasisExplanation", n => { LegalBasisExplanation = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "preferenceGroupName", n => { PreferenceGroupName = n.GetStringValue(); } },
                { "sourceOfStatus", n => { SourceOfStatus = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_sourceOfStatus>(); } },
                { "status", n => { Status = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_status>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteLongValue("brandId", BrandId);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("id", Id);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_legalBasis>("legalBasis", LegalBasis);
            writer.WriteStringValue("legalBasisExplanation", LegalBasisExplanation);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("preferenceGroupName", PreferenceGroupName);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_sourceOfStatus>("sourceOfStatus", SourceOfStatus);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.PublicSubscriptionStatus_status>("status", Status);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618