using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoques;

public sealed record ListarEstoquesQuery(
    int Page = 1,
    int PageSize = 10,
    Guid? FranqueadoId = null
) : IRequest<PagedResult<EstoqueItemDto>>;
