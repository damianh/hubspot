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
    public partial class StandardError : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The category property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Category { get; set; }
#nullable restore
#else
        public string Category { get; set; }
#endif
        /// <summary>The context property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_context? Context { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_context Context { get; set; }
#endif
        /// <summary>The errors property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ErrorDetail>? Errors { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ErrorDetail> Errors { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The links property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_links? Links { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_links Links { get; set; }
#endif
        /// <summary>The message property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Message { get; set; }
#nullable restore
#else
        public string Message { get; set; }
#endif
        /// <summary>The status property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Status { get; set; }
#nullable restore
#else
        public string Status { get; set; }
#endif
        /// <summary>The subCategory property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_subCategory? SubCategory { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_subCategory SubCategory { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError"/> and sets the default values.
        /// </summary>
        public StandardError()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "category", n => { Category = n.GetStringValue(); } },
                { "context", n => { Context = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_context>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_context.CreateFromDiscriminatorValue); } },
                { "errors", n => { Errors = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ErrorDetail>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ErrorDetail.CreateFromDiscriminatorValue)?.AsList(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "links", n => { Links = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_links>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_links.CreateFromDiscriminatorValue); } },
                { "message", n => { Message = n.GetStringValue(); } },
                { "status", n => { Status = n.GetStringValue(); } },
                { "subCategory", n => { SubCategory = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_subCategory>(global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_subCategory.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("category", Category);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_context>("context", Context);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.ErrorDetail>("errors", Errors);
            writer.WriteStringValue("id", Id);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_links>("links", Links);
            writer.WriteStringValue("message", Message);
            writer.WriteStringValue("status", Status);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V4.Models.StandardError_subCategory>("subCategory", SubCategory);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
