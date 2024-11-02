// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicObjectSearchRequest : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The after property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? After { get; set; }
#nullable restore
#else
        public string After { get; set; }
#endif
        /// <summary>The filterGroups property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.FilterGroup>? FilterGroups { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.FilterGroup> FilterGroups { get; set; }
#endif
        /// <summary>The limit property</summary>
        public int? Limit { get; set; }
        /// <summary>The properties property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Properties { get; set; }
#nullable restore
#else
        public List<string> Properties { get; set; }
#endif
        /// <summary>The query property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Query { get; set; }
#nullable restore
#else
        public string Query { get; set; }
#endif
        /// <summary>The sorts property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Sorts { get; set; }
#nullable restore
#else
        public List<string> Sorts { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.PublicObjectSearchRequest"/> and sets the default values.
        /// </summary>
        public PublicObjectSearchRequest()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.PublicObjectSearchRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.PublicObjectSearchRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.PublicObjectSearchRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "after", n => { After = n.GetStringValue(); } },
                { "filterGroups", n => { FilterGroups = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.FilterGroup>(global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.FilterGroup.CreateFromDiscriminatorValue)?.AsList(); } },
                { "limit", n => { Limit = n.GetIntValue(); } },
                { "properties", n => { Properties = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "query", n => { Query = n.GetStringValue(); } },
                { "sorts", n => { Sorts = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("after", After);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models.FilterGroup>("filterGroups", FilterGroups);
            writer.WriteIntValue("limit", Limit);
            writer.WriteCollectionOfPrimitiveValues<string>("properties", Properties);
            writer.WriteStringValue("query", Query);
            writer.WriteCollectionOfPrimitiveValues<string>("sorts", Sorts);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618