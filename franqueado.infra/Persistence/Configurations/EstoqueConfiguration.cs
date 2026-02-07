using Franqueado.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franqueado.Infra.Persistence.Configurations;

public sealed class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToTable("Estoques");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();


        builder.HasIndex(x => new { x.FranqueadoId, x.ProdutoId }).IsUnique();
    }
}
