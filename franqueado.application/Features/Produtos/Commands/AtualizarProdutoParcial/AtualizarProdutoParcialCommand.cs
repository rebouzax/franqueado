using MediatR;

namespace franqueado.application.Features.Produtos.Commands.AtualizarProdutoParcial;

public sealed record AtualizarProdutoParcialCommand(Guid Id, string? Nome, string? Sku) : IRequest;
