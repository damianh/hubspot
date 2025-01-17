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
    public partial class PublicCalendarDatePropertyOperation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The fiscalYearStart property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_fiscalYearStart? FiscalYearStart { get; set; }
        /// <summary>The includeObjectsWithNoValueSet property</summary>
        public bool? IncludeObjectsWithNoValueSet { get; set; }
        /// <summary>The operationType property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_operationType? OperationType { get; set; }
        /// <summary>The operator property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Operator { get; set; }
#nullable restore
#else
        public string Operator { get; set; }
#endif
        /// <summary>The timeUnit property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TimeUnit { get; set; }
#nullable restore
#else
        public string TimeUnit { get; set; }
#endif
        /// <summary>The timeUnitCount property</summary>
        public int? TimeUnitCount { get; set; }
        /// <summary>The useFiscalYear property</summary>
        public bool? UseFiscalYear { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation"/> and sets the default values.
        /// </summary>
        public PublicCalendarDatePropertyOperation()
        {
            AdditionalData = new Dictionary<string, object>();
            OperationType = global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_operationType.CALENDAR_DATE;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "fiscalYearStart", n => { FiscalYearStart = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_fiscalYearStart>(); } },
                { "includeObjectsWithNoValueSet", n => { IncludeObjectsWithNoValueSet = n.GetBoolValue(); } },
                { "operationType", n => { OperationType = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_operationType>(); } },
                { "operator", n => { Operator = n.GetStringValue(); } },
                { "timeUnit", n => { TimeUnit = n.GetStringValue(); } },
                { "timeUnitCount", n => { TimeUnitCount = n.GetIntValue(); } },
                { "useFiscalYear", n => { UseFiscalYear = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_fiscalYearStart>("fiscalYearStart", FiscalYearStart);
            writer.WriteBoolValue("includeObjectsWithNoValueSet", IncludeObjectsWithNoValueSet);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.Lists.V3.Models.PublicCalendarDatePropertyOperation_operationType>("operationType", OperationType);
            writer.WriteStringValue("operator", Operator);
            writer.WriteStringValue("timeUnit", TimeUnit);
            writer.WriteIntValue("timeUnitCount", TimeUnitCount);
            writer.WriteBoolValue("useFiscalYear", UseFiscalYear);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
