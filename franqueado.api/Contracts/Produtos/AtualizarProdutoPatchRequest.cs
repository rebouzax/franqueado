namespace Franqueado.Api.Contracts.Produtos;

public sealed class AtualizarProdutoPatchRequest
{
    public string? Nome { get; set; }
    public string? Sku { get; set; }
}
