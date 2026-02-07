using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using Franqueado.Infra.Persistence.Context;

namespace Franqueado.Infra.Repositories;

public sealed class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
{
    private readonly FranqueadoDbContext _db;
    public MovimentacaoEstoqueRepository(FranqueadoDbContext db) => _db = db;

    public Task AddAsync(MovimentacaoEstoque mov, CancellationToken ct)
        => _db.MovimentacoesEstoque.AddAsync(mov, ct).AsTask();
}
