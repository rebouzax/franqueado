using MediatR;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdouto;

public sealed record AtualizarProdutoCommand(Guid Id, string Nome, string Sku) : IRequest;
