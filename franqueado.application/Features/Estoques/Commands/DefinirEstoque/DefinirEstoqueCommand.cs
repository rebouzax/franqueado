using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.DefinirEstoque;

public sealed record DefinirEstoqueCommand(
    Guid FranqueadoId,
    Guid ProdutoId,
    int Quantidade,
    string RowVersion
) : IRequest;
