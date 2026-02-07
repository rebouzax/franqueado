using Franqueado.Application.Features.Estoques.Dtos;

namespace Franqueado.Application.Abstractions.Repositories;

public interface IEstoqueReadRepository
{

    Task<EstoqueItemDto?> GetAsync(Guid franqueadoId, Guid produtoId, CancellationToken ct);

    Task<int> CountAsync(Guid? franqueadoId, CancellationToken ct);

    Task<IReadOnlyList<EstoqueItemDto>> ListPagedAsync(
        int page,
        int pageSize,
        Guid? franqueadoId,
        CancellationToken ct);
    Task<int> CountWithProdutoAsync(Guid? franqueadoId, string? search, CancellationToken ct);

    Task<IReadOnlyList<EstoqueProdutoItemDto>> ListPagedWithProdutoAsync(
        int page,
        int pageSize,
        Guid? franqueadoId,
        string? search,
        CancellationToken ct);
}
