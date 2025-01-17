// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ExternalMeetingBookingResponse : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The bookingTimezone property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BookingTimezone { get; set; }
#nullable restore
#else
        public string BookingTimezone { get; set; }
#endif
        /// <summary>The calendarEventId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CalendarEventId { get; set; }
#nullable restore
#else
        public string CalendarEventId { get; set; }
#endif
        /// <summary>The contactId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ContactId { get; set; }
#nullable restore
#else
        public string ContactId { get; set; }
#endif
        /// <summary>The duration property</summary>
        public long? Duration { get; set; }
        /// <summary>The end property</summary>
        public DateTimeOffset? End { get; set; }
        /// <summary>The formFields property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalValidatedFormField>? FormFields { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalValidatedFormField> FormFields { get; set; }
#endif
        /// <summary>The guestEmails property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? GuestEmails { get; set; }
#nullable restore
#else
        public List<string> GuestEmails { get; set; }
#endif
        /// <summary>The isOffline property</summary>
        public bool? IsOffline { get; set; }
        /// <summary>The legalConsentResponses property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalLegalConsentResponse>? LegalConsentResponses { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalLegalConsentResponse> LegalConsentResponses { get; set; }
#endif
        /// <summary>The locale property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Locale { get; set; }
#nullable restore
#else
        public string Locale { get; set; }
#endif
        /// <summary>The location property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Location { get; set; }
#nullable restore
#else
        public string Location { get; set; }
#endif
        /// <summary>The start property</summary>
        public DateTimeOffset? Start { get; set; }
        /// <summary>The subject property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Subject { get; set; }
#nullable restore
#else
        public string Subject { get; set; }
#endif
        /// <summary>The webConferenceMeetingId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebConferenceMeetingId { get; set; }
#nullable restore
#else
        public string WebConferenceMeetingId { get; set; }
#endif
        /// <summary>The webConferenceUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WebConferenceUrl { get; set; }
#nullable restore
#else
        public string WebConferenceUrl { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingBookingResponse"/> and sets the default values.
        /// </summary>
        public ExternalMeetingBookingResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingBookingResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingBookingResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingBookingResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "bookingTimezone", n => { BookingTimezone = n.GetStringValue(); } },
                { "calendarEventId", n => { CalendarEventId = n.GetStringValue(); } },
                { "contactId", n => { ContactId = n.GetStringValue(); } },
                { "duration", n => { Duration = n.GetLongValue(); } },
                { "end", n => { End = n.GetDateTimeOffsetValue(); } },
                { "formFields", n => { FormFields = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalValidatedFormField>(global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalValidatedFormField.CreateFromDiscriminatorValue)?.AsList(); } },
                { "guestEmails", n => { GuestEmails = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "isOffline", n => { IsOffline = n.GetBoolValue(); } },
                { "legalConsentResponses", n => { LegalConsentResponses = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalLegalConsentResponse>(global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalLegalConsentResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "locale", n => { Locale = n.GetStringValue(); } },
                { "location", n => { Location = n.GetStringValue(); } },
                { "start", n => { Start = n.GetDateTimeOffsetValue(); } },
                { "subject", n => { Subject = n.GetStringValue(); } },
                { "webConferenceMeetingId", n => { WebConferenceMeetingId = n.GetStringValue(); } },
                { "webConferenceUrl", n => { WebConferenceUrl = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("bookingTimezone", BookingTimezone);
            writer.WriteStringValue("calendarEventId", CalendarEventId);
            writer.WriteStringValue("contactId", ContactId);
            writer.WriteLongValue("duration", Duration);
            writer.WriteDateTimeOffsetValue("end", End);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalValidatedFormField>("formFields", FormFields);
            writer.WriteCollectionOfPrimitiveValues<string>("guestEmails", GuestEmails);
            writer.WriteBoolValue("isOffline", IsOffline);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalLegalConsentResponse>("legalConsentResponses", LegalConsentResponses);
            writer.WriteStringValue("locale", Locale);
            writer.WriteStringValue("location", Location);
            writer.WriteDateTimeOffsetValue("start", Start);
            writer.WriteStringValue("subject", Subject);
            writer.WriteStringValue("webConferenceMeetingId", WebConferenceMeetingId);
            writer.WriteStringValue("webConferenceUrl", WebConferenceUrl);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
