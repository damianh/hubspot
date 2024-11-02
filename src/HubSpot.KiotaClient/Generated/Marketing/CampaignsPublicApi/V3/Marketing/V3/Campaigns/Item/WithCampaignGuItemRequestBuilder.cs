// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Assets;
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget;
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports;
using DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \marketing\v3\campaigns\{campaignGuid}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithCampaignGuItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The assets property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Assets.AssetsRequestBuilder Assets
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Assets.AssetsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The budget property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.BudgetRequestBuilder Budget
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Budget.BudgetRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The reports property</summary>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.ReportsRequestBuilder Reports
        {
            get => new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.Reports.ReportsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCampaignGuItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}{?endDate*,properties*,startDate*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCampaignGuItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/marketing/v3/campaigns/{campaignGuid}{?endDate*,properties*,startDate*}", rawUrl)
        {
        }
        /// <summary>
        /// Delete a specified campaign from the system.This call will return a 204 No Content response regardless of whether the campaignGuid provided corresponds to an existing campaign or not.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get a campaign identified by a specific campaignGuid with the given properties. Along with the campaign information, it also returns information about assets. Depending on the query parameters used, this can also be used to return information about the corresponding assets&apos; metrics. Metrics are available only if startDate and endDate are provided.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignWithAssets"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignWithAssets?> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder.WithCampaignGuItemRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignWithAssets> GetAsync(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder.WithCampaignGuItemRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignWithAssets>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignWithAssets.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Perform a partial update of a campaign identified by the specified campaignGuid. Provided property values will be overwritten. Read-only and non-existent properties will cause 400 error.If an empty string is passed for any property in the Batch Update, it will reset that property&apos;s value.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaign"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaign?> PatchAsync(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignInput body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaign> PatchAsync(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignInput body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPatchRequestInformation(body, requestConfiguration);
            return await RequestAdapter.SendAsync<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaign>(requestInfo, global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaign.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Delete a specified campaign from the system.This call will return a 204 No Content response regardless of whether the campaignGuid provided corresponds to an existing campaign or not.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "*/*");
            return requestInfo;
        }
        /// <summary>
        /// Get a campaign identified by a specific campaignGuid with the given properties. Along with the campaign information, it also returns information about assets. Depending on the query parameters used, this can also be used to return information about the corresponding assets&apos; metrics. Metrics are available only if startDate and endDate are provided.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder.WithCampaignGuItemRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder.WithCampaignGuItemRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Perform a partial update of a campaign identified by the specified campaignGuid. Provided property values will be overwritten. Read-only and non-existent properties will cause 400 error.If an empty string is passed for any property in the Batch Update, it will reset that property&apos;s value.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignInput body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPatchRequestInformation(global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Models.PublicCampaignInput body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PATCH, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithCampaignGuItemRequestBuilderDeleteRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Get a campaign identified by a specific campaignGuid with the given properties. Along with the campaign information, it also returns information about assets. Depending on the query parameters used, this can also be used to return information about the corresponding assets&apos; metrics. Metrics are available only if startDate and endDate are provided.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithCampaignGuItemRequestBuilderGetQueryParameters 
        {
            /// <summary> End date to fetch asset metrics, formatted as YYYY-MM-DD. This date is used to fetch the metrics associated with the assets for a specified period.If not provided, no asset metrics will be fetched.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("endDate")]
            public string? EndDate { get; set; }
#nullable restore
#else
            [QueryParameter("endDate")]
            public string EndDate { get; set; }
#endif
            /// <summary>A comma-separated list of the properties to be returned in the response. If any of the specified properties has empty value on the requested object, they will be ignored and not returned in response. If this parameter is empty, the response will include an empty properties map.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("properties")]
            public string[]? Properties { get; set; }
#nullable restore
#else
            [QueryParameter("properties")]
            public string[] Properties { get; set; }
#endif
            /// <summary>Start date to fetch asset metrics, formatted as YYYY-MM-DD. This date is used to fetch the metrics associated with the assets for a specified period.If not provided, no asset metrics will be fetched.</summary>
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
        public partial class WithCampaignGuItemRequestBuilderGetRequestConfiguration : RequestConfiguration<global::DamianH.HubSpot.KiotaClient.Marketing.CampaignsPublicApi.V3.Marketing.V3.Campaigns.Item.WithCampaignGuItemRequestBuilder.WithCampaignGuItemRequestBuilderGetQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class WithCampaignGuItemRequestBuilderPatchRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618