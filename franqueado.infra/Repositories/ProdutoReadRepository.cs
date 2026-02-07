using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Abstractions.Sorting;
using Franqueado.Application.Features.Produtos.Dtos;
using Franqueado.Domain.Entities;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Repositories;

public sealed class ProdutoReadRepository : IProdutoReadRepository
{
    private readonly FranqueadoDbContext _db;

    public ProdutoReadRepository(FranqueadoDbContext db) => _db = db;

    private IQueryable<Produto> ApplySearch(IQueryable<Produto> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return query;

        var s = search.Trim();

        // LIKE para SQL Server (melhor que Contains em alguns cenários)
        return query.Where(p =>
            EF.Functions.Like(p.Nome, $"%{s}%") ||
            EF.Functions.Like(p.Sku, $"%{s}%"));
    }

    private IQueryable<Produto> ApplySort(IQueryable<Produto> query, ProdutoSortBy sortBy, SortDirection direction)
    {
        return (sortBy, direction) switch
        {
            (ProdutoSortBy.Nome, SortDirection.Desc) => query.OrderByDescending(x => x.Nome).ThenByDescending(x => x.Sku),
            (ProdutoSortBy.Nome, SortDirection.Asc) => query.OrderBy(x => x.Nome).ThenBy(x => x.Sku),

            (ProdutoSortBy.Sku, SortDirection.Desc) => query.OrderByDescending(x => x.Sku).ThenByDescending(x => x.Nome),
            (ProdutoSortBy.Sku, SortDirection.Asc) => query.OrderBy(x => x.Sku).ThenBy(x => x.Nome),

            _ => query.OrderBy(x => x.Nome).ThenBy(x => x.Sku)
        };
    }

    public Task<int> CountAsync(string? search, CancellationToken ct)
    {
        var query = _db.Produtos.AsNoTracking();
        query = ApplySearch(query, search);
        return query.CountAsync(ct);
    }

    public async Task<IReadOnlyList<ProdutoDto>> ListPagedAsync(
        int page, int pageSize, string? search, ProdutoSortBy sortBy, SortDirection direction, CancellationToken ct)
    {
        var skip = (page - 1) * pageSize;

        var query = _db.Produtos.AsNoTracking();
        query = ApplySearch(query, search);
        query = ApplySort(query, sortBy, direction);

        // ✅ projeção direta no banco
        return await query
            .Skip(skip)
            .Take(pageSize)
            .Select(p => new ProdutoDto(p.Id, p.Nome, p.Sku))
            .ToListAsync(ct);
    }

    public Task<ProdutoDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        // ✅ projeção direta no banco
        return _db.Produtos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProdutoDto(p.Id, p.Nome, p.Sku))
            .FirstOrDefaultAsync(ct);
    }
}
