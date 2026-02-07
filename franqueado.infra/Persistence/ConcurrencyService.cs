using Franqueado.Application.Abstractions;
using Franqueado.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Persistence;

public sealed class ConcurrencyService : IConcurrencyService
{
    private readonly FranqueadoDbContext _db;

    public ConcurrencyService(FranqueadoDbContext db) => _db = db;

    public void SetOriginalRowVersion<T>(T entity, byte[] rowVersion) where T : class
    {
        _db.Entry(entity).Property("RowVersion").OriginalValue = rowVersion;
    }
}
