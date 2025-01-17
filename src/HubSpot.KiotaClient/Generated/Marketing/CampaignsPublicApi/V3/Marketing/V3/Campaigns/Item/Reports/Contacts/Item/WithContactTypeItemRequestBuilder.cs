// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\campaigns\{campaignGuid}\reports\contacts\{contactType}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithContactTypeItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithContactTypeItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/reports/contacts/{contactType}{?after*,endDate*,limit*,startDate*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithContactTypeItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}/reports/contacts/{contactType}{?after*,endDate*,limit*,startDate*}", rawUrl)
        {
        }
        /// <summary>
        /// Fetch the list of contact IDs for the specified campaign and contact type
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.CollectionResponseContactReferenceForwardPaging"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.CollectionResponseContactReferenceForwardPaging?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder.WithContactTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.CollectionResponseContactReferenceForwardPaging> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder.WithContactTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.CollectionResponseContactReferenceForwardPaging>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.CollectionResponseContactReferenceForwardPaging.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Fetch the list of contact IDs for the specified campaign and contact type
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder.WithContactTypeItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder.WithContactTypeItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Fetch the list of contact IDs for the specified campaign and contact type
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithContactTypeItemRequestBuilderGetQueryParameters 
        {
            /// <summary>A cursor for pagination. If provided, the results will start after the given cursor.Example: NTI1Cg%3D%3D</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("after")]
            public string? After { get; set; }
#nullable restore
#else
            [QueryParameter("after")]
            public string After { get; set; }
#endif
            /// <summary>End date for the report data, formatted as YYYY-MM-DD.Default value: Current date</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("endDate")]
            public string? EndDate { get; set; }
#nullable restore
#else
            [QueryParameter("endDate")]
            public string EndDate { get; set; }
#endif
            /// <summary>Limit for the number of contacts to fetchDefault: 100</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>The start date for the report data, formatted as YYYY-MM-DD.Default value: 2006-01-01</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("startDate")]
            public string? StartDate { get; set; }
#nullable restore
#else
            [QueryParameter("startDate")]
            public string StartDate { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithContactTypeItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.Contacts.Item.WithContactTypeItemRequestBuilder.WithContactTypeItemRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
