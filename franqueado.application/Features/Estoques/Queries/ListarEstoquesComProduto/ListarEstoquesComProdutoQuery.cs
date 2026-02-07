using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoquesComProduto;

public sealed record ListarEstoquesComProdutoQuery(
    int Page = 1,
    int PageSize = 10,
    Guid? FranqueadoId = null,
    string? Search = null
) : IRequest<PagedResult<EstoqueProdutoItemDto>>;
