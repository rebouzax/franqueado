using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoques;

public sealed class ListarEstoquesQueryHandler : IRequestHandler<ListarEstoquesQuery, PagedResult<EstoqueItemDto>>
{
    private readonly IEstoqueReadRepository _readRepo;

    public ListarEstoquesQueryHandler(IEstoqueReadRepository readRepo) => _readRepo = readRepo;

    public async Task<PagedResult<EstoqueItemDto>> Handle(ListarEstoquesQuery request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize > 100 ? 100 : request.PageSize;

        var total = await _readRepo.CountAsync(request.FranqueadoId, ct);
        var items = await _readRepo.ListPagedAsync(page, pageSize, request.FranqueadoId, ct);

        return new PagedResult<EstoqueItemDto>(items, page, pageSize, total);
    }
}
