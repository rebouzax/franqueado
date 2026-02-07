using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Produtos.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Produtos.Queries;

public sealed class ListarProdutosQueryHandler : IRequestHandler<ListarProdutosQuery, PagedResult<ProdutoDto>>
{
    private readonly IProdutoReadRepository _readRepo;

    public ListarProdutosQueryHandler(IProdutoReadRepository readRepo) => _readRepo = readRepo;

    public async Task<PagedResult<ProdutoDto>> Handle(ListarProdutosQuery request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize > 100 ? 100 : request.PageSize;

        var total = await _readRepo.CountAsync(request.Search, ct);

        var items = await _readRepo.ListPagedAsync(
            page, pageSize, request.Search, request.SortBy, request.SortDir, ct);

        return new PagedResult<ProdutoDto>(items, page, pageSize, total);
    }
}
