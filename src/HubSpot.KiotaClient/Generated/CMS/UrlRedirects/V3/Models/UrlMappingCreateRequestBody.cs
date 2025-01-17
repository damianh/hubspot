// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class UrlMappingCreateRequestBody : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The destination property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Destination { get; set; }
#nullable restore
#else
        public string Destination { get; set; }
#endif
        /// <summary>The isMatchFullUrl property</summary>
        public bool? IsMatchFullUrl { get; set; }
        /// <summary>The isMatchQueryString property</summary>
        public bool? IsMatchQueryString { get; set; }
        /// <summary>The isOnlyAfterNotFound property</summary>
        public bool? IsOnlyAfterNotFound { get; set; }
        /// <summary>The isPattern property</summary>
        public bool? IsPattern { get; set; }
        /// <summary>The isProtocolAgnostic property</summary>
        public bool? IsProtocolAgnostic { get; set; }
        /// <summary>The isTrailingSlashOptional property</summary>
        public bool? IsTrailingSlashOptional { get; set; }
        /// <summary>The precedence property</summary>
        public int? Precedence { get; set; }
        /// <summary>The redirectStyle property</summary>
        public int? RedirectStyle { get; set; }
        /// <summary>The routePrefix property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RoutePrefix { get; set; }
#nullable restore
#else
        public string RoutePrefix { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody"/> and sets the default values.
        /// </summary>
        public UrlMappingCreateRequestBody()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.UrlRedirects.V3.Models.UrlMappingCreateRequestBody();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "destination", n => { Destination = n.GetStringValue(); } },
                { "isMatchFullUrl", n => { IsMatchFullUrl = n.GetBoolValue(); } },
                { "isMatchQueryString", n => { IsMatchQueryString = n.GetBoolValue(); } },
                { "isOnlyAfterNotFound", n => { IsOnlyAfterNotFound = n.GetBoolValue(); } },
                { "isPattern", n => { IsPattern = n.GetBoolValue(); } },
                { "isProtocolAgnostic", n => { IsProtocolAgnostic = n.GetBoolValue(); } },
                { "isTrailingSlashOptional", n => { IsTrailingSlashOptional = n.GetBoolValue(); } },
                { "precedence", n => { Precedence = n.GetIntValue(); } },
                { "redirectStyle", n => { RedirectStyle = n.GetIntValue(); } },
                { "routePrefix", n => { RoutePrefix = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("destination", Destination);
            writer.WriteBoolValue("isMatchFullUrl", IsMatchFullUrl);
            writer.WriteBoolValue("isMatchQueryString", IsMatchQueryString);
            writer.WriteBoolValue("isOnlyAfterNotFound", IsOnlyAfterNotFound);
            writer.WriteBoolValue("isPattern", IsPattern);
            writer.WriteBoolValue("isProtocolAgnostic", IsProtocolAgnostic);
            writer.WriteBoolValue("isTrailingSlashOptional", IsTrailingSlashOptional);
            writer.WriteIntValue("precedence", Precedence);
            writer.WriteIntValue("redirectStyle", RedirectStyle);
            writer.WriteStringValue("routePrefix", RoutePrefix);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
