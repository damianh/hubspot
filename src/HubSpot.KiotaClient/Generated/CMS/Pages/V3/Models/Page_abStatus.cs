// <auto-generated/>
using System.Runtime.Serialization;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Models
{
    /// <summary>The status of the AB test associated with this page, if applicable</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public enum Page_abStatus
    {
        [EnumMember(Value = "master")]
        #pragma warning disable CS1591
        Master,
        #pragma warning restore CS1591
        [EnumMember(Value = "variant")]
        #pragma warning disable CS1591
        Variant,
        #pragma warning restore CS1591
        [EnumMember(Value = "loser_variant")]
        #pragma warning disable CS1591
        Loser_variant,
        #pragma warning restore CS1591
        [EnumMember(Value = "mab_master")]
        #pragma warning disable CS1591
        Mab_master,
        #pragma warning restore CS1591
        [EnumMember(Value = "mab_variant")]
        #pragma warning disable CS1591
        Mab_variant,
        #pragma warning restore CS1591
        [EnumMember(Value = "automated_master")]
        #pragma warning disable CS1591
        Automated_master,
        #pragma warning restore CS1591
        [EnumMember(Value = "automated_variant")]
        #pragma warning disable CS1591
        Automated_variant,
        #pragma warning restore CS1591
        [EnumMember(Value = "automated_loser_variant")]
        #pragma warning disable CS1591
        Automated_loser_variant,
        #pragma warning restore CS1591
    }
}
