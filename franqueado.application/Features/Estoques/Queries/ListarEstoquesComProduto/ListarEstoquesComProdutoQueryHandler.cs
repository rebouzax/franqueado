using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Queries.ListarEstoquesComProduto;

public sealed class ListarEstoquesComProdutoQueryHandler
    : IRequestHandler<ListarEstoquesComProdutoQuery, PagedResult<EstoqueProdutoItemDto>>
{
    private readonly IEstoqueReadRepository _readRepo;

    public ListarEstoquesComProdutoQueryHandler(IEstoqueReadRepository readRepo) => _readRepo = readRepo;

    public async Task<PagedResult<EstoqueProdutoItemDto>> Handle(ListarEstoquesComProdutoQuery request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize > 100 ? 100 : request.PageSize;

        var total = await _readRepo.CountWithProdutoAsync(request.FranqueadoId, request.Search, ct);
        var items = await _readRepo.ListPagedWithProdutoAsync(page, pageSize, request.FranqueadoId, request.Search, ct);

        return new PagedResult<EstoqueProdutoItemDto>(items, page, pageSize, total);
    }
}
