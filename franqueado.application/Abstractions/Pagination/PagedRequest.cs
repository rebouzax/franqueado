namespace Franqueado.Application.Abstractions.Pagination;

public sealed record PagedRequest(int Page = 1, int PageSize = 10)
{
    public int PageSafe => Page < 1 ? 1 : Page;
    public int PageSizeSafe => PageSize < 1 ? 10 : PageSize > 100 ? 100 : PageSize;
}
