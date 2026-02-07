namespace Franqueado.Application.Features.Estoques.Dtos;

public sealed record EstoqueDto(
    Guid Id,
    Guid FranqueadoId,
    Guid ProdutoId,
    int Quantidade
);
