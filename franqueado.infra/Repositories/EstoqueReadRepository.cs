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
            .Select(e => new EstoqueItemDto(e.Id, e.FranqueadoId, e.ProdutoId, e.Quantidade, Convert.ToBase64String(e.RowVersion)))
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
            .Select(e => new EstoqueItemDto(e.Id, e.FranqueadoId, e.ProdutoId, e.Quantidade, Convert.ToBase64String(e.RowVersion)))
            .ToListAsync(ct);
    }


    public Task<int> CountWithProdutoAsync(Guid? franqueadoId, string? search, CancellationToken ct)
    {
        var query =
            from e in _db.Estoques.AsNoTracking()
            join p in _db.Produtos.AsNoTracking() on e.ProdutoId equals p.Id
            select new { e, p };

        if (franqueadoId.HasValue)
            query = query.Where(x => x.e.FranqueadoId == franqueadoId.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            query = query.Where(x =>
                EF.Functions.Like(x.p.Nome, $"%{s}%") ||
                EF.Functions.Like(x.p.Sku, $"%{s}%"));
        }

        return query.CountAsync(ct);
    }

    public async Task<IReadOnlyList<EstoqueProdutoItemDto>> ListPagedWithProdutoAsync(
        int page, int pageSize, Guid? franqueadoId, string? search, CancellationToken ct)
    {
        var skip = (page - 1) * pageSize;

        var query =
            from e in _db.Estoques.AsNoTracking()
            join p in _db.Produtos.AsNoTracking() on e.ProdutoId equals p.Id
            select new { e, p };

        if (franqueadoId.HasValue)
            query = query.Where(x => x.e.FranqueadoId == franqueadoId.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            query = query.Where(x =>
                EF.Functions.Like(x.p.Nome, $"%{s}%") ||
                EF.Functions.Like(x.p.Sku, $"%{s}%"));
        }

        return await query
            .OrderBy(x => x.p.Nome)
            .ThenBy(x => x.p.Sku)
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new EstoqueProdutoItemDto(
                x.e.Id,
                x.e.FranqueadoId,
                x.e.ProdutoId,
                x.p.Nome,
                x.p.Sku,
                x.e.Quantidade
            ))
            .ToListAsync(ct);
    }


}
