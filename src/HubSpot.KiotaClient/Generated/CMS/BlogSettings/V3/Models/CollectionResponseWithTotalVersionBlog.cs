// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models
{
    /// <summary>
    /// Response object for collections of blog versions with pagination information.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CollectionResponseWithTotalVersionBlog : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Model definition for paging.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Paging? Paging { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Paging Paging { get; set; }
#endif
        /// <summary>Collection of blog versions.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.VersionBlog>? Results { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.VersionBlog> Results { get; set; }
#endif
        /// <summary>Total number of blog versions.</summary>
        public int? Total { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalVersionBlog"/> and sets the default values.
        /// </summary>
        public CollectionResponseWithTotalVersionBlog()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalVersionBlog"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalVersionBlog CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.CollectionResponseWithTotalVersionBlog();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "paging", n => { Paging = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Paging>(global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Paging.CreateFromDiscriminatorValue); } },
                { "results", n => { Results = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.VersionBlog>(global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.VersionBlog.CreateFromDiscriminatorValue)?.AsList(); } },
                { "total", n => { Total = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.Paging>("paging", Paging);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CMS.BlogSettings.V3.Models.VersionBlog>("results", Results);
            writer.WriteIntValue("total", Total);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
