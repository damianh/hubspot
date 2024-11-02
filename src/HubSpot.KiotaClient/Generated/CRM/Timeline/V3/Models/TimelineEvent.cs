// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models
{
    /// <summary>
    /// The state of the timeline event.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class TimelineEvent : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The event domain (often paired with utk).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Domain { get; set; }
#nullable restore
#else
        public string Domain { get; set; }
#endif
        /// <summary>The email address used for contact-specific events. This can be used to identify existing contacts, create new ones, or change the email for an existing contact (if paired with the `objectId`).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Email { get; set; }
#nullable restore
#else
        public string Email { get; set; }
#endif
        /// <summary>The event template ID.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventTemplateId { get; set; }
#nullable restore
#else
        public string EventTemplateId { get; set; }
#endif
        /// <summary>Additional event-specific data that can be interpreted by the template&apos;s markdown.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_extraData? ExtraData { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_extraData ExtraData { get; set; }
#endif
        /// <summary>Identifier for the event. This is optional, and we recommend you do not pass this in. We will create one for you if you omit this. You can also use `{{uuid}}` anywhere in the ID to generate a unique string, guaranteeing uniqueness.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The CRM object identifier. This is required for every event other than contacts (where utk or email can be used).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectId { get; set; }
#nullable restore
#else
        public string ObjectId { get; set; }
#endif
        /// <summary>The timelineIFrame property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventIFrame? TimelineIFrame { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventIFrame TimelineIFrame { get; set; }
#endif
        /// <summary>The time the event occurred. If not passed in, the curren time will be assumed. This is used to determine where an event is shown on a CRM object&apos;s timeline.</summary>
        public DateTimeOffset? Timestamp { get; set; }
        /// <summary>A collection of token keys and values associated with the template tokens.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_tokens? Tokens { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_tokens Tokens { get; set; }
#endif
        /// <summary>Use the `utk` parameter to associate an event with a contact by `usertoken`. This is recommended if you don&apos;t know a user&apos;s email, but have an identifying user token in your cookie.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Utk { get; set; }
#nullable restore
#else
        public string Utk { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent"/> and sets the default values.
        /// </summary>
        public TimelineEvent()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "domain", n => { Domain = n.GetStringValue(); } },
                { "email", n => { Email = n.GetStringValue(); } },
                { "eventTemplateId", n => { EventTemplateId = n.GetStringValue(); } },
                { "extraData", n => { ExtraData = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_extraData>(global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_extraData.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "objectId", n => { ObjectId = n.GetStringValue(); } },
                { "timelineIFrame", n => { TimelineIFrame = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventIFrame>(global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventIFrame.CreateFromDiscriminatorValue); } },
                { "timestamp", n => { Timestamp = n.GetDateTimeOffsetValue(); } },
                { "tokens", n => { Tokens = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_tokens>(global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_tokens.CreateFromDiscriminatorValue); } },
                { "utk", n => { Utk = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("domain", Domain);
            writer.WriteStringValue("email", Email);
            writer.WriteStringValue("eventTemplateId", EventTemplateId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_extraData>("extraData", ExtraData);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("objectId", ObjectId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventIFrame>("timelineIFrame", TimelineIFrame);
            writer.WriteDateTimeOffsetValue("timestamp", Timestamp);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEvent_tokens>("tokens", Tokens);
            writer.WriteStringValue("utk", Utk);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
