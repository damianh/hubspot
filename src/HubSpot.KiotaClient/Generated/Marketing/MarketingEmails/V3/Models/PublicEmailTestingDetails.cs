// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models
{
    /// <summary>
    /// AB testing related data. This property is only returned for AB type emails.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PublicEmailTestingDetails : IAdditionalDataHolder, IParsable
    {
        /// <summary>Version of the email that should be sent if there are too few recipients to conduct an AB test.</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSampleSizeDefault? AbSampleSizeDefault { get; set; }
        /// <summary>Version of the email that should be sent if the results are inconclusive after the test period, master or variant.</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSamplingDefault? AbSamplingDefault { get; set; }
        /// <summary>Status of the AB test.</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abStatus? AbStatus { get; set; }
        /// <summary>Metric to determine the version that will be sent to the remaining contacts.</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSuccessMetric? AbSuccessMetric { get; set; }
        /// <summary>The size of your test group.</summary>
        public int? AbTestPercentage { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Time limit on gathering test results. After this time is up, the winning version will be sent to the remaining contacts.</summary>
        public int? HoursToWait { get; set; }
        /// <summary>The ID of the AB test.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TestId { get; set; }
#nullable restore
#else
        public string TestId { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails"/> and sets the default values.
        /// </summary>
        public PublicEmailTestingDetails()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "abSampleSizeDefault", n => { AbSampleSizeDefault = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSampleSizeDefault>(); } },
                { "abSamplingDefault", n => { AbSamplingDefault = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSamplingDefault>(); } },
                { "abStatus", n => { AbStatus = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abStatus>(); } },
                { "abSuccessMetric", n => { AbSuccessMetric = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSuccessMetric>(); } },
                { "abTestPercentage", n => { AbTestPercentage = n.GetIntValue(); } },
                { "hoursToWait", n => { HoursToWait = n.GetIntValue(); } },
                { "testId", n => { TestId = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSampleSizeDefault>("abSampleSizeDefault", AbSampleSizeDefault);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSamplingDefault>("abSamplingDefault", AbSamplingDefault);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abStatus>("abStatus", AbStatus);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.Marketing.MarketingEmails.V3.Models.PublicEmailTestingDetails_abSuccessMetric>("abSuccessMetric", AbSuccessMetric);
            writer.WriteIntValue("abTestPercentage", AbTestPercentage);
            writer.WriteIntValue("hoursToWait", HoursToWait);
            writer.WriteStringValue("testId", TestId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
