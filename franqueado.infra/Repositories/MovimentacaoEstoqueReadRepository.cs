using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Dtos;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Repositories;

public sealed class MovimentacaoEstoqueReadRepository : IMovimentacaoEstoqueReadRepository
{
    private readonly FranqueadoDbContext _db;

    public MovimentacaoEstoqueReadRepository(FranqueadoDbContext db) => _db = db;

    private IQueryable<Domain.Entities.MovimentacaoEstoque> ApplyPeriodo(
        IQueryable<Domain.Entities.MovimentacaoEstoque> query,
        DateTimeOffset? de,
        DateTimeOffset? ate)
    {
        if (de.HasValue) query = query.Where(x => x.CriadoEm >= de.Value);
        if (ate.HasValue) query = query.Where(x => x.CriadoEm <= ate.Value);
        return query;
    }

    public Task<int> CountAsync(Guid franqueadoId, Guid produtoId, DateTimeOffset? de, DateTimeOffset? ate, CancellationToken ct)
    {
        var query = _db.MovimentacoesEstoque.AsNoTracking()
            .Where(x => x.FranqueadoId == franqueadoId && x.ProdutoId == produtoId);

        query = ApplyPeriodo(query, de, ate);

        return query.CountAsync(ct);
    }

    public async Task<IReadOnlyList<MovimentacaoEstoqueDto>> ListPagedAsync(
        Guid franqueadoId,
        Guid produtoId,
        int page,
        int pageSize,
        DateTimeOffset? de,
        DateTimeOffset? ate,
        CancellationToken ct)
    {
        var skip = (page - 1) * pageSize;

        var query = _db.MovimentacoesEstoque.AsNoTracking()
            .Where(x => x.FranqueadoId == franqueadoId && x.ProdutoId == produtoId);

        query = ApplyPeriodo(query, de, ate);

        return await query
            .OrderByDescending(x => x.CriadoEm)
            .Skip(skip)
            .Take(pageSize)
            .Select(x => new MovimentacaoEstoqueDto(
                x.Id,
                x.FranqueadoId,
                x.ProdutoId,
                x.Tipo,
                x.Quantidade,
                x.Motivo,
                x.Usuario,
                x.CriadoEm
            ))
            .ToListAsync(ct);
    }
}
