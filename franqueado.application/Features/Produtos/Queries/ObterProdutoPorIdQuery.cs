using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Produtos.Queries;

public sealed record ObterProdutoPorIdQuery(Guid Id) : IRequest<ProdutoDto?>;
