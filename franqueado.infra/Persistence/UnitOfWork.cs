using Franqueado.Application.Abstractions;
using Franqueado.Infra.Persistence.Context;

namespace Franqueado.Infra.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly FranqueadoDbContext _db;

    public UnitOfWork(FranqueadoDbContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _db.SaveChangesAsync(ct);
}
