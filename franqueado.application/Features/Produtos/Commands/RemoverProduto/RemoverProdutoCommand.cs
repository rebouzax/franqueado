using MediatR;

namespace franqueado.application.Features.Produtos.Commands.RemoverProduto;

public sealed record RemoverProdutoCommand(Guid Id) : IRequest;
