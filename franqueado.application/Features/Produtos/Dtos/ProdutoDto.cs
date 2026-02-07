namespace Franqueado.Application.Features.Produtos.Dtos;

public sealed record ProdutoDto(
    Guid Id,
    string Nome,
    string Sku
);
