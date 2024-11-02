// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class SimplePublicObjectInput : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Include this field along with a unique ID value to enable more granular debugging for error responses. Learn more about [multi-status errors](https://developers.hubspot.com/docs/reference/api/other-resources/error-handling#multi-status-errors).</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObjectWriteTraceId { get; set; }
#nullable restore
#else
        public string ObjectWriteTraceId { get; set; }
#endif
        /// <summary>The properties to set, in key-value pairs.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput_properties? Properties { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput_properties Properties { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput"/> and sets the default values.
        /// </summary>
        public SimplePublicObjectInput()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "objectWriteTraceId", n => { ObjectWriteTraceId = n.GetStringValue(); } },
                { "properties", n => { Properties = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput_properties>(global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput_properties.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("objectWriteTraceId", ObjectWriteTraceId);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CRM.PartnerClients.V3.Models.SimplePublicObjectInput_properties>("properties", Properties);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
