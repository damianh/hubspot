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
    public partial class SubscriptionDefinition : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The businessUnitId property</summary>
        public long? BusinessUnitId { get; set; }
        /// <summary>The method or technology used to contact.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CommunicationMethod { get; set; }
#nullable restore
#else
        public string CommunicationMethod { get; set; }
#endif
        /// <summary>Time at which the definition was created.</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>A description of the subscription.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The ID of the definition.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>Whether the definition is active or archived.</summary>
        public bool? IsActive { get; set; }
        /// <summary>A subscription definition created by HubSpot.</summary>
        public bool? IsDefault { get; set; }
        /// <summary>A default description that is used by some HubSpot tools and cannot be edited.</summary>
        public bool? IsInternal { get; set; }
        /// <summary>The name of the subscription.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The purpose of this subscription or the department in your organization that uses it.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Purpose { get; set; }
#nullable restore
#else
        public string Purpose { get; set; }
#endif
        /// <summary>Time at which the definition was last updated.</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.SubscriptionDefinition"/> and sets the default values.
        /// </summary>
        public SubscriptionDefinition()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.SubscriptionDefinition"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.SubscriptionDefinition CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CommunicationPreferences.Subscriptions.V3.Models.SubscriptionDefinition();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "businessUnitId", n => { BusinessUnitId = n.GetLongValue(); } },
                { "communicationMethod", n => { CommunicationMethod = n.GetStringValue(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "isActive", n => { IsActive = n.GetBoolValue(); } },
                { "isDefault", n => { IsDefault = n.GetBoolValue(); } },
                { "isInternal", n => { IsInternal = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "purpose", n => { Purpose = n.GetStringValue(); } },
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
            writer.WriteLongValue("businessUnitId", BusinessUnitId);
            writer.WriteStringValue("communicationMethod", CommunicationMethod);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("isActive", IsActive);
            writer.WriteBoolValue("isDefault", IsDefault);
            writer.WriteBoolValue("isInternal", IsInternal);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("purpose", Purpose);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618