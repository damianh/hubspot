// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.AttachToLangGroup;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.CreateLanguageVariation;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.DetachFromLangGroup;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.SetNewLangPrimary;
using DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.UpdateLanguages;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage
{
    /// <summary>
    /// Builds and executes requests for operations under \cms\v3\pages\site-pages\multi-language
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MultiLanguageRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The attachToLangGroup property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.AttachToLangGroup.AttachToLangGroupRequestBuilder AttachToLangGroup
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.AttachToLangGroup.AttachToLangGroupRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The createLanguageVariation property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.CreateLanguageVariation.CreateLanguageVariationRequestBuilder CreateLanguageVariation
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.CreateLanguageVariation.CreateLanguageVariationRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The detachFromLangGroup property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.DetachFromLangGroup.DetachFromLangGroupRequestBuilder DetachFromLangGroup
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.DetachFromLangGroup.DetachFromLangGroupRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The setNewLangPrimary property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder SetNewLangPrimary
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.SetNewLangPrimary.SetNewLangPrimaryRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The updateLanguages property</summary>
        public global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.UpdateLanguages.UpdateLanguagesRequestBuilder UpdateLanguages
        {
            get => new global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.UpdateLanguages.UpdateLanguagesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.MultiLanguageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MultiLanguageRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages/site-pages/multi-language", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CMS.Pages.V3.Cms.V3.Pages.SitePages.MultiLanguage.MultiLanguageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MultiLanguageRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/cms/v3/pages/site-pages/multi-language", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618