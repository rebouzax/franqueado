using Franqueado.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Franqueado.Infra.Persistence.Context;

public sealed class FranqueadoDbContext : DbContext
{
    public FranqueadoDbContext(DbContextOptions<FranqueadoDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FranqueadoDbContext).Assembly);
    }
}
