// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class ColumnRequest : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The id of the column from another table to which the column refers/points to.</summary>
        public int? ForeignColumnId { get; set; }
        /// <summary>The id of another table to which the column refers/points to.</summary>
        public long? ForeignTableId { get; set; }
        /// <summary>Column Id</summary>
        public int? Id { get; set; }
        /// <summary>Label of the column</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Label { get; set; }
#nullable restore
#else
        public string Label { get; set; }
#endif
        /// <summary>Name of the column</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>Options to choose for select and multi-select columns</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.Option>? Options { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.Option> Options { get; set; }
#endif
        /// <summary>Type of the column</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest_type? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest"/> and sets the default values.
        /// </summary>
        public ColumnRequest()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "foreignColumnId", n => { ForeignColumnId = n.GetIntValue(); } },
                { "foreignTableId", n => { ForeignTableId = n.GetLongValue(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "label", n => { Label = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "options", n => { Options = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.Option>(global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.Option.CreateFromDiscriminatorValue)?.AsList(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest_type>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("foreignColumnId", ForeignColumnId);
            writer.WriteLongValue("foreignTableId", ForeignTableId);
            writer.WriteIntValue("id", Id);
            writer.WriteStringValue("label", Label);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.Option>("options", Options);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.Hubdb.V3.Models.ColumnRequest_type>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
