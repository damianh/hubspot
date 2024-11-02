using System;
using System.Text.Json.Serialization;

namespace DamianH.HubSpot.KiotaClient.Extensions;

public static class HubSpotCrmCompaniesClientExtensions
{
    /*public static async Task<Company> GetById(
        this HubSpotCrmCompaniesClient client,
        string companyId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyId))
        {
            throw new ArgumentException($"'{nameof(companyId)}' cannot be null or whitespace", nameof(companyId));
        }

        var path = $"/crm/v3/objects/companies/{companyId}";

        return await client.Crm.V3.Objects.Companies[companyId];
    }*/
}

public record Company
{
    public string            Id         { get; init; } = string.Empty;
    public CompanyProperties Properties { get; init; } = default!;
    public DateTimeOffset    CreatedAt  { get; init; }
    public DateTimeOffset    UpdatedAt  { get; init; }

    public bool Archived { get; init; }
    //public CompanyAssociations? Associations { get; init; } = default!;
}

public record CompanyProperties
{
    public string Name    { get; init; } = string.Empty;
    public string City    { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;

    [JsonPropertyName("housing_number")]
    public string HousingNumber { get; init; } = string.Empty;

    [JsonPropertyName("zip")]
    public string PostalCode { get; init; } = string.Empty;

    [JsonPropertyName("country")]
    public string Region { get; init; } = string.Empty;

    [JsonPropertyName("country_dropdown")]
    public string Country { get; init; } = string.Empty;

    [JsonPropertyName("organization_number")]
    public string OrganisationNumber { get; init; } = string.Empty;

    [JsonPropertyName("pipedrive_organisation_id")]
    public string? PipedriveOrganisationId { get; init; }

    public string? Type { get; init; }
}

/*public record CompanyAssociations
{
    public CompanyConstants.Associations? Deals { get; init; } = default!;
    public CompanyConstants.Associations? Contacts { get; init; } = default!;
}*/