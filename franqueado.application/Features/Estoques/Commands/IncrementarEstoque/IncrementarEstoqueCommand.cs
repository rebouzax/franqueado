using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.IncrementarEstoque;

public sealed record IncrementarEstoqueCommand(
    Guid FranqueadoId,
    Guid ProdutoId,
    int Quantidade,
    string? Motivo = null,
    string? Usuario = null
) : IRequest;
