// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class MarketingEventPublicReadResponse : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The number of HubSpot contacts that attended this marketing event.</summary>
        public int? Attendees { get; set; }
        /// <summary>The number of HubSpot contacts that registered for this marketing event, but later cancelled their registration.</summary>
        public int? Cancellations { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>A list of PropertyValues. These can be whatever kind of property names and values you want. However, they must already exist on the HubSpot account&apos;s definition of the MarketingEvent Object. If they don&apos;t they will be filtered out and not set.In order to do this you&apos;ll need to create a new PropertyGroup on the HubSpot account&apos;s MarketingEvent object for your specific app and create the Custom Property you want to track on that HubSpot account. Do not create any new default properties on the MarketingEvent object as that will apply to all HubSpot accounts.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PropertyValue>? CustomProperties { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PropertyValue> CustomProperties { get; set; }
#endif
        /// <summary>The end date and time of the marketing event.</summary>
        public DateTimeOffset? EndDateTime { get; set; }
        /// <summary>Indicates if the marketing event has been cancelled.</summary>
        public bool? EventCancelled { get; set; }
        /// <summary>The eventCompleted property</summary>
        public bool? EventCompleted { get; set; }
        /// <summary>The description of the marketing event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventDescription { get; set; }
#nullable restore
#else
        public string EventDescription { get; set; }
#endif
        /// <summary>The name of the marketing event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventName { get; set; }
#nullable restore
#else
        public string EventName { get; set; }
#endif
        /// <summary>The name of the organizer of the marketing event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventOrganizer { get; set; }
#nullable restore
#else
        public string EventOrganizer { get; set; }
#endif
        /// <summary>The type of the marketing event.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventType { get; set; }
#nullable restore
#else
        public string EventType { get; set; }
#endif
        /// <summary>A URL in the external event application where the marketing event can be managed.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EventUrl { get; set; }
#nullable restore
#else
        public string EventUrl { get; set; }
#endif
        /// <summary>The id of the marketing event in the external event application.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ExternalEventId { get; set; }
#nullable restore
#else
        public string ExternalEventId { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The number of HubSpot contacts that registered for this marketing event, but did not attend. This field only had a value when the event is over.</summary>
        public int? NoShows { get; set; }
        /// <summary>The objectId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectId { get; set; }
#nullable restore
#else
        public string ObjectId { get; set; }
#endif
        /// <summary>The number of HubSpot contacts that registered for this marketing event.</summary>
        public int? Registrants { get; set; }
        /// <summary>The start date and time of the marketing event.</summary>
        public DateTimeOffset? StartDateTime { get; set; }
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventPublicReadResponse"/> and sets the default values.
        /// </summary>
        public MarketingEventPublicReadResponse()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventPublicReadResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventPublicReadResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.MarketingEventPublicReadResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "attendees", n => { Attendees = n.GetIntValue(); } },
                { "cancellations", n => { Cancellations = n.GetIntValue(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "customProperties", n => { CustomProperties = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PropertyValue>(global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PropertyValue.CreateFromDiscriminatorValue)?.AsList(); } },
                { "endDateTime", n => { EndDateTime = n.GetDateTimeOffsetValue(); } },
                { "eventCancelled", n => { EventCancelled = n.GetBoolValue(); } },
                { "eventCompleted", n => { EventCompleted = n.GetBoolValue(); } },
                { "eventDescription", n => { EventDescription = n.GetStringValue(); } },
                { "eventName", n => { EventName = n.GetStringValue(); } },
                { "eventOrganizer", n => { EventOrganizer = n.GetStringValue(); } },
                { "eventType", n => { EventType = n.GetStringValue(); } },
                { "eventUrl", n => { EventUrl = n.GetStringValue(); } },
                { "externalEventId", n => { ExternalEventId = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "noShows", n => { NoShows = n.GetIntValue(); } },
                { "objectId", n => { ObjectId = n.GetStringValue(); } },
                { "registrants", n => { Registrants = n.GetIntValue(); } },
                { "startDateTime", n => { StartDateTime = n.GetDateTimeOffsetValue(); } },
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
            writer.WriteIntValue("attendees", Attendees);
            writer.WriteIntValue("cancellations", Cancellations);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEvents.V3.Models.PropertyValue>("customProperties", CustomProperties);
            writer.WriteDateTimeOffsetValue("endDateTime", EndDateTime);
            writer.WriteBoolValue("eventCancelled", EventCancelled);
            writer.WriteBoolValue("eventCompleted", EventCompleted);
            writer.WriteStringValue("eventDescription", EventDescription);
            writer.WriteStringValue("eventName", EventName);
            writer.WriteStringValue("eventOrganizer", EventOrganizer);
            writer.WriteStringValue("eventType", EventType);
            writer.WriteStringValue("eventUrl", EventUrl);
            writer.WriteStringValue("externalEventId", ExternalEventId);
            writer.WriteStringValue("id", Id);
            writer.WriteIntValue("noShows", NoShows);
            writer.WriteStringValue("objectId", ObjectId);
            writer.WriteIntValue("registrants", Registrants);
            writer.WriteDateTimeOffsetValue("startDateTime", StartDateTime);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
