// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ActionResponseWithResultsPublicStatus : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The completedAt property</summary>
        public DateTimeOffset? CompletedAt { get; set; }
        /// <summary>The errors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError>? Errors { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError> Errors { get; set; }
#endif
        /// <summary>The links property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_links? Links { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_links Links { get; set; }
#endif
        /// <summary>The numErrors property</summary>
        public int? NumErrors { get; set; }
        /// <summary>The requestedAt property</summary>
        public DateTimeOffset? RequestedAt { get; set; }
        /// <summary>The results property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.PublicStatus>? Results { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.PublicStatus> Results { get; set; }
#endif
        /// <summary>The startedAt property</summary>
        public DateTimeOffset? StartedAt { get; set; }
        /// <summary>The status property</summary>
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_status? Status { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus"/> and sets the default values.
        /// </summary>
        public ActionResponseWithResultsPublicStatus()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "completedAt", n => { CompletedAt = n.GetDateTimeOffsetValue(); } },
                { "errors", n => { Errors = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError.CreateFromDiscriminatorValue)?.AsList(); } },
                { "links", n => { Links = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_links>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_links.CreateFromDiscriminatorValue); } },
                { "numErrors", n => { NumErrors = n.GetIntValue(); } },
                { "requestedAt", n => { RequestedAt = n.GetDateTimeOffsetValue(); } },
                { "results", n => { Results = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.PublicStatus>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.PublicStatus.CreateFromDiscriminatorValue)?.AsList(); } },
                { "startedAt", n => { StartedAt = n.GetDateTimeOffsetValue(); } },
                { "status", n => { Status = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_status>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteDateTimeOffsetValue("completedAt", CompletedAt);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError>("errors", Errors);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_links>("links", Links);
            writer.WriteIntValue("numErrors", NumErrors);
            writer.WriteDateTimeOffsetValue("requestedAt", RequestedAt);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.PublicStatus>("results", Results);
            writer.WriteDateTimeOffsetValue("startedAt", StartedAt);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ActionResponseWithResultsPublicStatus_status>("status", Status);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
