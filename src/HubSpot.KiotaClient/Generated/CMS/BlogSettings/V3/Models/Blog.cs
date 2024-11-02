// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class Blog : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The absoluteUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AbsoluteUrl { get; set; }
#nullable restore
#else
        public string AbsoluteUrl { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Boolean determining whether or not this blog allows public comments.</summary>
        public bool? AllowComments { get; set; }
        /// <summary>The created property</summary>
        public DateTimeOffset? Created { get; set; }
        /// <summary>The timestamp (ISO8601 format) when this Blog was deleted.</summary>
        public DateTimeOffset? DeletedAt { get; set; }
        /// <summary>The Description of this Blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The html title of this Blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HtmlTitle { get; set; }
#nullable restore
#else
        public string HtmlTitle { get; set; }
#endif
        /// <summary>The unique ID of the Blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The explicitly defined language of the Blog. If null, the Blog will default to the language of the Domain.</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog_language? Language { get; set; }
        /// <summary>The internal name of the blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>Rules for require member registration to access private content.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.PublicAccessRule>? PublicAccessRules { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.PublicAccessRule> PublicAccessRules { get; set; }
#endif
        /// <summary>Boolean to determine whether or not to respect publicAccessRules.</summary>
        public bool? PublicAccessRulesEnabled { get; set; }
        /// <summary>The public title of this Blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PublicTitle { get; set; }
#nullable restore
#else
        public string PublicTitle { get; set; }
#endif
        /// <summary>The path of the this blog. This field is appended to the domain to construct the url of this blog.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Slug { get; set; }
#nullable restore
#else
        public string Slug { get; set; }
#endif
        /// <summary>ID of the primary Blog this object was translated from.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TranslatedFromId { get; set; }
#nullable restore
#else
        public string TranslatedFromId { get; set; }
#endif
        /// <summary>The updated property</summary>
        public DateTimeOffset? Updated { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog"/> and sets the default values.
        /// </summary>
        public Blog()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "absoluteUrl", n => { AbsoluteUrl = n.GetStringValue(); } },
                { "allowComments", n => { AllowComments = n.GetBoolValue(); } },
                { "created", n => { Created = n.GetDateTimeOffsetValue(); } },
                { "deletedAt", n => { DeletedAt = n.GetDateTimeOffsetValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "htmlTitle", n => { HtmlTitle = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "language", n => { Language = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog_language>(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "publicAccessRules", n => { PublicAccessRules = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.PublicAccessRule>(global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.PublicAccessRule.CreateFromDiscriminatorValue)?.AsList(); } },
                { "publicAccessRulesEnabled", n => { PublicAccessRulesEnabled = n.GetBoolValue(); } },
                { "publicTitle", n => { PublicTitle = n.GetStringValue(); } },
                { "slug", n => { Slug = n.GetStringValue(); } },
                { "translatedFromId", n => { TranslatedFromId = n.GetStringValue(); } },
                { "updated", n => { Updated = n.GetDateTimeOffsetValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("absoluteUrl", AbsoluteUrl);
            writer.WriteBoolValue("allowComments", AllowComments);
            writer.WriteDateTimeOffsetValue("created", Created);
            writer.WriteDateTimeOffsetValue("deletedAt", DeletedAt);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("htmlTitle", HtmlTitle);
            writer.WriteStringValue("id", Id);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Blog_language>("language", Language);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.PublicAccessRule>("publicAccessRules", PublicAccessRules);
            writer.WriteBoolValue("publicAccessRulesEnabled", PublicAccessRulesEnabled);
            writer.WriteStringValue("publicTitle", PublicTitle);
            writer.WriteStringValue("slug", Slug);
            writer.WriteStringValue("translatedFromId", TranslatedFromId);
            writer.WriteDateTimeOffsetValue("updated", Updated);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618