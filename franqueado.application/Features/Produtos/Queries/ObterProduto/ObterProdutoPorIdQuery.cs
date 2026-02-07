using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace franqueado.application.Features.Produtos.Queries.ObterProduto;

public sealed record ObterProdutoPorIdQuery(Guid Id) : IRequest<ProdutoDto?>;
