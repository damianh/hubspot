// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.Timeline.V3.Models
{
    /// <summary>The status of the batch response. Should always be COMPLETED if processed.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum BatchResponseTimelineEventResponse_status
    {
        [EnumMember(Value = "PENDING")]
        #pragma warning disable CS1591
        PENDING,
        #pragma warning restore CS1591
        [EnumMember(Value = "PROCESSING")]
        #pragma warning disable CS1591
        PROCESSING,
        #pragma warning restore CS1591
        [EnumMember(Value = "CANCELED")]
        #pragma warning disable CS1591
        CANCELED,
        #pragma warning restore CS1591
        [EnumMember(Value = "COMPLETE")]
        #pragma warning disable CS1591
        COMPLETE,
        #pragma warning restore CS1591
    }
}
