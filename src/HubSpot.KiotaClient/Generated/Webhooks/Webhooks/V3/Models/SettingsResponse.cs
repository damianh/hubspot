// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models
{
    /// <summary>
    /// Webhook settings for an app.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SettingsResponse : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>When this subscription was created. Formatted as milliseconds from the [Unix epoch](#).</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>A publicly available URL for HubSpot to call where event payloads will be delivered. See [link-so-some-doc](#) for details about the format of these event payloads.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TargetUrl { get; set; }
#nullable restore
#else
        public string TargetUrl { get; set; }
#endif
        /// <summary>Configuration details for webhook throttling.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.ThrottlingSettings? Throttling { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.ThrottlingSettings Throttling { get; set; }
#endif
        /// <summary>When this subscription was last updated. Formatted as milliseconds from the [Unix epoch](#).</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SettingsResponse"/> and sets the default values.
        /// </summary>
        public SettingsResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SettingsResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SettingsResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.SettingsResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "targetUrl", n => { TargetUrl = n.GetStringValue(); } },
                { "throttling", n => { Throttling = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.ThrottlingSettings>(global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.ThrottlingSettings.CreateFromDiscriminatorValue); } },
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
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("targetUrl", TargetUrl);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models.ThrottlingSettings>("throttling", Throttling);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
