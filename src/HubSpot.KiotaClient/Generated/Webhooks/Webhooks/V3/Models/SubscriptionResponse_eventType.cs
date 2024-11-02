// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace DamianH.HubSpot.KiotaClient.Webhooks.Webhooks.V3.Models
{
    /// <summary>Type of event to listen for. Can be one of `create`, `delete`, `deletedForPrivacy`, or `propertyChange`.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum SubscriptionResponse_eventType
    {
        [EnumMember(Value = "contact.propertyChange")]
        #pragma warning disable CS1591
        ContactPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.propertyChange")]
        #pragma warning disable CS1591
        CompanyPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.propertyChange")]
        #pragma warning disable CS1591
        DealPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.propertyChange")]
        #pragma warning disable CS1591
        TicketPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "product.propertyChange")]
        #pragma warning disable CS1591
        ProductPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.propertyChange")]
        #pragma warning disable CS1591
        Line_itemPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.creation")]
        #pragma warning disable CS1591
        ContactCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.deletion")]
        #pragma warning disable CS1591
        ContactDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.privacyDeletion")]
        #pragma warning disable CS1591
        ContactPrivacyDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.creation")]
        #pragma warning disable CS1591
        CompanyCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.deletion")]
        #pragma warning disable CS1591
        CompanyDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.creation")]
        #pragma warning disable CS1591
        DealCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.deletion")]
        #pragma warning disable CS1591
        DealDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.creation")]
        #pragma warning disable CS1591
        TicketCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.deletion")]
        #pragma warning disable CS1591
        TicketDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "product.creation")]
        #pragma warning disable CS1591
        ProductCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "product.deletion")]
        #pragma warning disable CS1591
        ProductDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.creation")]
        #pragma warning disable CS1591
        Line_itemCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.deletion")]
        #pragma warning disable CS1591
        Line_itemDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "conversation.creation")]
        #pragma warning disable CS1591
        ConversationCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "conversation.deletion")]
        #pragma warning disable CS1591
        ConversationDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "conversation.newMessage")]
        #pragma warning disable CS1591
        ConversationNewMessage,
        #pragma warning restore CS1591
        [EnumMember(Value = "conversation.privacyDeletion")]
        #pragma warning disable CS1591
        ConversationPrivacyDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "conversation.propertyChange")]
        #pragma warning disable CS1591
        ConversationPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.merge")]
        #pragma warning disable CS1591
        ContactMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.merge")]
        #pragma warning disable CS1591
        CompanyMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.merge")]
        #pragma warning disable CS1591
        DealMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.merge")]
        #pragma warning disable CS1591
        TicketMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "product.merge")]
        #pragma warning disable CS1591
        ProductMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.merge")]
        #pragma warning disable CS1591
        Line_itemMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.restore")]
        #pragma warning disable CS1591
        ContactRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.restore")]
        #pragma warning disable CS1591
        CompanyRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.restore")]
        #pragma warning disable CS1591
        DealRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.restore")]
        #pragma warning disable CS1591
        TicketRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "product.restore")]
        #pragma warning disable CS1591
        ProductRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.restore")]
        #pragma warning disable CS1591
        Line_itemRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "contact.associationChange")]
        #pragma warning disable CS1591
        ContactAssociationChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "company.associationChange")]
        #pragma warning disable CS1591
        CompanyAssociationChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "deal.associationChange")]
        #pragma warning disable CS1591
        DealAssociationChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "ticket.associationChange")]
        #pragma warning disable CS1591
        TicketAssociationChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "line_item.associationChange")]
        #pragma warning disable CS1591
        Line_itemAssociationChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.propertyChange")]
        #pragma warning disable CS1591
        ObjectPropertyChange,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.creation")]
        #pragma warning disable CS1591
        ObjectCreation,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.deletion")]
        #pragma warning disable CS1591
        ObjectDeletion,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.merge")]
        #pragma warning disable CS1591
        ObjectMerge,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.restore")]
        #pragma warning disable CS1591
        ObjectRestore,
        #pragma warning restore CS1591
        [EnumMember(Value = "object.associationChange")]
        #pragma warning disable CS1591
        ObjectAssociationChange,
        #pragma warning restore CS1591
    }
}
