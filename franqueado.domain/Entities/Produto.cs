namespace Franqueado.Domain.Entities;

public sealed class Produto
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; } = default!;
    public string Sku { get; private set; } = default!;

    private Produto() { } // EF

    public Produto(string nome, string sku)
    {
        Nome = nome.Trim();
        Sku = sku.Trim().ToUpperInvariant();
    }
}
