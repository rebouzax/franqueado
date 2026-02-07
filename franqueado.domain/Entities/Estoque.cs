namespace Franqueado.Domain.Entities;

public sealed class Estoque
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid FranqueadoId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public int Quantidade { get; private set; }

    private Estoque() { } // EF

    public Estoque(Guid franqueadoId, Guid produtoId, int quantidade)
    {
        FranqueadoId = franqueadoId;
        ProdutoId = produtoId;
        DefinirQuantidade(quantidade);
    }

    public void DefinirQuantidade(int quantidade)
    {
        if (quantidade < 0)
            throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidade não pode ser negativa.");

        Quantidade = quantidade;
    }

    public void Incrementar(int valor)
    {
        if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor));
        Quantidade += valor;
    }

    public void Decrementar(int valor)
    {
        if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor));
        if (Quantidade - valor < 0) throw new InvalidOperationException("Estoque insuficiente.");
        Quantidade -= valor;
    }
}
