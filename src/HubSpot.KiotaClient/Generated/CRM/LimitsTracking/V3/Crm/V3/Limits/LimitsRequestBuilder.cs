// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CalculatedProperties;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomObjectTypes;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomProperties;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Pipelines;
using DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Records;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v3\limits
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class LimitsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The associations property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.AssociationsRequestBuilder Associations
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Associations.AssociationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The calculatedProperties property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CalculatedProperties.CalculatedPropertiesRequestBuilder CalculatedProperties
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CalculatedProperties.CalculatedPropertiesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The customObjectTypes property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomObjectTypes.CustomObjectTypesRequestBuilder CustomObjectTypes
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomObjectTypes.CustomObjectTypesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The customProperties property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomProperties.CustomPropertiesRequestBuilder CustomProperties
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.CustomProperties.CustomPropertiesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The pipelines property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Pipelines.PipelinesRequestBuilder Pipelines
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Pipelines.PipelinesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The records property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Records.RecordsRequestBuilder Records
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.Records.RecordsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.LimitsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public LimitsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/limits", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.LimitsTracking.V3.Crm.V3.Limits.LimitsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public LimitsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v3/limits", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
