using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Dtos;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Repositories;

public sealed class EstoqueReadRepository : IEstoqueReadRepository
{
    private readonly FranqueadoDbContext _db;

    public EstoqueReadRepository(FranqueadoDbContext db) => _db = db;

    public Task<EstoqueItemDto?> GetAsync(Guid franqueadoId, Guid produtoId, CancellationToken ct)
    {
        return _db.Estoques
            .AsNoTracking()
            .Where(e => e.FranqueadoId == franqueadoId && e.ProdutoId == produtoId)
            .Select(e => new EstoqueItemDto(e.Id, e.FranqueadoId, e.ProdutoId, e.Quantidade))
            .FirstOrDefaultAsync(ct);
    }

    public Task<int> CountAsync(Guid? franqueadoId, CancellationToken ct)
    {
        var query = _db.Estoques.AsNoTracking();

        if (franqueadoId.HasValue)
            query = query.Where(e => e.FranqueadoId == franqueadoId.Value);

        return query.CountAsync(ct);
    }

    public async Task<IReadOnlyList<EstoqueItemDto>> ListPagedAsync(int page, int pageSize, Guid? franqueadoId, CancellationToken ct)
    {
        var skip = (page - 1) * pageSize;

        var query = _db.Estoques.AsNoTracking();

        if (franqueadoId.HasValue)
            query = query.Where(e => e.FranqueadoId == franqueadoId.Value);

        return await query
            .OrderBy(e => e.ProdutoId)
            .Skip(skip)
            .Take(pageSize)
            .Select(e => new EstoqueItemDto(e.Id, e.FranqueadoId, e.ProdutoId, e.Quantidade))
            .ToListAsync(ct);
    }
}
