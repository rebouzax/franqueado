using Franqueado.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franqueado.Infra.Persistence.Configurations;

public sealed class MovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.ToTable("MovimentacoesEstoque");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Tipo)
            .IsRequired();

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.Motivo)
            .HasMaxLength(200);

        builder.Property(x => x.Usuario)
            .HasMaxLength(120);

        builder.Property(x => x.CriadoEm)
            .IsRequired();

        builder.HasIndex(x => new { x.FranqueadoId, x.ProdutoId, x.CriadoEm });
    }
}
