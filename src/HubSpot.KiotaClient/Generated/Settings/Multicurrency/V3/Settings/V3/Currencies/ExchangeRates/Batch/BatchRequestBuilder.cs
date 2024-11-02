// <auto-generated/>
#pragma warning disable CS0618
using DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Create;
using DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Read;
using DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Update;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch
{
    /// <summary>
    /// Builds and executes requests for operations under \settings\v3\currencies\exchange-rates\batch
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BatchRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The create property</summary>
        public global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Create.CreateRequestBuilder Create
        {
            get => new global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Create.CreateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The read property</summary>
        public global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Read.ReadRequestBuilder Read
        {
            get => new global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Read.ReadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The update property</summary>
        public global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Update.UpdateRequestBuilder Update
        {
            get => new global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.Update.UpdateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/settings/v3/currencies/exchange-rates/batch", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::DamianH.HubSpot.KiotaClient.Settings.Multicurrency.V3.Settings.V3.Currencies.ExchangeRates.Batch.BatchRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BatchRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/settings/v3/currencies/exchange-rates/batch", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618