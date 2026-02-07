using franqueado.application.Features.Estoques.Queries.ListarMovimentacoes;
using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Abstractions.Repositories;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace Franqueado.Application.Features.Estoques.Queries.ListarMovimentacoes;

public sealed class ListarMovimentacoesQueryHandler
    : IRequestHandler<ListarMovimentacoesQuery, PagedResult<MovimentacaoEstoqueDto>>
{
    private readonly IMovimentacaoEstoqueReadRepository _readRepo;

    public ListarMovimentacoesQueryHandler(IMovimentacaoEstoqueReadRepository readRepo) => _readRepo = readRepo;

    public async Task<PagedResult<MovimentacaoEstoqueDto>> Handle(ListarMovimentacoesQuery request, CancellationToken ct)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize > 100 ? 100 : request.PageSize;

        var total = await _readRepo.CountAsync(request.FranqueadoId, request.ProdutoId, request.De, request.Ate, ct);

        var items = await _readRepo.ListPagedAsync(
            request.FranqueadoId,
            request.ProdutoId,
            page,
            pageSize,
            request.De,
            request.Ate,
            ct);

        return new PagedResult<MovimentacaoEstoqueDto>(items, page, pageSize, total);
    }
}
