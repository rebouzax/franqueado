using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Repositories;

public sealed class EstoqueRepository : IEstoqueRepository
{
    private readonly FranqueadoDbContext _db;
    public EstoqueRepository(FranqueadoDbContext db) => _db = db;

    public Task<Estoque?> GetAsync(Guid franqueadoId, Guid produtoId, CancellationToken ct)
        => _db.Estoques.FirstOrDefaultAsync(e => e.FranqueadoId == franqueadoId && e.ProdutoId == produtoId, ct);

    public Task AddAsync(Estoque estoque, CancellationToken ct)
        => _db.Estoques.AddAsync(estoque, ct).AsTask();
}
