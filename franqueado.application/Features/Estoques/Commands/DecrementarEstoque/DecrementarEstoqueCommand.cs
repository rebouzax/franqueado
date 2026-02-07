using MediatR;

namespace Franqueado.Application.Features.Estoques.Commands.DecrementarEstoque;

public sealed record DecrementarEstoqueCommand(Guid FranqueadoId, Guid ProdutoId, int Quantidade, string? Motivo = null) : IRequest;
