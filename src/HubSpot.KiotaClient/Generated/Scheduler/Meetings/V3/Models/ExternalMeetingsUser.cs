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
    public partial class ExternalMeetingsUser : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The calendarProvider property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CalendarProvider { get; set; }
#nullable restore
#else
        public string CalendarProvider { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The isSalesStarter property</summary>
        public bool? IsSalesStarter { get; set; }
        /// <summary>The userId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserId { get; set; }
#nullable restore
#else
        public string UserId { get; set; }
#endif
        /// <summary>The userProfile property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalUserProfile? UserProfile { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalUserProfile UserProfile { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingsUser"/> and sets the default values.
        /// </summary>
        public ExternalMeetingsUser()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingsUser"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingsUser CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalMeetingsUser();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "calendarProvider", n => { CalendarProvider = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "isSalesStarter", n => { IsSalesStarter = n.GetBoolValue(); } },
                { "userId", n => { UserId = n.GetStringValue(); } },
                { "userProfile", n => { UserProfile = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalUserProfile>(global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalUserProfile.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("calendarProvider", CalendarProvider);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("isSalesStarter", IsSalesStarter);
            writer.WriteStringValue("userId", UserId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3.Models.ExternalUserProfile>("userProfile", UserProfile);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
