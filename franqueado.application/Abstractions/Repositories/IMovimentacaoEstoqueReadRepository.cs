using Franqueado.Application.Features.Estoques.Dtos;

namespace Franqueado.Application.Abstractions.Repositories;

public interface IMovimentacaoEstoqueReadRepository
{
    Task<int> CountAsync(Guid franqueadoId, Guid produtoId, DateTimeOffset? de, DateTimeOffset? ate, CancellationToken ct);

    Task<IReadOnlyList<MovimentacaoEstoqueDto>> ListPagedAsync(
        Guid franqueadoId,
        Guid produtoId,
        int page,
        int pageSize,
        DateTimeOffset? de,
        DateTimeOffset? ate,
        CancellationToken ct);
}
