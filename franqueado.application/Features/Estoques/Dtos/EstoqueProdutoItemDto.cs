namespace Franqueado.Application.Features.Estoques.Dtos;

public sealed record EstoqueProdutoItemDto(
    Guid Id,
    Guid FranqueadoId,
    Guid ProdutoId,
    string ProdutoNome,
    string ProdutoSku,
    int Quantidade
);
