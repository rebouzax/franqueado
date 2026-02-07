using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Abstractions.Sorting;
using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace franqueado.application.Features.Produtos.Queries.ListarProdutos;

public sealed record ListarProdutosQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null,
    ProdutoSortBy SortBy = ProdutoSortBy.Nome,
    SortDirection SortDir = SortDirection.Asc
) : IRequest<PagedResult<ProdutoDto>>;
