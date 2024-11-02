// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class SimplePublicObject : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The archived property</summary>
        public bool? Archived { get; set; }
        /// <summary>The archivedAt property</summary>
        public DateTimeOffset? ArchivedAt { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The properties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_properties? Properties { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_properties Properties { get; set; }
#endif
        /// <summary>The propertiesWithHistory property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_propertiesWithHistory? PropertiesWithHistory { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_propertiesWithHistory PropertiesWithHistory { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject"/> and sets the default values.
        /// </summary>
        public SimplePublicObject()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "archived", n => { Archived = n.GetBoolValue(); } },
                { "archivedAt", n => { ArchivedAt = n.GetDateTimeOffsetValue(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "properties", n => { Properties = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_properties>(global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_properties.CreateFromDiscriminatorValue); } },
                { "propertiesWithHistory", n => { PropertiesWithHistory = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_propertiesWithHistory>(global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_propertiesWithHistory.CreateFromDiscriminatorValue); } },
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
            writer.WriteBoolValue("archived", Archived);
            writer.WriteDateTimeOffsetValue("archivedAt", ArchivedAt);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("id", Id);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_properties>("properties", Properties);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models.SimplePublicObject_propertiesWithHistory>("propertiesWithHistory", PropertiesWithHistory);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
