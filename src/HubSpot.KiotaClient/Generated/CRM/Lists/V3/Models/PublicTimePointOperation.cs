// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicTimePointOperation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The endpointBehavior property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EndpointBehavior { get; set; }
#nullable restore
#else
        public string EndpointBehavior { get; set; }
#endif
        /// <summary>The includeObjectsWithNoValueSet property</summary>
        public bool? IncludeObjectsWithNoValueSet { get; set; }
        /// <summary>The operationType property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation_operationType? OperationType { get; set; }
        /// <summary>The operator property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Operator { get; set; }
#nullable restore
#else
        public string Operator { get; set; }
#endif
        /// <summary>The propertyParser property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PropertyParser { get; set; }
#nullable restore
#else
        public string PropertyParser { get; set; }
#endif
        /// <summary>The timePoint property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint? TimePoint { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint TimePoint { get; set; }
#endif
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation"/> and sets the default values.
        /// </summary>
        public PublicTimePointOperation()
        {
            AdditionalData = new Dictionary<string, object>();
            OperationType = global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation_operationType.TIME_POINT;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "endpointBehavior", n => { EndpointBehavior = n.GetStringValue(); } },
                { "includeObjectsWithNoValueSet", n => { IncludeObjectsWithNoValueSet = n.GetBoolValue(); } },
                { "operationType", n => { OperationType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation_operationType>(); } },
                { "operator", n => { Operator = n.GetStringValue(); } },
                { "propertyParser", n => { PropertyParser = n.GetStringValue(); } },
                { "timePoint", n => { TimePoint = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint>(global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint.CreateFromDiscriminatorValue); } },
                { "type", n => { Type = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("endpointBehavior", EndpointBehavior);
            writer.WriteBoolValue("includeObjectsWithNoValueSet", IncludeObjectsWithNoValueSet);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation_operationType>("operationType", OperationType);
            writer.WriteStringValue("operator", Operator);
            writer.WriteStringValue("propertyParser", PropertyParser);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint>("timePoint", TimePoint);
            writer.WriteStringValue("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
        /// <summary>
        /// Composed type wrapper for classes <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint"/>, <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint"/>, <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime"/>
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class PublicTimePointOperation_timePoint : IComposedTypeWrapper, IParsable
        {
            /// <summary>Composed type representation for type <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint"/></summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint? PublicDatePoint { get; set; }
#nullable restore
#else
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint PublicDatePoint { get; set; }
#endif
            /// <summary>Composed type representation for type <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint"/></summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint? PublicIndexedTimePoint { get; set; }
#nullable restore
#else
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint PublicIndexedTimePoint { get; set; }
#endif
            /// <summary>Composed type representation for type <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime"/></summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime? PublicPropertyReferencedTime { get; set; }
#nullable restore
#else
            public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime PublicPropertyReferencedTime { get; set; }
#endif
            /// <summary>
            /// Creates a new instance of the appropriate class based on discriminator value
            /// </summary>
            /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint"/></returns>
            /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
            public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint CreateFromDiscriminatorValue(IParseNode parseNode)
            {
                _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
                var mappingValue = parseNode.GetChildNode("")?.GetStringValue();
                var result = new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicTimePointOperation.PublicTimePointOperation_timePoint();
                if("PublicDatePoint".Equals(mappingValue, StringComparison.OrdinalIgnoreCase))
                {
                    result.PublicDatePoint = new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint();
                }
                else if("PublicIndexedTimePoint".Equals(mappingValue, StringComparison.OrdinalIgnoreCase))
                {
                    result.PublicIndexedTimePoint = new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint();
                }
                else if("PublicPropertyReferencedTime".Equals(mappingValue, StringComparison.OrdinalIgnoreCase))
                {
                    result.PublicPropertyReferencedTime = new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime();
                }
                return result;
            }
            /// <summary>
            /// The deserialization information for the current model
            /// </summary>
            /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
            public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
            {
                if(PublicDatePoint != null)
                {
                    return PublicDatePoint.GetFieldDeserializers();
                }
                else if(PublicIndexedTimePoint != null)
                {
                    return PublicIndexedTimePoint.GetFieldDeserializers();
                }
                else if(PublicPropertyReferencedTime != null)
                {
                    return PublicPropertyReferencedTime.GetFieldDeserializers();
                }
                return new Dictionary<string, Action<IParseNode>>();
            }
            /// <summary>
            /// Serializes information the current object
            /// </summary>
            /// <param name="writer">Serialization writer to use to serialize this model</param>
            public virtual void Serialize(ISerializationWriter writer)
            {
                _ = writer ?? throw new ArgumentNullException(nameof(writer));
                if(PublicDatePoint != null)
                {
                    writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicDatePoint>(null, PublicDatePoint);
                }
                else if(PublicIndexedTimePoint != null)
                {
                    writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicIndexedTimePoint>(null, PublicIndexedTimePoint);
                }
                else if(PublicPropertyReferencedTime != null)
                {
                    writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicPropertyReferencedTime>(null, PublicPropertyReferencedTime);
                }
            }
        }
    }
}
#pragma warning restore CS0618