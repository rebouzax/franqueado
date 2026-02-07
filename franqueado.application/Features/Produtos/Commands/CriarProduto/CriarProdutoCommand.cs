using MediatR;
using Franqueado.Application.Features.Produtos.Dtos;

namespace franqueado.application.Features.Produtos.Commands.CriarProduto;

public sealed record CriarProdutoCommand(
    string Nome,
    string Sku
) : IRequest<ProdutoDto>;
