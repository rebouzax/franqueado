using MediatR;
using Franqueado.Application.Features.Produtos.Dtos;

namespace Franqueado.Application.Features.Produtos.Commands;

public sealed record CriarProdutoCommand(
    string Nome,
    string Sku
) : IRequest<ProdutoDto>;
