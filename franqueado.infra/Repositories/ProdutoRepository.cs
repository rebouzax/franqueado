using Franqueado.Domain.Entities;
using Franqueado.Domain.Repositories;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Repositories;

public sealed class ProdutoRepository : IProdutoRepository
{
    private readonly FranqueadoDbContext _db;

    public ProdutoRepository(FranqueadoDbContext db) => _db = db;

    public async Task AddAsync(Produto produto, CancellationToken ct)
        => await _db.Produtos.AddAsync(produto, ct);

    public Task<bool> ExistsBySkuAsync(string sku, CancellationToken ct)
        => _db.Produtos.AnyAsync(x => x.Sku == sku.Trim().ToUpperInvariant(), ct);

    public Task<int> CountAsync(CancellationToken ct)
        => _db.Produtos.CountAsync(ct);

    public async Task<IReadOnlyList<Produto>> ListPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        var skip = (page - 1) * pageSize;

        return await _db.Produtos
            .AsNoTracking()
            .OrderBy(x => x.Nome)
            .ThenBy(x => x.Sku)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<Produto?> GetByIdAsync(Guid id, CancellationToken ct)
    => _db.Produtos.FirstOrDefaultAsync(x => x.Id == id, ct);

    public void Remove(Produto produto)
        => _db.Produtos.Remove(produto);

}
