using Franqueado.Domain.Enums;

namespace Franqueado.Domain.Entities;

public sealed class MovimentacaoEstoque
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid FranqueadoId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public TipoMovimentacaoEstoque Tipo { get; private set; }
    public int Quantidade { get; private set; }

    public string? Motivo { get; private set; }
    public string? Usuario { get; private set; }

    public DateTimeOffset CriadoEm { get; private set; } = DateTimeOffset.UtcNow;

    private MovimentacaoEstoque() { } // EF

    public MovimentacaoEstoque(Guid franqueadoId, Guid produtoId, TipoMovimentacaoEstoque tipo, int quantidade, string? motivo, string? usuario)
    {
        if (quantidade <= 0) throw new ArgumentOutOfRangeException(nameof(quantidade));

        FranqueadoId = franqueadoId;
        ProdutoId = produtoId;
        Tipo = tipo;
        Quantidade = quantidade;
        Motivo = string.IsNullOrWhiteSpace(motivo) ? null : motivo.Trim();
        Usuario = string.IsNullOrWhiteSpace(usuario) ? null : usuario.Trim();
    }
}
