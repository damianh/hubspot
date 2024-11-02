// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models
{
    /// <summary>
    /// Model definition for paging.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class Paging : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Model definition for a next page.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.NextPage? Next { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.NextPage Next { get; set; }
#endif
        /// <summary>Model definition for a previous page</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.PreviousPage? Prev { get; set; }
#nullable restore
#else
        public global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.PreviousPage Prev { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.Paging"/> and sets the default values.
        /// </summary>
        public Paging()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.Paging"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.Paging CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.Paging();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "next", n => { Next = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.NextPage>(global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.NextPage.CreateFromDiscriminatorValue); } },
                { "prev", n => { Prev = n.GetObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.PreviousPage>(global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.PreviousPage.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.NextPage>("next", Next);
            writer.WriteObjectValue<global::DamianH.HubSpot.KiotaClient.CMS.Posts.V3.Models.PreviousPage>("prev", Prev);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
