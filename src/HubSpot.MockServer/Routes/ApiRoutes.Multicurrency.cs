using DamianH.HubSpot.MockServer.Objects;
using DamianH.HubSpot.MockServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DamianH.HubSpot.MockServer.Routes;

internal static partial class ApiRoutes
{
    public static class Multicurrency
    {
        public static void RegisterMulticurrencyV3Api(WebApplication app)
        {
            var group = app.MapGroup("/settings/v3/currencies");

            group.MapGet("/company-currency", (
                [FromServices] CurrencyRepository repository) =>
            {
                var companyCurrency = repository.GetCompanyCurrency();
                return Results.Ok(companyCurrency);
            });

            group.MapPut("/company-currency", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<CompanyCurrencyUpdate>();
                if (body == null || string.IsNullOrEmpty(body.CurrencyCode))
                {
                    return Results.BadRequest(new { message = "currencyCode is required" });
                }

                repository.UpdateCompanyCurrency(body.CurrencyCode);
                var updated = repository.GetCompanyCurrency();
                return Results.Ok(updated);
            });

            group.MapGet("/codes", (
                [FromServices] CurrencyRepository repository) =>
            {
                var codes = repository.GetSupportedCurrencyCodes();
                return Results.Ok(new { results = codes });
            });

            group.MapPost("/add-currency", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<CurrencyCreateRequest>();
                if (body == null || string.IsNullOrEmpty(body.CurrencyCode))
                {
                    return Results.BadRequest(new { message = "currencyCode is required" });
                }

                var currency = new Currency
                {
                    CurrencyCode = body.CurrencyCode,
                    DisplayName = body.DisplayName ?? body.CurrencyCode,
                    Symbol = body.Symbol ?? body.CurrencyCode,
                    IsVisible = true
                };

                repository.AddCurrency(currency);
                return Results.Ok(currency);
            });

            group.MapPut("/update-visibility", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<CurrencyVisibilityUpdate>();
                if (body == null || string.IsNullOrEmpty(body.CurrencyCode))
                {
                    return Results.BadRequest(new { message = "currencyCode is required" });
                }

                repository.UpdateCurrencyVisibility(body.CurrencyCode, body.IsVisible);
                return Results.Ok(new { message = "Currency visibility updated" });
            });

            var exchangeRatesGroup = group.MapGroup("/exchange-rates");

            exchangeRatesGroup.MapGet("", (
                [FromServices] CurrencyRepository repository,
                [FromQuery] int? limit,
                [FromQuery] string? after) =>
            {
                var rates = repository.GetExchangeRates(limit, after);
                var hasMore = limit.HasValue && rates.Count >= limit.Value;

                var response = new
                {
                    results = rates,
                    paging = hasMore && rates.Count > 0 ? new
                    {
                        next = new
                        {
                            after = rates.Last().Id,
                            link = $"/settings/v3/currencies/exchange-rates?limit={limit}&after={rates.Last().Id}"
                        }
                    } : null
                };

                return Results.Ok(response);
            });

            exchangeRatesGroup.MapPost("", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<ExchangeRateCreateRequest>();
                if (body == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var rate = repository.CreateExchangeRate(
                    body.FromCurrencyCode,
                    body.ToCurrencyCode,
                    body.ExchangeRate);

                return Results.Ok(rate);
            });

            exchangeRatesGroup.MapGet("/{exchangeRateId}", (
                [FromServices] CurrencyRepository repository,
                string exchangeRateId) =>
            {
                var rate = repository.GetExchangeRate(exchangeRateId);
                if (rate == null)
                {
                    return Results.NotFound(new { message = "Exchange rate not found" });
                }

                return Results.Ok(rate);
            });

            exchangeRatesGroup.MapPut("/{exchangeRateId}", async (
                [FromServices] CurrencyRepository repository,
                string exchangeRateId,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<ExchangeRateUpdateRequest>();
                if (body == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var existing = repository.GetExchangeRate(exchangeRateId);
                if (existing == null)
                {
                    return Results.NotFound(new { message = "Exchange rate not found" });
                }

                repository.UpdateExchangeRate(exchangeRateId, body.ExchangeRate);
                var updated = repository.GetExchangeRate(exchangeRateId);
                return Results.Ok(updated);
            });

            exchangeRatesGroup.MapDelete("/{exchangeRateId}", (
                [FromServices] CurrencyRepository repository,
                string exchangeRateId) =>
            {
                repository.DeleteExchangeRate(exchangeRateId);
                return Results.NoContent();
            });

            exchangeRatesGroup.MapGet("/current", (
                [FromServices] CurrencyRepository repository) =>
            {
                var rates = repository.GetCurrentExchangeRates();
                return Results.Ok(new { results = rates });
            });

            var batchGroup = exchangeRatesGroup.MapGroup("/batch");

            batchGroup.MapPost("/create", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<BatchExchangeRateCreateRequest>();
                if (body == null || body.Inputs == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var results = new List<ExchangeRate>();
                foreach (var input in body.Inputs)
                {
                    var rate = repository.CreateExchangeRate(
                        input.FromCurrencyCode,
                        input.ToCurrencyCode,
                        input.ExchangeRate);
                    results.Add(rate);
                }

                return Results.Ok(new
                {
                    status = "COMPLETE",
                    results = results,
                    startedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });

            batchGroup.MapPost("/read", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<BatchReadRequest>();
                if (body == null || body.Inputs == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var ids = body.Inputs.Select(i => i.Id).ToList();
                var results = repository.GetExchangeRatesByIds(ids);

                return Results.Ok(new
                {
                    status = "COMPLETE",
                    results = results,
                    startedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });

            batchGroup.MapPost("/update", async (
                [FromServices] CurrencyRepository repository,
                HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<BatchExchangeRateUpdateRequest>();
                if (body == null || body.Inputs == null)
                {
                    return Results.BadRequest(new { message = "Invalid request body" });
                }

                var results = new List<ExchangeRate>();
                foreach (var input in body.Inputs)
                {
                    repository.UpdateExchangeRate(input.Id, input.ExchangeRate);
                    var updated = repository.GetExchangeRate(input.Id);
                    if (updated != null)
                    {
                        results.Add(updated);
                    }
                }

                return Results.Ok(new
                {
                    status = "COMPLETE",
                    results = results,
                    startedAt = DateTimeOffset.UtcNow,
                    completedAt = DateTimeOffset.UtcNow
                });
            });

            var centralFxGroup = group.MapGroup("/central-fx-rates");

            centralFxGroup.MapGet("/information", () =>
            {
                return Results.Ok(new
                {
                    enabled = true,
                    baseCurrency = "USD",
                    lastUpdated = DateTimeOffset.UtcNow.AddHours(-1)
                });
            });

            centralFxGroup.MapGet("/unsupported-currencies", () =>
            {
                return Results.Ok(new
                {
                    results = new List<string> { "BTC", "ETH", "XRP" }
                });
            });
        }
    }

    private record CompanyCurrencyUpdate(string CurrencyCode);
    private record CurrencyCreateRequest(string CurrencyCode, string? DisplayName, string? Symbol);
    private record CurrencyVisibilityUpdate(string CurrencyCode, bool IsVisible);
    private record ExchangeRateCreateRequest(string FromCurrencyCode, string ToCurrencyCode, decimal ExchangeRate);
    private record ExchangeRateUpdateRequest(decimal ExchangeRate);
    private record BatchExchangeRateCreateRequest(List<ExchangeRateCreateRequest> Inputs);
    private record BatchReadRequest(List<IdInput> Inputs);
    private record BatchExchangeRateUpdateRequest(List<ExchangeRateUpdateInput> Inputs);
    private record IdInput(string Id);
    private record ExchangeRateUpdateInput(string Id, decimal ExchangeRate);
}
