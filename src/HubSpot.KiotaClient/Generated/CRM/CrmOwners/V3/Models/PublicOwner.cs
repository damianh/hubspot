// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class PublicOwner : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The archived property</summary>
        public bool? Archived { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The email property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Email { get; set; }
#nullable restore
#else
        public string Email { get; set; }
#endif
        /// <summary>The firstName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FirstName { get; set; }
#nullable restore
#else
        public string FirstName { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The lastName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastName { get; set; }
#nullable restore
#else
        public string LastName { get; set; }
#endif
        /// <summary>The teams property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicTeam>? Teams { get; set; }
#nullable restore
#else
        public List<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicTeam> Teams { get; set; }
#endif
        /// <summary>The type property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner_type? Type { get; set; }
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The userId property</summary>
        public int? UserId { get; set; }
        /// <summary>The userIdIncludingInactive property</summary>
        public int? UserIdIncludingInactive { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner"/> and sets the default values.
        /// </summary>
        public PublicOwner()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "archived", n => { Archived = n.GetBoolValue(); } },
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "email", n => { Email = n.GetStringValue(); } },
                { "firstName", n => { FirstName = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "lastName", n => { LastName = n.GetStringValue(); } },
                { "teams", n => { Teams = n.GetCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicTeam>(global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicTeam.CreateFromDiscriminatorValue)?.AsList(); } },
                { "type", n => { Type = n.GetEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner_type>(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "userId", n => { UserId = n.GetIntValue(); } },
                { "userIdIncludingInactive", n => { UserIdIncludingInactive = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("archived", Archived);
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("email", Email);
            writer.WriteStringValue("firstName", FirstName);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("lastName", LastName);
            writer.WriteCollectionOfObjectValues<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicTeam>("teams", Teams);
            writer.WriteEnumValue<global::DamianH.HubSpot.KiotaClient.CRM.CrmOwners.V3.Models.PublicOwner_type>("type", Type);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteIntValue("userId", UserId);
            writer.WriteIntValue("userIdIncludingInactive", UserIdIncludingInactive);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
