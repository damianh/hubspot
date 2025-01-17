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
    /// The current state of the template definition.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class TimelineEventTemplate : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The date and time that the Event Template was created, as an ISO 8601 timestamp. Will be null if the template was created before Feb 18th, 2020.</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>This uses Markdown syntax with Handlebars and event-specific data to render HTML on a timeline when you expand the details.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DetailTemplate { get; set; }
#nullable restore
#else
        public string DetailTemplate { get; set; }
#endif
        /// <summary>This uses Markdown syntax with Handlebars and event-specific data to render HTML on a timeline as a header.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HeaderTemplate { get; set; }
#nullable restore
#else
        public string HeaderTemplate { get; set; }
#endif
        /// <summary>The template ID.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The template name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The type of CRM object this template is for. [Contacts, companies, tickets, and deals] are supported.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectType { get; set; }
#nullable restore
#else
        public string ObjectType { get; set; }
#endif
        /// <summary>A collection of tokens that can be used as custom properties on the event and to create fully fledged CRM objects.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplateToken>? Tokens { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplateToken> Tokens { get; set; }
#endif
        /// <summary>The date and time that the Event Template was last updated, as an ISO 8601 timestamp. Will be null if the template was created before Feb 18th, 2020.</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplate"/> and sets the default values.
        /// </summary>
        public TimelineEventTemplate()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplate"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplate CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplate();
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
                { "detailTemplate", n => { DetailTemplate = n.GetStringValue(); } },
                { "headerTemplate", n => { HeaderTemplate = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "objectType", n => { ObjectType = n.GetStringValue(); } },
                { "tokens", n => { Tokens = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplateToken>(global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplateToken.CreateFromDiscriminatorValue)?.AsList(); } },
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
            writer.WriteStringValue("detailTemplate", DetailTemplate);
            writer.WriteStringValue("headerTemplate", HeaderTemplate);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("objectType", ObjectType);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models.TimelineEventTemplateToken>("tokens", Tokens);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
