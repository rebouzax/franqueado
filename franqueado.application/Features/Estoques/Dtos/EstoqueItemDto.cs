namespace Franqueado.Application.Features.Estoques.Dtos;

public sealed record EstoqueItemDto(
    Guid Id,
    Guid FranqueadoId,
    Guid ProdutoId,
    int Quantidade
);
