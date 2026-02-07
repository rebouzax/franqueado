using Franqueado.Application.Abstractions.Sorting;
using Franqueado.Application.Features.Produtos.Dtos;

namespace Franqueado.Application.Abstractions.Repositories;

public interface IProdutoReadRepository
{
    Task<int> CountAsync(string? search, CancellationToken ct);

    Task<IReadOnlyList<ProdutoDto>> ListPagedAsync(
        int page,
        int pageSize,
        string? search,
        ProdutoSortBy sortBy,
        SortDirection direction,
        CancellationToken ct);

    Task<ProdutoDto?> GetByIdAsync(Guid id, CancellationToken ct);
}
