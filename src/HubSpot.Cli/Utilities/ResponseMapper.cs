using DamianH.HubSpot.KiotaClient.CRM.Objects.V3.Models;

namespace DamianH.HubSpot.Cli.Utilities;

internal static class ResponseMapper
{
    public static IDictionary<string, object?> MapObject(SimplePublicObjectWithAssociations obj)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = obj.Id,
            ["createdAt"] = obj.CreatedAt?.ToString("O"),
            ["updatedAt"] = obj.UpdatedAt?.ToString("O"),
            ["archived"] = obj.Archived,
        };

        if (obj.Properties?.AdditionalData is { } props)
        {
            foreach (var kvp in props)
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }

    public static IDictionary<string, object?> MapObject(SimplePublicObject obj)
    {
        var result = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["id"] = obj.Id,
            ["createdAt"] = obj.CreatedAt?.ToString("O"),
            ["updatedAt"] = obj.UpdatedAt?.ToString("O"),
            ["archived"] = obj.Archived,
        };

        if (obj.Properties?.AdditionalData is { } props)
        {
            foreach (var kvp in props)
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }

    public static IReadOnlyList<IDictionary<string, object?>> MapCollection(
        CollectionResponseSimplePublicObjectWithAssociationsForwardPaging response)
    {
        if (response.Results is null)
        {
            return [];
        }

        return response.Results.Select(MapObject).ToList();
    }

    public static IReadOnlyList<IDictionary<string, object?>> MapCollection(
        CollectionResponseWithTotalSimplePublicObject response)
    {
        if (response.Results is null)
        {
            return [];
        }

        return response.Results.Select(MapObject).ToList();
    }
}
