using Franqueado.Domain.Entities;

namespace Franqueado.Domain.Repositories;

public interface IProdutoRepository
{
    Task AddAsync(Produto produto, CancellationToken ct);
    Task<bool> ExistsBySkuAsync(string sku, CancellationToken ct);

    Task<int> CountAsync(CancellationToken ct);
    Task<IReadOnlyList<Produto>> ListPagedAsync(int page, int pageSize, CancellationToken ct);
}
