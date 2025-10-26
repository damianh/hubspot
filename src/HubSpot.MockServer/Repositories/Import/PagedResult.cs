namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal class PagedResult<T>
{
    public required List<T> Results { get; init; }
    public required PagingInfo Paging { get; init; }
}
