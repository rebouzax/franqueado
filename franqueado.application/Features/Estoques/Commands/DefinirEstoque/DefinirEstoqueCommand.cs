using MediatR;

namespace franqueado.application.Features.Estoques.Commands.DefinirEstoque;

public sealed record DefinirEstoqueCommand(Guid FranqueadoId, Guid ProdutoId, int Quantidade) : IRequest;
