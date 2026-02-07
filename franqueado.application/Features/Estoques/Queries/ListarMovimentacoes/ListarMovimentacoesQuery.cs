using Franqueado.Application.Abstractions.Pagination;
using Franqueado.Application.Features.Estoques.Dtos;
using MediatR;

namespace franqueado.application.Features.Estoques.Queries.ListarMovimentacoes;

public sealed record ListarMovimentacoesQuery(
    Guid FranqueadoId,
    Guid ProdutoId,
    int Page = 1,
    int PageSize = 10,
    DateTimeOffset? De = null,
    DateTimeOffset? Ate = null
) : IRequest<PagedResult<MovimentacaoEstoqueDto>>;
