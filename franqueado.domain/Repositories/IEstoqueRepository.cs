using Franqueado.Domain.Entities;

namespace Franqueado.Domain.Repositories;

public interface IEstoqueRepository
{
    Task<Estoque?> GetAsync(Guid franqueadoId, Guid produtoId, CancellationToken ct);
    Task AddAsync(Estoque estoque, CancellationToken ct);
}
