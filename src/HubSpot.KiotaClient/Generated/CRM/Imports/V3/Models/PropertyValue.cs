// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PropertyValue : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The dataSensitivity property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_dataSensitivity? DataSensitivity { get; set; }
        /// <summary>The isEncrypted property</summary>
        public bool? IsEncrypted { get; set; }
        /// <summary>The isLargeValue property</summary>
        public bool? IsLargeValue { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The persistenceTimestamp property</summary>
        public long? PersistenceTimestamp { get; set; }
        /// <summary>The requestId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RequestId { get; set; }
#nullable restore
#else
        public string RequestId { get; set; }
#endif
        /// <summary>The selectedByUser property</summary>
        public bool? SelectedByUser { get; set; }
        /// <summary>The selectedByUserTimestamp property</summary>
        public long? SelectedByUserTimestamp { get; set; }
        /// <summary>The source property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_source? Source { get; set; }
        /// <summary>The sourceId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SourceId { get; set; }
#nullable restore
#else
        public string SourceId { get; set; }
#endif
        /// <summary>The sourceLabel property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SourceLabel { get; set; }
#nullable restore
#else
        public string SourceLabel { get; set; }
#endif
        /// <summary>The sourceMetadata property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SourceMetadata { get; set; }
#nullable restore
#else
        public string SourceMetadata { get; set; }
#endif
        /// <summary>The sourceVid property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<long?>? SourceVid { get; set; }
#nullable restore
#else
        public List<long?> SourceVid { get; set; }
#endif
        /// <summary>The timestamp property</summary>
        public long? Timestamp { get; set; }
        /// <summary>The unit property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Unit { get; set; }
#nullable restore
#else
        public string Unit { get; set; }
#endif
        /// <summary>The updatedByUserId property</summary>
        public int? UpdatedByUserId { get; set; }
        /// <summary>The useTimestampAsPersistenceTimestamp property</summary>
        public bool? UseTimestampAsPersistenceTimestamp { get; set; }
        /// <summary>The value property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Value { get; set; }
#nullable restore
#else
        public string Value { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue"/> and sets the default values.
        /// </summary>
        public PropertyValue()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "dataSensitivity", n => { DataSensitivity = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_dataSensitivity>(); } },
                { "isEncrypted", n => { IsEncrypted = n.GetBoolValue(); } },
                { "isLargeValue", n => { IsLargeValue = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "persistenceTimestamp", n => { PersistenceTimestamp = n.GetLongValue(); } },
                { "requestId", n => { RequestId = n.GetStringValue(); } },
                { "selectedByUser", n => { SelectedByUser = n.GetBoolValue(); } },
                { "selectedByUserTimestamp", n => { SelectedByUserTimestamp = n.GetLongValue(); } },
                { "source", n => { Source = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_source>(); } },
                { "sourceId", n => { SourceId = n.GetStringValue(); } },
                { "sourceLabel", n => { SourceLabel = n.GetStringValue(); } },
                { "sourceMetadata", n => { SourceMetadata = n.GetStringValue(); } },
                { "sourceVid", n => { SourceVid = n.GetCollectionOfPrimitiveValues<long?>()?.AsList(); } },
                { "timestamp", n => { Timestamp = n.GetLongValue(); } },
                { "unit", n => { Unit = n.GetStringValue(); } },
                { "updatedByUserId", n => { UpdatedByUserId = n.GetIntValue(); } },
                { "useTimestampAsPersistenceTimestamp", n => { UseTimestampAsPersistenceTimestamp = n.GetBoolValue(); } },
                { "value", n => { Value = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_dataSensitivity>("dataSensitivity", DataSensitivity);
            writer.WriteBoolValue("isEncrypted", IsEncrypted);
            writer.WriteBoolValue("isLargeValue", IsLargeValue);
            writer.WriteStringValue("name", Name);
            writer.WriteLongValue("persistenceTimestamp", PersistenceTimestamp);
            writer.WriteStringValue("requestId", RequestId);
            writer.WriteBoolValue("selectedByUser", SelectedByUser);
            writer.WriteLongValue("selectedByUserTimestamp", SelectedByUserTimestamp);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Imports.V3.Models.PropertyValue_source>("source", Source);
            writer.WriteStringValue("sourceId", SourceId);
            writer.WriteStringValue("sourceLabel", SourceLabel);
            writer.WriteStringValue("sourceMetadata", SourceMetadata);
            writer.WriteCollectionOfPrimitiveValues<long?>("sourceVid", SourceVid);
            writer.WriteLongValue("timestamp", Timestamp);
            writer.WriteStringValue("unit", Unit);
            writer.WriteIntValue("updatedByUserId", UpdatedByUserId);
            writer.WriteBoolValue("useTimestampAsPersistenceTimestamp", UseTimestampAsPersistenceTimestamp);
            writer.WriteStringValue("value", Value);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
