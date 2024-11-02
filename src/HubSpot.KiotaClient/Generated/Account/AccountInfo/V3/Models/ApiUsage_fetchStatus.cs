// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace DamianH.HubSpot.KiotaClient.Account.AccountInfo.V3.Models
{
    /// <summary>Status of fetching the information, including if the data came from the cache.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum ApiUsage_fetchStatus
    {
        [EnumMember(Value = "SUCCESS")]
        #pragma warning disable CS1591
        SUCCESS,
        #pragma warning restore CS1591
        [EnumMember(Value = "TIMEOUT")]
        #pragma warning disable CS1591
        TIMEOUT,
        #pragma warning restore CS1591
        [EnumMember(Value = "FAILURE")]
        #pragma warning disable CS1591
        FAILURE,
        #pragma warning restore CS1591
        [EnumMember(Value = "CACHED")]
        #pragma warning disable CS1591
        CACHED,
        #pragma warning restore CS1591
        [EnumMember(Value = "NOTFOUND")]
        #pragma warning disable CS1591
        NOTFOUND,
        #pragma warning restore CS1591
    }
}