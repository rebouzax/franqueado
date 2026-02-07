using Franqueado.Domain.Entities;

namespace Franqueado.Domain.Repositories;

public interface IMovimentacaoEstoqueRepository
{
    Task AddAsync(MovimentacaoEstoque mov, CancellationToken ct);
}
