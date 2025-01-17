// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V4.Models
{
    /// <summary>
    /// A JSON object containing anything you want to override.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PublicSingleSendEmail : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>List of email addresses to send as Bcc.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Bcc { get; set; }
#nullable restore
#else
        public List<string> Bcc { get; set; }
#endif
        /// <summary>List of email addresses to send as Cc.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Cc { get; set; }
#nullable restore
#else
        public List<string> Cc { get; set; }
#endif
        /// <summary>The From header for the email.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? From { get; set; }
#nullable restore
#else
        public string From { get; set; }
#endif
        /// <summary>List of Reply-To header values for the email.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ReplyTo { get; set; }
#nullable restore
#else
        public List<string> ReplyTo { get; set; }
#endif
        /// <summary>ID for a particular send. No more than one email will be sent per sendId.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SendId { get; set; }
#nullable restore
#else
        public string SendId { get; set; }
#endif
        /// <summary>The recipient of the email.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? To { get; set; }
#nullable restore
#else
        public string To { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V4.Models.PublicSingleSendEmail"/> and sets the default values.
        /// </summary>
        public PublicSingleSendEmail()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V4.Models.PublicSingleSendEmail"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V4.Models.PublicSingleSendEmail CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.Marketing.Singlesend.V4.Models.PublicSingleSendEmail();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "bcc", n => { Bcc = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "cc", n => { Cc = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "from", n => { From = n.GetStringValue(); } },
                { "replyTo", n => { ReplyTo = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "sendId", n => { SendId = n.GetStringValue(); } },
                { "to", n => { To = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("bcc", Bcc);
            writer.WriteCollectionOfPrimitiveValues<string>("cc", Cc);
            writer.WriteStringValue("from", From);
            writer.WriteCollectionOfPrimitiveValues<string>("replyTo", ReplyTo);
            writer.WriteStringValue("sendId", SendId);
            writer.WriteStringValue("to", To);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
