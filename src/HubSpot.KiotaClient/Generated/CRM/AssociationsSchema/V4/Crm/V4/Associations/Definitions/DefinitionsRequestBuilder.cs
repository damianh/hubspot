// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions.Configurations;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions
{
    /// <summary>
    /// Builds and executes requests for operations under \crm\v4\associations\definitions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class DefinitionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The configurations property</summary>
        public global::DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions.Configurations.ConfigurationsRequestBuilder Configurations
        {
            get => new global::DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions.Configurations.ConfigurationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions.DefinitionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DefinitionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/definitions", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.CRM.AssociationsSchema.V4.Crm.V4.Associations.Definitions.DefinitionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DefinitionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/crm/v4/associations/definitions", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
