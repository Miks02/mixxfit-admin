namespace Mixxfit.Admin.Common
{
    public record PagedResult<T>(List<T> Items, int Page, int PageSize, int TotalCount, int PaginatedCount)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;
    }
}
