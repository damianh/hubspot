// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models
{
    /// <summary>
    /// A pipeline stage definition.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PipelineStage : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Whether the pipeline is archived.</summary>
        public bool? Archived { get; set; }
        /// <summary>The date the pipeline was archived. `archivedAt` will only be present if the pipeline is archived.</summary>
        public DateTimeOffset? ArchivedAt { get; set; }
        /// <summary>The date the pipeline stage was created. The stages on default pipelines will have createdAt = 0.</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The order for displaying this pipeline stage. If two pipeline stages have a matching `displayOrder`, they will be sorted alphabetically by label.</summary>
        public int? DisplayOrder { get; set; }
        /// <summary>A unique identifier generated by HubSpot that can be used to retrieve and update the pipeline stage.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>A label used to organize pipeline stages in HubSpot&apos;s UI. Each pipeline stage&apos;s label must be unique within that pipeline.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>A JSON object containing properties that are not present on all object pipelines.For `deals` pipelines, the `probability` field is required (`{ &quot;probability&quot;: 0.5 }`), and represents the likelihood a deal will close. Possible values are between 0.0 and 1.0 in increments of 0.1.For `tickets` pipelines, the `ticketState` field is optional (`{ &quot;ticketState&quot;: &quot;OPEN&quot; }`), and represents whether the ticket remains open or has been closed by a member of your Support team. Possible values are `OPEN` or `CLOSED`.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_metadata? Metadata { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_metadata Metadata { get; set; }
#endif
        /// <summary>The date the pipeline stage was last updated.</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The writePermissions property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_writePermissions? WritePermissions { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage"/> and sets the default values.
        /// </summary>
        public PipelineStage()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage();
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
                { "displayOrder", n => { DisplayOrder = n.GetIntValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "metadata", n => { Metadata = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_metadata>(global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_metadata.CreateFromDiscriminatorValue); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "writePermissions", n => { WritePermissions = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_writePermissions>(); } },
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
            writer.WriteIntValue("displayOrder", DisplayOrder);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("label", Label);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_metadata>("metadata", Metadata);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Pipelines.V3.Models.PipelineStage_writePermissions>("writePermissions", WritePermissions);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
